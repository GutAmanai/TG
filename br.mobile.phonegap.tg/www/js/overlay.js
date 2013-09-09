
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
    open: function () {
        $.blockUI(overlay.objetoOverlay);
    },

    close: function () {
        $.unblockUI();
    },
    
    openByElement : function(div){
        div.block(overlay.objetoOverlay);
    },
    
    closeByElement : function(div){
        div.unblock();
    }
}