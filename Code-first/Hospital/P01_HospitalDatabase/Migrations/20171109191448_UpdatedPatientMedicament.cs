using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace P01_HospitalDatabase.Migrations
{
    public partial class UpdatedPatientMedicament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament");

            migrationBuilder.DropIndex(
                name: "IX_PatientMedicament_PatientId",
                table: "PatientMedicament");

            migrationBuilder.DropColumn(
                name: "PatientMedicamentId",
                table: "PatientMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament",
                columns: new[] { "PatientId", "MedicamentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament");

            migrationBuilder.AddColumn<int>(
                name: "PatientMedicamentId",
                table: "PatientMedicament",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament",
                column: "PatientMedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedicament_PatientId",
                table: "PatientMedicament",
                column: "PatientId");
        }
    }
}
