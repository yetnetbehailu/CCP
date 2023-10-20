using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class addImagesMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImagesMetaData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KennelId = table.Column<int>(type: "int", nullable: false),
                    DogID = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesMetaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesMetaData_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImagesMetaData_Dog_DogID",
                        column: x => x.DogID,
                        principalTable: "Dog",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ImagesMetaData_Kennel_KennelId",
                        column: x => x.KennelId,
                        principalTable: "Kennel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagesMetaData_DogID",
                table: "ImagesMetaData",
                column: "DogID");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesMetaData_KennelId",
                table: "ImagesMetaData",
                column: "KennelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagesMetaData_UserId",
                table: "ImagesMetaData",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesMetaData");
        }
    }
}
