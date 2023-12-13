using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class DBContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        public DBContext()
        {
        }

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Comminication> InternalComminication { get; set; }
        public virtual DbSet<InfoType> InternalInfoType { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region  BilgiTipi

            modelBuilder.Entity<InfoType>()
               .HasMany(A => A.Comminications)
               .WithOne(A => A.InfoType)
               .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region  BilgiTipi

            modelBuilder.Entity<Comminication>();

            #endregion

            #region  Kisi

            modelBuilder.Entity<Person>();

            #endregion

            #region  Rapor

            modelBuilder.Entity<Raport>();

            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var x = "Host=localhost;Port=5432;Database=Rehber;User Id=postgres;Password=123456;";

            optionsBuilder.UseNpgsql(x);
        }

      
    }
}
