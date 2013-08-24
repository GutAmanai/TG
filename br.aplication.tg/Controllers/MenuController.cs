using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace br.aplication.tg.Controllers
{
    public class MenuController : Controller
    {
        [Authorize]
        public ActionResult Menuvemka(int idCliente)
        {
            return View(idCliente);
        }
    }
}
