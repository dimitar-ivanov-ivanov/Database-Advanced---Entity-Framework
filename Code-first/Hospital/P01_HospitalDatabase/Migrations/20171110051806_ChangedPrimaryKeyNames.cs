using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace P01_HospitalDatabase.Migrations
{
    public partial class ChangedPrimaryKeyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisitationId",
                table: "Visitations",
                newName: "VisitationID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "MedicamentId",
                table: "Medicaments",
                newName: "MedicamentID");

            migrationBuilder.RenameColumn(
                name: "DiagnoseId",
                table: "Diagnoses",
                newName: "DiagnoseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisitationID",
                table: "Visitations",
                newName: "VisitationId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "MedicamentID",
                table: "Medicaments",
                newName: "MedicamentId");

            migrationBuilder.RenameColumn(
                name: "DiagnoseID",
                table: "Diagnoses",
                newName: "DiagnoseId");
        }
    }
}
