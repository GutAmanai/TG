$(document).ready(function() {
    loginCliente.Init();
});

var loginCliente = {

    Init: function () {
        loginCliente.bind();
    }

    , bind: function () {
        $(".logar").on("click", function (event) {
            if (loginCliente.validarCampos()) {
                loginCliente.logar();
            }
        });
    }

    , validarCampos: function () {
        var returno = true;

        var email = $(".email").val();
        var senha = $(".senha").val();

        if (email.trim() == "") {
            jAlert("Informe seu email", "Atenção");
            returno = false;
        } else {
            if (!loginCliente.IsEmail(email)) {
                jAlert("Informe um email valido", "Atenção");
                returno = false;
            }
        }

        if (senha.trim() == "") {
            jAlert("Informe sua senha", "Atenção");
            returno = false;
        }

        return returno;
    }

    , logar: function () {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Login/Logar",
            data: { email: $(".email").val(), senha: $(".senha").val(), r: (new Date().getTime()) },
            async: false,
            success: function (data) {
                if (data.autorizado) {
                    window.location = baseUrl + "Menu/Menuvemka/?idCliente=" + data.IdCliente;
                } else {
                    jAlert("Informações inválidas!", "Atenção");
                }
            }
        });
    }

    , IsEmail: function (email) {
        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test(email)) {
            return false;
        } else {
            return true;
        }
    }
};