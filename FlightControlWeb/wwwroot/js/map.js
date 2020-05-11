var map;
var icon;

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
        scaledSize: new google.maps.Size(24, 24), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(0, 0) // anchor
    };    
}
