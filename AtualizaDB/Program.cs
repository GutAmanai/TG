using br.persistencia.tg.Infra;

namespace AtualizaDB
{
    class Program
    {
        static void Main(string[] args)
        {
            DbHelper.ExcluirBanco();
            DbHelper.CriarBanco();
        }
    }
}
