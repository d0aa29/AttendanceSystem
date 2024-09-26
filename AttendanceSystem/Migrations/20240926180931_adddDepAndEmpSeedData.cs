using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class adddDepAndEmpSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Building A", "HR" },
                    { 2, "Building B", "IT" },
                    { 3, "Building C", "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "ImgUrl", "JoinedOn", "Name", "SSN", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Male", null, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", 123456789, null },
                    { 2, new DateTime(1992, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Female", null, new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith", 987654321, null },
                    { 3, new DateTime(1985, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Female", null, new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Johnson", 112233445, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
