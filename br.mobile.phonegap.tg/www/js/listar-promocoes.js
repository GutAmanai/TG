document.addEventListener("deviceready", onDeviceReady, false);



$(document).ready(function () {
    listarPromocoes.init();
    $('#mensagem-carregando').css("display", "none");
    $('#mensagem-erro').css("display", "none");
    $('#mensagem-conexao').css("display", "none");
    
});

function onDeviceReady() {   
    recuperarPosicaoGPS();    
    if (statusInternet(checkConnection()) == true) {
        chamadaServidor(window.localStorage.getItem("latitude"), window.localStorage.getItem("longitude"));
    }
    else {
        //Mensagem de falta de conexao
        $(function () {
            $("#mensagem-conexao").dialog({
                height: 140,
                modal: true
            });
        });
    }
       
}

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

function chamadaServidor(latitude, longitude) {
    var url = 'http://localhost:9999/Promocao/ListarPromocao';
    $(function () {
        $("#mensagem-carregando").dialog({
            height: 140,
            dialogClass: "no-close",
            modal: true
        });
    });
    $('li').remove();
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
            window.localStorage.setItem("dadosJson", JSON.stringify(res))
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
        }
    });
}




var listarPromocoes = {

    init: function () {
        //listarPromocoes.mobileInit();
        listarPromocoes.bind();

    },

    mobileInit: function () {
        document.addEventListener("deviceready", appReady, false);
        $(document).bind("mobileinit", function () {
            $.mobile.allowCrossDomainPages = true;
        })
    },

    bind: function () {
        $('ul').on('click', '.imagem-promocao',
            function () {
                var idPromocao = 0;
                var id = $(this).attr('id');
                idPromocao = id;
                window.localStorage.setItem("idSelecionado", id);
                window.localStorage.idSelecionado = idPromocao;
                window.location = "gps.html";
                
            })

        $("#atualizar-promocao").click(function () {
            chamadaServidor();
        })
    },

    geraLista: function (data) {
        for (i = 0; i < data.promocoes.length; i++) {
            $('.lista-promocoes').append('<li class="well">' +
		    '<div class="container corpo-promocoes">' +
			    '<!--Barra titulo empresa -->' +
			    '<div class="titulo-empresa navbar-inner" id="titulo-empresa">' +
            //imagem da empresa da promocao
			    '	<img src="' + data.promocoes[i].UrlEmpresa + '"' +
				    ' class="pull-left logo-promocao" id="id-url-empresa">' +
				    '<div class="container">' +
            //nome da empresa
			    '		<a class="brand" href="#">' + data.promocoes[i].NomeEmpresa + '</a>' +
			    '	</div>' +
			    '</div>' +
			    '<!--Barra titulo empresa -->' +
			    '<!--Imagem da promocao -->' +
			    '<div class="imagem-promocao thumbnail" id="' + data.promocoes[i].IdEmpresa + '">' +
            //imagem da promocao
			    '	<img class="imagem" src="' + data.promocoes[i].UrlPromocao + '"' +
			    '	width="" height="">' +
			    '</div>' +
			    '<!--Imagem da promocao -->' +
			    '<div class="control-group">' +
				    '<div class="controls">' +
					    '<label>' +
            //texto da promocao			
					    data.promocoes[i].Promocao +
					    '</label>' +
				    '</div>' +
			    '</div>' +
		    '</div>' +
	    '</li>');
        }
    }
}


