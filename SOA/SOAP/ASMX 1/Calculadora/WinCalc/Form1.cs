/**
 * Arquiteturas Orientadas a Serviços
 * Serviços Web SOAP
 * Cliente Windows .NET 
 * 
 * Uso de serviços
 * Chamada síncrona e assíncrona
 * Uso de dados estruturados
 * 
 **/


using System;
using System.Windows.Forms;

namespace WinCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WS_EXT_1.CalcSoapClient ws = new WS_EXT_1.CalcSoapClient();

            int x = ws.Soma(2, 3);
            MessageBox.Show(x.ToString());

            WS_EXT_2.CalcSuperSoapClient ws2 = new WS_EXT_2.CalcSuperSoapClient();
            x = ws2.SomaPlus(2, 3);
            MessageBox.Show(x.ToString());

            ws2.SomaPlusCompleted += Ws2_SomaPlusCompleted;
            ws2.SomaPlusAsync(2, 3);
            MessageBox.Show("Depois");

            WS_EXT_2.Varios v = ws2.SomaMul(2, 3);

        }

        private void Ws2_SomaPlusCompleted(object sender, WS_EXT_2.SomaPlusCompletedEventArgs e)
        {
            MessageBox.Show("Assic: " +e.Result.ToString());
        }
    }
}
