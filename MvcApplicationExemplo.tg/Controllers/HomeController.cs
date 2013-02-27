using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using br.aplicacao.Servicos;

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
