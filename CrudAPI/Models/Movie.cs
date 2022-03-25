using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string  Language { get; set; }
        public string Duration { get; set; }
        public DateTime PlayingDate { get; set; }
        public DateTime PlayingTime { get; set; }
        public double TicketPrice { get; set; }
        public int Rating { get; set; }
        public string Genre  { get; set; }
        public string TrailorUrl { get; set; }
        public string  ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
