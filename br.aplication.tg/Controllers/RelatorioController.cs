using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using br.aplicacao.tg.Servicos;
using br.infra.tg.InjecaoDependencia;

namespace br.aplication.tg.Controllers
{
    public class RelatorioController : Controller
    {
        public JavaScriptSerializer js = new JavaScriptSerializer();
        public ServicoRelatorio ServicoRelatorio;

        public RelatorioController()
        {
            ServicoRelatorio = Fabrica.Instancia.Obter<ServicoRelatorio>();
        }

        public ActionResult QuantidadeAcesso(string idCliente)
        {
            var id = Convert.ToInt32(ServicoCriptografia.Decrypt(idCliente));
            return View(id);
        }

        public ActionResult ObterDadosRelatorio(int idCliente)
        {
            return Json(ServicoRelatorio.ObterDadosAcesso(idCliente));
        }
    }
}
