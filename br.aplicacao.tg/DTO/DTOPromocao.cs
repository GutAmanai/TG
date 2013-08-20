using System;

namespace br.aplicacao.tg.DTO
{
    public class DTOPromocao
    {
        public int IdPromocao { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataLiberacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public bool Like { get; set; }
        public string TempImg { get; set; }
        public string Extension { get; set; }
    }
}
