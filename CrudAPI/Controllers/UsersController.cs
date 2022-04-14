using CrudAPI.Data;
using CrudAPI.Models;
using CrudAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationPlugin;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private readonly IGenericRepository<User> _user;

        public UsersController( IGenericRepository<User> user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }
        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var Checkuser = _user.GetAll().Where(e => e.Email == user.Email).FirstOrDefault();
            if(Checkuser !=null)
            {
                return BadRequest("User with the same Email Already exist!");
            }

            var newUser = new User
            {
                Email = user.Email,
                Name = user.Name,
                Password =SecurePasswordHasherHelper.Hash(user.Password),
                Role="Users"
            };
            /* _dbContext.Users.Add(newUser);
             _dbContext.SaveChanges();*/
            _user.Insert(newUser);
            _user.Save();
            return Ok("Registratiin Successful!");
        }
        [HttpPost]
        public IActionResult Update(int Id, [FromForm] User user)
        {
            var existsUser = _user.GetByID(user.Id);
            if(existsUser == null)
            {
                return NotFound("The record you are trying to update does not exist");
            }
            existsUser.Id = user.Id;
            existsUser.Name = user.Name;
            existsUser.Email = user.Email;
            existsUser.Password = user.Password;
            existsUser.Role = user.Role;

            _user.Update(existsUser);

            return Ok("Data Updated");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var checkUser = _user.GetByID(Id);
            if(checkUser ==null)
            {
                return NotFound("The record you are trying to update does not exist");
            }

            _user.Delete(checkUser);
            _user.Save();

            return Ok("Value Removed successfully");
        }

        [HttpGet]
        public IActionResult FindByEmailAsync(string email)
        {
            var checkUser = _user.GetAll().Where(e => e.Email == email).FirstOrDefault();
            if (checkUser == null)
            {
                return NotFound("The record you are trying to update does not exist");
            }

            

            return Ok(checkUser);
        }
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var userEmail = _user.GetAll().Where(e => e.Email == user.Email).FirstOrDefault();

            if(userEmail==null)
            {
                return NotFound();
            }
            if(!SecurePasswordHasherHelper.Verify(user.Password, userEmail.Password))
            {
                return Unauthorized();
            }

            var claims = new[]
             {
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Role, userEmail.Role),
             };

            var token = _auth.GenerateAccessToken(claims);

            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id=userEmail.Id
            });
        }

    }
}
