using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    public DbSet<School> Schools { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<School>().HasData(
            new School
            {
                Id = 1,
                Name = "ENISO",
                Sections = "IA, GTE, GMP",
                Director = "AC-COLL",
                Rating = 3.5,
                WebSite = "http://www.eniso.ruu.tn"
            },
            new School
            {
                Id = 2,
                Name = "ENTM",
                Sections = "Mécanique, Aéronautique, Informatique",
                Director = "B-RAMI",
                Rating = 4.0,
                WebSite = "http://www.entm.tn"
            },
            new School
            {
                Id = 3,
                Name = "ESI",
                Sections = "Informatique, Réseaux, Télécoms",
                Director = "K-JEMAA",
                Rating = 4.2,
                WebSite = null  // Cette école n'a pas de site web
            }
        );
    }
}
