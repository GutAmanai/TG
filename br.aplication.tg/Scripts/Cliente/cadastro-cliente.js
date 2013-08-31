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
        beforeSend: function () {
            overlay.open();
        },
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
        cadastroCliente.cnpjAlterar();
    }

    , cnpjAlterar: function () {
        if ($("#alteracao").val() == 'True') {
            $("#cnpj").attr("disabled", "disabled");
        }
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
            $(".error-cnpj").html("Informe um CNPJ");
            $(".error-cnpj").show("fast");
            retorno = false;
        } else {
            if (!cnpj.validar(dados.Cnpj)) {
                $(".error-cnpj").html("CNPJ inválido!");
                $(".error-cnpj").show("fast");
                retorno = false;
            }
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
            jAlert('Informe uma imagem!', 'Atenção');
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
            jAlert('Informe uma localização!', 'Atenção');
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
            beforeSend: function () {
                overlay.open();
            },
            success: function (data) {
                if (data) {
                    if ($("#alteracao").val() != 'True') {
                        jAlert('Cadastro feito com sucesso! Faça o login.', 'Atenção', function (event) {
                            window.location = baseUrl + "Login/index";
                        });
                    } else {
                        jAlert('Alteração feita com sucesso! Acesse as área administrativa.', 'Atenção', function (event) {
                            window.location = baseUrl + "Menu/Menuvemka/?idCliente=" + $("#id-cliente").val();
                        });
                    }
                }
                else {
                    jAlert('Não foi possível salvar!', 'Atenção');
                }
            },
            failure: function (msg, status) {
                jAlert('Não foi possível salvar!', 'Atenção');
            },
            error: function (msg, status) {
                jAlert('Não foi possível salvar!', 'Atenção');
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
            beforeSend: function () {
                overlay.open();
            },
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

    initialize: function() {
        var latlng = new google.maps.LatLng(-23.546, -46.638);
        var options = { zoom: 14, center: latlng, mapTypeId: google.maps.MapTypeId.ROADMAP };
        googleMaps.map = new google.maps.Map(document.getElementById("map_canvas"), options);

        google.maps.event.trigger(googleMaps.map, 'resize');

        google.maps.event.addListener(googleMaps.map, 'click', function(event) {
            googleMaps.addMarker(event.latLng);
        });

        googleMaps.inicializaMapaAlterar();
    },
    inicializaMapaAlterar: function() {
        var strJsonLocalizacao = $("#localizacoes-json").val();
        if (strJsonLocalizacao.length > 0) {
            var localizacao = JSON.parse(strJsonLocalizacao);
            $.each(localizacao, function(key, value) {
                var latlng = new google.maps.LatLng(parseFloat(value.Latitude), parseFloat(value.Longitude));
                googleMaps.addMarker(latlng);
            });
        }
    },
    addMarker: function(location) {
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
    },
    setAllMap: function(map) {
        for (var i = 0; i < googleMaps.markers.length; i++) {
            googleMaps.markers[i].setMap(map);
        }
    },
    clearOverlays: function() {
        googleMaps.setAllMap(null);
    },
    showOverlays: function() {
        googleMaps.setAllMap(googleMaps.map);
    },
    deleteOverlays: function() {
        googleMaps.clearOverlays();
        googleMaps.markers = new Array();
        googleMaps.localizacoes = new Array();
    }
};

var cnpj = {

    validar: function (cnpj) {
        cnpj = cnpj.replace(/[^\d]+/g, '');

        if (cnpj == '') return false;

        if (cnpj.length != 14)
            return false;

        if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
            return false;

        // Valida DVs
        var tamanho = cnpj.length - 2;
        var numeros = cnpj.substring(0, tamanho);
        var digitos = cnpj.substring(tamanho);
        var soma = 0;
        var pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (var i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;

        return true;
    }
}