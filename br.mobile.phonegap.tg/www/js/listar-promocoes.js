document.addEventListener("deviceready", function () {
    listarPromocoes.init();
    gps.obterPosicao();
}, true);

var listarPromocoes = {
    init: function () {
        listarPromocoes.bind();

        if (window.localStorage.getItem("listagemPromocao") != null) {
            listarPromocoes.geraListaCache();
        } else {
            listarPromocoes.chamadaServidor(gps.latitude, gps.longitude);
        }
    },

    bind: function () {
        $('.imagem-promocao').on('click', function () {
            window.localStorage.setItem("promocaoSelecionado", $(this).parents(".corpo-promocoes").eq(0).attr("id"));
            window.location = "gps.html";
        });

        $("#atualizar-promocao").click(function () {
            listarPromocoes.chamadaServidor(gps.latitude, gps.longitude);
        });
    }

    , chamadaServidor: function (latitude, longitude) {

        var url = 'http://www.lotustg.com.br/Promocao/PromocaoAcesso';
        //online  http://www.lotustg.com.br/Promocao/PromocaoAcesso
        //offline http://localhost:9999/Promocao/PromocaoAcesso

        $('li').remove();

        $.ajax({
            type: 'GET',
            url: url,
            contentType: "application/json",
            dataType: 'jsonp',
            data: { latitude: latitude, longitude: longitude },
            beforeSend: function () {
                overlay.open();
            },
            crossDomain: true,
            global: true,
            success: function (res) {
                listarPromocoes.geraLista(res);
                listarPromocoes.bind();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $('.lista-promocoes').empty();
                $('.lista-promocoes').append("<div class='row'> <h4>Erro: De acesso, verifique a sua conex&atilde;o de internet.</h4></div>");
            },
            complete: function (data) {
                overlay.close();
            }
        });
    }

    , geraLista: function (data) {
        $('.lista-promocoes').empty();
        window.localStorage.setItem("listagemPromocao", JSON.stringify(data));

        if(data.length == 0) {
            $('.lista-promocoes').append("<div class='row'> <h4>N&atilde;o foram encontradas promo&ccedil;&otilde;es.</h4></div>");
        }

        for (i = 0; i < data.length; i++) {
            $('.lista-promocoes').append(
                '	<li class="well">																						' +
                '		<div class="container corpo-promocoes" id="' + data[i].IdPromocao + '" >  							' +
                '			<div class="titulo-empresa navbar-inner" >														' +
                '				<img src="' + data[i].UrlEmpresa + '"' + 'class="pull-left logo-promocao">					' +
                '				<div class="container">																		' +
                '					<a class="brand" href="#">' + data[i].NomeEmpresa + '</a>								' +
                '				</div>																						' +
                '			</div>																							' +
                '			<div class="imagem-promocao thumbnail" id="' + data[i].IdEmpresa + '">							' +
                '				<img class="imagem" src="' + data[i].UrlPromocao + '"/> 									' +
                '			</div>																							' +
                '			<div class="control-group">																		' +
                '				<div class="controls">																		' +
                '					<label>' + data[i].DescricaoPromocao + '</label>    									' +
                '                   <br/ >                                                                                  ' +
                '					<label> Validade: ' + data[i].Validade + '</label>    									' +
                '				</div>																						' +
                '			</div>																							' +
                '	    </div>																						        ' +
                '	</li>																									'
            );
        }
    }

    , geraListaCache: function () {
        $('.lista-promocoes').empty();
        var data = JSON.parse(window.localStorage.getItem("listagemPromocao"));

        if (data.length == 0) {
            $('.lista-promocoes').append("<div class='row'> <h4>N&atilde;o foram encontradas promo&ccedil;&otilde;es.</h4></div>");
        }

        for (i = 0; i < data.length; i++) {
            $('.lista-promocoes').append(
                '	<li class="well">																						' +
                '		<div class="container corpo-promocoes" id="' + data[i].IdPromocao + '" >  							' +
                '			<div class="titulo-empresa navbar-inner" >														' +
                '				<img src="' + data[i].UrlEmpresa + '"' + 'class="pull-left logo-promocao">					' +
                '				<div class="container">																		' +
                '					<a class="brand" href="#">' + data[i].NomeEmpresa + '</a>								' +
                '				</div>																						' +
                '			</div>																							' +
                '			<div class="imagem-promocao thumbnail" id="' + data[i].IdEmpresa + '">							' +
                '				<img class="imagem" src="' + data[i].UrlPromocao + '"/> 									' +
                '			</div>																							' +
                '			<div class="control-group">																		' +
                '				<div class="controls">																		' +
                '					<label>' + data[i].DescricaoPromocao + '</label>    									' +
                '                   <br/ >                                                                                  ' +
                '					<label> Validade: ' + data[i].Validade + '</label>    									' +
                '				</div>																						' +
                '			</div>																							' +
                '	    </div>																						        ' +
                '	</li>																									'
            );
        }

        listarPromocoes.bind();
    }
};

var gps = {
    latitude: 0.0,
    longitude: 0.0,

    obterPosicao: function () {
        navigator.geolocation.getCurrentPosition(gps.localizacaoSuccesso, gps.localizacaoError);
    },
    inicializar: function (lat, lon) {
        gps.latitude = lat;
        gps.longitude = lon;
    },
    localizacaoError: function (error) {
        gps.inicializar(-23.546, -46.638);
    },
    localizacaoSuccesso: function (position) {
        gps.inicializar(position.coords.latitude, position.coords.longitude);
    }
};