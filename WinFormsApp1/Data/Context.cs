using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
namespace WinFormsApp1.Data
{
    public class Context:DbContext
    {
        public DbSet<Link> Links { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=scrap;user=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Link>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Url).IsRequired();
                entity.Property(e => e.Title).IsRequired();

            });
        }
    }
}
