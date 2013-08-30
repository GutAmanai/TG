using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using br.aplicacao.tg.DTO;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoImagem
    {

        public string RecuperaImagemPromocao(int idCliente, int idPromocao)
        {
            try
            {
                var caminhoFotos = HttpContext.Current.Request.PhysicalApplicationPath + "\\Arquivos\\Promocao\\Normal\\" + idCliente + "\\";
                var arquivos = Directory.GetFiles(caminhoFotos);

                if (arquivos.Count(a => Path.GetFileNameWithoutExtension(a) == idPromocao.ToString()) > 0)
                {
                    var foto = arquivos.FirstOrDefault(a => Path.GetFileNameWithoutExtension(a) == idPromocao.ToString());
                    return VirtualPathUtility.ToAbsolute("~/Arquivos/Promocao/Normal/"+ idCliente + "/" + Path.GetFileName(foto));
                }
                return VirtualPathUtility.ToAbsolute("~/Arquivos/Promocao/icon_image.png");
            }
            catch (Exception)
            {
                return VirtualPathUtility.ToAbsolute("~/Arquivos/Promocao/icon_image.png");
            }
        }

        public string RecuperaImagemCliente(int idCliente)
        {
            try
            {
                var caminhoFotos = HttpContext.Current.Request.PhysicalApplicationPath + "\\Arquivos\\Cliente\\Normal\\";
                var arquivos = Directory.GetFiles(caminhoFotos);

                if (arquivos.Count(a => Path.GetFileNameWithoutExtension(a) == idCliente.ToString()) > 0)
                {
                    var foto = arquivos.FirstOrDefault(a => Path.GetFileNameWithoutExtension(a) == idCliente.ToString());
                    return VirtualPathUtility.ToAbsolute("~/Arquivos/Cliente/Normal/" + Path.GetFileName(foto));
                }
                return VirtualPathUtility.ToAbsolute("~/Arquivos/Cliente/cliente-default.png");
            }
            catch (Exception)
            {
                return VirtualPathUtility.ToAbsolute("~/Arquivos/Cliente/cliente-default.png");
            }
        }

        public bool SalvarImagemFinalPromocao(int idCliente, int idPromocao, string tempImg, string extension)
        {
            if (!string.IsNullOrEmpty(tempImg))
            {
                var caminhoFotosMin = HttpContext.Current.Request.PhysicalApplicationPath + "\\Arquivos\\Promocao\\Min\\" + idCliente + "\\";
                var caminhoFotosNormal = HttpContext.Current.Request.PhysicalApplicationPath + "\\Arquivos\\Promocao\\Normal\\" + idCliente + "\\";
                var caminhoFotosTemp = HttpContext.Current.Request.PhysicalApplicationPath + "\\Arquivos\\Promocao\\Temp\\";

                var arquivos = Directory.GetFiles(caminhoFotosTemp);

                var filePath = "";
                if (arquivos.Count(a => Path.GetFileNameWithoutExtension(a) == tempImg) > 0)
                    filePath = arquivos.FirstOrDefault(a => Path.GetFileNameWithoutExtension(a) == tempImg);

                string filePathMin = string.Format("{0}{1}{2}", caminhoFotosMin, idPromocao, extension);
                string filePathNormal = string.Format("{0}{1}{2}", caminhoFotosNormal, idPromocao, extension);

                var img = Image.FromFile(filePath);

                var dtoImagemNormal = new DTOImagem();
                dtoImagemNormal.Imagem = img;
                dtoImagemNormal.MaxLargura = img.Height;
                dtoImagemNormal.MaxAltura = img.Width;
                dtoImagemNormal.FormatoImagem = RecuperaFormatoImagem(extension);
                dtoImagemNormal.PastaDestinoRaiz = caminhoFotosNormal;
                dtoImagemNormal.PastaDestino = filePathNormal;

                var dtoImagemMin = new DTOImagem();
                dtoImagemMin.Imagem = img;
                dtoImagemMin.MaxLargura = 48;
                dtoImagemMin.MaxAltura = 48;
                dtoImagemMin.FormatoImagem = RecuperaFormatoImagem(extension);
                dtoImagemMin.PastaDestinoRaiz = caminhoFotosMin;
                dtoImagemMin.PastaDestino = filePathMin;

                SalvarImagem(dtoImagemNormal);
                SalvarImagem(dtoImagemMin);
            }
            return true;
        }

        public Image SalvarImagem(DTOImagem dtoImagem)
        {
            dtoImagem.Imagem.RotateFlip(RotateFlipType.Rotate180FlipNone);
            dtoImagem.Imagem.RotateFlip(RotateFlipType.Rotate180FlipNone);
            var imagemArrumada = dtoImagem.Imagem.GetThumbnailImage(dtoImagem.MaxLargura, dtoImagem.MaxAltura, null, IntPtr.Zero);
            Image img;
            using (var mStream = new MemoryStream())
            {
                imagemArrumada.Save(mStream, dtoImagem.FormatoImagem);
                var imagemEmBytes = mStream.ToArray();

                if(dtoImagem.PastaDestinoRaiz != null)
                {
                    bool isExists = Directory.Exists(dtoImagem.PastaDestinoRaiz);
                    if (!isExists)
                        Directory.CreateDirectory(dtoImagem.PastaDestinoRaiz);
                }

                using (var fileStream = new FileStream(dtoImagem.PastaDestino, FileMode.Create, FileAccess.Write))
                {
                    img = Image.FromStream(mStream);
                    fileStream.Write(imagemEmBytes, 0, imagemEmBytes.Length);
                    fileStream.Close();
                }
            }
            return img;
        }

        public ImageFormat RecuperaFormatoImagem(string formatoImagem)
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
    }

    public static class UrlExtensions
    {
        public static string AbsoluteContent(string contentPath)
        {
            var requestUrl = HttpContext.Current.Request.Url;
            return string.Format("{0}{1}", requestUrl.GetLeftPart(UriPartial.Authority), VirtualPathUtility.ToAbsolute(contentPath));
        }
    }
}

