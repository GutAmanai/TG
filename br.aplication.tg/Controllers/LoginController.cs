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

        public ActionResult Logar(string model)
        {
            var dto = new JavaScriptSerializer().Deserialize<DTOCliente>(model);

            if (_servicoCliente.ValidarUsuario(dto.Email, dto.Senha))
            {
                FormsAuthentication.SetAuthCookie(dto.Email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Json("O e-mail ou a sua senha está inválida!");
            }
        }

    }
}
