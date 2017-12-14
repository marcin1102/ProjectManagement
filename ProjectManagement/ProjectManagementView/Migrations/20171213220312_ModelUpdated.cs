using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagementView.Migrations
{
    public partial class ModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Nfr_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Subtask_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Task_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Nfr_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Subtask_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Nfr_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Subtask_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Task_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Task_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                newName: "IX_Issues_ProjectId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "project-management-views",
                table: "Issues",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "ProjectId",
                principalSchema: "project-management-views",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                schema: "project-management-views",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "project-management-views",
                table: "Issues",
                newName: "Task_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                newName: "IX_Issues_Task_ProjectId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Task_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                schema: "project-management-views",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Nfr_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Subtask_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Nfr_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "Nfr_ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Subtask_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "Subtask_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "ProjectId",
                principalSchema: "project-management-views",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Nfr_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "Nfr_ProjectId",
                principalSchema: "project-management-views",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Subtask_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "Subtask_ProjectId",
                principalSchema: "project-management-views",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Task_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "Task_ProjectId",
                principalSchema: "project-management-views",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
