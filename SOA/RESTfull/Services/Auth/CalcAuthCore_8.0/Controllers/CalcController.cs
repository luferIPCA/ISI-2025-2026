/*
 * lufer
 * ISI
 * */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthCore.Models;


namespace AuthCore.Controllers
{
    /// <summary>
    /// ISI RESTful services
    /// Controller: API + HTML
    /// ControlerBase: only API
    /// </summary>
    //[Authorize]                          //exige autorização para todos os routings
    [ApiController]
    [Route("Calcula")]
    public class CalcController : ControllerBase
    {

        private readonly ICalculatorService c;
        public CalcController(ICalculatorService cs)
        {
            c = cs;
        }

        /// <summary>
        /// Subtract operation requires Authentication as "admin"
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [Authorize(Policy ="Admin")]
        [HttpGet("subtract/{x}/{y}")]
        public IActionResult Subtract(int x, int y)
        {
            return Ok(new { Result = c.Subtract(x, y) });
        }

        
        [Authorize(Roles = "Admin,Guest")]
        [HttpGet("add/{x}/{y}")]
        public IActionResult Add(int x, int y)
        {
            return Ok(new { Result= c.Add(x , y) });
        }

        [HttpGet]
        [Route("OutroSub/{x}/{y}")]      //localhost/Calc/Sub/x/y
        public int Sub(int x, int y)
        {
            return x - y;
        }

        /// <summary>
        /// AddAdmin requires Admin authentication
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /add/admin
        ///     {
        ///        "x": valueX,
        ///        "y": valueY,
        ///     }
        ///
        /// </remarks>
        [Authorize(Policy = "Admin")]
        [HttpPost("add/admin")]
        public IActionResult AddAdmin(Dados data)
        {
            if (data == null) return BadRequest("Invalid input data");
            return Ok(new { Result = data.x + data.y });
        }

        //POST /Calcula/SomaPost
        [Authorize(Policy = "Guest")]
        [HttpPost]
        [Route("SomaPOSTAdmin")]         //Soma?x=12&y=13      //EVITAR
        public int SomaPost(int x, int y)
        {
            return x + y;
        }

        //POST /Calcula/SomaPostII
        [HttpPost]
        [Route("SomaPOSTOpen")]         //Soma?x=12&y=13      //EVITAR
        public int SomaPost(Dados d)
        {
            return d.x + d.y;
            //or com IActionResult
            //return Ok(new { Result = c.Add(d.x, d.y) });
        }
    }
}
