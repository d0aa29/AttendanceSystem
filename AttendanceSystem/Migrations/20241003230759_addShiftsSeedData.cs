using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class addShiftsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeShift_Employees_EmployeesId",
                table: "EmployeeShift");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeShift_Shifts_ShiftsId",
                table: "EmployeeShift");

            migrationBuilder.RenameColumn(
                name: "ShiftsId",
                table: "EmployeeShift",
                newName: "ShiftId");

            migrationBuilder.RenameColumn(
                name: "EmployeesId",
                table: "EmployeeShift",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeShift_ShiftsId",
                table: "EmployeeShift",
                newName: "IX_EmployeeShift_ShiftId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7914f492-16ea-4b27-acce-8541730b523b", "AQAAAAIAAYagAAAAEIm1vChm31pTRCLRxJ56NiK91ZOtmiHx8Ugss2REvVFbKL/sBbhyWfcLCiKwnFfyPw==", "3eae3924-4d7b-481f-9cfb-4e65890ef51c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b42b60f9-7d45-4469-a5f4-79f8d822815d", "AQAAAAIAAYagAAAAEEEg2+qCBipSPnnhF2C/Le84xEHDxZE59egzPWrG9Q8mGZXcj800zb41FqOZZG35CQ==", "a83aae37-c97d-4659-ae2c-fda77cfc0c38" });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "From", "Num", "To" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 8, 0, 0, 0), 1, new TimeSpan(0, 16, 0, 0, 0) },
                    { 2, new TimeSpan(0, 16, 0, 0, 0), 2, new TimeSpan(0, 0, 0, 0, 0) },
                    { 3, new TimeSpan(0, 0, 0, 0, 0), 3, new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "EmployeeShift",
                columns: new[] { "EmployeeId", "ShiftId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeShift_Employees_EmployeeId",
                table: "EmployeeShift",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeShift_Shifts_ShiftId",
                table: "EmployeeShift",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeShift_Employees_EmployeeId",
                table: "EmployeeShift");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeShift_Shifts_ShiftId",
                table: "EmployeeShift");

            migrationBuilder.DeleteData(
                table: "EmployeeShift",
                keyColumns: new[] { "EmployeeId", "ShiftId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeShift",
                keyColumns: new[] { "EmployeeId", "ShiftId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EmployeeShift",
                keyColumns: new[] { "EmployeeId", "ShiftId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "ShiftId",
                table: "EmployeeShift",
                newName: "ShiftsId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeeShift",
                newName: "EmployeesId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeShift_ShiftId",
                table: "EmployeeShift",
                newName: "IX_EmployeeShift_ShiftsId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6bb55f1-acb5-4d79-9039-8659c88dd93b", "AQAAAAIAAYagAAAAEIwv0H+DTenbDr+d/kX3TBTXNIcuyhgC5MckE4F1dfQO5xqBuKm2X14YYZaZz8kpMw==", "4941f559-289f-4e15-b5b4-67ee27dfe982" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e098395-d721-48cc-a900-2da9f78d6d24", "AQAAAAIAAYagAAAAEAlODFoZtM9PwjeBOVhwOwwQjJwFAgc4/viQs2MYmreudW/oeCb7Ct1V0mjU9/b8Yw==", "9521ce8f-28cb-4c4f-90b5-16f123dc2a1e" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeShift_Employees_EmployeesId",
                table: "EmployeeShift",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeShift_Shifts_ShiftsId",
                table: "EmployeeShift",
                column: "ShiftsId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
