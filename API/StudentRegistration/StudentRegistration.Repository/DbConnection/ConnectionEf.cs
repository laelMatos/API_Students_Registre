﻿using Microsoft.EntityFrameworkCore;
using StudentRegistration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Repository
{
    public class ConnectionEf : DbContext
    {
        public ConnectionEf(DbContextOptions<ConnectionEf> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Não permite cadastro com RA já existente
            modelBuilder.Entity<Student>()
                .HasIndex(u => u.RA).IsUnique();
            //Não permite cadastro com CPF já existente
            modelBuilder.Entity<Student>()
                .HasIndex(u => u.CPF).IsUnique();
        }
    }
}