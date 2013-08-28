document.addEventListener("deviceready", onDeviceReady, false);



$(document).ready(function () {
    listarPromocoes.init();
    $.support.cors = true; 

});

function onDeviceReady() {
    $.mobile.showPageLoadingMsg();
    recuperarPosicaoGPS();
    $('#place').append(localStorage.getItem("latitude"));
    chamadaServidor(window.localStorage.getItem("latitude"), window.localStorage.getItem("latitude"));    
}


function recuperarPosicaoGPS() {
        var win = function (position) {
            localStorage.setItem("latitude", position.coords.latitude);
            localStorage.setItem("longitude", position.coords.longitude);
                                  
        };

        var fail = function (e) {
            $('#place').append('FAIL GPS: ');
        };

        navigator.geolocation.getCurrentPosition(win, fail);
}

function chamadaServidor(latitude, longitude) {
    var url = 'http://localhost:9999/Promocao/ListarPromocao';    

    $.ajax({
        type: 'GET',
        url: url,
        contentType: "application/json",
        dataType: 'jsonp',
        data: { latitude: latitude, longitude: longitude },
        crossDomain: true,
        success: function (res) {
            $('#place').append('sucesso');               
            listarPromocoes.geraLista(res);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#place').append('error');
            $('#place').append('xhr' + xhr.status);
            $('#place').append('' + thrownError);
            $("#ajax_error").html(e.message);
            console.log(e.message);

        },
        complete: function (data) {
            $('#place').append('cplete');
            console.log(e.message);
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
                localStorage.idSelecionado = idPromocao;

                //$.mobile.changePage("../gps.html", { transition: "slideup", changeHash: false });
                //$.mobile.changePage("gps2.html"); 
                //window.location = "gps2.html";
                $('.conteudo').load('gps2.html');
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


