using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiHybrid
{
    public class AppDbContext : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "alumnos.db");
            optionsBuilder.UseSqlite($"Data Source={path}");
        }


    }
}
