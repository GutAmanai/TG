using Microsoft.VisualStudio.TestTools.UnitTesting;
using br.persistencia.Infra;

namespace br.banco.tg
{
    [TestClass]
    public class BancoTeste
    {
        [TestMethod]
        public void gera_script()
        {
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