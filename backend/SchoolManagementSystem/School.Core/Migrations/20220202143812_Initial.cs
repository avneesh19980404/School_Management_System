using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("71b7cd13-4f32-413a-b8c8-75d3b8aaf103"), new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(1855), "Admin", new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(2615) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("8c4039ff-6fcd-467d-98ac-36f47078d872"), new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3317), "Partner", new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3338) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("9f643888-a178-4ce0-b3e7-2b1a68abdfcd"), new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3362), "Client", new DateTime(2022, 2, 2, 14, 38, 11, 889, DateTimeKind.Utc).AddTicks(3417) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "Password", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { new Guid("5ba72018-f382-41a2-aaa3-fc48d8a854f3"), new DateTime(2022, 2, 2, 14, 38, 11, 917, DateTimeKind.Utc).AddTicks(6530), "admin@gmail.com", "", true, false, "", "BJJcVkTHd9Qkv8iCM8srzsyU50CjkRe4ckHbWkHVlAc=", new Guid("71b7cd13-4f32-413a-b8c8-75d3b8aaf103"), new DateTime(2022, 2, 2, 14, 38, 11, 917, DateTimeKind.Utc).AddTicks(6576), "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
