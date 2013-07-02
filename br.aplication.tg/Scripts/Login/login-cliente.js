$(document).ready(function () {
    loginCliente.Init();
})

var loginCliente = {

    Init: function () {
        loginCliente.bind();
    }

    , bind: function () {
        $(".logar").on("click", function (event) {
            loginCliente.logar();
        });
    }

    , validarCampos: function () {
        var returno = true;

        var email = $(".email").val();
        var senha = $(".senha").val();

        if (email.trim() == "") {
            JAlert()
            returno = false;
        }

    }

    , obterDados: function () {

    }

    , logar: function () {

    }
};