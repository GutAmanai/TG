$(function () {
    cadastroPromocao.init();
});

var guia;
var __ext;


$(document).ready(function () {
    guia = $("#nome-imagem").val();
    new Ajax_upload($("#upload-imagem")[0], {
        action: baseUrl + "Promocao/UploadImagem",
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
            $("#imagemPreview").attr("src", img);
            $("#temp-image").val(spl[0]);
            $("#extension").val(spl[1]);
        }
    });
    $("#upload-imagem").click();
});


var cadastroCliente = {

    init: function () {
        cadastroPromocao.bind();
        cadastroPromocao.mask();
    }

    , bind: function () {

        $(".salvar-cadastro").on("click", function (event) {
            if (cadastroPromocao.valid()) {
                cadastroPromocao.salvar();
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
            IdPromocao: $("#id-promocao").val(),
            Nome: $("#nome").val(),
            DataLiberacao: $("#dataliberacao").val(),
            DataExpiracao: $("#dataexpiracao").val(),
            Descricao: $("#descricao").val(),
            TempImg: $("#temp-image").val(),
            Extension: $("#extension").val()
        };

        return configuracao;
    }

    , valid: function () {
        var retorno = true;
        var dados = cadastroPromocao.recuperarDados();

        if (dados.Nome == "") {
            $(".error-nome").html("Informe um nome");
            $(".error-nome").show("fast");
            retorno = false;
        }

        if ($("#alteracao").val() != 'True' && $("#temp-image").val() == "") {
            alert("Atenção", "Informe uma imagem");
            retorno = false;
        }

        if (dados.DataLiberacao == "") {
            $(".error-data").html("Informe uma data de liberação da promoção");
            $(".error-data").show("fast");
            retorno = false;
        }

        if (dados.DataExpiracao == "") {
            $(".error-data").html("Informe uma data de expiração da promoção");
            $(".error-data").show("fast");
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

        return retorno;
    }

    , salvar: function () {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Promocao/Salvar",
            data: { configuracao: JSON.stringify(cadastroPromocao.recuperarDados()), r: (new Date().getTime()) },
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
};