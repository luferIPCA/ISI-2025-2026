using System.Web.Services;

namespace CalcPlus.Services
{
    /// <summary>
    /// Summary description for CalcSuper
    /// </summary>
    [WebService(Namespace = "http://www.ipca.pt/isi", Description ="Serviços ISI")]
    public class CalcSuper : System.Web.Services.WebService
    {

        [WebMethod(Description ="Calcula uma soma avançada :)")]
        public int SomaPlus(int x, int y)
        {
            WS_EXT.CalcSoapClient ws = new WS_EXT.CalcSoapClient();
            return (ws.Soma(x, y));
        }

        /// <summary>
        /// Executa duas operações: Soma e Multiplicação
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [WebMethod]
        public Varios SomaMul(int x, int y)
        {
            Varios v = new Varios();
            v.Sum = SomaPlus(x, y);
            v.Mul = x * y;
            return v;
        }
    }

   /// <summary>
   /// Auxiliar para Serialização/Desserialização
   /// </summary>
    public class Varios
    {
        int mul;
        int sum;

        public int Mul
        {
            get { return mul;}
            set { mul = value; }
        }

        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }
    }

}
