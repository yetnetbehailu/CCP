using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Migrations
{
    /// <inheritdoc />
    public partial class ProfileImagePropertiesToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KennelOwnerProfileImagePath",
                table: "Kennel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BreederProfileImagePath",
                table: "Dog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KennelOwnerProfileImagePath",
                table: "Dog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerProfileImagePath",
                table: "Dog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BreederProfileImagePath",
                table: "Breeder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 1,
                column: "BreederProfileImagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 2,
                column: "BreederProfileImagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Breeder",
                keyColumn: "ID",
                keyValue: 3,
                column: "BreederProfileImagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Dog",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "BreederProfileImagePath", "KennelOwnerProfileImagePath", "OwnerProfileImagePath" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Dog",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "BreederProfileImagePath", "KennelOwnerProfileImagePath", "OwnerProfileImagePath" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Dog",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "BreederProfileImagePath", "KennelOwnerProfileImagePath", "OwnerProfileImagePath" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 1,
                column: "KennelOwnerProfileImagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Kennel",
                keyColumn: "ID",
                keyValue: 2,
                column: "KennelOwnerProfileImagePath",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KennelOwnerProfileImagePath",
                table: "Kennel");

            migrationBuilder.DropColumn(
                name: "BreederProfileImagePath",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "KennelOwnerProfileImagePath",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "OwnerProfileImagePath",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "BreederProfileImagePath",
                table: "Breeder");
        }
    }
}
