using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WS_Rest1.Models;

namespace WS_Rest1.Controllers
{
    [Route("[controller]/api/")]
    [ApiController]
    public class ClubesController : ControllerBase
    {
        static List<Club> clubes = new List<Club>() { new Club("Benfica") };

        static ClubesController()
        {
            //clubes = new List<Club>();
        }

        [HttpGet("Maior")]
        public string QualMaiorClube()
        {
            return ("BENFICA");
        }

        [HttpGet("OutrosQuaseLa")]
        public string OutrosQuaseGrandes()
        {
            return ("Portinho e " + "Braguinha");
        }

        [HttpGet("All")]        //Clubes/api/All
        //[HttpGet]               //Clubes/api/     metodo por omissão
        //[Produces("application/xml")]
        //[Consumes("")]
        [ProducesResponseType(typeof(List<Club>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //[Produces("application/json")]
        public List<Club> ShowAll()
        {
            //if (clubes.Count == 0) return (NotFound());
            return clubes;

        }

        /// <summary>
        /// POST Clubes/api/AddClub
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddClubXML")]
        [Consumes("application/xml")]
        public bool InsertClubXML(Club c)
        {
            if (clubes.Contains(c)) return false;
            clubes.Add(c);
            return true;
        }

        /// <summary>
        /// POST Clubes/api/AddClub
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddClubJson")]
        [Consumes("application/json")]      //por defeito
        public bool InsertClubJSON(Club c)
        {
            if (clubes.Contains(c)) return false;
            clubes.Add(c);
            return true;
        }

        [HttpDelete]
        [Route("DellClub/{n}")]
        public bool DeleteClub(string n)
        {
            foreach (Club c in clubes)
                if (c.Name == n)
                {
                    clubes.Remove(c);
                    return true;
                }
            return false;
        }

    }
}
