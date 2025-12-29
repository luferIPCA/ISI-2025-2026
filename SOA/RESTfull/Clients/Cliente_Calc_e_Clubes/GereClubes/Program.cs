/*
 * lufer
 * ISI
 * RESTful client
 * Using POST e GET methods
 * */

using System;

namespace GereClubes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RootObject r;
            try
            {
                //Listar todos os clubes
                r = Clubes.GetAll();
               
            }
            catch
            {
                Console.WriteLine("O Serviço não está disponível!");
                Console.ReadKey();
                return;
            }

            //POST
            Clubes.AddClub(new Team { name = "Braguinha" });

            r = Clubes.GetAll();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
