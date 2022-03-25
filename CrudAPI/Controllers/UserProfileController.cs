using CrudAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private static List<UserProfile> userprofile = new List<UserProfile>
        {
            new UserProfile(){ID=1, FirstName="Adeagbo", LastName="Joshua", PhoneNo="08067013148"},
            new UserProfile(){ID=2, FirstName="Femi", LastName="Olojo", PhoneNo="0906585222"},
            new UserProfile(){ID=3, FirstName="Gbenga", LastName="Adeolu", PhoneNo="07065822255"},
            
        };

        [HttpGet]
        public IEnumerable<UserProfile>Get()
        {
            return userprofile;
        }
       
    }
}
