using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectManagementView.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "project-management-views");

            migrationBuilder.CreateTable(
                name: "EventEnvelope",
                schema: "project-management-views",
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
                name: "Projects",
                schema: "project-management-views",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                schema: "project-management-views",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Labels_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project-management-views",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                schema: "project-management-views",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    unfinishedIssues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprints_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project-management-views",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "project-management-views",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project-management-views",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                schema: "project-management-views",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ReporterId = table.Column<Guid>(nullable: true),
                    SprintId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    comments = table.Column<string>(nullable: true),
                    labels = table.Column<string>(nullable: true),
                    NfrId = table.Column<Guid>(nullable: true),
                    TaskId = table.Column<Guid>(nullable: true),
                    Subtask_TaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalSchema: "project-management-views",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalSchema: "project-management-views",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Issues_NfrId",
                        column: x => x.NfrId,
                        principalSchema: "project-management-views",
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project-management-views",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalSchema: "project-management-views",
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Issues_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "project-management-views",
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Issues_Subtask_TaskId",
                        column: x => x.Subtask_TaskId,
                        principalSchema: "project-management-views",
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                schema: "project-management-views",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReporterId",
                schema: "project-management-views",
                table: "Issues",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_NfrId",
                schema: "project-management-views",
                table: "Issues",
                column: "NfrId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                schema: "project-management-views",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_SprintId",
                schema: "project-management-views",
                table: "Issues",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_TaskId",
                schema: "project-management-views",
                table: "Issues",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Subtask_TaskId",
                schema: "project-management-views",
                table: "Issues",
                column: "Subtask_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_ProjectId",
                schema: "project-management-views",
                table: "Labels",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectId",
                schema: "project-management-views",
                table: "Sprints",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectId",
                schema: "project-management-views",
                table: "Users",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEnvelope",
                schema: "project-management-views");

            migrationBuilder.DropTable(
                name: "Issues",
                schema: "project-management-views");

            migrationBuilder.DropTable(
                name: "Labels",
                schema: "project-management-views");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "project-management-views");

            migrationBuilder.DropTable(
                name: "Sprints",
                schema: "project-management-views");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "project-management-views");
        }
    }
}
