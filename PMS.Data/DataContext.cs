using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Data
{
    public class DataContext: DbContext
    {
        private IConfiguration _configure;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configure) : base(options)
        {
            _configure = configure;
        }

        public DataContext()
        {

        }

        public DbSet<User> User => Set<User>();
        public DbSet<Role> Role => Set<Role>();
        public DbSet<PriceProposal> PriceProposal => Set<PriceProposal>();
        public DbSet<Proposal> Proposal => Set<Proposal>();
        public DbSet<Order> Order => Set<Order>();
        public DbSet<Proposal> Test => Set<Proposal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_configure["ConnectionString:MoveITConnectionString"]);
        //}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
               .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MoveIT");
            }
        }
    }
}
