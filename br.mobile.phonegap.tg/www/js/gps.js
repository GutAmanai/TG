
document.addEventListener("deviceready", function () {
    mapaPromocao.init();
    localizacao.init();    
}, true);


var localizacao = {
    init: function () {
        localizacao.bind();
        localizacao.adicionarRota();
    },

    bind: function () {
        $("#voltar").on("click", function () {
            window.history.back();
        });
    },

    obterDadosPromocao: function () {
        var idSelecionado = parseInt(window.localStorage.getItem("promocaoSelecionado"));
        var data = JSON.parse(window.localStorage.getItem("listagemPromocao"));
        var dataReturn;

        $.each(data, function (index, value) {
            if (parseInt(value.IdPromocao) == idSelecionado) {
                dataReturn = value;
            }
        });
        return dataReturn;
    },

    adicionarRota: function () {
        var $dado = localizacao.obterDadosPromocao();
        googleMaps.calculaRotaByLatLng(parseFloat($dado.Latitude), parseFloat($dado.Longitude));
    }
};

var mapaPromocao = {

    init: function () {
        document.addEventListener("deviceready", mapaPromocao.showGeolocationInfo, true);
    }

    , showGeolocationInfo: function () {
        navigator.geolocation.getCurrentPosition(googleMaps.localizacaoSuccesso, googleMaps.localizacaoError);
    }

    , bind: function () {
    }
};

var googleMaps = {

    map: new Object(),
    posicaoAtual: new Object(),
    directionsDisplay: new Object(),
    directionsService: new Object(),

    inicializar: function (lat, lon) {
        googleMaps.directionsDisplay = new google.maps.DirectionsRenderer();
        googleMaps.directionsService = new google.maps.DirectionsService();

        googleMaps.posicaoAtual = new google.maps.LatLng(lat, lon);

        googleMaps.map = new google.maps.Map(document.getElementById('map_canvas'), {
            zoom: 13,
            center: googleMaps.posicaoAtual,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        googleMaps.directionsDisplay.setMap(googleMaps.map);

        var currentPositionMarker = new google.maps.Marker({
            position: googleMaps.posicaoAtual,
            map: googleMaps.map,
            title: "Posição atual"
        });

        var infowindow = new google.maps.InfoWindow();
        google.maps.event.addListener(currentPositionMarker, 'click', function () {
            infowindow.setContent("Posição atual: latitude: " + lat + " longitude: " + lon);
            infowindow.open(map, currentPositionMarker);
        });
    }

    , localizacaoError: function (error) {
        googleMaps.inicializar(-23.546, -46.638);
    }

    , localizacaoSuccesso: function (position) {
        googleMaps.inicializar(position.coords.latitude, position.coords.longitude);
    }

    , calculaRotaByEnd: function (destino) {

        if (posicaoAtual != '' && destino != '') {
            var request = {
                origin: posicaoAtual,
                destination: destino,
                travelMode: google.maps.DirectionsTravelMode["DRIVING"]
            };

            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setPanel(document.getElementById("directions"));
                    directionsDisplay.setDirections(response);
                    $("#results").show();
                } else {
                    $("#mensagem").val("Sem rota " + destino);
                    $("#results").hide();
                }
            });
        } else {
            $("#mensagem").val("Posição ou destino invalidos " + destino);
            $("#results").hide();
        }
    }

    , calculaRotaByLatLng: function (lat, lng) {
        
        if (googleMaps.posicaoAtual != '') {
            
            var destino = new google.maps.LatLng(lat, lng);
            
            var request = {
                origin: googleMaps.posicaoAtual,
                destination: destino,
                travelMode: google.maps.DirectionsTravelMode["DRIVING"]
            };

            googleMaps.directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    $(".conteudo").append(JSON.stringify(response));
                    //directionsDisplay.setPanel(document.getElementById("directions"));
                    googleMaps.directionsDisplay.setDirections(response);
                    //$("#results").show();
                } else {
                    $(".conteudo").append("Sem rota");
                }
            });
            
        } else {
            $(".conteudo").append("Posição ou destino invalidos");
        }
    }

};
