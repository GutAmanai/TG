using System.Collections.Generic;

namespace br.aplicacao.tg.DTO.DTORelatorioAcesso
{
    public class DTOPromocaoQuantidade
    {
        public int IdPromocao               { get; set; }
        public string Nome                  { get; set; }
        public List<DTOAcessos> Acessos     { get; set; }

        public DTOPromocaoQuantidade()
        {
            Acessos = new List<DTOAcessos>(); 
        }
    }
}
