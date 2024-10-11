using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateAttendanceRecordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "AttendanceRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c537f33-1289-461c-8269-60512da32c04", "AQAAAAIAAYagAAAAENmtWmLcQbZ/fBkAr3da6DwDSgep6ZCyuRfiYWevFSUGsuPuOPnXFjr6DZ1G2skWMg==", "e3276da3-501a-48d2-8ca1-cfef0a96897e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66cb55e0-016d-4656-8537-e1619a01fb65", "AQAAAAIAAYagAAAAELWaIR5NaVUCmkX2nasyLSRKQXKHaB5yYA+/ehMUw+EnnvWtoFfsoteXM/wkSoIgXg==", "b4d43612-2a33-4648-a8c1-af912fda03e2" });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecords_ShiftId",
                table: "AttendanceRecords",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_Shifts_ShiftId",
                table: "AttendanceRecords",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_Shifts_ShiftId",
                table: "AttendanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceRecords_ShiftId",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "AttendanceRecords");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31bcad75-18fa-42da-bbf1-4bc7db9b27a8", "AQAAAAIAAYagAAAAEMw2Guftk+5pe0A/puMbcg6bHFOiw8h1quIZ4xOBB/ypZW+tHMvgALsBe0TY13tJwQ==", "9fd72d50-6c1e-479d-9f69-50b45e5e4baf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f63b522-25cc-4e84-8df8-d5fb716f716c", "AQAAAAIAAYagAAAAENqjgqWOOA0rVJYSthr2DrF6Hh0bNU7pzOQXezZv7zBSxkq1xrw0GE8PTDHl590StQ==", "d355bddb-be8a-420a-ba6e-44fa903ec592" });
        }
    }
}
