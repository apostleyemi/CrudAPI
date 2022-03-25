using CrudAPI.Interfaces;
using CrudAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IGenericRepository<Movie> _dbcontext;
        private readonly IGenericRepository<Reservation> _reservationRepository;

        public MoviesController(IGenericRepository<Movie> dbcontext, IGenericRepository<Reservation> reservationRepository)
        {
            _dbcontext = dbcontext;
            _reservationRepository = reservationRepository;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Post([FromForm] Movie movie)
        {
            var guid = Guid.NewGuid();
            var filepath = Path.Combine("wwwroot/uploadimg", guid + ".jpg");
            //this for image
            if (movie.Image != null)
            {

                var filestream = new FileStream(filepath, FileMode.Create);
                movie.Image.CopyTo(filestream);

                if (ModelState.IsValid)
                {
                    movie.ImageUrl  = filepath;
                    _dbcontext.Insert(movie);
                    _dbcontext.Save();

                    //  userProfile.ImagePath = filepath;

                                    

                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return NotFound("Model has problem");

                }

            }
            {
                return NotFound("Image is Empty");
            }







        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Movie movieObj)
        {
            var movie = _dbcontext.GetByID(id);
            if (movie == null)
            {
                return NotFound("No record found against the id ");
            }
            else
            {
                var guid = Guid.NewGuid();

                var filepath = Path.Combine(@"wwwroot\uploadimg", guid + ".jpg");

                if (movieObj.Image != null)
                {
                    var fs = new FileStream(filepath, FileMode.Create);
                    movieObj.Image.CopyTo(fs);
                    movie.ImageUrl = filepath;
                }


                movie.Name = movieObj.Name;
                movie.ImageUrl = movieObj.ImageUrl;
                movie.Language = movieObj.Language;
                movie.PlayingDate = movieObj.PlayingDate;
                movie.PlayingDate = movieObj.PlayingTime;
                movie.Rating = movieObj.Rating;
                movie.Reservations = movieObj.Reservations;
                movie.TicketPrice = movieObj.TicketPrice;
                movie.TrailorUrl = movieObj.TrailorUrl;
                _dbcontext.Update(movie);
                _dbcontext.Save();
                return Ok("Record Updated Successfully");
            }
        }

        [Authorize(Roles = "Admin")]

        // DELETE api/<Profile>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var movie = _dbcontext.GetByID(id);
            if (movie == null)
            {
                return NotFound("This id does not exist");
            }
            else
            {
                _dbcontext.Delete(id);
                _dbcontext.Save();
                return Ok("Data Deleted");
            }

        }
       // [Authorize(Roles ="Admin")]
        [HttpGet("{action}")]
        public IActionResult AllMovies(string sort, int pageNumber, int pageSize)
        {
            switch(sort)
            {
                case "desc": return Ok(_dbcontext.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderByDescending(m => m.Rating));
                case "asc": return Ok(_dbcontext.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(m => m.Rating));
                default: return Ok(_dbcontext.GetAll().Skip((pageNumber -1)*pageSize).Take(pageSize));
            }

           
        }

        [Authorize(Roles = "Admin")]

        // DELETE api/<Profile>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var movie = _dbcontext.GetByID(id);
            if (movie == null)
            {
                return NotFound("This id does not exist");
            }
            else
            {
               
                return Ok(movie);
            }

        }
        
        [HttpGet("[action]")]
        public IActionResult FindMovies(string movieName)
        {
            return Ok(_dbcontext.GetAll().Where(m => m.Name.StartsWith(movieName)).FirstOrDefault());
        }


    }
}
