using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "31bcad75-18fa-42da-bbf1-4bc7db9b27a8", "admin@system.com", true, false, null, "ADMIN@SYSTEM.COM", "ADMIN@SYSTEM.COM", "AQAAAAIAAYagAAAAEMw2Guftk+5pe0A/puMbcg6bHFOiw8h1quIZ4xOBB/ypZW+tHMvgALsBe0TY13tJwQ==", null, false, "9fd72d50-6c1e-479d-9f69-50b45e5e4baf", false, "admin@system.com" },
                    { "2", 0, "1f63b522-25cc-4e84-8df8-d5fb716f716c", "jane@system.com", true, false, null, "JANE@SYSTEM.COM", "JANE@SYSTEM.COM", "AQAAAAIAAYagAAAAENqjgqWOOA0rVJYSthr2DrF6Hh0bNU7pzOQXezZv7zBSxkq1xrw0GE8PTDHl590StQ==", null, false, "d355bddb-be8a-420a-ba6e-44fa903ec592", false, "jane@system.com" }
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "2");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);
        }
    }
}
