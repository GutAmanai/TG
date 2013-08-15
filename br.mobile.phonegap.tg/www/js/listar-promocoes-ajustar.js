
// ------------- Parte de chamada para o sistema ----------------------------------
// --------------------------------------------------------------------------------
// --------------------------------------------------------------------------------
$(document).ready(function () {
    recuperarPosicaoGPS();
    // direciona para mapa
    $('ul').on('click', '.imagem-promocao', function () {
        var idPromocao = 0;
        var id = $(this).attr('id');
        idPromocao = id;
        localStorage.idSelecionado = idPromocao;
        $('.conteudo').load('gps.html');
    });

    $("#atualizar-promocao").click(function () {
        handleClick();
    });

    $("#ajax_error").ajaxError(function (e, jqxhr, settings, exception) {
        $(this).text("Error requesting page " + settings.url);
    });
});

document.addEventListener("deviceready", appReady, false);

$(document).bind("mobileinit", function () {
    $("#infodiv").html('mobileinit worked');
    $.mobile.allowCrossDomainPages = true;
});

function handleClick() {
    var url = 'http://localhost:9999/Promocao/ListarPromocao';

    $.ajax({
        type: 'GET',
        url: url,
        contentType: "application/json",
        dataType: 'jsonp',
        data: { latitude: localStorage.latitude, longitude: localStorage.longitude },
        crossDomain: true,
        success: function (res) {
            //$(".conteudo").html(JSON.stringify(res));
            //alert(JSON.stringify(res));
            mantemJson(res);
            geraLista(res);
        },
        error: function (e) {
            $("#ajax_error").html(e.message);
            console.log(e.message);
        },
        complete: function (data) {
            console.log(e.message);
        }
    });

}

//recuperar valor de latitude e longitude
function recuperarPosicaoGPS() {
    var win = function (position) {
        localStorage.latitude = position.coords.latitude;
        localStorage.longitude = position.coords.longitude;
    };

    var fail = function (e) {
        alert('Can\'t retrieve position.\nError: ' + e);
    };

    navigator.geolocation.getCurrentPosition(win, fail);
}

function geraLista(data) {
         
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

function mantemJson(json) {
    localStorage.dataJSON = json;
}