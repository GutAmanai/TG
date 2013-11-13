using System.Web.Mvc;

namespace br.aplication.tg.Controllers
{
    public class MenuController : Controller
    {
        [Authorize]
        public ActionResult Menuvemka(string idCliente)
        {
            ViewBag.IdCliente = idCliente;
            return View();
        }
    }
}
