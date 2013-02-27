﻿using FluentNHibernate.Mapping;
using br.dominio.Entidades;

namespace br.persistencia.tg.Mapeamentos
{
    public class EntidadeBaseMap<T> : ClassMap<T> where T : EntidadeBase
    {
        public EntidadeBaseMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
        }
    }
}
