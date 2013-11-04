using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using br.aplicacao.tg.DTO;
using br.aplicacao.tg.Servicos;
using br.infra.tg.InjecaoDependencia;
using br.aplication.tg.helper;
using br.dominio.tg.ObjetoValor;

namespace br.aplication.tg.Controllers
{
    [AllowCrossSiteJsonAttribute]
    public class PromocaoController : Controller
    {
        public JavaScriptSerializer Js;
        public ServicoPromocao ServicoPromocao;
        public ServicoImagem ServicoImagem;

        public PromocaoController()
        {
            ServicoPromocao = Fabrica.Instancia.Obter<ServicoPromocao>();
            ServicoImagem = new ServicoImagem();
            Js = new JavaScriptSerializer();
        }

        #region Cadastro de Promoção

        public ActionResult Cadastro(int idCliente, int idPromocao = 0)
        {
            ViewBag.Imagem = ServicoImagem.RecuperaImagemPromocao(idCliente, idPromocao);
            return View(idCliente);
        }

        public ActionResult Salvar(string configuracao)
        {
            try
            {
                var dtoPromocao = Js.Deserialize<DTOPromocao>(configuracao);
                var dtoPromocaoSalva = ServicoPromocao.SalvarPromocao(dtoPromocao);
                if (dtoPromocaoSalva != null)
                {
                    ServicoImagem.SalvarImagemFinalPromocao(dtoPromocaoSalva.IdCliente, dtoPromocaoSalva.IdPromocao, dtoPromocao.TempImg, dtoPromocao.Extension);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                ExceptionCustom.Log(ex);
                return Json(false);
            }
        }

        public ActionResult PesquisarPromocao(string dtoPesquisa)
        {
            var dto = Js.Deserialize<DTOPesquisaPromocao>(dtoPesquisa);
            return Json(ServicoPromocao.ObterDTOPromocao(dto));
        }

        #endregion

        #region PhoneGap Requisição
        
        public ActionResult ListarPromocao(double latitude, double longitude)
        {
            return this.Jsonp(ServicoPromocao.ObterPromocaoPorLocalizacao(new Posicao() {Latitude = latitude, Longitude = longitude}));
        }

        public ActionResult PromocaoAcesso(int idPromocao)
        {
            return this.Jsonp(ServicoPromocao.SalvarPromocaAcesso(idPromocao));
        }

        #endregion 

        #region Salvar Imagem

        [HttpPost]
        public ActionResult UploadImagem(string ext = "", string tempName = "")
        {
            if (Request.Files.Count > 0)
            {
                string tempPath = Server.MapPath("/Arquivos/Promocao/Temp/");

                var stream = Request.Files[0].InputStream;
                byte[] file = new byte[stream.Length];
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    file = memoryStream.ToArray();
                }
                if (!Directory.Exists(tempPath)) 
                    Directory.CreateDirectory(tempPath);

                if (string.IsNullOrEmpty(tempName) || tempName.ToLower() == "undefined".ToLower()) 
                    tempName = Convert.ToString(Guid.NewGuid());

                string extension = "." + ext;
                string filePath = string.Format("{0}{1}{2}", tempPath, tempName, extension);

                var imagem = Image.FromStream(stream);

                var dtoImagemNormal = new DTOImagem();
                dtoImagemNormal.Imagem = imagem;
                dtoImagemNormal.MaxLargura = imagem.Height;
                dtoImagemNormal.MaxAltura = imagem.Width;
                dtoImagemNormal.FormatoImagem =  ServicoImagem.RecuperaFormatoImagem(extension);
                dtoImagemNormal.PastaDestinoRaiz = tempPath;
                dtoImagemNormal.PastaDestino = filePath;

                ServicoImagem.SalvarImagem(dtoImagemNormal);
                return Content(string.Format("{0}|{1}|{2}", tempName, extension, DateTime.Now.Ticks));
            }
            return Content("");
        }
        
        #endregion
    }
}
