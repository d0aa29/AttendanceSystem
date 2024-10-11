using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class addShiftsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fee98ec3-6407-482c-b736-18b22bf9694b", "AQAAAAIAAYagAAAAEMz6LBYgAEtuJ9JxXMvHlwP8Lovn9sMY+++HV4cZSz0p+53iTst/l0dml//K5IWLfQ==", "b40ba27d-97e7-4cf5-aeff-c4291b433d8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ace45fc-3db4-4f19-8df2-9d6e026dde4c", "AQAAAAIAAYagAAAAEB4SpkSGuT8f+ZpfDNPb8xfCrZTZnHuqW2QLd1MbfGsAPXz6L7WDeB2NWMEYBPSgGQ==", "3f971055-d79a-4717-8329-f5a57aad8f35" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
