using System.Web.Mvc;
using br.aplicacao.tg.Servicos;

namespace MvcApplicationExemplo.tg.Controllers
{
    public class HomeController : Controller
    {
        public readonly ServicoExemplo ServicoExemplo;

        public HomeController(ServicoExemplo servicoExemplo)
        {
            ServicoExemplo = servicoExemplo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Bem vindo ao pacote default";

            var listExemplo = ServicoExemplo.ObterTodos();

            return View(listExemplo);
        }
    }
}
