﻿using System;

namespace br.aplicacao.tg.DTO
{
    public class DTOPromocao
    {
        public int IdPromocao { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string DataCadastro { get; set; }
        public string DataLiberacao { get; set; }
        public string DataExpiracao { get; set; }        
        public string ImagemUrl { get; set; }
        public string TempImg { get; set; }
        public string Extension { get; set; }
        
        public DateTime DataLiberacaoToDate 
        { 
            get { return DateTime.Parse(DataLiberacao); }
        }
        
        public DateTime DataExpiracaoToDate
        {
            get { return DateTime.Parse(DataExpiracao); }    
        }

        public string AtivoToString
        {
            get { return Ativo ? "Ativo" : "Desativo"; }
        }

        public string DataLiberacaoFormatada
        {
            get { return DataLiberacao.ToString().Replace("/", "-"); }
        }

        public string DataExpiracaoFormatada
        {
            get { return DataExpiracao.ToString().Replace("/", "-"); }
        }

    }
}
