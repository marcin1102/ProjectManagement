using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Migrations
{
    public partial class CorrectedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                schema: "project-management",
                table: "Subtask");

            migrationBuilder.DropColumn(
                name: "NfrId",
                schema: "project-management",
                table: "Nfr");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "project-management",
                table: "Nfr");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "project-management",
                table: "IssuesLabels");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "project-management",
                table: "Bug");

            migrationBuilder.CreateIndex(
                name: "IX_Subtask_TaskId",
                schema: "project-management",
                table: "Subtask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_NfrId",
                schema: "project-management",
                table: "Bug",
                column: "NfrId");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_TaskId",
                schema: "project-management",
                table: "Bug",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_Nfr_NfrId",
                schema: "project-management",
                table: "Bug",
                column: "NfrId",
                principalSchema: "project-management",
                principalTable: "Nfr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_Task_TaskId",
                schema: "project-management",
                table: "Bug",
                column: "TaskId",
                principalSchema: "project-management",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subtask_Task_TaskId",
                schema: "project-management",
                table: "Subtask",
                column: "TaskId",
                principalSchema: "project-management",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bug_Nfr_NfrId",
                schema: "project-management",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_Bug_Task_TaskId",
                schema: "project-management",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_Subtask_Task_TaskId",
                schema: "project-management",
                table: "Subtask");

            migrationBuilder.DropIndex(
                name: "IX_Subtask_TaskId",
                schema: "project-management",
                table: "Subtask");

            migrationBuilder.DropIndex(
                name: "IX_Bug_NfrId",
                schema: "project-management",
                table: "Bug");

            migrationBuilder.DropIndex(
                name: "IX_Bug_TaskId",
                schema: "project-management",
                table: "Bug");

            migrationBuilder.AddColumn<long>(
                name: "Version",
                schema: "project-management",
                table: "Subtask",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "NfrId",
                schema: "project-management",
                table: "Nfr",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                schema: "project-management",
                table: "Nfr",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "project-management",
                table: "IssuesLabels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Version",
                schema: "project-management",
                table: "Bug",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
