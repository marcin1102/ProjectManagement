using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Migrations
{
    public partial class LabelsMovedToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Label_Name",
                schema: "project-management",
                table: "Label",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Label_ProjectId",
                schema: "project-management",
                table: "Label",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Project_ProjectId",
                schema: "project-management",
                table: "Label",
                column: "ProjectId",
                principalSchema: "project-management",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Project_ProjectId",
                schema: "project-management",
                table: "Label");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Label_Name",
                schema: "project-management",
                table: "Label");

            migrationBuilder.DropIndex(
                name: "IX_Label_ProjectId",
                schema: "project-management",
                table: "Label");
        }
    }
}
