using Microsoft.EntityFrameworkCore;
using RazaAndCoTestApp.Model;
using System.Collections.Generic;

namespace RazaAndCoTestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
    }

}
