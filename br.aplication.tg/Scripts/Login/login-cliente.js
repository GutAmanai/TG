$(document).ready(function () {
    loginCliente.Init();
})

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
            alert("Atenção", "Informe seu email");
            returno = false;
        } else {
            if (!loginCliente.IsEmail(email)) {
                alert("Atenção", "Informe um email valido");
                returno = false;
            }
        }

        if (senha.trim() == "") {
            alert("Atenção", "Informe sua senha");
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
                if (data) {
                    window.location = baseUrl + "Home/MenuVemKa";
                } else {
                    alert("Atenção", "Informações inválidas!");
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