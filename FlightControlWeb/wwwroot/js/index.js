﻿let map;
let icon;
let markers = []
let currentPath;
let selectedFlightPlanId;

setInterval(fetchFlightsSyncAll, 5000);

//map functions
function initMap() {
    //map options
    let options = {
        zoom: 15,
        center: { lat: 32.130232, lng: 34.847036 }
    }

    //new Map
    map = new google.maps.Map(document.getElementById('map'), options);

    //TODO: should save icon in wwwroot folder
    icon = {
        url: "images/marker_icon.png", // url
        scaledSize: new google.maps.Size(36, 36), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(16, 32) // anchor
    };
}

function addMarker(flight) {
    //console.log("addMarker: " + flight.flight_id);

    let coords = { lat: flight.latitude, lng: flight.longitude };

    //Add marker
    let marker = new google.maps.Marker({
        position: coords,
        map: map,
        icon: icon,
        title: flight.flight_id,
    })
    markers.push(marker);

    let infowindow = new google.maps.InfoWindow({
        content: flight.flight_id
    });

    //add listener
    marker.addListener('click', function () {
        flightClicked(flight.flight_id);
        toggleBounce(marker);
        infowindow.open(marker.get('map'), marker);
    });
}

function clearMarkers() {
    //console.log("clearMarkers");

    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

function toggleBounce(marker) {
    console.log("toggleBounce: " + marker.title);

    if (marker.getAnimation() !== null) {
        marker.setAnimation(null);
    } else {
        marker.setAnimation(google.maps.Animation.BOUNCE);
        setTimeout(function () { marker.setAnimation(null); }, 750);
    }
}

function removeMarkerById(id) {
    console.log("removeMarkerById: " + id);

    //iterate all markers and loop for the marker of the flight that removed
    for (let i = 0; i < markers.length; i++) {
        if (markers[i].title === id) {
            //remove marker from map and from array
            markers[i].setMap(null);
            markers.splice(i, 1); i--;

            //if the current path belongs to that marker, remove it from map
            if (currentPath !== undefined && currentPath.tag === id) {
                currentPath.setMap(null);
            }
        }
    }
}

function buildAndShowRoute(flightPlan) {
    console.log("buildAndShowRoute");

    // do not clear the current path from map on the first time
    if (currentPath !== undefined && currentPath !== null) {
        currentPath.setMap(null);
    }

    //init array of coords
    let flightPlanCoordinates = [];

    //add initial location landmark

    let initLocation = flightPlan.initial_location || flightPlan.initialLocation;

    let init = {
        lat: Number(initLocation.latitude),
        lng: Number(initLocation.longitude)
    };

    //push init location to arr
    flightPlanCoordinates.push(init);

    let segments = flightPlan.segments || flightPlan.Segments;

    //add all segments landmarks
    for (let i = 0; i < segments.length; i++) {
        let landMark = {
            lat: Number(segments[i].latitude),
            lng: Number(segments[i].longitude)
        };

        flightPlanCoordinates.push(landMark);
    }

    //create path and add it to map
    let flightPath = new google.maps.Polyline({
        path: flightPlanCoordinates,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2,
        tag: flightPlan.flightId
    });

    //add path to map and update the currentPath variable
    flightPath.setMap(map);
    currentPath = flightPath;
}

function isMarkerExist(id) {
    //console.log("isMarkerExist: " + id);

    for (let i = 0; i < markers.length; i++) {
        if (markers[i].title === id) {
            return true;
        }
    }
    return false;
}


//other

function appendFlightToList(flight) {
    //console.log("appendFlightToList: " + flight.flight_id);

    let flights = document.getElementById("flight_list");
    let flightsExternal = document.getElementById("flight_list_external");
    let item = document.createElement("li");
    item.id = flight.flight_id;
    item.className = "list-group-item list-group-item-action list-group-hover";
    item.onclick = function () { flightClicked(item.id); };
    item.setAttribute("from", flight.from);

    if (!flight.is_external) {
        //generate the inner content of the item
        const content = `<i class="fas fa-plane"></i>
                    ${item.id}
                    <br>
                    <i class="fas fa-user-tie"></i>
                    ${flight.company_name}
                    </i>

                    <Button id="${item.id}_delete_btn" 
                    class="btn btn-xs btn-outline-danger btn-position-right"                    
                    style="z-index: 3;"
                    onclick="removeFlight('${item.id}'); event.stopPropagation();">
                    X
                    </Button>`;

        item.innerHTML = content;

        //add item to list
        flights.append(item);
    } else {
        //generate the inner content of the item
        const content = `<i class="fas fa-plane"></i>
                    ${item.id}
                    <br>
                    <i class="fas fa-user-tie"></i>
                    ${flight.company_name}
                    </i>`;

        item.innerHTML = content;

        //add item to list
        flightsExternal.append(item);
    }
    if (item.id === selectedFlightPlanId) {
        setActive(item.id);
    }
    return flight;
}

function addFlightsArrayToFlightList(array) {
    //console.log("addFlightsArrayToFlightList");

    clearLists(); // remove all flights from list (internal + external)
    clearMarkers(); // remove all markers from map

    for (let i = 0; i < array.length; i++) {
        //marker = array[i];

        appendFlightToList(array[i]);
        addMarker(array[i]);
    }

    if (array.length === 0) {
        showSnackbar("There is no active flights at the moment...", "info");
    }
}

function showSnackbar(msg, type) {
    //console.log("showSnackbar: " + msg);

    // Get the snackbar DIV
    let x = document.getElementById("snackbar");

    // Add the "show" class to DIV
    x.className = "show";
    x.textContent = msg || "No Message Input";

    type = type || "error";

    if (type === "error") {
        x.classList.add("error");
    } else if (type === "success") {
        x.classList.add("success");
    } else if (type === "info") {
        x.classList.add("info");
    }

    // After 3 seconds, remove the show class from DIV
    setTimeout(function () {
        x.className = x.className.replace("show", "");
        x.className = x.className.replace("error", "");
        x.className = x.className.replace("success", "");
    }, 7000);

    return msg;
}

function showFlightDetails(flightPlan) {
    console.log("showFlightDetails");

    let title = document.getElementById("flightCardTitle");
    let company = document.getElementById("flightCardCompany");
    let passengers = document.getElementById("flightCardPassengers");
    let startPoint = document.getElementById("flightCardStartPoint");
    let endPoint = document.getElementById("flightCardEndPoint");

    if (flightPlan === null) {
        title.textContent = "";
        company.textContent = "";
        passengers.textContent = "";
        startPoint.textContent = "";
        endPoint.textContent = "";
    } else {
        let segments = flightPlan.segments || flightPlan.Segments;
        let segmentsLength = segments.length;
        let initialLocation = flightPlan.initial_location || flightPlan.initialLocation;
        title.textContent = "Flight ID: " + selectedFlightPlanId;
        company.textContent = "Company: " + (flightPlan.company_name || flightPlan.companyName);
        passengers.textContent = "Passengers: " + flightPlan.passengers;
        startPoint.textContent = "Start: " + initialLocation.latitude + "," + initialLocation.longitude;
        endPoint.textContent = "End: " +
            (segments[segmentsLength - 1].latitude) + "," +
            (segments[segmentsLength - 1].longitude);
    }

    return flightPlan;
}

function clearLists() {
    //console.log("clearLists");

    let list_internal = document.getElementById("flight_list");
    let childrenCount = list_internal.childElementCount;
    for (let i = 0; i < childrenCount; i++) {
        list_internal.removeChild(list_internal.firstElementChild);
    }

    let list_external = document.getElementById("flight_list_external");
    childrenCount = list_external.childElementCount;
    for (let i = 0; i < childrenCount; i++) {
        list_external.removeChild(list_external.firstElementChild);
    }
}

function setActive(id) {
    console.log("setActive: " + id);

    let listItem = document.getElementById(id);
    listItem.classList.add("active");

    let item_btn = document.getElementById(id + "_delete_btn");
    if (item_btn !== null) {
        item_btn.classList.replace("btn-outline-danger", "btn-danger");
    }

    selectedFlightPlanId = id;
}

function setNotActive(id) {
    console.log("setNotActive: " + id);

    let listItem = document.getElementById(id);
    if (listItem !== null) {
        listItem.classList.remove("active");
    }

    let item_btn = document.getElementById(id + "_delete_btn");
    if (item_btn !== null) {
        item_btn.classList.replace("btn-danger", "btn-outline-danger");
    }

    selectedFlightPlanId = null;
}

function checkSelectedPlan() {
    //console.log("checkSelectedPlan");


    if (selectedFlightPlanId != null && selectedFlightPlanId != undefined) {
        if (!isMarkerExist(selectedFlightPlanId)) {
            currentPath.setMap(null);
            selectedFlightPlanId = null;
            return false;
        }
    }

    return true;
}

function flightClicked(id) {
    console.log("flightClicked: " + id);

    if (id !== selectedFlightPlanId) {
        if (selectedFlightPlanId !== null && selectedFlightPlanId !== undefined) {
            setNotActive(selectedFlightPlanId);
        }
        setActive(id);
    }

    fetchFlightPlanById(id);
}


//dropzone handling
(function () {
    const dropZone = document.getElementById('myFlights');

    //handle Json file from DropZone
    let upload = function (files) {
        //file reader
        let reader = new FileReader();
        reader.readAsText(files[0]);

        //Invoked when reader reads the file
        reader.onload = function (e) {
            //stores text from json file
            let text = reader.result;
            let postOption = {
                "method": "POST",
                "headers": {
                    'Content-Type': "application/json;charset=utf-8"
                },
                "body": text
            };

            fetch("/api/FlightPlan", postOption)
                .then(response => response.json())
                .then(showSnackbar("FlightPlan Uploaded Successfully!", "success"))
                .catch(error => showSnackbar(error))
        }

    };

    dropZone.ondrop = function (e) {
        //prevent open file in browser - deafault drop behavior
        e.preventDefault();
        this.className = 'myFlights';
        upload(e.dataTransfer.files);
    }

    //mouse enter
    dropZone.ondragover = function () {
        this.className = 'myFlights dragOver';
        return false;
    }
    //mouse leave
    dropZone.ondragleave = function () {
        this.className = 'myFlights';
        return false;
    }
}());


//API requests
function fetchFlightsSyncAll() {
    //console.log("fetchFlightsSyncAll");

    let getOptions = {
        "method": "GET"
    }
    let now = new Date();
    //send GET request
    fetch("/api/Flights?relative_to=" + now.toISOString() + "&sync_all", getOptions)
        .then(response => response.json())
        .then(flight => addFlightsArrayToFlightList(flight))
        .then(checkSelectedPlan())
        .catch(error => showSnackbar(error))
}

async function fetchFlightPlanById(id) {
    console.log("fetchFlightPlanById: " + id);

    let getOptions = { "method": "GET" }

    let item = document.getElementById(id);
    let url = item.getAttribute("from");

    //send GET request
    fetch(url + "/api/FlightPlan/" + id, getOptions)
        .then(response => {
            if (response.status === 200) {
                return response.json();
            }
        })
        .then(plan => showFlightDetails(plan))
        .then(plan => buildAndShowRoute(plan))
        .catch(error => showSnackbar(error))
}

function removeFlight(flightId) {
    console.log("removeFlight: " + flightId);

    if (flightId === selectedFlightPlanId) {
        setNotActive(selectedFlightPlanId);
        showFlightDetails(null);
    }

    removeMarkerById(flightId);

    //create DELETE request
    let deleteOptions = {
        "method": "DELETE",
    };

    //send DELETE request
    fetch("/api/Flights/" + flightId, deleteOptions)
        .then(response => response.json())
        .then(showSnackbar("FlightPlan " + flightId + " Deleted Successfully!", "success"))
        .then(document.getElementById(flightId).remove())
        .catch(error => console.log(error))
}


//function for test buttons
function postFlight() {
    console.log("postFlight");

    let now = new Date();
    //create demo flight object
    let demoFlight = {
        "passengers": 69,
        "company_name": "JavaScriptAir",
        "initial_location": {
            "longitude": 34.847036,
            "latitude": 32.130232,
            "date_time": now.toISOString()
        },
        "segments": [
            {
                "longitude": 34.851563,
                "latitude": 32.129792,
                "timespan_seconds": 30
            },
            {
                "longitude": 34.851841,
                "latitude": 32.134977,
                "timespan_seconds": 30
            }
        ]
    };

    //create POST request
    let postOptions = preparePost(demoFlight);

    //send POST request
    fetch("/api/FlightPlan", postOptions)
        .then(response => response.json())
        .then(showSnackbar("FlightPlan Posted Successfully!", "success"))
        .catch(error => showSnackbar(error))


}

function preparePost(flight) {
    let flightAsStr = JSON.stringify(flight);
    return {
        "method": "POST",
        "headers": {
            'Content-Type': "application/json;charset=utf-8"
        },
        "body": flightAsStr
    }
}
