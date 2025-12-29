using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restful_1.Controllers
{
    [Route("servicos/calc")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        [Route("fazAlgo")]
        [HttpGet("simples")]
        public int Devolve1()
        {
            return 1;
        }

        [Route("soma/{x}/{y}")]
        [HttpGet]
        public int Soma(int x, int y)
        {
            return x + y;
        }
    }
}
