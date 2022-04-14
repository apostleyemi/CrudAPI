using CrudAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Data
{
    public class UserProfileDbContext :DbContext
    {
        //constructor
        public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) :base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
