using br.persistencia.tg.Infra;
using System;

namespace AtualizaDB
{
    class Program
    {
        static void Main(string[] args)
        {
            DbHelper.ExcluirBanco();
            DbHelper.CriarBanco();
            Console.Write("Criação realizada");
            Console.ReadKey();
            DbHelper.AtualizarBanco();

            Console.Write("Atualização Finalizada");
            Console.ReadKey();
        }
    }
}
