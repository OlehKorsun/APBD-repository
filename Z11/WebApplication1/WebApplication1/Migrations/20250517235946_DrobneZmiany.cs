using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class DrobneZmiany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriptionIdPrescription",
                table: "Medicament",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1,
                column: "PrescriptionIdPrescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2,
                column: "PrescriptionIdPrescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 3,
                column: "PrescriptionIdPrescription",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Medicament_PrescriptionIdPrescription",
                table: "Medicament",
                column: "PrescriptionIdPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicament_Prescriptions_PrescriptionIdPrescription",
                table: "Medicament",
                column: "PrescriptionIdPrescription",
                principalTable: "Prescriptions",
                principalColumn: "IdPrescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicament_Prescriptions_PrescriptionIdPrescription",
                table: "Medicament");

            migrationBuilder.DropIndex(
                name: "IX_Medicament_PrescriptionIdPrescription",
                table: "Medicament");

            migrationBuilder.DropColumn(
                name: "PrescriptionIdPrescription",
                table: "Medicament");
        }
    }
}
