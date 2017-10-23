using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagementView.Migrations
{
    public partial class IgnoreComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sprints_SprintId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Subtasks");

            migrationBuilder.DropTable(
                name: "Nfrs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Issues");

            migrationBuilder.RenameColumn(
                name: "SprintId",
                table: "Issues",
                newName: "Task_SprintId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_SprintId",
                table: "Issues",
                newName: "IX_Issues_Task_SprintId");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Issues",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ReporterId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "comments",
                table: "Issues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "labels",
                table: "Issues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NfrId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SprintId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Nfr_ProjectId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Nfr_SprintId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Subtask_ProjectId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Subtask_SprintId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Subtask_TaskId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Task_ProjectId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Sprints",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sprints",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Sprints",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sprints",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "unfinishedIssues",
                table: "Sprints",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issues",
                table: "Issues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Labels_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReporterId",
                table: "Issues",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_NfrId",
                table: "Issues",
                column: "NfrId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_SprintId",
                table: "Issues",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_TaskId",
                table: "Issues",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Nfr_ProjectId",
                table: "Issues",
                column: "Nfr_ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Nfr_SprintId",
                table: "Issues",
                column: "Nfr_SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Subtask_ProjectId",
                table: "Issues",
                column: "Subtask_ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Subtask_SprintId",
                table: "Issues",
                column: "Subtask_SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Subtask_TaskId",
                table: "Issues",
                column: "Subtask_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Task_ProjectId",
                table: "Issues",
                column: "Task_ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_ProjectId",
                table: "Labels",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_ReporterId",
                table: "Issues",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Issues_NfrId",
                table: "Issues",
                column: "NfrId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_SprintId",
                table: "Issues",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Issues_TaskId",
                table: "Issues",
                column: "TaskId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Nfr_ProjectId",
                table: "Issues",
                column: "Nfr_ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Nfr_SprintId",
                table: "Issues",
                column: "Nfr_SprintId",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Subtask_ProjectId",
                table: "Issues",
                column: "Subtask_ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Subtask_SprintId",
                table: "Issues",
                column: "Subtask_SprintId",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Issues_Subtask_TaskId",
                table: "Issues",
                column: "Subtask_TaskId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Task_ProjectId",
                table: "Issues",
                column: "Task_ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Task_SprintId",
                table: "Issues",
                column: "Task_SprintId",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssigneeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_ReporterId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Issues_NfrId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_SprintId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Issues_TaskId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Nfr_ProjectId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Nfr_SprintId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Subtask_ProjectId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Subtask_SprintId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Issues_Subtask_TaskId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Task_ProjectId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Task_SprintId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issues",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ReporterId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_NfrId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_SprintId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_TaskId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Nfr_ProjectId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Nfr_SprintId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Subtask_ProjectId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Subtask_SprintId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Subtask_TaskId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Task_ProjectId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "unfinishedIssues",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "comments",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "labels",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "NfrId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "SprintId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Nfr_ProjectId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Nfr_SprintId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Subtask_ProjectId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Subtask_SprintId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Subtask_TaskId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Task_ProjectId",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "Issues",
                newName: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Task_SprintId",
                table: "Tasks",
                newName: "SprintId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Task_SprintId",
                table: "Tasks",
                newName: "IX_Tasks_SprintId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Nfrs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nfrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nfrs_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subtasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subtasks_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subtasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    NfrId = table.Column<Guid>(nullable: true),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bugs_Nfrs_NfrId",
                        column: x => x.NfrId,
                        principalTable: "Nfrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bugs_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_NfrId",
                table: "Bugs",
                column: "NfrId");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_SprintId",
                table: "Bugs",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_TaskId",
                table: "Bugs",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Nfrs_SprintId",
                table: "Nfrs",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Subtasks_SprintId",
                table: "Subtasks",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Subtasks_TaskId",
                table: "Subtasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sprints_SprintId",
                table: "Tasks",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
