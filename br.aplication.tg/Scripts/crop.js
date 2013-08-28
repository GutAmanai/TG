$(function () {
    crop.init();
});

/* / crop*/
var jcrop_api;
var crop = {
    wF: 0,
    hF: 0,
    xF: 0,
    yF: 0,
    wDF: 0,
    hGF: 0,
    container: $("#fotoPreview"),
    path: $("#preview-crop"),
    url: "BiografiaCarreira/Crop",
    aspectRatio: (3 / 4),
    larguraMaxima: 80,
    alturaMaxima: 100,
    btnSalvar: $("#salvar-crop"),
    btnCancelar: $("#cancelar-crop"),
    maximos: [885, 370],
    init: function () {
        this.addEvents();
    },
    montarCrop: function (img, largura, altura) {
        var conteudo = $("#divOverlayFoto").clone();
        $(conteudo).insertAfter("#bgOverlay");

        $('#imagem-crop').attr('src', img);
        $('#preview-crop').attr('src', img);
        $('.jcrop-holder img').attr('src', img);

        $('#bgOverlayCrop').fadeIn();
        $('#boxOverlayCrop').fadeIn();

        if ( $("header").length > 0 ) {
            $('html, body').animate({
                scrollTop: $("#header").offset().top
            }, 300);
        }        

        $("#bgOverlayCrop").css({
            "width": "100%",//$(window).width(),
            "height": $(document).height(),
            "display": "block"
        });

        $("#imagem-crop").css({
            width: largura + 'px',
            height: altura + 'px'
        });

        $("#preview-crop").parent().css({
            width: crop.larguraMaxima + 'px',
            height: crop.alturaMaxima + 'px'
        });

        $('#imagem-crop').Jcrop({
            onChange: crop.mostrarPreview,
            onSelect: crop.mostrarPreview,
            aspectRatio: crop.aspectRatio,
            setSelect: [100, 100, 50, 50]
        }, function () {
            jcrop_api = this;
        });

        crop.wF = largura;
        crop.hF = altura;

        $("#divOverlayFoto").show();
    },
    mostrarPreview: function (cords) {
        if (parseInt(cords.w) > 0) {
            crop.atualizarCordenadas(cords);

            var rx = crop.larguraMaxima / cords.w;
            var ry = crop.alturaMaxima / cords.h;

            $('#preview-crop').css({
                width: Math.round(rx * crop.wF) + 'px',
                height: Math.round(ry * crop.hF) + 'px',
                marginLeft: '-' + Math.round(rx * cords.x) + 'px',
                marginTop: '-' + Math.round(ry * cords.y) + 'px'
            });
        }
    },
    atualizarCordenadas: function (cords) {
        crop.xF = cords.x;
        crop.yF = cords.y;
        crop.wDF = cords.w;
        crop.hGF = cords.h;
    },
    salvarCrop: function () {
        var path = crop.path.attr('src');

        $.ajax({
            type: "POST",
            url: baseUrl + crop.url,
            data: {
                idProcesso: ferramenta.ObterProcesso()
                , top: parseInt(crop.yF)
                , left: parseInt(crop.xF)
                , width: parseInt(crop.wDF)
                , height: parseInt(crop.hGF)
                //                , top: dadosPessoais.alterouProporcaoFoto ? parseInt(crop.yF) * 10 : parseInt(crop.yF)
                //                , left: dadosPessoais.alterouProporcaoFoto ? parseInt(crop.xF) * 10 : parseInt(crop.xF)
                //                , width: dadosPessoais.alterouProporcaoFoto ? parseInt(crop.wDF) * 10 : parseInt(crop.wDF)
                //                , height: dadosPessoais.alterouProporcaoFoto ? parseInt(crop.hGF) * 10 : parseInt(crop.hGF)
                , path: path
                , baseUrl: baseUrl
            },
            success: function (retorno) {
                var error = retorno.split("§");

                if (error.length > 0 && error[0] == "False") {
                    jAlert("Ocorreu um erro durante o cadastro da foto.", "Atenção");
                }
                else {
                    var imagens = retorno.split('§');

                    $('#imagem-crop').attr('src', '');
                    $('#preview-crop').attr('src', '');
                    $('.jcrop-holder img').attr('src', '');

                    $("#fotoPreview").attr('src', imagens[0]);

                    $('#bgOverlayCrop, #boxOverlayCrop').hide();

                    //resetando configuração
                    jcrop_api.destroy();
                }
            },
            failure: function (error, status) {
                //alert(status);
            },
            error: function (error, status) {
                //alert(status);
            }
        });
    },
    cancelarCrop: function () {
        $('#bgOverlayCrop').hide();
        $('#boxOverlayCrop').hide();
        $("#divOverlayFoto").remove();

        $("#nome-arquivo-upado").val("");

        jcrop_api.destroy();
    },
    addEvents: function () {
        this.btnSalvar.on("click", function () {
            crop.salvarCrop();
        });

        this.btnCancelar.on("click", function () {
            crop.cancelarCrop();
        });
    }
};