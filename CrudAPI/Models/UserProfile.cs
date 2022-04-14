using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Models
{
    public class UserProfile
    {

        public int ID { get; set; }
        [Required(ErrorMessage ="Name cannot be Null or Empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name cannot be Null or Empty")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Phone number is mandatory")]
        public string PhoneNo  { get; set; }
        public double Rating { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }
}
