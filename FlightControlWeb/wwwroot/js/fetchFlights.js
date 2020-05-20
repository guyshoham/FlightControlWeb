
setInterval(fetchFlight, 5000);


function fetchFlight() {
    let getOptions = {
        "method": "GET"
    }
    let now = new Date();
    //send GET request
    fetch("http://localhost:51271/api/Flights?relative_to=" + now.toISOString(), getOptions)
        .then(response => response.json())
        .then(flight => addFlightsArrayToFlightList(flight))
        .then(checkCurrentPath())
        .catch(error => showSnackbar(error))
}

function fetchFlightPlanById(id) {
    let getOptions = {
        "method": "GET"
    }

    //send GET request
    fetch("http://localhost:51271/api/FlightPlan/" + id, getOptions)
        .then(response => response.json())
        .then(plan => showFlightDetails(plan))
        .then(plan => buildAndShowRoute(plan))
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

function showSnackbar(error, type) {
    // Get the snackbar DIV
    var x = document.getElementById("snackbar");

    // Add the "show" class to DIV
    x.className = "show";
    x.textContent = error || "No Message Input";

    type = type || "error";

    if (type === "error") {
        x.classList.add("error");
    } else {
        x.classList.add("success");
    }

    // After 3 seconds, remove the show class from DIV
    setTimeout(function () {
        x.className = x.className.replace("show", "");
        x.className = x.className.replace("error", "");
        x.className = x.className.replace("success", "");
    }, 7000);
}

function showFlightDetails(flightPlan) {
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
        let segmentsLength = flightPlan.segments.length;
        title.textContent = "Flight ID: " + flightPlan.flightId;
        company.textContent = "Company: " + flightPlan.companyName;
        passengers.textContent = "Passengers: " + flightPlan.passengers;
        startPoint.textContent = "Start: " +
            flightPlan.initialLocation.latitude + "," +
            flightPlan.initialLocation.longitude;

        endPoint.textContent = "End: " +
            flightPlan.segments[segmentsLength - 1].latitude + "," +
            flightPlan.segments[segmentsLength - 1].longitude;
    }

    return flightPlan;
}
