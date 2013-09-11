
document.addEventListener("deviceready", function () {    
    localizacao.init();
}, true);


var localizacao = {
    init: function () {
        localizacao.bind();
        localizacao.adicionarDados();
    },

    bind: function () {
        $("#voltar").on("click", function () {
            window.history.back();
        });
    },

    obterDadosPromocao: function () {
        var idSelecionado = window.localStorage.getItem("promocaoSelecionado");
        return JSON.parse(window.localStorage.getItem(idSelecionado));
    },

    adicionarDados: function () {
        var $dados = localizacao.obterDadosPromocao();
        $(".conteudo").append(
            '		<div class="container corpo-promocoes well">														' +
            '			<div class="titulo-empresa navbar-inner" >														' +
            '				<img src="' + $dados.UrlEmpresa + '"' + 'class="pull-left logo-promocao">					' +
            '				<div class="container">																		' +
            '					<a class="brand" href="#">' + $dados.NomeEmpresa + '</a>								' +
            '				</div>																						' +
            '			</div>																							' +
            '			<div class="imagem-promocao thumbnail" id="' + $dados.IdEmpresa + '">							' +
            '				<img class="imagem" src="' + $dados.UrlPromocao + '"/> 									    ' +
            '			</div>																							' +
            '			<div class="control-group">																		' +
            '				<div class="controls">																		' +
            '					<label>' + $dados.DescricaoPromocao + '</label>    									    ' +
            '				</div>																						' +
            '			</div>																							' +
            '	    </div>																						        '
        );
    }
};