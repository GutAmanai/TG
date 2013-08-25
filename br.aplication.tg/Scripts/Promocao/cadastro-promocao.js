﻿$(function () {
    cadastroPromocao.init();
    updateImagem.inicializar();
});

var updateImagem = {
    guia: new Object(),
    __ext: new Object(),

    inicializar: function() {
        $(document).ready(function() {
            guia = $("#nome-imagem").val();
            new Ajax_upload($("#upload-imagem")[0], {
                action: baseUrl + "Promocao/UploadImagem",
                data: { 'tempName': guia },
                onSubmit: function(file, ext) {
                    this.setData({ 'tempName': updateImagem.guia, 'ext': ext });
                    if (!(ext && /^(jpg|png|jpeg|bmp|gif)$/.test(ext))) {
                        jAlert("Selecione apenas os formatos de imagem: JPG, JPEG, PNG, BMP ou GIF.", "Atenção!");
                        return false;
                    } else {
                        updateImagem.__ext = ext;
                    }
                },
                onComplete: function(file, response) {
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
    }
};


var cadastroPromocao = {

    init: function () {
        cadastroPromocao.bind();
        cadastroPromocao.pesquisaInicial();
        cadastroPromocao.mascara();
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

        $("input[type='text']").on("change", function () {
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
                button.on('click', function () {
                    hidden.val($(this).val());
                });
                if (button.val() == hidden.val()) {
                    button.addClass('active');
                }
            });
        });
    }

    , bindAlterar: function () {

        $(".alterar").on("click", function () {

            var tr = $(this).parents("tr");
            var promocao =
            {
                IdPromocao: tr.eq(0).attr("rel"),
                IdCliente: $("#id-cliente").val(),
                Nome: tr.find(".nome").html(),
                Descricao: tr.find(".descricao").html(),
                DataLiberacao: tr.find(".data-liberacao").html(),
                DataExpiracao: tr.find(".data-expiracao").html(),
                Ativo: tr.find(".ativo").attr("rel"),
                TempImg: "",
                Extension: ""
            };

            $("#id-promocao").val(promocao.IdPromocao);
            $("#id-cliente").val(promocao.IdCliente);
            $("#nome").val(promocao.Nome);
            $("#descricao").val(promocao.Descricao);
            $("#dataliberacao").val(promocao.DataLiberacao);
            $("#dataexpiracao").val(promocao.DataExpiracao);
            $("#promocao-ativa").val(promocao.Ativo);
            $("#temp-image").val("");
            $("#extension").val("");

        });
    }

    , mascara: function () {

        $("#dataexpiracao, #dataliberacao").datetimepicker({
            closeText: 'Fechar',
            prevText: '&#x3c;Anterior',
            nextText: 'Pr&oacute;ximo&#x3e;',
            currentText: 'Hoje',
            monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
            dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
            weekHeader: 'Sm',
            dateFormat: 'dd-mm-yy',
            firstDay: 0,
            showMonthAfterYear: false,
            yearSuffix: '',
            timeText: 'Tempo',
            hourText: 'Hora',
            minuteText: 'Minuto',
            timeFormat: 'HH:mm:ss',
            stepHour: 1,
            stepMinute: 5,
            showAnim: 'slide',
            showButtonPanel: false,
            secondText: 'Segundos',
            stepSecond: 20,
            showSecond: false
        });

    }

    , parseDate: function (s) {
        var re = /^(\d\d)-(\d\d)-(\d{4}) (\d\d):(\d\d):(\d\d)$/;
        var m = re.exec(s);
        return m ? new Date(m[3], m[2] - 1, m[1], m[4], m[5], m[6]) : null;
    }

    , recuperarDados: function () {
        var configuracao =
        {
            IdPromocao: $("#id-promocao").val(),
            IdCliente: $("#id-cliente").val(),
            Nome: $("#nome").val(),
            Descricao: $("#descricao").val(),
            DataLiberacao: $("#dataliberacao").val(),
            DataExpiracao: $("#dataexpiracao").val(),
            Ativo: ($("#promocao-ativa").val() == 1),
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

        var dataExpiracao = cadastroPromocao.parseDate(dados.DataExpiracao);
        var dataLiberacao = cadastroPromocao.parseDate(dados.DataLiberacao);

        if (dados.DataLiberacao == "") {
            $(".error-data-liberacao").html("Informe uma data de liberação da promoção");
            $(".error-data-liberacao").show("fast");
            retorno = false;
        } else {
            if (dataLiberacao == null) {
                $(".error-data-liberacao").html("Informe uma data de liberação da promoção");
                $(".error-data-liberacao").show("fast");
                retorno = false;
            }
        }

        if (dados.DataExpiracao == "") {
            $(".error-data-expiracao").html("Informe uma data de expiração da promoção");
            $(".error-data-expiracao").show("fast");
            retorno = false;
        } else {
            if (dataExpiracao == null) {
                $(".error-data-expiracao").html("Informe uma data de expiração da promoção");
                $(".error-data-expiracao").show("fast");
                retorno = false;
            }
        }

        if (dataExpiracao != null && dataLiberacao != null) {
            if (dataLiberacao >= dataExpiracao) {
                jAlert("A data de liberação deve ser menor que a data de expiração", "Atenção");
                retorno = false;
            }
        }

        if (dados.Descricao == "") {
            $(".error-descricao").html("Informe uma descrição");
            $(".error-descricao").show("fast");
            retorno = false;
        }

        if (dados.Ativo == "") {
            $(".error-promocao-ativa").html("Informe status da promoção");
            $(".error-promocao-ativa").show("fast");
            retorno = false;
        }

        //        if ($("#alteracao").val() != 'True' && $("#temp-image").val() == "") {
        //            jAlert("Informe uma imagem", "Atenção");
        //            retorno = false;
        //        }

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
                    jAlert("Promoção salva com sucesso!", "Atenção");
                }
                else {
                    jAlert("Não foi possível salvar!", "Atenção");
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
                        $(".table.promocao").find("tbody").append($("#promocao-pesquisa-template").render(data.Promocao));
                        cadastroPromocao.adicionarPaginacao(data.NPaginas);
                        cadastroPromocao.bindAlterar();
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