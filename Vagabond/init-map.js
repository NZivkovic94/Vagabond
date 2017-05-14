var markers = [];
var markerCluster;
var map;
function initMap() {
	map = new google.maps.Map(document.getElementById('map'), {
		zoom: 13,
		center: { lat: 44.817, lng: 20.467 },
		styles: [
            {
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#ebe8cd"
					}
				]
            },
            {
				"elementType": "labels.text.fill",
				"stylers": [
					{
						"color": "#523735"
					}
				]
            },
            {
				"elementType": "labels.text.stroke",
				"stylers": [
					{
						"color": "#f5f1e6"
					}
				]
            },
            {
				"featureType": "administrative",
				"elementType": "geometry.stroke",
				"stylers": [
					{
						"color": "#c9b2a6"
					}
				]
            },
            {
				"featureType": "administrative.land_parcel",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "administrative.land_parcel",
				"elementType": "geometry.stroke",
				"stylers": [
					{
						"color": "#dcd2be"
					}
				]
            },
            {
				"featureType": "administrative.land_parcel",
				"elementType": "labels.text.fill",
				"stylers": [
					{
						"color": "#ae9e90"
					}
				]
            },
            {
				"featureType": "administrative.neighborhood",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "landscape.natural",
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#dfd2ae"
					}
				]
            },
            {
				"featureType": "poi",
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#dfd2ae"
					}
				]
            },
            {
				"featureType": "poi",
				"elementType": "labels.text",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "poi",
				"elementType": "labels.text.fill",
				"stylers": [
					{
						"color": "#93817c"
					}
				]
            },
            {
				"featureType": "poi.business",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "poi.park",
				"elementType": "geometry.fill",
				"stylers": [
					{
						"color": "#a5b076"
					}
				]
            },
            {
				"featureType": "poi.park",
				"elementType": "labels.text.fill",
				"stylers": [
					{
						"color": "#447530"
					}
				]
            },
            {
				"featureType": "road",
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#58491e"
					}
				]
            },
            {
				"featureType": "road",
				"elementType": "labels",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "road",
				"elementType": "labels.icon",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "road.highway",
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#f8c967"
					}
				]
            },
            {
				"featureType": "road.highway",
				"elementType": "geometry.stroke",
				"stylers": [
					{
						"color": "#e9bc62"
					}
				]
            },
            {
				"featureType": "road.highway.controlled_access",
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#e98d58"
					}
				]
            },
            {
				"featureType": "road.highway.controlled_access",
				"elementType": "geometry.stroke",
				"stylers": [
					{
						"color": "#db8555"
					}
				]
            },
            {
				"featureType": "road.local",
				"elementType": "labels.text.fill",
				"stylers": [
					{
						"color": "#806b63"
					}
				]
            },
            {
				"featureType": "transit",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "transit.line",
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#dfd2ae"
					}
				]
            },
            {
				"featureType": "transit.line",
				"elementType": "labels.text.fill",
				"stylers": [
					{
						"color": "#8f7d77"
					}
				]
            },
            {
				"featureType": "transit.line",
				"elementType": "labels.text.stroke",
				"stylers": [
					{
						"color": "#ebe3cd"
					}
				]
            },
            {
				"featureType": "transit.station",
				"elementType": "geometry",
				"stylers": [
					{
						"color": "#dfd2ae"
					}
				]
            },
            {
				"featureType": "water",
				"elementType": "geometry.fill",
				"stylers": [
					{
						"color": "#b9d3c2"
					}
				]
            },
            {
				"featureType": "water",
				"elementType": "labels.text",
				"stylers": [
					{
						"visibility": "off"
					}
				]
            },
            {
				"featureType": "water",
				"elementType": "labels.text.fill",
				"stylers": [
					{
						"color": "#92998d"
					}
				]
            }
		]
	});

	var labels = [];

	var infowindow = new google.maps.InfoWindow();

	// Add some markers to the map.
	// Note: The code uses the JavaScript Array.prototype.map() method to
	// create an array of markers based on a given "locations" array.
	// The map() method here has nothing to do with the Google Maps API.
	markers = locations.map(function (location, i) {
		var score = messages[i].LikesNum - messages[i].DislikeNum; 
		var d = (score > 20)? "Hall of fame!" : messages[i].MessageText;
		var m = new google.maps.Marker({
            position: location,
            label: labels[i % labels.length],
			desc: d +"<br />" + 'Likes: ' + messages[i].LikesNum + ' Dislikes: ' + messages[i].DislikeNum,
			icon: pinSymbol(RGB2HTML(100+score*3, 0, 0))
		});
		google.maps.event.addListener(m, 'mouseover', function (e) {
			infowindow.setContent(m.desc);
			infowindow.open(map, m);
		});
		google.maps.event.addListener(m, 'mouseout', function(e){
			infowindow.close();
		});
		return m;
	});


	var oms = new OverlappingMarkerSpiderfier(map,
        { markersWontMove: true, markersWontHide: true, basicFormatEvents: true});

	// This is necessary to make the Spiderfy work
	oms.addListener('click', function (marker) {
        infowindow.open(map, marker);
	});

	oms.addListener('format', function (marker, status) {
		var lab = status == OverlappingMarkerSpiderfier.markerStatus.SPIDERFIED ? '+' :
				status == OverlappingMarkerSpiderfier.markerStatus.UNSPIDERFIED ? '' :
					null;
		marker.label = lab;
	});

	for (var i = 0; i < markers.length; i++) {
		oms.addMarker(markers[i]);
	}

	// Add a marker clusterer to manage the markers.
	markerCluster = new MarkerClusterer(map, markers,
		{ 
			imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m',
			ignoreHiddenMarkers: true 
	});

	var minClusterZoom = 15;
	markerCluster.setMaxZoom(minClusterZoom);
}

function RGB2HTML(red, green, blue)
{

    var decColor =0x1000000+ ((blue < 255)? blue : 255) + 0x100 * ((green < 255)? green : 255) + 0x10000 *((red < 255)? red : 255);
    return '#'+decColor.toString(16).substr(1);
}

function pinSymbol(color) {
    return {
        path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z M -2,-30 a 2,2 0 1,1 4,0 2,2 0 1,1 -4,0',
        fillColor: color,
        fillOpacity: 1,
        strokeColor: '#000',
        strokeWeight: 2,
        scale: 1,
   };
}