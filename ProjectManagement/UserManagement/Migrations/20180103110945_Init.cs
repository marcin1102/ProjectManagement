using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UserManagement.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user-management");

            migrationBuilder.CreateTable(
                name: "EventEnvelope",
                schema: "user-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Delivered = table.Column<bool>(nullable: false),
                    DomainEvent = table.Column<string>(nullable: false),
                    DomainEventType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEnvelope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                schema: "user-management",
                columns: table => new
                {
                    Value = table.Column<string>(nullable: false),
                    LastlyUsed = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "user-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_Email", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEnvelope",
                schema: "user-management");

            migrationBuilder.DropTable(
                name: "Tokens",
                schema: "user-management");

            migrationBuilder.DropTable(
                name: "User",
                schema: "user-management");
        }
    }
}
