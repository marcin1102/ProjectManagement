using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Migrations
{
    public partial class CommandModelCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLabel",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "IssueSubtask",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "ProjectUser",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Issue",
                schema: "project-management");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                schema: "project-management",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "unfinishedIssues",
                schema: "project-management",
                table: "Sprint");

            migrationBuilder.RenameTable(
                name: "Labels",
                schema: "project-management",
                newName: "Label");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "project-management",
                table: "Label",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Label",
                schema: "project-management",
                table: "Label",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Bug",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    NfrId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "int4", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Version = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bug", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssuesLabels",
                schema: "project-management",
                columns: table => new
                {
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuesLabels", x => new { x.IssueId, x.LabelId });
                });

            migrationBuilder.CreateTable(
                name: "Nfr",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    NfrId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "int4", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Version = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nfr", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subtask",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "int4", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Version = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "int4", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Version = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BugId = table.Column<Guid>(type: "uuid", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    NfrId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubtaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Bug_BugId",
                        column: x => x.BugId,
                        principalSchema: "project-management",
                        principalTable: "Bug",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Nfr_NfrId",
                        column: x => x.NfrId,
                        principalSchema: "project-management",
                        principalTable: "Nfr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Subtask_SubtaskId",
                        column: x => x.SubtaskId,
                        principalSchema: "project-management",
                        principalTable: "Subtask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Task_TaskId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "IssuesLabels",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Bug",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Nfr",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Subtask",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "project-management");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Label",
                schema: "project-management",
                table: "Label");

            migrationBuilder.RenameTable(
                name: "Label",
                schema: "project-management",
                newName: "Labels");

            migrationBuilder.AddColumn<string>(
                name: "unfinishedIssues",
                schema: "project-management",
                table: "Sprint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "project-management",
                table: "Labels",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                schema: "project-management",
                table: "Labels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Issue",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ReporterId = table.Column<Guid>(nullable: true),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issue_User_AssigneeId",
                        column: x => x.AssigneeId,
                        principalSchema: "project-management",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issue_User_ReporterId",
                        column: x => x.ReporterId,
                        principalSchema: "project-management",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project-management",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "project-management",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueLabel",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IssueId = table.Column<Guid>(nullable: false),
                    LabelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueLabel_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "project-management",
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLabel_Labels_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "project-management",
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueSubtask",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IssueId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    SubtaskId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueSubtask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueSubtask_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "project-management",
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AssigneeId",
                schema: "project-management",
                table: "Issue",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ReporterId",
                schema: "project-management",
                table: "Issue",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabel_IssueId",
                schema: "project-management",
                table: "IssueLabel",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabel_LabelId",
                schema: "project-management",
                table: "IssueLabel",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSubtask_IssueId",
                schema: "project-management",
                table: "IssueSubtask",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_ProjectId",
                schema: "project-management",
                table: "ProjectUser",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UserId",
                schema: "project-management",
                table: "ProjectUser",
                column: "UserId");
        }
    }
}
