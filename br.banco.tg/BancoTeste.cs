﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using br.persistencia.tg.Infra;

namespace br.banco.tg
{
    [TestClass]
    public class BancoTeste
    {
        [TestMethod]
        public void gera_script()
        {
            //teste GIT
            //DbHelper.GerarScriptDeCriacaoDoSchema(@"c:\produto-schema-eguru_");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void carga_dados()
        {
            DbHelper.ExcluirBanco();
            DbHelper.CriarBanco();
        }
    }
}