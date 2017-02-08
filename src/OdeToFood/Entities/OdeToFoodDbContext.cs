﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OdeToFood.Entities
{
    public class OdeToFoodDbContext : IdentityDbContext<User>
    {
        public OdeToFoodDbContext(DbContextOptions options): base(options)
        {

           // var user = this.Users.Where(r => r.UserName == "Billy"  );
        }
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}