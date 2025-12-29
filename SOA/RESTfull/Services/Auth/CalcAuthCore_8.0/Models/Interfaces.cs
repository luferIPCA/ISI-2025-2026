namespace AuthCore.Models
{
    public interface ICalculatorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int Subtract(int x, int y);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int Add(int x, int y);

        //Outras
    }
}
