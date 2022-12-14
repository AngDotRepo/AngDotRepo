using AzureCosmosEFCoreCRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Runtime;

namespace AzureCosmosEFCoreCRUD.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Videogames>()
            //     .HasNoDiscriminator()
            //     .ToContainer(nameof(Videogames))
            //     .HasPartitionKey(da => da.Id)
            //     .HasKey(da => new { da.Id });
            //modelBuilder.ApplyConfiguration(new VideogameEntityConfiguration());

            //modelBuilder.Entity<Item>()
            //     .HasNoDiscriminator()
            //     .ToContainer(nameof(Item))
            //     .HasPartitionKey(da => da.Id)
            //     .HasKey(da => new { da.Id });

        }
        //        public DbSet<Videogames> Videogames { get; set; }

        public DbSet<Item> Item { get; set; }
    }
    //public class VideogameEntityConfiguration : IEntityTypeConfiguration<Videogames>
    //{
    //    public void Configure(EntityTypeBuilder<Videogames> builder)
    //    {
    //       builder.OwnsOne(x => x.Company);
    //    }
    //}
    
}
