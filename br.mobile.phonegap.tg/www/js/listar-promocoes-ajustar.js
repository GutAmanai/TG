
var idPromocao = 0;

$('ul').on('click', '.imagem-promocao', function () {
    var id = $(this).attr('id');
    idPromocao = id;
    localStorage.idSelecionado = idPromocao;
    $('.conteudo').load('gps.html');
});

//recuperar valor de latitude e longitude
var win = function (position) {
    $('.place').append('Longitude:' + position.coords.longitude);
    $('.place').append('Latitude:' + position.coords.latitude);
};

var fail = function (e) {
    alert('Can\'t retrieve position.\nError: ' + e);
};

navigator.geolocation.getCurrentPosition(win, fail);


// ------------- Parte de chamada para o sistema ----------------------------------
// --------------------------------------------------------------------------------
// --------------------------------------------------------------------------------
$(document).ready(function () {
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
        //data: { first: $("#firstname").val(), last: $("#lastname").val() },
        crossDomain: true,
        success: function (res) {
            $(".conteudo").html(JSON.stringify(res));
            alert(JSON.stringify(res));
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

    
