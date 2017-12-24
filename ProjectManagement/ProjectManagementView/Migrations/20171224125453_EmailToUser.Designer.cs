﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagementView;
using System;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagementView.Migrations
{
    [DbContext(typeof(ProjectManagementViewContext))]
    [Migration("20171224125453_EmailToUser")]
    partial class EmailToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("project-management-views")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("ProjectManagement.Infrastructure.Message.EventEnvelope", b =>
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

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Abstract.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssigneeId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("ProjectId");

                    b.Property<Guid?>("ReporterId");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<long>("Version");

                    b.Property<string>("comments");

                    b.Property<string>("labels");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Issues");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Issue");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ProjectId");

                    b.Property<DateTime>("Start");

                    b.Property<int>("Status");

                    b.Property<long>("Version");

                    b.Property<string>("unfinishedIssues");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<Guid?>("ProjectId");

                    b.Property<int>("Role");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Bug", b =>
                {
                    b.HasBaseType("ProjectManagementView.Storage.Models.Abstract.Issue");

                    b.Property<Guid?>("NfrId");

                    b.Property<Guid?>("SprintId");

                    b.Property<Guid?>("TaskId");

                    b.HasIndex("NfrId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.HasIndex("TaskId");

                    b.ToTable("Bug");

                    b.HasDiscriminator().HasValue("Bug");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Nfr", b =>
                {
                    b.HasBaseType("ProjectManagementView.Storage.Models.Abstract.Issue");

                    b.Property<Guid?>("SprintId")
                        .HasColumnName("Nfr_SprintId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.ToTable("Nfr");

                    b.HasDiscriminator().HasValue("Nfr");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Subtask", b =>
                {
                    b.HasBaseType("ProjectManagementView.Storage.Models.Abstract.Issue");

                    b.Property<Guid?>("SprintId")
                        .HasColumnName("Subtask_SprintId");

                    b.Property<Guid?>("TaskId")
                        .HasColumnName("Subtask_TaskId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.HasIndex("TaskId");

                    b.ToTable("Subtask");

                    b.HasDiscriminator().HasValue("Subtask");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Task", b =>
                {
                    b.HasBaseType("ProjectManagementView.Storage.Models.Abstract.Issue");

                    b.Property<Guid?>("SprintId")
                        .HasColumnName("Task_SprintId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.ToTable("Task");

                    b.HasDiscriminator().HasValue("Task");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Abstract.Issue", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.User", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("ProjectManagementView.Storage.Models.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Label", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Labels")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Sprint", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.User", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Bug", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Nfr")
                        .WithMany("Bugs")
                        .HasForeignKey("NfrId");

                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Bugs")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Bugs")
                        .HasForeignKey("SprintId");

                    b.HasOne("ProjectManagementView.Storage.Models.Task")
                        .WithMany("Bugs")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Nfr", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Nfrs")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Nfrs")
                        .HasForeignKey("SprintId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Subtask", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Subtasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Subtasks")
                        .HasForeignKey("SprintId");

                    b.HasOne("ProjectManagementView.Storage.Models.Task")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Task", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Tasks")
                        .HasForeignKey("SprintId");
                });
#pragma warning restore 612, 618
        }
    }
}
