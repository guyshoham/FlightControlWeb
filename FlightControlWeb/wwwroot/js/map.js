function initMap() {
    //Map options
    var options = {
        zoom: 15,
        center: { lat: 32.130232, lng: 34.847036 }
    }
    //New Map
    var map = new google.maps.Map(document.getElementById('map'), options);

    /*
    //Add marker
    var marker = new google.maps.Marker({
        position: { lat: 32.130232, lng: 34.847036 },
        map: map,
        icon: 'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png'
    })

    */

    //TODO: should save icon in wwwroot folder
    var icon = {
        url: "https://cdn3.iconfinder.com/data/icons/map-markers-1/512/airplane-512.png", // url
        scaledSize: new google.maps.Size(24, 24), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(0, 0) // anchor
    };

    //Array of markers
    var markers = [
        { lat: 32.130232, lng: 34.847036 },
        { lat: 32.131666, lng: 34.847036 },
        { lat: 32.132999, lng: 34.847036 }
    ]

    //Loop through markers
    for (var i = 0; i < markers.length; i++) {
        addMarker(markers[i]);
    }

    //Add Marker Function
    function addMarker(coords) {
        //Add marker
        var marker = new google.maps.Marker({
            position: coords,
            map: map,
            icon: icon
        })
        var infoWindow = new google.maps.InfoWindow({
            content: "<h1>Guy's house</h1>"
        });
        marker.addListener('click', function () {
            var child = document.getElementById("currentFlight");
            var node = document.createTextNode("Guy's house");
            child.appendChild(node);
            var element = document.getElementById("flightDetails");
            element.appendChild(child);
        });

    }
}