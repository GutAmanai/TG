$(document).ready(function () {
    overlay.init();
});

var overlay = {
    objetoOverlay: {
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000', '-webkit-border-radius': '10px', '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        },
        message: '<h4>Por favor, aguarde...</h4>'
    },

    init: function () {

        $(document).ajaxStart(function (e, xhr, opt) {
            $.blockUI(overlay.objetoOverlay);
        });

        $(document).ajaxSend(function (e, xhr, opt) {
            $.blockUI(overlay.objetoOverlay);
        });

        $(document).ajaxStop(function (e, xhr, opt) {
            $.unblockUI();
        });

        $(document).ajaxSuccess(function (e, xhr, opt) {
            $.unblockUI();
        });

        $(document).ajaxComplete(function (e, xhr, opt) {
            $.unblockUI();
        });
    },

    open: function () {
        $.blockUI(overlay.objetoOverlay);
    },

    close: function () {
        $.unblockUI();
    }
}