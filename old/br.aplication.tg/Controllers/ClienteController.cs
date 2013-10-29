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
using System.Web.Services;
using System.Web.Script.Services;

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

        public ActionResult Cadastro()
        {
            ViewBag.Alterar = false;
            ViewBag.FotoCliente = RecuperaFotoCliente(0);
            return View(ServicoCliente.ObterDTOCliente(0));
        }

        public ActionResult Alterar(int id)
        {
            ViewBag.Alterar = true;
            ViewBag.FotoCliente = RecuperaFotoCliente(id);
            return View("Cadastro", ServicoCliente.ObterDTOCliente(id));
        }

        public ActionResult Salvar(string configuracao)
        {
            try
            {
                var dtoCliente = js.Deserialize<DTOCliente>(configuracao);
                if (ServicoCliente.SalvarCliente(dtoCliente))
                {
                    var cliente = ServicoCliente.ObterDTOCliente(dtoCliente.Email);
                    this.SalvarImagemFinal(cliente.IdCliente, dtoCliente.TempImg, dtoCliente.Extension);
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

        public ActionResult EmailIsExist(string email)
        {
            return ServicoCliente.EmailIsExist(email) ? Json(true) : Json(false);
        }

        #region Salvar Imagem
        private bool SalvarImagemFinal(int idCliente, string tempImg, string extension)
        {
            if (!string.IsNullOrEmpty(tempImg))
            {
                var caminhoFotosMin = Server.MapPath("~/Arquivos/Cliente/Min/");
                var caminhoFotosNormal = Server.MapPath("~/Arquivos/Cliente/Normal/");
                var caminhoFotosTemp = Server.MapPath("~/Arquivos/Temp/");

                var arquivos = Directory.GetFiles(caminhoFotosTemp);

                var filePath = "";
                if (arquivos.Count(a => Path.GetFileNameWithoutExtension(a) == tempImg) > 0)
                    filePath = arquivos.FirstOrDefault(a => Path.GetFileNameWithoutExtension(a) == tempImg);

                string filePathMin = string.Format("{0}{1}{2}", caminhoFotosMin, idCliente, extension);
                string filePathNormal = string.Format("{0}{1}{2}", caminhoFotosNormal, idCliente, extension);

                var img = Image.FromFile(filePath);

                ResizeImagem(img, 48, 48, filePathMin, extension);
                ResizeImagem(img, img.Height, img.Width, filePathNormal, extension);
            }
            return true;
        }

        [HttpPost]
        public ActionResult UploadLogo(string ext = "", string tempName = "")
        {
            if (Request.Files.Count > 0)
            {
                string tempPath = Server.MapPath("~/Arquivos/Temp/");

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
                var img = ResizeImagem(imagem, imagem.Height, imagem.Width, filePath, ext);

                return Content(string.Format("{0}|{1}|{2}", tempName, extension, DateTime.Now.Ticks));
            }
            return Content("");
        }

        private Image ResizeImagem(Image imagem, int maxAltura, int maxLargura, string pastaDestino, string formatoImagem)
        {
            try
            {
                return SalvarImagem(imagem, maxLargura, maxAltura, RecuperaFormatoImagem(formatoImagem), pastaDestino);
            }
            catch (Exception ex)
            {
                ExceptionCustom.Log(ex);
                throw new Exception("Erro ao redimensionar a imagem");
            }
        }

        private Image SalvarImagem(Image imagem, int widthFinal, int heightFinal, ImageFormat formatoImagem, string pastaDestino)
        {
            imagem.RotateFlip(RotateFlipType.Rotate180FlipNone);
            imagem.RotateFlip(RotateFlipType.Rotate180FlipNone);
            var imagemArrumada = imagem.GetThumbnailImage(widthFinal, heightFinal, null, IntPtr.Zero);
            Image img;
            using (var mStream = new MemoryStream())
            {
                imagemArrumada.Save(mStream, formatoImagem);
                var imagemEmBytes = mStream.ToArray();
                using (var fileStream = new FileStream(pastaDestino, FileMode.Create, FileAccess.Write))
                {
                    img = Image.FromStream(mStream);
                    fileStream.Write(imagemEmBytes, 0, imagemEmBytes.Length);
                    fileStream.Close();
                }
            }
            return img;
        }

        private ImageFormat RecuperaFormatoImagem(string formatoImagem)
        {
            switch (formatoImagem.ToLower())
            {
                case "jpg":
                case ".jpg":
                case "jpeg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case "png":
                case ".png":
                    return ImageFormat.Png;
                case "bmp":
                case ".bmp":
                    return ImageFormat.Bmp;
                case "gif":
                case ".gif":
                    return ImageFormat.Gif;
                default:
                    return null;
            }
        }

        private string RecuperaFotoCliente(int idCliente)
        {
            try
            {
                var caminhoFotos = Server.MapPath("~/Arquivos/Cliente/Normal/");
                var arquivos = Directory.GetFiles(caminhoFotos);

                if (arquivos.Count(a => Path.GetFileNameWithoutExtension(a) == idCliente.ToString()) > 0)
                {
                    var foto = arquivos.FirstOrDefault(a => Path.GetFileNameWithoutExtension(a) == idCliente.ToString());
                    return VirtualPathUtility.ToAbsolute("~/Arquivos/Cliente/Normal/" + Path.GetFileName(foto));
                }
                return VirtualPathUtility.ToAbsolute("~/Arquivos/Cliente/cliente-default.png");
            }
            catch (Exception ex)
            {
                ExceptionCustom.Log(ex);
                return VirtualPathUtility.ToAbsolute("~/Arquivos/Cliente/cliente-default.png");
            }
        }

        #endregion
    }

}
