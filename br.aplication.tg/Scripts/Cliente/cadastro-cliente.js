$(function () {
    cadastroCliente.init();
});

var cadastroCliente = {

    init: function () {
        cadastroCliente.bind();
        cadastroCliente.mask();
    }

    , mask: function () {

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
            SenhaConfirmacao: $("#senha_confirmacao").val()
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

        return retorno;
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


};