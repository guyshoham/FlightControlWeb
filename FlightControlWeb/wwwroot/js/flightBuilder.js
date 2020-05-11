function AddFlightToList() {

    //create demo flight object
    let flight = {
        "passengers": 69,
        "company_name": "JavaScriptAir",
        "initial_location": {
            "longitude": 33.000,
            "latitude": 31.000,
            "date_time": "2020-12-26T20:00:00Z"
        },
        "segments": [
            {
                "longitude": 33.500,
                "latitude": 31.500,
                "timespan_seconds": 600
            }
        ]
    };

    //create POST request
    let postOptions = preparePost(flight);

    //send POST request
    fetch("/api/FlightPlan", postOptions)
        .then(response => response.json())
        .then(appendItem) //add flight to list
        .catch(error => console.log(error))


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
    let item = document.createElement("a");
    item.id = flight.flightPlanId;
    item.className = "list-group-item list-group-item-action";
    item.onclick = function () { showDetails(item.id) };

    //generate the inner content of the item
    const content = `<i class="fas fa-plane"></i>
                    ${flight.flightPlanId}
                    <br>
                    <i class="fas fa-user-tie"></i>
                    ${flight.companyName}
                    </i>
                    <div class="btn btn-xs btn-outline-danger btn-position-right" onclick="alert(' ${flight.flightPlanId} -> Remove'); event.stopPropagation();">
                    X
                    </div>`;

    item.innerHTML = content;

    //add item to list
    flights.append(item);
}

function showDetails(flightId) {
    alert(flightId + "-> Details");
}