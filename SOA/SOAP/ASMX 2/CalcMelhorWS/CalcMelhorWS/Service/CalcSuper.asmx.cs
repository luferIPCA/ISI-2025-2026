/*
*	<copyright file="CalcMelhorWS.cs" company="IPCA">
*		Copyright (c) 2025 All Rights Reserved
*	</copyright>
* 	<author>lufer</author>
*   <date>11/6/2025</date>
*	<description>
*	 Arquiteturas Orientadas a Serviços
 *   Serviço SOAP
 *   Uso serviço SOAP externo
*	</description>
**/

using System.Web.Services;

namespace CalcMelhorWS.Service
{
    /// <summary>
    /// Summary description for CalcSuper
    /// </summary>
    [WebService(Namespace = "http://xxx.yyy.pt/", Description ="Serviços SOAP - ISI - 2025")]

    public class CalcSuper : System.Web.Services.WebService
    {

        [WebMethod(Description ="Calcula a soma de dois valores inteiros")]
        public int Soma(int x, int y)
        {
            //usa serviço SOAP externo
            WS_EXT.CalcSimplesSoapClient ws = new WS_EXT.CalcSimplesSoapClient();
            return ws.Soma(x, y);
        }

        [WebMethod(Description ="Calcula a subtração de dois inteiros")]
        public int Sub(int x, int y)
        {
            return x - y;
        }

        [WebMethod(Description ="Calcula aa Soma e A Subtração dos dois parâmetros")]
        public VariasCoisas SumESub(int x, int y)
        {
            VariasCoisas aux = new VariasCoisas();
            aux.Sub = Sub(x, y);
            aux.Sum = Soma(x, y);
            return aux;
        }

    }

    /// <summary>
    /// Classe auxiliar para serializar resultados
    /// </summary>
    public class VariasCoisas
    {
        int sum;
        int sub;

        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }

        public int Sub
        {
            get { return sub; }
            set { sub = value; }
        }
    }
}
