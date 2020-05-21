var selectedFlightPlanId;

function AddFlightToList() {
    let now = new Date();
    //create demo flight object
    let flight = {
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
    let postOptions = preparePost(flight);

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

function appendItem(flight) {
    let flights = document.getElementById("flight_list");
    let item = document.createElement("li");
    item.id = flight.flight_id;
    item.className = "list-group-item list-group-item-action list-group-hover";
    item.onclick = function () {
        if ((selectedFlightPlanId !== undefined) && (selectedFlightPlanId !== null)) {
            let listItem = document.getElementById(selectedFlightPlanId);
            listItem.classList.remove("active");
            let item_btn = document.getElementById(selectedFlightPlanId + "_delete_btn");
            item_btn.classList.replace("btn-danger", "btn-outline-danger");
        }
        selectedFlightPlanId = item.id;
        showDetails()
    };
    item.setAttribute("data-lat", flight.latitude);
    item.setAttribute("data-lng", flight.longitude);

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

    if (item.id === selectedFlightPlanId) {
        item.classList.add("active");
        let item_btn = document.getElementById(selectedFlightPlanId + "_delete_btn");
        item_btn.classList.replace("btn-outline-danger", "btn-danger");
    }

    return flight;
}

function showDetails() {
    fetchFlightPlanById(selectedFlightPlanId);
    let listItem = document.getElementById(selectedFlightPlanId);
    listItem.classList.add("active");
    let item_btn = document.getElementById(selectedFlightPlanId + "_delete_btn");
    item_btn.classList.replace("btn-outline-danger", "btn-danger");

}

function removeFlight(flightId) {

    if (flightId === selectedFlightPlanId) {
        selectedFlightPlanId = null;
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
