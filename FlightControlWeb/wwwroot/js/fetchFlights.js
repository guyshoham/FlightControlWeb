
setInterval(fetchFlight, 5000);


function fetchFlight() {
    let getOptions = {
        "method": "GET"
    }

    //send GET request
    fetch("http://localhost:51271/api/Flights?relative_to=2020-12-26T18:05:00Z", getOptions)
        .then(response => response.json())
        .then(data => addFlightsArrayToFlightList(data))
        .catch(error => console.log(error))
}

function addFlightsArrayToFlightList(array) {
    clearList();
    deleteMarkers();

    for (var i = 0; i < array.length; i++) {
        appendItem(array[i]);
        addMarker(array[i]);
    }
}

function clearList() {
    let list = document.getElementById("flight_list");
    let childrenCount = list.childElementCount;
    for (var i = 0; i < childrenCount; i++) {
        list.removeChild(list.firstElementChild);
    }
}

