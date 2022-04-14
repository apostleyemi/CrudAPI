using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class SampleController : ControllerBase
    {
        // GET: api/<SampleController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public string Get()
        {
            return "Only Admin can come here";
        }

        // GET api/<SampleController>/5
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public string Get(int id)
        {
            return "This is for general User";
        }

        // POST api/<SampleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SampleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
