
$(document).ready(function () {    
    imageFix();
    addListeners();    
});

function onDeviceReady() {
    var data = localStorage.dataJSON;
    var latitude = data.promocoes[1].Latitude;

    $('.place').append('ssa' + localStorage.getItem("latitude"));
    preencheDadosLugar();
    CoordMapType();
}

function addListeners(){
    document.addEventListener("deviceready", onDeviceReady, false);
}

/*
========Image size fix==================
*/      
function imageFix(){
    $('.logo-url-empresa').width(40);
    $('.logo-empresa').width(70);
    $('.logo-lotus').width(47);
}     

/*
=========Discover actual gps coord==============
*/

function discoverGPS() {
    var win = function (position) {
        localStorage.latitude = position.coords.latitude;
        localStorage.longitude = position.coords.longitude;
    };

    var fail = function (e) {
        alert('Can\'t retrieve position.\nError: ' + e);
    };

    navigator.geolocation.getCurrentPosition(win, fail);
}

/*
=============Fill place information===================
*/
function preencheDadosLugar(){
    var data = localStorage.dataJSON;

    for (var i in data.promocoes) {

        if (data.promocoes[i].IdEmpresa == localStorage.idSelecionado) {
            $('#imagem-empresa').attr('src', data.promocoes[i].UrlEmpresa);
            $('#nome-empresa').replaceWith('<a class="brand titulo-empresa" id="nome-empresa" href="#">' + data.promocoes[i].NomeEmpresa + '</a>');            
            $('#endereco').replaceWith('<span class="label" id="endereco">Endereço:' + data.promocoes[i].Endereco + '</span>');
        }
    }
}

/*
============google map===============
*/

function CoordMapType(tileSize) {
    this.tileSize = tileSize;
}

CoordMapType.prototype.tileSize = new google.maps.Size(256, 256);
CoordMapType.prototype.maxZoom = 19;

CoordMapType.prototype.getTile = function (coord, zoom, ownerDocument) {
    var div = ownerDocument.createElement('div');
    div.innerHTML = coord;
    div.style.width = this.tileSize.width + 'px';
    div.style.height = this.tileSize.height + 'px';
    div.style.fontSize = '0';
    div.style.borderStyle = 'solid';
    div.style.borderWidth = '0px';
    div.style.borderColor = '#AAAAAA';
    return div;
};

var data = localStorage.dataJSON;
var latitude;
var longitude;
for (var i in data.promocoes) {

    if (data.promocoes[i].IdEmpresa == localStorage.idSelecionado) {
        longitude = data.promocoes[i].Longitude;
        latitude = data.promocoes[i].Latitude;

    }
}


var map;
var chicago = new google.maps.LatLng(longitude, latitude);

function initialize() {
    var mapOptions = {
        zoom: 15,
        center: chicago,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById('map-canvas'),
                                    mapOptions);

    // Insert this overlay map type as the first overlay map type at
    // position 0. Note that all overlay map types appear on top of
    // their parent base map.
    map.overlayMapTypes.insertAt(
      0, new CoordMapType(new google.maps.Size(256, 256)));
}

google.maps.event.addDomListener(window, 'load', initialize);
