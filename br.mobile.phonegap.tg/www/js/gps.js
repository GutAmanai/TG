
document.addEventListener("deviceready", function () {
    mapaPromocao.init();
    localizacao.init();    
}, true);


var localizacao = {
    init: function () {
        localizacao.bind();
        localizacao.promocaoAcesso();
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
    }

    , promocaoAcesso: function () {
        var idSelecionado = parseInt(window.localStorage.getItem("promocaoSelecionado"));
        var url = 'http://localhost:9999/Promocao/PromocaoAcesso';
        //online  http://www.lotustg.com.br/Promocao/PromocaoAcesso
        //offline http://localhost:9999/Promocao/PromocaoAcesso
        $.ajax({
            type: 'GET',
            url: url,
            contentType: "application/json",
            dataType: 'jsonp',
            data: { idPromocao: idSelecionado },
            crossDomain: true,
            global: true,
            success: function (res) {
            },
            error: function (xhr, ajaxOptions, thrownError) {
            },
            complete: function (data) {
            }
        });
    }
};

var mapaPromocao = {

    init: function () {
        document.addEventListener("deviceready", mapaPromocao.showGeolocationInfo, true);
        mapaPromocao.bind();
    }

    , showGeolocationInfo: function () {
        navigator.geolocation.getCurrentPosition(googleMaps.localizacaoSuccesso, googleMaps.localizacaoError);
    }

    , bind: function () {
        $("#rota-latlong").on('click', function (e) {
            e.preventDefault();
            var $dado = localizacao.obterDadosPromocao();
            googleMaps.calculaRotaByLatLng(parseFloat($dado.Latitude), parseFloat($dado.Longitude));
        });
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
            zoom: 15,
            center: googleMaps.posicaoAtual,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        googleMaps.directionsDisplay.setMap(googleMaps.map);

        var currentPositionMarker = new google.maps.Marker({
            position: googleMaps.posicaoAtual,
            map: googleMaps.map,
            title: "Posição atual"
        });
    }

    , localizacaoError: function (error) {
        googleMaps.inicializar(-23.546, -46.638);
    }

    , localizacaoSuccesso: function (position) {
        googleMaps.inicializar(position.coords.latitude, position.coords.longitude);
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
                    googleMaps.directionsDisplay.setDirections(response);
                } else {
                    $(".conteudo").append("Sem rota!");
                }
            });

        } else {
            $(".conteudo").append("Posição ou destino inválidos!");
        }
    }

};
