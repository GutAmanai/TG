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
            //15615165
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
