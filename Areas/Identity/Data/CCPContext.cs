using System;
using System.Reflection.Emit;
using CCP.Areas.Identity.Data;
using CCP.Models;
using CCP.Models.BreederModels;
using CCP.Models.DogModels;
using CCP.Models.KennelModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static CCP.Models.DogModels.Dog;

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
    public DbSet<ImagesMetaData> ImagesMetaData { get; set; }
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
        
        //Make kennel name unique
        builder.Entity<Kennel>()
        .HasIndex(k => k.Name)
        .IsUnique();


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

        // Seed data for the Country entity
        builder.Entity<Country>().HasData(
        new Country
        {
            ID = 1,
            Name = "Denmark",
            Continent = "Europe",
        },
        new Country
        {
            ID = 2,
            Name = "United Kingdom",
            Continent = "Europe",
        },

        new Country
        {
            ID = 3,
            Name = "Canada",
            Continent = "North America",
        },
        new Country
        {
            ID = 4,
            Name = "Australia",
            Continent = "Australia",
        },
        new Country
        {
            ID = 5,
            Name = "Brazil",
            Continent = "South America",
        },
        new Country
        {
            ID = 6,
            Name = "Russia",
            Continent = "Europe",
        },
        new Country
        {
            ID = 7,
            Name = "India",
            Continent = "Asia",
        },
        new Country
        {
            ID = 8,
            Name = "South Africa",
            Continent = "Africa",
        },
        new Country
        {
            ID = 9,
            Name = "Argentina",
            Continent = "South America",
        },
        new Country
        {
            ID = 10,
            Name = "Japan",
            Continent = "Asia",
        },
        new Country
        {
            ID = 11,
            Name = "Germany",
            Continent = "Europe",
        },
        new Country
        {
            ID = 12,
            Name = "France",
            Continent = "Europe",
        },
        new Country
        {
            ID = 13,
            Name = "Mexico",
            Continent = "North America",
        },
        new Country
        {
            ID = 14,
            Name = "Egypt",
            Continent = "Africa",
        },
        new Country
        {
            ID = 15,
            Name = "Italy",
            Continent = "Europe",
        },
        new Country
        {
            ID = 16,
            Name = "Thailand",
            Continent = "Asia",
        },
        new Country
        {
            ID = 17,
            Name = "Greece",
            Continent = "Europe",
        },
        new Country
        {
            ID = 18,
            Name = "Nigeria",
            Continent = "Africa",
        },
        new Country
        {
            ID = 19,
            Name = "Sweden",
            Continent = "Europe",
        },
        new Country
        {
            ID = 20,
            Name = "China",
            Continent = "Asia",
        });

        // Seed data for User
        builder.Entity<CCPUser>().HasData(
        new CCPUser
        {
            Id = "user1",
            UserName = "user1@example.com",
            NormalizedUserName = "USER1@EXAMPLE.COM",
            Email = "user1@example.com",
            NormalizedEmail = "USER1@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "your_hashed_password",
            SecurityStamp = "security_stamp_1",
            ConcurrencyStamp = "concurrency_stamp_1",
        },
        new CCPUser
        {
            Id = "user2",
            UserName = "user2@example.com",
            NormalizedUserName = "USER2@EXAMPLE.COM",
            Email = "user2@example.com",
            NormalizedEmail = "USER2@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "your_hashed_password",
            SecurityStamp = "security_stamp_2",
            ConcurrencyStamp = "concurrency_stamp_2",
        },
        new CCPUser
        {
            Id = "user3",
            UserName = "user3@example.com",
            NormalizedUserName = "USER3@EXAMPLE.COM",
            Email = "user3@example.com",
            NormalizedEmail = "USER3@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_3",
            SecurityStamp = "security_stamp_3",
            ConcurrencyStamp = "concurrency_stamp_3",
        },
        new CCPUser
        {
            Id = "user4",
            UserName = "user4@example.com",
            NormalizedUserName = "USER4@EXAMPLE.COM",
            Email = "user4@example.com",
            NormalizedEmail = "USER4@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_4",
            SecurityStamp = "security_stamp_4",
            ConcurrencyStamp = "concurrency_stamp_4",
        },
        new CCPUser
        {
            Id = "user5",
            UserName = "user5@example.com",
            NormalizedUserName = "USER5@EXAMPLE.COM",
            Email = "user5@example.com",
            NormalizedEmail = "USER5@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_5",
            SecurityStamp = "security_stamp_5",
            ConcurrencyStamp = "concurrency_stamp_5",
        },
        new CCPUser
        {
            Id = "user6",
            UserName = "user6@example.com",
            NormalizedUserName = "USER6@EXAMPLE.COM",
            Email = "user6@example.com",
            NormalizedEmail = "USER6@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_6",
            SecurityStamp = "security_stamp_6",
            ConcurrencyStamp = "concurrency_stamp_6",
        },
        new CCPUser
        {
            Id = "user7",
            UserName = "user7@example.com",
            NormalizedUserName = "USER7@EXAMPLE.COM",
            Email = "user7@example.com",
            NormalizedEmail = "USER7@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_7",
            SecurityStamp = "security_stamp_7",
            ConcurrencyStamp = "concurrency_stamp_7",
        },
        new CCPUser
        {
            Id = "user8",
            UserName = "user8@example.com",
            NormalizedUserName = "USER8@EXAMPLE.COM",
            Email = "user8@example.com",
            NormalizedEmail = "USER8@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_8",
            SecurityStamp = "security_stamp_8",
            ConcurrencyStamp = "concurrency_stamp_8",
        },
        new CCPUser
        {
            Id = "user9",
            UserName = "user9@example.com",
            NormalizedUserName = "USER9@EXAMPLE.COM",
            Email = "user9@example.com",
            NormalizedEmail = "USER9@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_9",
            SecurityStamp = "security_stamp_9",
            ConcurrencyStamp = "concurrency_stamp_9",
        },
        new CCPUser
        {
            Id = "user10",
            UserName = "user10@example.com",
            NormalizedUserName = "USER10@EXAMPLE.COM",
            Email = "user10@example.com",
            NormalizedEmail = "USER10@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password_10",
            SecurityStamp = "security_stamp_10",
            ConcurrencyStamp = "concurrency_stamp_10",
        });

        // Seed data for Breder entity
        builder.Entity<Breeder>().HasData(
        new Breeder
        {
            ID = 1,
            UserID = "user1",
            CountryID = 1,
            Name = "John Smith",
            Address = "Breeder Address 1",
            Phone = "123-456-1234"
        },
        new Breeder
        {
            ID = 2,
            UserID = "user2",
            CountryID = 2,
            Name = "Alice Johnson",
            Address = "Breeder Address 2",
            Phone = "123-456-1235"
        },
        new Breeder
        {
            ID = 3,
            UserID = "user3",
            CountryID = 3,
            Name = "David Brown",
            Address = null,
            Phone = null
        });

        //Seed data for Kennel entity
        builder.Entity<Kennel>().HasData(
        new Kennel
        {
            ID = 1,
            CountryID = 1, // Replace with the actual CountryID
            Name = "Kennel1",
            OwnerName = "Owner1",
            WebsiteURL = "https://www.kennel1.com",
            Address = "Kennel Address 1",
            Phone = "123-456-7890",
            Mobile = "987-654-3210",
            About = "Kennel Description 1",
            UserId = "User1"
        },
        new Kennel
        {
            ID = 2,
            CountryID = 2, // Replace with the actual CountryID
            Name = "Kennel2",
            OwnerName = "Owner2",
            WebsiteURL = "https://www.kennel2.com",
            Address = "Kennel Address 2",
            Phone = "234-567-8901",
            Mobile = "876-543-2109",
            About = "Kennel Description 2",
            UserId = "User2"
        });

        // Seed data for Official Titles
        builder.Entity<OfficialTitle>().HasData(
        new OfficialTitle
        {
            ID = 1,
            Title = "Champion",
            FullTitle = "Champion of the Show",
        },
        new OfficialTitle
        {
            ID = 2,
            Title = "Grand Champion",
            FullTitle = "Grand Champion of the Show",
        },
        new OfficialTitle
        {
            ID = 3,
            Title = "Best in Breed",
            FullTitle = "Best in Breed Award",
        },
        new OfficialTitle
        {
            ID = 4,
            Title = "Best in Show",
            FullTitle = "Best in Show Award",
        },
        new OfficialTitle
        {
            ID = 5,
            Title = "Reserve Champion",
            FullTitle = "Reserve Champion of the Show",
        },
        new OfficialTitle
        {
            ID = 6,
            Title = "Best Puppy",
            FullTitle = "Best Puppy Award",
        },
        new OfficialTitle
        {
            ID = 7,
            Title = "Best Veteran",
            FullTitle = "Best Veteran Award",
        });

        // Seed data for Dog with ChampionshipTitle
        builder.Entity<ChampionshipTitle>().HasData(
        new ChampionshipTitle
        {
            ID = 1,
            DogID = 1,
            OfficialTitleID = 1,
        },
        new ChampionshipTitle
        {
            ID = 2,
            DogID = 2,
            OfficialTitleID = 2,
        });

        // Seed Data for Pedigree
        builder.Entity<Pedigree>().HasData(
        new Pedigree
        {
            ID = 1,
            SireID = null,  // Dog ID here if needed.
            DamID = null,   // Dog ID here if needed.
            LitterID = 2,  // Dog ID for the litter here.
        },
        new Pedigree
        {
            ID = 2,
            SireID = 1,
            DamID = 3,
            LitterID = 2,
        });

        // Seed data for the Dog entity
        builder.Entity<Dog>().HasData(
        new Dog
        {
            ID = 1,
            RegName = "Dog1",
            RegNo = "RegNo1",
            PetName = "Pet1",
            DOB = new DateTime(2019, 1, 1),
            YearOfDeath = new DateTime(2021, 5, 10),
            Coat = Coats.Hairless,
            Gender = Genders.Male,
            Color = "Brown",
            Height = 24.5m,  // Height in inches
            Weight = 55.2m,  // Weight in in kilos
            OwnerID = "user2",
            BreederID = "user2",
            KennelID = null,
        },
        new Dog
        {
            ID = 2,
            RegName = "Dog2",
            RegNo = "D67890",
            PetName = "Buddy",
            DOB = new DateTime(2019, 9, 3),
            YearOfDeath = null,
            Coat = Coats.Powderpuff,
            Gender = Genders.Female,
            Color = "White",
            Height = 18.3m,
            Weight = 42.7m,
            OwnerID = "user1",
            BreederID = null,
            KennelID = "user1",
        },
        new Dog
        {
            ID = 3,
            RegName = "Dog3",
            RegNo = "D67893",
            PetName = "Kiki",
            DOB = new DateTime(2014, 9, 7),
            YearOfDeath = null,
            Coat = Coats.Powderpuff,
            Gender = Genders.Female,
            Color = "Red",
            Height = 18.3m,
            Weight = 42.7m,
            OwnerID = "user3",
            BreederID = "user3",
            KennelID = "user3",
        });
    }
}
