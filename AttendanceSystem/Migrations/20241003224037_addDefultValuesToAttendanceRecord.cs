using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class addDefultValuesToAttendanceRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
