using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OfficialTitle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialTitle", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YearOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Coat = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OwnerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BreederID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    KennelID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dog_AspNetUsers_BreederID",
                        column: x => x.BreederID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dog_AspNetUsers_KennelID",
                        column: x => x.KennelID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dog_AspNetUsers_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Breeder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Breeder_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Breeder_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Kennel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kennel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kennel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kennel_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChampionshipTitle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DogID = table.Column<int>(type: "int", nullable: false),
                    OfficialTitleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionshipTitle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChampionshipTitle_Dog_DogID",
                        column: x => x.DogID,
                        principalTable: "Dog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChampionshipTitle_OfficialTitle_OfficialTitleID",
                        column: x => x.OfficialTitleID,
                        principalTable: "OfficialTitle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedigree",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SireID = table.Column<int>(type: "int", nullable: true),
                    DamID = table.Column<int>(type: "int", nullable: true),
                    LitterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedigree", x => x.ID);
                    table.ForeignKey(
                        name: "FK_pedigree_Dog_DamID",
                        column: x => x.DamID,
                        principalTable: "Dog",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_pedigree_Dog_LitterID",
                        column: x => x.LitterID,
                        principalTable: "Dog",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_pedigree_Dog_SireID",
                        column: x => x.SireID,
                        principalTable: "Dog",
                        principalColumn: "ID");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Breeder_CountryID",
                table: "Breeder",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Breeder_UserID",
                table: "Breeder",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChampionshipTitle_DogID",
                table: "ChampionshipTitle",
                column: "DogID");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionshipTitle_OfficialTitleID",
                table: "ChampionshipTitle",
                column: "OfficialTitleID");

            migrationBuilder.CreateIndex(
                name: "IX_Dog_BreederID",
                table: "Dog",
                column: "BreederID");

            migrationBuilder.CreateIndex(
                name: "IX_Dog_KennelID",
                table: "Dog",
                column: "KennelID");

            migrationBuilder.CreateIndex(
                name: "IX_Dog_OwnerID",
                table: "Dog",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Kennel_CountryID",
                table: "Kennel",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Kennel_UserId",
                table: "Kennel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_pedigree_DamID",
                table: "pedigree",
                column: "DamID");

            migrationBuilder.CreateIndex(
                name: "IX_pedigree_LitterID",
                table: "pedigree",
                column: "LitterID");

            migrationBuilder.CreateIndex(
                name: "IX_pedigree_SireID",
                table: "pedigree",
                column: "SireID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Breeder");

            migrationBuilder.DropTable(
                name: "ChampionshipTitle");

            migrationBuilder.DropTable(
                name: "Kennel");

            migrationBuilder.DropTable(
                name: "pedigree");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OfficialTitle");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Dog");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
