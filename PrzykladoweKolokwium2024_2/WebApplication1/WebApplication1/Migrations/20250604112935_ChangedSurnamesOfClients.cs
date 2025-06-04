using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSurnamesOfClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastName",
                value: "Doe1");

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 2,
                column: "LastName",
                value: "Doe2");

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 3,
                column: "LastName",
                value: "Doe3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastName",
                value: "Doe");

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 2,
                column: "LastName",
                value: "Doe");

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 3,
                column: "LastName",
                value: "Doe");
        }
    }
}
