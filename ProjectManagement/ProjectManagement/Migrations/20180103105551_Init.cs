using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "project-management");

            migrationBuilder.CreateTable(
                name: "AggregateIssue",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_AggregateIssue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventEnvelope",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Delivered = table.Column<bool>(nullable: false),
                    DomainEvent = table.Column<string>(nullable: false),
                    DomainEventType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEnvelope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssuesLabels",
                schema: "project-management",
                columns: table => new
                {
                    IssueId = table.Column<Guid>(nullable: false),
                    LabelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuesLabels", x => new { x.IssueId, x.LabelId });
                });

            migrationBuilder.CreateTable(
                name: "Member",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    members = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sprint",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ReporterId = table.Column<Guid>(nullable: false),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    NfrId = table.Column<Guid>(nullable: true),
                    TaskId = table.Column<Guid>(nullable: true),
                    Subtask_TaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_AggregateIssue_NfrId",
                        column: x => x.NfrId,
                        principalSchema: "project-management",
                        principalTable: "AggregateIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_AggregateIssue_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "project-management",
                        principalTable: "AggregateIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_AggregateIssue_Subtask_TaskId",
                        column: x => x.Subtask_TaskId,
                        principalSchema: "project-management",
                        principalTable: "AggregateIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                    table.UniqueConstraint("AK_Label_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Label_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project-management",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AggregateIssueId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    IssueId = table.Column<Guid>(nullable: true),
                    MemberId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AggregateIssue_AggregateIssueId",
                        column: x => x.AggregateIssueId,
                        principalSchema: "project-management",
                        principalTable: "AggregateIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Issues_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "project-management",
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Issues_Subtask_TaskId",
                schema: "project-management",
                table: "Issues",
                column: "Subtask_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Label_ProjectId",
                schema: "project-management",
                table: "Label",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "EventEnvelope",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "IssuesLabels",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Label",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Member",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Sprint",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Issues",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "AggregateIssue",
                schema: "project-management");
        }
    }
}
