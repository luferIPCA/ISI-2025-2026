/*
 * Controlador de Users
 * lufer
 **/ 

using AuthRest.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthRest.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AuthRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Operações sobre o Repositorio de users
        private readonly IUserRepository repoInstance;

        public UserController(IUserRepository repoIns)
        {
            repoInstance = repoIns;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<User> GetAllUsers()
        {
            return repoInstance.GetAll();
        }
    }
}
