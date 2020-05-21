var map;
var icon;
var marker;
var markers = []
var currentPath;

function initMap() {
    //map options
    var options = {
        zoom: 15,
        center: { lat: 32.130232, lng: 34.847036 }
    }

    //new Map
    map = new google.maps.Map(document.getElementById('map'), options);

    //TODO: should save icon in wwwroot folder
    icon = {
        url: "images/marker_icon.webp", // url
        scaledSize: new google.maps.Size(36, 36), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(0, 0) // anchor
    };
}

// Create flight marker and add it to map
function addMarker(flight) {
    let coords = { lat: flight.latitude, lng: flight.longitude };

    //Add marker
    marker = new google.maps.Marker({
        position: coords,
        map: map,
        icon: icon,
        title: flight.flight_id
    })
    markers.push(marker);

    //add listener
    marker.addListener('click', function () {
        selectedFlightPlanId = flight.flight_id;
        fetchFlightPlanById(selectedFlightPlanId);

        let item = document.getElementById(selectedFlightPlanId);
        item.classList.add("active");
        let item_btn = document.getElementById(selectedFlightPlanId + "_delete_btn");
        item_btn.classList.replace("btn-outline-danger", "btn-danger");
    });
}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}

// Marker bounce animation
function toggleBounce() {
    if (marker.getAnimation() !== null) {
        marker.setAnimation(null);
    } else {
        marker.setAnimation(google.maps.Animation.BOUNCE);
        setTimeout(function () { marker.setAnimation(null); }, 750);
    }
}

function removeMarkerById(id) {
    //iterate all markers and loop for the marker of the flight that removed
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].title === id) {
            //remove marker from map and from array
            markers[i].setMap(null);
            markers.splice(i, 1); i--;

            //if the current path belongs to that marker, remove it from map
            if (currentPath.tag === id) {
                currentPath.setMap(null);
            }
        }
    }
}

function buildAndShowRoute(flightPlan) {

    // do not clear the current path from map on the first time
    if (currentPath !== undefined && currentPath !== null) {
        currentPath.setMap(null);
    }

    //init array of coords
    let flightPlanCoordinates = [];

    //collect the current position of the flight (relative to time)
    let item = document.getElementById(flightPlan.flightId);
    let pos = {
        lat: Number(item.getAttribute("data-lat")),
        lng: Number(item.getAttribute("data-lng"))
    };

    //add initial location landmark
    let init = {
        lat: flightPlan.initialLocation.latitude,
        lng: flightPlan.initialLocation.longitude
    };

    let prev = init;
    let after;
    let flag = false;

    //push init location to arr
    flightPlanCoordinates.push(init);

    //add all segments landmarks
    for (let i = 0; i < flightPlan.segments.length; i++) {
        let landMark = {
            lat: flightPlan.segments[i].latitude,
            lng: flightPlan.segments[i].longitude
        };

        after = landMark;

        //check if the current pos of the flight is between prev and next segments
        if (!flag && checkPos(prev, pos, after)) {
            flightPlanCoordinates.push(pos);
            flag = true;
        }

        flightPlanCoordinates.push(landMark);

        prev = landMark;
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

function checkPos(prev, pos, after) {

    //check lat
    if ((after.lat - prev.lat) >= 0) {
        if (pos.lat >= prev.lat && pos.lat <= after.lat) { return true; }
        else { return false; }
    } else {
        if (pos.lat <= prev.lat && pos.lat >= after.lat) { return true; }
        else { return false; }
    }

    //check lng
    if ((after.lng - prev.lng) >= 0) {
        if (pos.lng >= prev.lng && pos.lng <= after.lng) { return true; }
        else { return false; }
    } else {
        if (pos.lng <= prev.lng && pos.lng >= after.lng) { return true; }
        else { return false; }
    }

}

function checkCurrentPath() {

    // if path is not drawn, do nothing
    if (currentPath === undefined || currentPath === null) {
        return;
    }

    //get path id
    let id = currentPath.tag;
    let flag = false;
    
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].title === id) { // found the marker that belongs to path, set flag to true
            flag = true;           
        }
    }

    if (!flag) { // if flag is false, that means the path belongs to an old flight. remove path from map
        currentPath.setMap(null);
    }
}
