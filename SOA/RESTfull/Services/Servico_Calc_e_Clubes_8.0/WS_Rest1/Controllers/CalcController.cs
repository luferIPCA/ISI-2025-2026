/*
 * lufer
 * ISI
 * */
using Microsoft.AspNetCore.Mvc;
using WS_Rest1.Models;

namespace WS_Rest1.Controllers
{
    /// <summary>
    /// Controller: API + HTML
    /// ControlerBase: only API
    /// </summary>
    [ApiController]
    [Route("Calcula")]
    public class CalcController : ControllerBase
    {
        //[HttpGet("Soma")]          //Soma?x=12&y=13
        //[HttpGet("Soma/{x}/{y}")]   //Soma/12/13
        //ou
        [HttpGet]
        [Route("Soma/{x}/{y}")]       //Soma/12/13
        //[Route("")]                 //Soma?x=12&y=13
        public int Soma(int x, int y)
        {
            return x + y;
        }

        [HttpGet]
        [Consumes("application/xml")]
        [Produces("application/json")]
        [Route("Sub/{x}/{y}")]      //localhost/Calc/Sub/x/y
        public int Sub(int x, int y)
        {
            return x - y;
        }

        [HttpPost]
        [Route("SomaPost")]         //Soma?x=12&y=13      //EVITAR
        public int SomaPost(int x, int y)
        {
            return x + y;
        }

        [HttpPost]
        [Route("SomaPostII")]         //Soma?x=12&y=13      //EVITAR
        public int SomaPostII(Dados d)
        {
            return d.x + d.y;
        }
    }
}
