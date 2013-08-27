using System.Drawing;
using System.Drawing.Imaging;

namespace br.aplicacao.tg.DTO
{
    public class DTOImagem
    {
        public Image Imagem { get; set; }
        public int MaxLargura { get; set; }
        public int MaxAltura { get; set; }
        public ImageFormat FormatoImagem { get; set; }
        public string PastaDestino { get; set; }
    }
}
