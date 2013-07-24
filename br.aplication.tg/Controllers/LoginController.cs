using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using br.aplicacao.tg.DTO;
using br.aplicacao.tg.Servicos;
using br.infra.tg.InjecaoDependencia;

namespace br.aplication.tg.Controllers
{
    public class LoginController : Controller
    {
        private readonly ServicoCliente _servicoCliente;

        public LoginController()
        {
            _servicoCliente = Fabrica.Instancia.Obter<ServicoCliente>(); 
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logar(string email, string senha)
        {
            if (_servicoCliente.ValidarCliente(email, senha))
            {
                var cliente = _servicoCliente.ObterViewModelCliente(email);
                FormsAuthentication.SetAuthCookie(cliente.Nome, false);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
