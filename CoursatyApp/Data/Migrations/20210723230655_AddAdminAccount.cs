using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

namespace CoursatyApp.Data.Migrations
{
    public partial class AddAdminAccount : Migration
    {
        const string Admin_User_GUID = "50007246-edd3-4e21-baf3-2d74e373dc38";
        const string Admin_Role_GUID = "437d00a1-92d6-452d-8f00-d36ca53a5b0c";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var passwordHash = hasher.HashPassword(null, "P@ssw0rd"); // Hide Password

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO AspNetUsers (Id,Username," +
                "NormalizedUserName,Email,EmailConfirmed,[PasswordHash]," +
                "[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled]," +
                "[LockoutEnabled],[AccessFailedCount],[Country]," +
                "[FirstName],[LasttName],[MobilePhone])");
            sb.AppendLine("VALUES (");
            sb.AppendLine($"'{Admin_User_GUID}'");
            sb.AppendLine(",'AyaOsama'");
            sb.AppendLine(",'AyaOsama@gmail.com'");
            sb.AppendLine(",'AyaOsama@gmail.com'");
            sb.AppendLine(",0");
            sb.AppendLine($",'{passwordHash}'");
            sb.AppendLine(",'01060000000'");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",0");
            sb.AppendLine(",'Aya'");
            sb.AppendLine(",'Osama'");
            sb.AppendLine(",'01060000000'");
            sb.AppendLine(")");
            migrationBuilder.Sql(sb.ToString());
            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id,Name,NormalizedName) VALUES ('{Admin_Role_GUID}','Admin','ADMIN')");
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles ([UserId],[RoleId]) VALUES ('{Admin_User_GUID}','{Admin_Role_GUID}')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE RoleId = {Admin_Role_GUID} AND UserId = {Admin_User_GUID}");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = {Admin_Role_GUID}");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = {Admin_User_GUID}");
        }
    }
}
