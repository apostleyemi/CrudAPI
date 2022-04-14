using CrudAPI.Data;
using CrudAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserProfileDbContext _dbcontex;

        public ProfileController(UserProfileDbContext Dbcontex)
        {
            this._dbcontex = Dbcontex;
        }

        //
        // : api/<Profile>
        [HttpGet]
        public IActionResult  Get()
        {
            
            return Ok( _dbcontex.UserProfiles);
        }

        // GET api/<Profile>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _dbcontex.UserProfiles.Find(id);
            if(user ==null)
            {
                return NotFound("Record Not found");
            }
            else
            {
                return Ok(user);
            }
            
        }

        //attribute routing
        [HttpGet("[action]/{id}")]

        public int  Test(int id)
        {
            return id;
        }

        // POST api/<Profile>
        /* [HttpPost]
         public IActionResult Post([FromBody] UserProfile userProfile)
         {
             if (ModelState.IsValid)
             {
                 _dbcontex.UserProfiles.Add(userProfile);
                 _dbcontex.SaveChanges();

                 return StatusCode(StatusCodes.Status201Created);
             }
             return StatusCode(StatusCodes.Status203NonAuthoritative);
         }*/

        [HttpPost]
        public IActionResult Post([FromForm] UserProfile userProfile)
        {
           var guid= Guid.NewGuid();
           var filepath= Path.Combine("wwwroot/uploadimg", guid+".jpg");

            if(userProfile.Image !=null)
            {

                var filestream = new FileStream(filepath, FileMode.Create);
                userProfile.Image.CopyTo(filestream);

                if (ModelState.IsValid)
                {

                      var user = new UserProfile
                       {
                           FirstName=userProfile.FirstName,
                           LastName=userProfile.LastName,
                           PhoneNo=userProfile.PhoneNo,
                           Rating=userProfile.Rating,
                           ImagePath= filepath

                       };

                  //  userProfile.ImagePath = filepath;

                    

                    _dbcontex.UserProfiles.Add(user);
                    _dbcontex.SaveChanges();

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

        // PUT api/<Profile>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] UserProfile userProfile)
        {
          var user=  _dbcontex.UserProfiles.Find(id);
            if(user==null)
            {
                return NotFound("No record found against the id ");
            }
            else
            {
                var guid = Guid.NewGuid();

                var filepath= Path.Combine(@"wwwroot\uploadimg", guid+".jpg");

                if (userProfile.Image !=null)
                {
                    var fs = new FileStream(filepath, FileMode.Create);
                    userProfile.Image.CopyTo(fs);
                    user.ImagePath = filepath;
                }              
            
                    user.FirstName = userProfile.FirstName;
                    user.LastName = userProfile.LastName;
                    user.PhoneNo = userProfile.PhoneNo;
                    user.Rating = userProfile.Rating;

            _dbcontex.SaveChanges();
            return Ok("Record Updated Successfully");
            }
        }

        // DELETE api/<Profile>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var user = _dbcontex.UserProfiles.Find(id);
            if(user ==null)
            {
                return NotFound("This id does not exist");
            }
            else
            {
                _dbcontex.UserProfiles.Remove(user);
                _dbcontex.SaveChanges();
                return Ok("Data Deleted");
            }
           
        }
    }
}
