using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class BreederSeedDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Breeder Address 1", "123-456-1234" });

            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Address", "Phone" },
                values: new object[] { "Breeder Address 2", "123-456-1235" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
