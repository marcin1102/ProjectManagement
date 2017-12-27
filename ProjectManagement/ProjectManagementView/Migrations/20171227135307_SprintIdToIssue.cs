using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagementView.Migrations
{
    public partial class SprintIdToIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Nfr_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Subtask_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Task_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Nfr_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Subtask_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Nfr_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Subtask_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Task_SprintId",
                schema: "project-management-views",
                table: "Issues",
                newName: "SprintId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Task_SprintId",
                schema: "project-management-views",
                table: "Issues",
                newName: "IX_Issues_SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "SprintId",
                principalSchema: "project-management-views",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_SprintId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "SprintId",
                schema: "project-management-views",
                table: "Issues",
                newName: "Task_SprintId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_SprintId",
                schema: "project-management-views",
                table: "Issues",
                newName: "IX_Issues_Task_SprintId");

            migrationBuilder.AddColumn<Guid>(
                name: "SprintId",
                schema: "project-management-views",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Nfr_SprintId",
                schema: "project-management-views",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Subtask_SprintId",
                schema: "project-management-views",
                table: "Issues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Nfr_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "Nfr_SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Subtask_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "Subtask_SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "SprintId",
                principalSchema: "project-management-views",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Nfr_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "Nfr_SprintId",
                principalSchema: "project-management-views",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Subtask_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "Subtask_SprintId",
                principalSchema: "project-management-views",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Task_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "Task_SprintId",
                principalSchema: "project-management-views",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
