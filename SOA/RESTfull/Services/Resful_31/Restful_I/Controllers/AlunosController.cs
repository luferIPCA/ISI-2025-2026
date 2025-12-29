using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Restful_1.Models;

namespace Restful_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        static List<Aluno> turma = new List<Aluno>() { new Aluno(12) };


        [HttpPost("insereAluno")]
        public bool InsertAluno(Aluno a)
        {
            if (turma.Contains(a)) return false;
            turma.Add(a);
            return true;
        }

        [HttpGet("all")]
        public List<Aluno> GetAll()
        {
            return turma;
        }

    }
}
