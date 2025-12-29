/**
 * Arquiteturas Orientadas a Serviços
 * Serviços Web SOAP
 * Cliente Desktop .NET 
 * 
 * Uso de serviços
 * Chamada síncrona e assíncrona
 * Uso de dados estruturados
 * 
 **/
using System;

namespace UsaWSCalc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WS_EXT.CalcSuperSoapClient ws = new WS_EXT.CalcSuperSoapClient();
            //Chamada sincrona de serviço externo
            int x = ws.SomaPlus(2, 3);
            Console.WriteLine("Soma: ",x);
            Console.ReadKey();

            //Chamada assincrona
            //a referência ao serviço externo tem de suportar chamadas assíncronas
            ws.SomaPlusCompleted += Ws_SomaPlusCompleted;   //Suportador de eventos | Event Handler
            ws.SomaPlusAsync(2, 3);                         //chamada assíncrona
            Console.ReadKey();
        }

        /// <summary>
        /// Método executado aquando o evento SomaPlusCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Resultado</param>
        private static void Ws_SomaPlusCompleted(object sender, WS_EXT.SomaPlusCompletedEventArgs e)
        {
            Console.WriteLine("Soma=" + e.Result.ToString());
        }
    }
}
