using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Migrations
{
    public partial class IssueAndRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Labels",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueSubtask",
                schema: "project-management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IssueId = table.Column<Guid>(nullable: false),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLabel",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "IssueSubtask",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Labels",
                schema: "project-management");

            migrationBuilder.DropTable(
                name: "Issue",
                schema: "project-management");
        }
    }
}
