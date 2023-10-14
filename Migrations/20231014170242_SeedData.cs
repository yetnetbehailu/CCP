using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user1", 0, "concurrency_stamp_1", "user1@example.com", true, false, null, "USER1@EXAMPLE.COM", "USER1@EXAMPLE.COM", "your_hashed_password", null, false, "security_stamp_1", false, "user1@example.com" },
                    { "user10", 0, "concurrency_stamp_10", "user10@example.com", true, false, null, "USER10@EXAMPLE.COM", "USER10@EXAMPLE.COM", "hashed_password_10", null, false, "security_stamp_10", false, "user10@example.com" },
                    { "user2", 0, "concurrency_stamp_2", "user2@example.com", true, false, null, "USER2@EXAMPLE.COM", "USER2@EXAMPLE.COM", "your_hashed_password", null, false, "security_stamp_2", false, "user2@example.com" },
                    { "user3", 0, "concurrency_stamp_3", "user3@example.com", true, false, null, "USER3@EXAMPLE.COM", "USER3@EXAMPLE.COM", "hashed_password_3", null, false, "security_stamp_3", false, "user3@example.com" },
                    { "user4", 0, "concurrency_stamp_4", "user4@example.com", true, false, null, "USER4@EXAMPLE.COM", "USER4@EXAMPLE.COM", "hashed_password_4", null, false, "security_stamp_4", false, "user4@example.com" },
                    { "user5", 0, "concurrency_stamp_5", "user5@example.com", true, false, null, "USER5@EXAMPLE.COM", "USER5@EXAMPLE.COM", "hashed_password_5", null, false, "security_stamp_5", false, "user5@example.com" },
                    { "user6", 0, "concurrency_stamp_6", "user6@example.com", true, false, null, "USER6@EXAMPLE.COM", "USER6@EXAMPLE.COM", "hashed_password_6", null, false, "security_stamp_6", false, "user6@example.com" },
                    { "user7", 0, "concurrency_stamp_7", "user7@example.com", true, false, null, "USER7@EXAMPLE.COM", "USER7@EXAMPLE.COM", "hashed_password_7", null, false, "security_stamp_7", false, "user7@example.com" },
                    { "user8", 0, "concurrency_stamp_8", "user8@example.com", true, false, null, "USER8@EXAMPLE.COM", "USER8@EXAMPLE.COM", "hashed_password_8", null, false, "security_stamp_8", false, "user8@example.com" },
                    { "user9", 0, "concurrency_stamp_9", "user9@example.com", true, false, null, "USER9@EXAMPLE.COM", "USER9@EXAMPLE.COM", "hashed_password_9", null, false, "security_stamp_9", false, "user9@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "ID", "Continent", "Name" },
                values: new object[,]
                {
                    { 1, "Europe", "Denmark" },
                    { 2, "Europe", "United Kingdom" },
                    { 3, "North America", "Canada" },
                    { 4, "Australia", "Australia" },
                    { 5, "South America", "Brazil" },
                    { 6, "Europe", "Russia" },
                    { 7, "Asia", "India" },
                    { 8, "Africa", "South Africa" },
                    { 9, "South America", "Argentina" },
                    { 10, "Asia", "Japan" },
                    { 11, "Europe", "Germany" },
                    { 12, "Europe", "France" },
                    { 13, "North America", "Mexico" },
                    { 14, "Africa", "Egypt" },
                    { 15, "Europe", "Italy" },
                    { 16, "Asia", "Thailand" },
                    { 17, "Europe", "Greece" },
                    { 18, "Africa", "Nigeria" },
                    { 19, "Europe", "Sweden" },
                    { 20, "Asia", "China" }
                });

            migrationBuilder.InsertData(
                table: "OfficialTitle",
                columns: new[] { "ID", "FullTitle", "Title" },
                values: new object[,]
                {
                    { 1, "Champion of the Show", "Champion" },
                    { 2, "Grand Champion of the Show", "Grand Champion" },
                    { 3, "Best in Breed Award", "Best in Breed" },
                    { 4, "Best in Show Award", "Best in Show" },
                    { 5, "Reserve Champion of the Show", "Reserve Champion" },
                    { 6, "Best Puppy Award", "Best Puppy" },
                    { 7, "Best Veteran Award", "Best Veteran" }
                });

            migrationBuilder.InsertData(
                table: "Breeder",
                columns: new[] { "ID", "CountryID", "Name", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "John Smith", "user1" },
                    { 2, 2, "Alice Johnson", "user2" },
                    { 3, 3, "David Brown", "user3" }
                });

            migrationBuilder.InsertData(
                table: "Dog",
                columns: new[] { "ID", "BreederID", "Coat", "Color", "DOB", "Gender", "Height", "KennelID", "OwnerID", "PetName", "RegName", "RegNo", "Weight", "YearOfDeath" },
                values: new object[,]
                {
                    { 1, "user2", 0, "Brown", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 24.5m, null, "user2", "Pet1", "Dog1", "RegNo1", 55.2m, new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, 1, "White", new DateTime(2019, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 18.3m, "user1", "user1", "Buddy", "Dog2", "D67890", 42.7m, null },
                    { 3, "user3", 1, "Red", new DateTime(2014, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 18.3m, "user3", "user3", "Kiki", "Dog3", "D67893", 42.7m, null }
                });

            migrationBuilder.InsertData(
                table: "Kennel",
                columns: new[] { "ID", "About", "Address", "CountryID", "Mobile", "Name", "OwnerName", "Phone", "UserId", "WebsiteURL" },
                values: new object[,]
                {
                    { 1, "Kennel Description 1", "Kennel Address 1", 1, "987-654-3210", "Kennel1", "Owner1", "123-456-7890", null, "https://www.kennel1.com" },
                    { 2, "Kennel Description 2", "Kennel Address 2", 2, "876-543-2109", "Kennel2", "Owner2", "234-567-8901", null, "https://www.kennel2.com" }
                });

            migrationBuilder.InsertData(
                table: "ChampionshipTitle",
                columns: new[] { "ID", "DogID", "OfficialTitleID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "pedigree",
                columns: new[] { "ID", "DamID", "LitterID", "SireID" },
                values: new object[,]
                {
                    { 1, null, 2, null },
                    { 2, 3, 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user10");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user9");

            migrationBuilder.DeleteData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ChampionshipTitle",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChampionshipTitle",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfficialTitle",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfficialTitle",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OfficialTitle",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OfficialTitle",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OfficialTitle",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "pedigree",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "pedigree",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dog",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dog",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dog",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfficialTitle",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfficialTitle",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3");
        }
    }
}
