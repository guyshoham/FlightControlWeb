
setInterval(fetchFlight, 7000);


function fetchFlight() {
    let getOptions = {
        "method": "GET"
    }

    //send GET request
    fetch("http://localhost:51271/api/Flights?relative_to=2020-12-26T18:05:00Z", getOptions)
        .then(response => response.json())
        .then(data => addFlightsArrayToFlightList(data))
        .catch(error => showSnackbar(error))
}

function addFlightsArrayToFlightList(array) {
    clearList();
    deleteMarkers();

    for (var i = 0; i < array.length; i++) {
        marker = array[i];

        appendItem(marker);
        addMarker(marker);
        marker.addListener('click', toggleBounce);
    }
}

function clearList() {
    let list = document.getElementById("flight_list");
    let childrenCount = list.childElementCount;
    for (var i = 0; i < childrenCount; i++) {
        list.removeChild(list.firstElementChild);
    }
}

function showSnackbar(error) {
    // Get the snackbar DIV
    var x = document.getElementById("snackbar");

    // Add the "show" class to DIV
    x.className = "show";
    x.textContent = error || "No Message Input";

    // After 3 seconds, remove the show class from DIV
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 7000);
}

