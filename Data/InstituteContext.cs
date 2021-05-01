using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using InstituteWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InstituteWeb.Models.ViewModels;



namespace InstituteWeb.Data
{
    public class InstituteContext : IdentityDbContext<Usuario>
    {
        public InstituteContext(DbContextOptions<InstituteContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // modelBuilder.Entity<Students_Subjects>()
            // .HasKey(prop => new { prop.IDSubject, prop.IDStudent });
            modelBuilder.Entity<Student>()
                .HasMany(p => p.Subjects)
                .WithMany(g => g.Students)
                .UsingEntity<Students_Subjects>(
                    pg => pg.HasOne(prop => prop.Subject)
                    .WithMany()
                    .HasForeignKey(prop => prop.IDSubject)
                    .OnDelete(DeleteBehavior.Restrict),

                    pg => pg.HasOne(prop => prop.Student)
                    .WithMany()
                    .HasForeignKey(prop => prop.IDStudent)
                    .OnDelete(DeleteBehavior.Restrict),
                    
                    pg =>
                    {
                        
                        pg.HasKey(prop => new { prop.IDSubject, prop.IDStudent });
                    }
                );
                

            base.OnModelCreating(modelBuilder);
        }
            

        //dotnet ef migrations add dotnet ef database update
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Students_Subjects> Students_Subjects { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        

        
        
    }
}
