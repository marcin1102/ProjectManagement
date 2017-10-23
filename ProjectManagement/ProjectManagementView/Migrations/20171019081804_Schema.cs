using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagementView.Migrations
{
    public partial class Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Users",
                newSchema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Sprints",
                newSchema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Projects",
                newSchema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Labels",
                newSchema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Issues",
                newSchema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "EventEnvelope",
                newSchema: "project-management-views");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Sprints",
                schema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Projects",
                schema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Labels",
                schema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "Issues",
                schema: "project-management-views");

            migrationBuilder.RenameTable(
                name: "EventEnvelope",
                schema: "project-management-views");
        }
    }
}
