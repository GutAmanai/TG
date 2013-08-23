$(function () {
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
        message: '<h1>Por favor, aguarde...</h1>'
    },

    init: function () {
        
        $(document).ajaxStart(function () {
            $.blockUI(overlay.objetoOverlay);
        });

        $(document).ajaxSend(function () {
            $.blockUI(overlay.objetoOverlay);
        });

        $(document).ajaxStop(function () {
            $.unblockUI();
        });

        $(document).ajaxSuccess(function () {
            $.unblockUI();
        });

        $(document).ajaxComplete(function () {
            $.unblockUI();
        });
    },

    open: function () {
        $.blockUI(overlay.objetoOverlay);
    },

    close: function () {
        $.blockUI(overlay.objetoOverlay);
    }
}