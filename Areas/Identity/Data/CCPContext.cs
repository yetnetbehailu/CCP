using System.Reflection.Emit;
using System.Security.Policy;
using CCP.Areas.Identity.Data;
using CCP.Models;
using CCP.Models.BreederModels;
using CCP.Models.DogModels;
using CCP.Models.KennelModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CCP.Data;

public class CCPContext : IdentityDbContext<CCPUser>
{
    public DbSet<Country> Country { get; set; }
    public DbSet<CCPUser> User { get; set; }
    public DbSet<Dog> Dog { get; set; }
    public DbSet<Breeder> Breeder { get; set; }
    public DbSet<Kennel> Kennel { get; set; }
    public DbSet<OfficialTitle> OfficialTitle { get; set; }
    public DbSet<Pedigree> pedigree { get; set; }
    public DbSet<ChampionshipTitle> ChampionshipTitle { get; set; }

    public CCPContext(DbContextOptions<CCPContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /*  Pedigree stop cascade loop  */
            builder.Entity<Pedigree>()
            .HasOne(p => p.Sire)
            .WithMany(d => d.SirePedigree)
            .HasForeignKey(p => p.SireID)
            .OnDelete(DeleteBehavior.NoAction);



            builder.Entity<Pedigree>()
            .HasOne(p => p.Dam)
            .WithMany(d => d.DamPedigree)
            .HasForeignKey(p => p.DamID)
            .OnDelete(DeleteBehavior.NoAction);



            builder.Entity<Pedigree>()
            .HasOne(p => p.Litter)
            .WithMany(d => d.LitterPedigree)
            .HasForeignKey(p => p.LitterID)
            .OnDelete(DeleteBehavior.NoAction);



            builder.Entity<Pedigree>()
            .Property(p => p.SireID)
            .IsRequired(false);



            builder.Entity<Pedigree>()
            .Property(p => p.DamID)
            .IsRequired(false);

        //modelBuilder.Entity<Pedigree>()
        //    .Property(p => p.LitterID)
        //    .IsRequired(false);

        /*  Relationship between dog and user   */
            builder.Entity<Dog>().HasOne(d => d.Owner).WithMany(u => u.DogOwner).HasForeignKey(d => d.OwnerID).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Dog>().HasOne(d => d.Breeder).WithMany(u => u.DogBreeder).HasForeignKey(d => d.BreederID).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Dog>().HasOne(d => d.Kennel).WithMany(u => u.DogKennel).HasForeignKey(d => d.KennelID).OnDelete(DeleteBehavior.NoAction);



        /*  Breeder Country cascade   */
             builder.Entity<Breeder>()
            .HasOne(b => b.Country)
            .WithMany()
            .HasForeignKey(b => b.CountryID)
            .OnDelete(DeleteBehavior.SetNull);

        // Define a one-to-one relationship between Breeder and CCPUser
             builder.Entity<Breeder>()
            .HasOne(breeder => breeder.User)
            .WithOne(user => user.Breeder)
            .HasForeignKey<Breeder>(breeder => breeder.UserID);
    }
}
