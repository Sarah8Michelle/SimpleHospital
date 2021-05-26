using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleHospital.Models;

namespace SimpleHospital.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<CollaboratorType> CollaboratorType { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<Pacient> Pacient { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
    }
}
