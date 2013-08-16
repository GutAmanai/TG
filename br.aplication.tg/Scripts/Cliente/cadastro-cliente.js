$(function () {
    cadastroCliente.init();
    googleMaps.initialize();
});

var guia;
var __ext;


$(document).ready(function () {
    guia = $("#nome-logo").val();
    new Ajax_upload($("#upload-logo")[0], {
        action: baseUrl + "Cliente/UploadLogo",
        data: { 'tempName': guia },
        onSubmit: function (file, ext) {
            this.setData({ 'tempName': guia, 'ext': ext });
            if (!(ext && /^(jpg|png|jpeg|bmp|gif)$/.test(ext))) {
                jAlert("Selecione apenas os formatos de imagem: JPG, JPEG, PNG, BMP ou GIF.", "Atenção!");
                return false;
            } else {
                __ext = ext;
            }
        }
        , onComplete: function (file, response) {
            var spl = response.split("|");
            var img = baseUrl + "Arquivos/Temp/" + spl[0] + spl[1] + "?x=" + spl[2];
            var imgDownload = baseUrl + "Arquivos/Temp/" + spl[0] + spl[1] + "?v=" + new Date().getMilliseconds();
            $("#fotoPreview").attr("src", img);
            $("#temp-image").val(spl[0]);
            $("#extension").val(spl[1]);
        }
    });
    $("#upload-logo").click();
});


var cadastroCliente = {

    init: function () {
        cadastroCliente.bind();
        cadastroCliente.mask();
    }

    , mask: function () {

        $("#telefone").mask("(99) 9999-9999?9").ready(function (event) {
            var target, phone, element;
            target = (event.currentTarget) ? event.currentTarget : event.srcElement;
            phone = target.value.replace(/\D/g, '');
            element = $(target);
            element.unmask();
            if (phone.length > 10) {
                element.mask("(99) 99999-999?9");
            } else {
                element.mask("(99) 9999-9999?9");
            }
        });

        $("#cnpj").mask("99.999.999/9999-99");
    }

    , bind: function () {

        $(".salvar-cadastro").on("click", function (event) {
            if (cadastroCliente.valid()) {
                cadastroCliente.salvar();
            }
        });

        $("input[type='text'], textarea, input[type='password']").on("keydown", function () {
            if ($(this).next(".error").length > 0) {
                $(this).next(".error").empty();
                $(this).next(".error").hide("fast");
            }
        });
    }

    , recuperarDados: function () {
        
        var configuracao =
        {
            IdCliente: $("#id-cliente").val(),
            Nome: $("#nome").val(),
            Cnpj: $("#cnpj").val(),
            Responsavel: $("#responsavel").val(),
            Email: $("#email").val(),
            Contato: $("#telefone").val(),
            Senha: $("#senha").val(),
            SenhaConfirmacao: $("#senha_confirmacao").val(),
            TempImg: $("#temp-image").val(),
            Extension: $("#extension").val(),
            Localizacoes: googleMaps.localizacoes
        };

        return configuracao;
    }

    , valid: function () {
        var retorno = true;
        var dados = cadastroCliente.recuperarDados();

        if (dados.Nome == "") {
            $(".error-nome").html("Informe um nome");
            $(".error-nome").show("fast");
            retorno = false;
        }

        if (dados.Cnpj == "") {
            $(".error-cnpj").html("Informe um cnpj");
            $(".error-cnpj").show("fast");
            retorno = false;
        }

        if (dados.Responsavel == "") {
            $(".error-responsavel").html("Informe um responsável");
            $(".error-responsavel").show("fast");
            retorno = false;
        }

        if (dados.Email == "") {
            $(".error-email").html("Informe um email");
            $(".error-email").show("fast");
            retorno = false;
        } else {
            if (!cadastroCliente.IsEmail(dados.Email)) {
                $(".error-email").html("Email inválido");
                $(".error-email").show("fast");
                retorno = false;
            } else {
                if ($("#alteracao").val() != 'True' && cadastroCliente.EmailIsExist(dados.Email)) {
                    $(".error-email").html("Email já existente");
                    $(".error-email").show("fast");
                    retorno = false;
                }
            }
        }

        if ($("#alteracao").val() != 'True' && $("#temp-image").val() == "") {
            alert("Informe uma imagem");
            retorno = false;
        }

        if (dados.Telefone == "") {
            $(".error-telefone").html("Informe um telefone");
            $(".error-telefone").show("fast");
            retorno = false;
        }

        if (dados.Senha == "") {
            $(".error-senha").html("Informe uma senha");
            $(".error-senha").show("fast");
            retorno = false;
        } else {
            if (dados.Senha != dados.SenhaConfirmacao) {
                $(".error-senha-comparacao").html("Senha não são validas");
                $(".error-senha-comparacao").show("fast");
                retorno = false;
            }
        }

        if (dados.Localizacoes.length == 0) {
            alert("Informe uma localização");
            retorno = false;
        }

        return retorno;
    }

    , IsEmail: function (email) {
        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test(email)) {
            return false;
        } else {
            return true;
        }
    }

    , salvar: function () {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Cliente/Salvar",
            data: { configuracao: JSON.stringify(cadastroCliente.recuperarDados()), r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                if (data) {
                    alert("Cadastro feito com sucesso!");
                }
                else {
                    alert("Não foi possível salvar!");
                }
            },
            failure: function (msg, status) {
                alert("Não foi possível salvar!");
            },
            error: function (msg, status) {
                alert("Não foi possível salvar!");
            },
            complete: function () {
            }
        });
    }

    , EmailIsExist: function (email) {
        var retorno;
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Cliente/EmailIsExist",
            data: { email: email, r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                retorno = data;
            }
        });
        return retorno;
    }

};

var googleMaps = {

    map: new Object(),
    markers: new Array(),
    localizacoes: new Array(),

    initialize: function () {
        var latlng = new google.maps.LatLng(-23.546, -46.638);
        var options = { zoom: 14, center: latlng, mapTypeId: google.maps.MapTypeId.ROADMAP };
        googleMaps.map = new google.maps.Map(document.getElementById("map_canvas"), options);

        google.maps.event.trigger(googleMaps.map, 'resize');

        google.maps.event.addListener(googleMaps.map, 'click', function (event) {
            googleMaps.addMarker(event.latLng);
        });
    }

    , addMarker: function (location) {
        googleMaps.deleteOverlays();

        var marker = new google.maps.Marker({
            position: location,
            map: googleMaps.map
        });

        var infowindow = new google.maps.InfoWindow({
            content: 'Sua Localização ficará aqui!'
        });

        infowindow.open(googleMaps.map, marker);
        googleMaps.markers.push(marker);
        googleMaps.localizacoes.push({ Latitude: location.lat(), Longitude: location.lng() });
    }

    , setAllMap: function (map) {
        for (var i = 0; i < googleMaps.markers.length; i++) {
            googleMaps.markers[i].setMap(map);
        }
    }

    , clearOverlays: function () {
        googleMaps.setAllMap(null);
    }

    , showOverlays: function () {
        googleMaps.setAllMap(googleMaps.map);
    }

    , deleteOverlays: function () {
        googleMaps.clearOverlays();
        googleMaps.markers = new Array();
        googleMaps.localizacoes = new Array();
    }
}