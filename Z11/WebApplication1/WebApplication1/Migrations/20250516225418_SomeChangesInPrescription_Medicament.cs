using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class SomeChangesInPrescription_Medicament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicaments_Medicament_IdPrescription",
                table: "Prescription_Medicaments");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicaments_Medicament_IdMedicament",
                table: "Prescription_Medicaments",
                column: "IdMedicament",
                principalTable: "Medicament",
                principalColumn: "IdMedicament",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicaments_Medicament_IdMedicament",
                table: "Prescription_Medicaments");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicaments_Medicament_IdPrescription",
                table: "Prescription_Medicaments",
                column: "IdPrescription",
                principalTable: "Medicament",
                principalColumn: "IdMedicament",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
