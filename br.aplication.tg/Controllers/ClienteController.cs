using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using br.aplicacao.tg.DTO;
using br.aplicacao.tg.Servicos;
using br.aplicacao.tg.ViewModel;
using br.infra.tg.InjecaoDependencia;

namespace br.aplication.tg.Controllers
{
    public class ClienteController : Controller
    {
        public JavaScriptSerializer js = new JavaScriptSerializer();
        public ServicoCliente ServicoCliente;

        public ClienteController()
        {
            ServicoCliente = Fabrica.Instancia.Obter<ServicoCliente>();
        }

        public ActionResult Cadastro(int id = 0)
        {
            return View(ServicoCliente.ObterViewModelCliente(id));
        }

        public ActionResult Salvar(string configuracao)
        {
            try
            {
                var dtoCliente = js.Deserialize<DTOCliente>(configuracao);
                return Json(ServicoCliente.SalvarCliente(dtoCliente));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

    }

}
