/*
*	<copyright file="CalcMelhorWS.cs" company="IPCA">
*		Copyright (c) 2025 All Rights Reserved
*	</copyright>
* 	<author>lufer</author>
*   <date>11/6/2025</date>
*	<description>
*	 Arquiteturas Orientadas a Serviços
 *   Serviços Web SOAP
 *   Cliente Windows .NET 
 * 
 *   Uso de serviços
 *   Chamada síncrona e assíncrona
 *   Uso de dados estruturados
*	</description>
**/
using System;

namespace UsaCalcWS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //instância de serviço externo
            WS_EXT_1.CalcSimplesSoapClient ws = new WS_EXT_1.CalcSimplesSoapClient();
            //chamada síncrona
            int r = ws.Soma(2, 3);
            Console.WriteLine(r);

            //instância de outro serviço externo
            WS_EXT_2.CalcSuperSoapClient ws2 = new WS_EXT_2.CalcSuperSoapClient();
            //chamada síncrona
            r = ws2.Soma(3, 5);
            Console.WriteLine("Ola");

            //instância de outro serviço externo
            //devolve dados estrururados
            WS_EXT_2.VariasCoisas aux = ws2.SumESub(2, 3);
            Console.WriteLine("Soma: ", aux.Sum);
            Console.WriteLine("Sub: ", aux.Sub);

            #region ASSINCRONO
            //Declração de Suportadore de Eventos
            ws2.SumESubCompleted += Ws2_SumESubCompleted; //Event Handler
            ws2.SumESubAsync(2, 3);                       //chamada assincrona!
            Console.WriteLine("Está a executar...");

            #endregion

            //Console.Write(r);

            Console.ReadKey();


        }

        private static void Ws2_SumESubCompleted(object sender, WS_EXT_2.SumESubCompletedEventArgs e)
        {
            Console.WriteLine("Soma= ", ((WS_EXT_2.VariasCoisas)e.Result).Sum);
        }
    }
}
