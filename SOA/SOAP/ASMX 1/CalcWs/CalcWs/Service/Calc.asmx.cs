using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CalcWs.Service
{
    /// <summary>
    /// Summary description for Calc
    /// </summary>
    [WebService(Namespace = "http://www.ipca.pt/isi/")]

    public class Calc : System.Web.Services.WebService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [WebMethod]
        public int Soma(int x, int y)
        {
            return x + y;
        }
    }
}
