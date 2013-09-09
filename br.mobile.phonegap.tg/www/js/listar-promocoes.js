document.addEventListener("deviceready", function () {
    listarPromocoes.init();
    gps.obterPosicao();
}, true);

//$(document).ready(function () {
    
//    $('#mensagem-carregando').css("display", "none");
//    $('#mensagem-erro').css("display", "none");
//    $('#mensagem-conexao').css("display", "none");
    
//});

//function onDeviceReady() {
//    $("#mensagem").html("onDeviceReady()");
//    listarPromocoes.init();


//    recuperarPosicaoGPS();    
//    if (statusInternet(checkConnection()) == true) {
//        chamadaServidor(window.localStorage.getItem("latitude"), window.localStorage.getItem("longitude"));
//    }
//    else {
//        //Mensagem de falta de conexao
//        $(function () {
//            $("#mensagem-conexao").dialog({
//                height: 140,
//                modal: true
//            });
//        });
//    }
//       
//}

function statusInternet(states) {
    var status = true;
    var networkState = navigator.connection.type;
    $('#place').append('status internet: ' + states[networkState]);
    if (states[networkState] == "No network connection") {
        status = false;
    }
    return status;
}

function checkConnection() {
    var networkState = navigator.connection.type;

    var states = {};
    states[Connection.UNKNOWN] = 'Unknown connection';
    states[Connection.ETHERNET] = 'Ethernet connection';
    states[Connection.WIFI] = 'WiFi connection';
    states[Connection.CELL_2G] = 'Cell 2G connection';
    states[Connection.CELL_3G] = 'Cell 3G connection';
    states[Connection.CELL_4G] = 'Cell 4G connection';
    states[Connection.NONE] = 'No network connection';   
    return states;
}


function recuperarPosicaoGPS() {
        var win = function (position) {
            window.localStorage.setItem("latitude", position.coords.latitude);
            window.localStorage.setItem("longitude", position.coords.longitude);
                                  
        };

        var fail = function (e) {
            $('#place').append('FAIL GPS: ');
        };

        navigator.geolocation.getCurrentPosition(win, fail);
}

//function chamadaServidor(latitude, longitude) {
//    var url = 'http://localhost:9999/Promocao/ListarPromocao';
//    $(function () {
//        $("#mensagem-carregando").dialog({
//            height: 140,
//            dialogClass: "no-close",
//            modal: true
//        });
//    });
//    $('li').remove();
//    $.ajax({
//        type: 'GET',
//        url: url,
//        contentType: "application/json",
//        dataType: 'jsonp',
//        data: { latitude: latitude, longitude: longitude },
//        crossDomain: true,
//        global: true,
//        success: function (res) {
//            $('#place').append('sucesso');
//            $('#mensagem-carregando').dialog("destroy");
//            $('#mensagem-carregando').css("display", "none");
//            window.localStorage.setItem("dadosJson", JSON.stringify(res));
//            listarPromocoes.geraLista(res);
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            $('#mensagem-carregando').dialog("destroy");
//            $(function () {
//                $("#mensagem-erro").dialog({
//                    height: 140,
//                    modal: true
//                });
//            });
//        },
//        complete: function (data) {
//            $('#place').append('cplete');
//        }
//    });
//}


var listarPromocoes = {
    init: function () {
        listarPromocoes.bind();
        listarPromocoes.chamadaServidor(gps.latitude, gps.longitude);
    },

    //    mobileInit: function() {
    //        document.addEventListener("deviceready", appReady, false);
    //        $(document).bind("mobileinit", function() {
    //            $.mobile.allowCrossDomainPages = true;
    //        });
    //    },

    bind: function () {
        $('ul').on('click', '.imagem-promocao', function () {
            var idPromocao = 0;
            var id = $(this).attr('id');
            idPromocao = id;
            window.localStorage.setItem("idSelecionado", id);
            window.localStorage.idSelecionado = idPromocao;
            window.location = "gps.html";
        });

        $("#atualizar-promocao").click(function () {
            listarPromocoes.chamadaServidor(gps.latitude, gps.longitude);
        });
    }

    , chamadaServidor: function (latitude, longitude) {

        var url = 'http://localhost:9999/Promocao/ListarPromocao';

        //        $(function () {
        //            $("#mensagem-carregando").dialog({
        //                height: 140,
        //                dialogClass: "no-close",
        //                modal: true
        //            });
        //        });

        $('li').remove();

        overlay.open();

        $.ajax({
            type: 'GET',
            url: url,
            contentType: "application/json",
            dataType: 'jsonp',
            data: { latitude: latitude, longitude: longitude },
            crossDomain: true,
            global: true,
            success: function (res) {
                $('#place').append('sucesso');
                $('#mensagem-carregando').dialog("destroy");
                $('#mensagem-carregando').css("display", "none");
                window.localStorage.setItem("dadosJson", JSON.stringify(res));
                listarPromocoes.geraLista(res);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $('#mensagem-carregando').dialog("destroy");
                $(function () {
                    $("#mensagem-erro").dialog({
                        height: 140,
                        modal: true
                    });
                });
            },
            complete: function (data) {
                $('#place').append('cplete');
                overlay.close();
            }
        });
    }

    , geraLista: function (data) {
        for (i = 0; i < data.length; i++) {
            $('.lista-promocoes').
                append(
                '<li class="well">' +
                '<div class="container corpo-promocoes">' +
                '<div class="titulo-empresa navbar-inner" id="titulo-empresa">' +
                '<img src="' + data[i].UrlEmpresa + '"' + 'class="pull-left logo-promocao" id="id-url-empresa">' +
                '<div class="container">' +
                '		<a class="brand" href="#">' + data[i].NomeEmpresa + '</a>' +
                '	</div>' +
                '</div>' +
                '<div class="imagem-promocao thumbnail" id="' + data[i].IdEmpresa + '">' +
                '	<img class="imagem" src="' + data[i].UrlPromocao + '"' +
                '	width="" height="">' +
                '</div>' +
                '<div class="control-group">' +
                '<div class="controls">' +
                '<label>' +
                data[i].DescricaoPromocao +
                '</label>' +
                '</div>' +
                '</div>' +
                '</li>'
            );
        }
    }
};


var gps = {
    latitude: 0.0,
    longitude: 0.0,

    obterPosicao: function () {
        navigator.geolocation.getCurrentPosition(gps.localizacaoSuccesso, gps.localizacaoError);
    },
    inicializar: function (lat, lon) {
        gps.latitude = lat;
        gps.longitude = lon;
    },
    localizacaoError: function (error) {
        gps.inicializar(-23.546, -46.638);
    },
    localizacaoSuccesso: function (position) {
        gps.inicializar(position.coords.latitude, position.coords.longitude);
    }
};