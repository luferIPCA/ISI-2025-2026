using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_5_0_Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("Soma/{x}/{y}")]
        public int Soma(int x, int y)
        {
            return (x + y);
        }

        /// <summary>
        /// Controlling the output error
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [HttpGet("SomaPositive/{x}/{y}")]
        public ActionResult<int> SomaPositiveValues(int x, int y)
        {
            if (x>0 && y>0)
                return (x + y);
            return NotFound();
        }
    }
}
