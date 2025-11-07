
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WS_EXT_2.CalcSuperSoapClient ws = new WS_EXT_2.CalcSuperSoapClient();
            int x = int.Parse(textBox1.Text);
            int y = int.Parse(textBox2.Text);
            int r = ws.Soma(x, y);
            textBox3.Text = r.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WS_EXT_2.CalcSuperSoapClient ws = new WS_EXT_2.CalcSuperSoapClient();
            int x = int.Parse(textBox1.Text);
            int y = int.Parse(textBox2.Text);
            int r = ws.Sub(x, y);
            textBox3.Text = r.ToString();
        }
    }
}
