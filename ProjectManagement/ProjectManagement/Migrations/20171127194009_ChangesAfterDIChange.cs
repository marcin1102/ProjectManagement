using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Migrations
{
    public partial class ChangesAfterDIChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Bug_BugId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Nfr_NfrId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Subtask_SubtaskId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Task_TaskId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Subtask_Task_TaskId",
                schema: "project-management",
                table: "Subtask");

            migrationBuilder.DropTable(
                name: "Bug",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Nfr",
                schema: "project-management");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                schema: "project-management",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtask",
                schema: "project-management",
                table: "Subtask");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BugId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_NfrId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SubtaskId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TaskId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BugId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "NfrId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SubtaskId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Task",
                schema: "project-management",
                newName: "AggregateIssue");

            migrationBuilder.RenameTable(
                name: "Subtask",
                schema: "project-management",
                newName: "Issues");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                schema: "project-management",
                table: "Issues",
                newName: "Subtask_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Subtask_TaskId",
                schema: "project-management",
                table: "Issues",
                newName: "IX_Issues_Subtask_TaskId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "project-management",
                table: "AggregateIssue",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "project-management",
                table: "Issues",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "NfrId",
                schema: "project-management",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                schema: "project-management",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AggregateIssueId",
                schema: "project-management",
                table: "Comments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IssueId",
                schema: "project-management",
                table: "Comments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AggregateIssue",
                schema: "project-management",
                table: "AggregateIssue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issues",
                schema: "project-management",
                table: "Issues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_NfrId",
                schema: "project-management",
                table: "Issues",
                column: "NfrId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_TaskId",
                schema: "project-management",
                table: "Issues",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AggregateIssueId",
                schema: "project-management",
                table: "Comments",
                column: "AggregateIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IssueId",
                schema: "project-management",
                table: "Comments",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AggregateIssue_AggregateIssueId",
                schema: "project-management",
                table: "Comments",
                column: "AggregateIssueId",
                principalSchema: "project-management",
                principalTable: "AggregateIssue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Issues_IssueId",
                schema: "project-management",
                table: "Comments",
                column: "IssueId",
                principalSchema: "project-management",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AggregateIssue_NfrId",
                schema: "project-management",
                table: "Issues",
                column: "NfrId",
                principalSchema: "project-management",
                principalTable: "AggregateIssue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AggregateIssue_TaskId",
                schema: "project-management",
                table: "Issues",
                column: "TaskId",
                principalSchema: "project-management",
                principalTable: "AggregateIssue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AggregateIssue_Subtask_TaskId",
                schema: "project-management",
                table: "Issues",
                column: "Subtask_TaskId",
                principalSchema: "project-management",
                principalTable: "AggregateIssue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AggregateIssue_AggregateIssueId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Issues_IssueId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AggregateIssue_NfrId",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AggregateIssue_TaskId",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AggregateIssue_Subtask_TaskId",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issues",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_NfrId",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_TaskId",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AggregateIssueId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IssueId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AggregateIssue",
                schema: "project-management",
                table: "AggregateIssue");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "NfrId",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "project-management",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AggregateIssueId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IssueId",
                schema: "project-management",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "project-management",
                table: "AggregateIssue");

            migrationBuilder.RenameTable(
                name: "Issues",
                schema: "project-management",
                newName: "Subtask");

            migrationBuilder.RenameTable(
                name: "AggregateIssue",
                schema: "project-management",
                newName: "Task");

            migrationBuilder.RenameColumn(
                name: "Subtask_TaskId",
                schema: "project-management",
                table: "Subtask",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Subtask_TaskId",
                schema: "project-management",
                table: "Subtask",
                newName: "IX_Subtask_TaskId");

            migrationBuilder.AddColumn<Guid>(
                name: "BugId",
                schema: "project-management",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NfrId",
                schema: "project-management",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubtaskId",
                schema: "project-management",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                schema: "project-management",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtask",
                schema: "project-management",
                table: "Subtask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                schema: "project-management",
                table: "Task",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Nfr",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ReporterId = table.Column<Guid>(nullable: false),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nfr", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bug",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    NfrId = table.Column<Guid>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ReporterId = table.Column<Guid>(nullable: false),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bug", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bug_Nfr_NfrId",
                        column: x => x.NfrId,
                        principalSchema: "project-management",
                        principalTable: "Nfr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bug_Task_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "project-management",
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BugId",
                schema: "project-management",
                table: "Comments",
                column: "BugId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_NfrId",
                schema: "project-management",
                table: "Comments",
                column: "NfrId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SubtaskId",
                schema: "project-management",
                table: "Comments",
                column: "SubtaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TaskId",
                schema: "project-management",
                table: "Comments",
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
                name: "FK_Comments_Bug_BugId",
                schema: "project-management",
                table: "Comments",
                column: "BugId",
                principalSchema: "project-management",
                principalTable: "Bug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Nfr_NfrId",
                schema: "project-management",
                table: "Comments",
                column: "NfrId",
                principalSchema: "project-management",
                principalTable: "Nfr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Subtask_SubtaskId",
                schema: "project-management",
                table: "Comments",
                column: "SubtaskId",
                principalSchema: "project-management",
                principalTable: "Subtask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Task_TaskId",
                schema: "project-management",
                table: "Comments",
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
    }
}
