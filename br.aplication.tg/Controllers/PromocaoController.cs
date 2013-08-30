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
using br.dominio.tg.ObjetoValor;
using br.infra.tg.InjecaoDependencia;

namespace br.aplication.tg.Controllers
{
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
            catch (Exception)
            {
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

        public ActionResult ListarPromocao(string latitude, string longitude)
        {

            return this.Jsonp(new
            {
                promocoes = new[] {
                new {IdEmpresa = "1" ,
                    NomeEmpresa = "Submarino", 
                    UrlEmpresa = "https://s3.amazonaws.com/media.jetstrap.com/mR2FrQKeQg2Us3u4CWPB_submarino_logo.png",
                    UrlPromocao = "https://s3.amazonaws.com/media.jetstrap.com/IZjLVWRQVqc7G8EDyEwe_20130521_submarino.jpg",
                    Promocao = "Dia de eletrônicos, Sub Tek, o seu guia perfeito no mundo High-tech. Toda a loja de Eletrônicos com 10% de desconto. Dia de eletrônicos, Sub Tek, o seu guia perfeito no mundo High-tech. Toda a loja de Eletrônicos com 10% de desconto. Dia de eletrônicos, Sub Tek, o seu guia perfeito no mundo High-tech. Toda a loja de Eletrônicos com 10% de desconto.",
                    Endereco = "Rua santa ifigênia, 2526",
                    Latitude = latitude,
                    Longitude = longitude
                }, 
                new {IdEmpresa = "2" ,
                    NomeEmpresa = "Wine", 
                    UrlEmpresa = "https://s3.amazonaws.com/media.jetstrap.com/Ac6V1WzpRGSjEmPpQhJW_wine_logo.jpg",
                    UrlPromocao = "https://s3.amazonaws.com/media.jetstrap.com/AuGv7oubRe2IiIwIvZH1_20130521_wine.jpg",
                    Promocao = "Comprar vinho é na Wine.com.br, o melhor do vinho em suas mãos.",
                    Endereco = "Rua augusta, 1582",
                    Latitude = latitude,
                    Longitude = longitude
                }
}
            });
        }
        /*
        public ActionResult ListarPromocao(double latitude, double longitude)
        {
            return this.Jsonp(ServicoPromocao.ObterLocalizacaoMobile(new Posicao() {Latitude = latitude, Longitude = longitude}));
        }
        */
        #endregion 

        #region Salvar Imagem

        [HttpPost]
        public ActionResult UploadImagem(string ext = "", string tempName = "")
        {
            if (Request.Files.Count > 0)
            {
                string tempPath = Server.MapPath("~/Arquivos/Promocao/Temp/");

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
