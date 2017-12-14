using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UserManagement.Migrations
{
    public partial class UserModelCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "user-management",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                schema: "user-management",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                schema: "user-management",
                table: "User",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
