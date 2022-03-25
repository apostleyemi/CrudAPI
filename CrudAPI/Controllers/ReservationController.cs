using CrudAPI.Interfaces;
using CrudAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IGenericRepository<Reservation> _dbcontext;

        public ReservationController(IGenericRepository<Reservation> dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost]
        public IActionResult Post([FromForm]Reservation reservationObj)
        {
            _dbcontext.Insert(reservationObj);
            _dbcontext.Save();
            return Ok("reservation created");
        }
        //view all rreservations 
        [HttpGet("{action}")]
        public IActionResult AllReservations()
        {
            return Ok(_dbcontext.GetAll());
        }

        [HttpGet("{id}")]
         public IActionResult Delete(int id)
         {
             _dbcontext.Delete(id);
             return Ok("Data removed sucessfully");
         }
        
    }
}
