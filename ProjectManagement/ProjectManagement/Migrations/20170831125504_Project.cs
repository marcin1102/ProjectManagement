using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "project-management");

            migrationBuilder.CreateTable(
                name: "EventEnvelope",
                schema: "project-management",
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
                name: "Project",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEnvelope",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "project-management");
        }
    }
}
