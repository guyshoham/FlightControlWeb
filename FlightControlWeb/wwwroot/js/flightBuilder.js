function AddFlightToList() {

    //create demo flight object
    let flight = {
        "passengers": 69,
        "company_name": "JavaScriptAir",
        "initial_location": {
            "longitude": 34.847036,
            "latitude": 32.130232,
            "date_time": "2020-12-26T20:00:00Z"
        },
        "segments": [
            {
                "longitude": 34.847037,
                "latitude": 32.130233,
                "timespan_seconds": 600
            }
        ]
    };

    //create POST request
    let postOptions = preparePost(flight);

    //send POST request
    fetch("/api/FlightPlan", postOptions)
        .then(response => response.json())
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
    item.id = flight.flightId;
    item.className = "list-group-item list-group-item-action";
    item.onclick = function () { showDetails(item.id) };

    //generate the inner content of the item
    const content = `<i class="fas fa-plane"></i>
                    ${item.id}
                    <br>
                    <i class="fas fa-user-tie"></i>
                    ${flight.companyName}
                    </i>
                    <div class="btn btn-xs btn-outline-danger btn-position-right" onclick="removeFlight('${item.id}'); event.stopPropagation();">
                    X
                    </div>`;

    item.innerHTML = content;

    //add item to list
    flights.append(item);

    return flight;
}

function showDetails(flightId) {
    alert(flightId + "-> Details");
}

function removeFlight(flightId) {

    //create DELETE request
    let deleteOptions = {
        "method": "DELETE",
    };

    //send DELETE request
    fetch("/api/Flights/" + flightId, deleteOptions)
        .then(response => response.json())
        .then(document.getElementById(flightId).remove())
        .catch(error => console.log(error))
}
