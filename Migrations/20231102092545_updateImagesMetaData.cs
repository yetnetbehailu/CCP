using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class updateImagesMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesMetaData_Kennel_KennelId",
                table: "ImagesMetaData");

            migrationBuilder.DropIndex(
                name: "IX_ImagesMetaData_KennelId",
                table: "ImagesMetaData");

            migrationBuilder.AlterColumn<int>(
                name: "KennelId",
                table: "ImagesMetaData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesMetaData_KennelId",
                table: "ImagesMetaData",
                column: "KennelId",
                unique: true,
                filter: "[KennelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesMetaData_Kennel_KennelId",
                table: "ImagesMetaData",
                column: "KennelId",
                principalTable: "Kennel",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesMetaData_Kennel_KennelId",
                table: "ImagesMetaData");

            migrationBuilder.DropIndex(
                name: "IX_ImagesMetaData_KennelId",
                table: "ImagesMetaData");

            migrationBuilder.AlterColumn<int>(
                name: "KennelId",
                table: "ImagesMetaData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagesMetaData_KennelId",
                table: "ImagesMetaData",
                column: "KennelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesMetaData_Kennel_KennelId",
                table: "ImagesMetaData",
                column: "KennelId",
                principalTable: "Kennel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
