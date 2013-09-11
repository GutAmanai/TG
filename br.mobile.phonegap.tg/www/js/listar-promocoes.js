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

//function statusInternet(states) {
//    var status = true;
//    var networkState = navigator.connection.type;
//    $('#place').append('status internet: ' + states[networkState]);
//    if (states[networkState] == "No network connection") {
//        status = false;
//    }
//    return status;
//}

//function checkConnection() {
//    var networkState = navigator.connection.type;

//    var states = {};
//    states[Connection.UNKNOWN] = 'Unknown connection';
//    states[Connection.ETHERNET] = 'Ethernet connection';
//    states[Connection.WIFI] = 'WiFi connection';
//    states[Connection.CELL_2G] = 'Cell 2G connection';
//    states[Connection.CELL_3G] = 'Cell 3G connection';
//    states[Connection.CELL_4G] = 'Cell 4G connection';
//    states[Connection.NONE] = 'No network connection';   
//    return states;
//}


//function recuperarPosicaoGPS() {
//        var win = function (position) {
//            window.localStorage.setItem("latitude", position.coords.latitude);
//            window.localStorage.setItem("longitude", position.coords.longitude);
//                                  
//        };

//        var fail = function (e) {
//            $('#place').append('FAIL GPS: ');
//        };

//        navigator.geolocation.getCurrentPosition(win, fail);
//}



var listarPromocoes = {
    init: function () {
        listarPromocoes.bind();
        listarPromocoes.chamadaServidor(gps.latitude, gps.longitude);
    },

    bind: function () {
        $('.imagem-promocao').on('click', function () {
            window.localStorage.setItem("promocaoSelecionado", $(this).parents(".corpo-promocoes").eq(0).attr("id"));
            window.location = "gps.html";
        });

        $("#atualizar-promocao").click(function () {
            listarPromocoes.chamadaServidor(gps.latitude, gps.longitude);
        });
    }

    , chamadaServidor: function (latitude, longitude) {

        var url = 'http://localhost:9999/Promocao/ListarPromocao';

        $('li').remove();

        $.ajax({
            type: 'GET',
            url: url,
            contentType: "application/json",
            dataType: 'jsonp',
            data: { latitude: latitude, longitude: longitude },
            beforeSend: function () {
                overlay.open();
            },
            crossDomain: true,
            global: true,
            success: function (res) {
                listarPromocoes.geraLista(res);
                listarPromocoes.bind();
            },
            error: function (xhr, ajaxOptions, thrownError) {
            },
            complete: function (data) {
                overlay.close();
            }
        });
    }

    , geraLista: function (data) {
        for (i = 0; i < data.length; i++) {
            window.localStorage.setItem(data[i].IdPromocao, JSON.stringify(data[i]));
            $('.lista-promocoes').append(
                '	<li class="well">																						' +
                '		<div class="container corpo-promocoes" id="' + data[i].IdPromocao + '" >  							' +
                '			<div class="titulo-empresa navbar-inner" >														' +
                '				<img src="' + data[i].UrlEmpresa + '"' + 'class="pull-left logo-promocao">					' +
                '				<div class="container">																		' +
                '					<a class="brand" href="#">' + data[i].NomeEmpresa + '</a>								' +
                '				</div>																						' +
                '			</div>																							' +
                //'		    <input type="hidden" class="obj-promocao" value="' + JSON.stringify(data[i]) + '"/>			    ' +
                '			<div class="imagem-promocao thumbnail" id="' + data[i].IdEmpresa + '">							' +
                '				<img class="imagem" src="' + data[i].UrlPromocao + '"/> 									' +
                '			</div>																							' +
                '			<div class="control-group">																		' +
                '				<div class="controls">																		' +
                '					<label>' + data[i].DescricaoPromocao + '</label>    									' +
                '				</div>																						' +
                '			</div>																							' +
                '	    </div>																						        ' +
                '	</li>																									'
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