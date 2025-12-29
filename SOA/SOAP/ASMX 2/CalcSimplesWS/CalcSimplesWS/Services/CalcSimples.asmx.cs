
/*
*	<copyright file="CalcMelhorWS.cs" company="IPCA">
*		Copyright (c) 2025 All Rights Reserved
*	</copyright>
* 	<author>lufer</author>
*   <date>11/6/2025</date>
*	<description>
*	 Arquiteturas Orientadas a Serviços
 *   Serviço SOAP
*	</description>
**/

using System.Web.Services;

namespace CalcSimplesWS.Services
{
    /// <summary>
    /// Summary description for CalcSimples
    /// </summary>
    [WebService(Namespace = "http://ola.como.estas/")]

    public class CalcSimples : System.Web.Services.WebService
    {

        [WebMethod(Description ="Calcula a soma de dois valores inteiros")]
        public int Soma(int x, int y)
        {
            return x + y;
        }
    }
}
