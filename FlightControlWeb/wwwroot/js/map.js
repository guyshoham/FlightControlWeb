var map;
var icon;
var marker
var markers = []

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
        url: "https://cdn3.iconfinder.com/data/icons/map-markers-1/512/airplane-512.png", // url
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
        title: flight.flightId
    })
    markers.push(marker);

    //add listener
    marker.addListener('click', function () {
        fetchFlightPlanById(marker.title);
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
