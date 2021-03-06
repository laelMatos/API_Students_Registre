using Microsoft.EntityFrameworkCore;
using StudentRegistration.Domain;

namespace StudentRegistration.Repository
{
    public class ConnectionEf : DbContext
    {
        public ConnectionEf(DbContextOptions<ConnectionEf> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Não permite cadastro com RA já existente
            modelBuilder.Entity<Student>()
                .HasIndex(u => u.RA).IsUnique();
            //Não permite cadastro que tenha email já existente
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
        }
    }
}
