using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Migrations
{
    public partial class RemoveUserVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregateVersion",
                schema: "project-management",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                schema: "project-management",
                table: "User",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                schema: "project-management",
                table: "User",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "AggregateVersion",
                schema: "project-management",
                table: "User",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
