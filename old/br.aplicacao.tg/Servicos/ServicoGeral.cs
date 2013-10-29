using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.persistencia.tg.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoGeral
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

        public ServicoGeral(
                                IUnidadeDeTrabalho unidadeDeTrabalho
                           )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }

        //private string RecuperaFotoCliente(int id)
        //{
        //    try
        //    {
        //        var caminhoFotos = Server.MapPath("~/Arquivos/Fotos/" + id);
        //        var arquivos = Directory.GetFiles(caminhoFotos);

        //        if (arquivos.Count(a => Path.GetFileNameWithoutExtension(a) == usuario.Login.Cpf) > 0)
        //        {
        //            var foto = arquivos.FirstOrDefault(a => Path.GetFileNameWithoutExtension(a) == usuario.Login.Cpf);
        //            return VirtualPathUtility.ToAbsolute("~/Arquivos/FotosUsuarios/" + usuario.Processo.Id + "/" + Path.GetFileName(foto));
        //        }
        //        return VirtualPathUtility.ToAbsolute("~/Content/images/default-user-avatar.png");
        //    }
        //    catch (Exception ex)
        //    {
        //        return VirtualPathUtility.ToAbsolute("~/Content/images/default-user-avatar.png");
        //    }
        //}
    }
}
