using AuthCore.Models;

namespace AuthCore.Services
{
    /// <summary>
    /// Service para cálculo
    /// </summary>
    public class CalculatorService: ICalculatorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Subtract(int x, int y) => x - y;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Add(int x, int y) => x + y;

        //outras
    }
}
