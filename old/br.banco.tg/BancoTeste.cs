using Microsoft.VisualStudio.TestTools.UnitTesting;
using br.persistencia.tg.Infra;

namespace br.banco.tg
{
    [TestClass]
    public class BancoTeste
    {
        [TestMethod]
        public void gera_script()
        {            
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void carga_dados()
        {
            DbHelper.ExcluirBanco();
            DbHelper.CriarBanco();
        }

        [TestMethod]
        public void altera_estrutura()
        {
            DbHelper.AtualizarBanco();
        }

    }
}
