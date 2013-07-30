using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using br.aplicacao.tg.DTO;

namespace br.aplication.tg.Controllers
{
    public class PromocaoController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var DTOLogin = (DTOCliente)Session["DTOLogin"];
            return View();
        }
    }
}
