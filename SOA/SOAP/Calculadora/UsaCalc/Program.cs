
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
 *   Chamada síncrona
*	</description>
**/

using System;
using System.Windows.Forms;

namespace UsaCalc
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
