using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class changeKennelListToKennel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kennel_AspNetUsers_UserId",
                table: "Kennel");

            migrationBuilder.DropIndex(
                name: "IX_Kennel_UserId",
                table: "Kennel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Kennel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kennel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 1,
                column: "UserId",
                value: "User1");

            migrationBuilder.UpdateData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 2,
                column: "UserId",
                value: "User2");

            migrationBuilder.CreateIndex(
                name: "IX_Kennel_Name",
                table: "Kennel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kennel_UserId",
                table: "Kennel",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kennel_AspNetUsers_UserId",
                table: "Kennel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kennel_AspNetUsers_UserId",
                table: "Kennel");

            migrationBuilder.DropIndex(
                name: "IX_Kennel_Name",
                table: "Kennel");

            migrationBuilder.DropIndex(
                name: "IX_Kennel_UserId",
                table: "Kennel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Kennel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kennel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Kennel_UserId",
                table: "Kennel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kennel_AspNetUsers_UserId",
                table: "Kennel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
