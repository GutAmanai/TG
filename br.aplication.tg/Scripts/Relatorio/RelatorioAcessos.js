$(document).ready(function () {
    relatorioAcesso.init();
});

var relatorioAcesso = {

    init: function () {
        relatorioAcesso.obterDadosRelatorio();
    }

    , bind: function () {
    }

    , adicionarRelatorio: function (contente, data, titulo, xTitulo, yTitulo) {

        contente.highcharts({
            chart: {
                zoomType: 'x',
                spacingRight: 20
            },
            title: {
                text: titulo
            },
            xAxis: {
                type: 'datetime',
                minTickInterval: 24 * 3600 * 1000,
                title: {
                    text: xTitulo
                }
            },
            yAxis: {
                title: {
                    text: yTitulo
                }
            },
            tooltip: {
                shared: true
            },
            legend: {
                enabled: false
            },
            plotOptions: {
                area: {
                    fillColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, Highcharts.getOptions().colors[0]],
                            [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                        ]
                    },
                    lineWidth: 1,
                    marker: {
                        enabled: false
                    },
                    shadow: false,
                    states: {
                        hover: {
                            lineWidth: 1
                        }
                    },
                    threshold: null
                }
            },

            series: data
        });
    }

    , obterDataRelatorio: function (data, type) {

        var lidata = new Array();
        var acessos = new Array();

        $.each(data.Acessos, function (index02, value02) {
            var valores = new Array();
            var dataEntrada = Date.UTC(value02.Ano, value02.Mes -1, value02.Dia, value02.Horas, value02.Minutos, value02.Segundos, 0);
            valores.push(dataEntrada);
            valores.push(value02.Acessos);
            acessos.push(valores);
        });

        lidata.push({
            type: type,
            name: 'Quantidade de Acesso',
            data: acessos
        });

        return lidata;
    }

    , obterDadosRelatorio: function () {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: baseUrl + "Relatorio/ObterDadosRelatorio",
            data: { IdCliente: $("#id-cliente").val(), r: (new Date().getTime()) },
            async: false,
            beforeSend: function () {
                //overlay.open();
            },
            success: function (data) {
                if (data) {
                    relatorioAcesso.criarRelatorio(data);
                }
            },
            failure: function (msg, status) {
                jAlert("Não foi possível executar!", "Atenção");
            },
            error: function (msg, status) {
                jAlert("Não foi possível executar!", "Atenção");
            },
            complete: function () {
                //overlay.close();
            }
        });
    }

    , criarRelatorio: function (data) {
        $.each(data, function (index, value) {
            $(".relatorios").append($("#container-relatorio-template").render({ IdPromocao: value.IdPromocao }));
            var dataRelatorio = relatorioAcesso.obterDataRelatorio(value, 'area');
            var contente = $("#" + value.IdPromocao);
            relatorioAcesso.adicionarRelatorio(contente, dataRelatorio, value.Nome, "Período", "Quantidades de acessos");
        });
    }
}