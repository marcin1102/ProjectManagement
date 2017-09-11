﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectManagement;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Migrations
{
    [DbContext(typeof(ProjectManagementContext))]
    partial class ProjectManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("project-management")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Infrastructure.Message.EventEnvelope", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Delivered");

                    b.Property<string>("DomainEvent")
                        .IsRequired();

                    b.Property<string>("DomainEventType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("EventEnvelope");
                });

            modelBuilder.Entity("ProjectManagement.Issue.Model.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssigneeId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid>("ProjectId");

                    b.Property<Guid?>("ReporterId");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<long>("Version");

                    b.Property<string>("comments");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("ProjectManagement.IssueLabel.IssueLabel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IssueId");

                    b.Property<Guid>("LabelId");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("LabelId");

                    b.ToTable("IssueLabel");
                });

            modelBuilder.Entity("ProjectManagement.IssueSubtasks.IssueSubtask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IssueId");

                    b.Property<Guid>("SubtaskId");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueSubtask");
                });

            modelBuilder.Entity("ProjectManagement.Label.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid>("ProjectId");

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("ProjectManagement.Project.Model.Project", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectManagement.ProjectUser.ProjectUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("ProjectManagement.User.Model.User", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<long>("AggregateVersion");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Role")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ProjectManagement.Issue.Model.Issue", b =>
                {
                    b.HasOne("ProjectManagement.User.Model.User", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("ProjectManagement.User.Model.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId");
                });

            modelBuilder.Entity("ProjectManagement.IssueLabel.IssueLabel", b =>
                {
                    b.HasOne("ProjectManagement.Issue.Model.Issue")
                        .WithMany("Labels")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagement.Label.Label")
                        .WithMany("Issues")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagement.IssueSubtasks.IssueSubtask", b =>
                {
                    b.HasOne("ProjectManagement.Issue.Model.Issue")
                        .WithMany("Subtasks")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagement.ProjectUser.ProjectUser", b =>
                {
                    b.HasOne("ProjectManagement.Project.Model.Project")
                        .WithMany("Members")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagement.User.Model.User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
