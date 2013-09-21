using System;

namespace br.aplicacao.tg.DTO.DTORelatorioAcesso
{
    public class DTOAcessos
    {
        public int Acessos      { get; set; }
        public DateTime Data    { get; set; }
        public int Ano          { get { return Data.Year; } }
        public int Mes          { get { return Data.Month; }}
        public int Dia          { get { return Data.Day; }}
        public int Horas        { get { return Data.Hour; }}
        public int Minutos      { get { return Data.Minute; }}
        public int Segundos     { get { return Data.Second; }}
        public int Milesimos    { get { return Data.Millisecond; } }

        public DTOAcessos(int acessos, DateTime data)
        {
            this.Acessos = acessos;
            this.Data = data;
        }
    }
}
