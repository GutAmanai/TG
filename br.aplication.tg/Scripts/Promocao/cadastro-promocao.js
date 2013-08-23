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


var cadastroPromocao = {

    init: function () {
        cadastroPromocao.bind();
        cadastroPromocao.pesquisaInicial();
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

        $('div.btn-group').each(function () {
            var group = $(this);
            var form = group.parents('.container').eq(0);
            var name = group.attr('data-toggle-name');
            var hidden = $('input[name="' + name + '"]', form);
            $('button', group).each(function () {
                var button = $(this);
                button.live('click', function () {
                    hidden.val($(this).val());
                });
                if (button.val() == hidden.val()) {
                    button.addClass('active');
                }
            });
        });

    }

    , recuperarDados: function () {
        var configuracao =
        {
            IdPromocao: $("#id-promocao").val(),
            IdCliente: $("#id-cliente").val(),
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

        if (dados.Descricao == "") {
            $(".error-sedescricaonha").html("Informe uma descrição");
            $(".error-descricao").show("fast");
            retorno = false;
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
                    alert("Promoção salva com sucesso!");
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

    , pesquisaInicial: function () {
        var idCliente = $("#id-cliente").val();
        var nPagina = 1;
        var nome = "";
        var descricao = "";
        var pesquisa = cadastroPromocao.obterPesquisa(idCliente, nPagina, nome, descricao);
        cadastroPromocao.obterListagem(pesquisa);
    }

    , pesquisaPaginada: function (pagina) {
        var idCliente = $("#id-cliente").val();
        var nPagina = pagina;
        var nome = "";
        var descricao = "";
        var pesquisa = cadastroPromocao.obterPesquisa(idCliente, nPagina, nome, descricao);
        cadastroPromocao.obterListagem(pesquisa);
    }

    , obterPesquisa: function (idcliente, nPagina, nome, descricao) {
        var pesquisa =
        {
            IdCliente: idcliente,
            NPagina: nPagina,
            QtdPagina: 10,
            Nome: nome,
            Descricao: descricao
        };
        return pesquisa;
    }

    , obterListagem: function (pesquisa) {

        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Promocao/PesquisarPromocao",
            data: { dtoPesquisa: JSON.stringify(pesquisa), r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                if (data) {

                    $(".table.promocao tbody tr").empty();

                    if (data.Localizacoes.length > 0) {
                        $(".table.promocao").find("tbody").append($("#promocao-pesquisa-template").render(data.Localizacoes));
                        cadastroPromocao.adicionarPaginacao(data.NPaginas);
                    }
                    else {
                        $(".table.promocao").find("tbody").append($("#promocao-pesquisa-sem-registro-template").render({}));
                        $('.pagination').hide("fast");
                    }
                }
            },
            failure: function (msg, status) {
                jAlert("Não foi possível salvar!", "Atenção");
            },
            error: function (msg, status) {
                jAlert("Não foi possível salvar!", "Atenção");
            },
            complete: function () {
            }
        });
    }

    , adicionarPaginacao: function (nPagina) {
        $(".pagination input[type='text']").attr("data-max-page", nPagina);

        $('.pagination').jqPagination({
            paged: function (pagina) {
                cadastroPromocao.pesquisaPaginada(pagina);
            }
        });
    }

};