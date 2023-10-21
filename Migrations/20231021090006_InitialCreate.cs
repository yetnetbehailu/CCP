using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Breeder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Breeder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Address", "Phone" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Address", "Phone" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Address", "Phone" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Breeder");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Breeder");
        }
    }
}
