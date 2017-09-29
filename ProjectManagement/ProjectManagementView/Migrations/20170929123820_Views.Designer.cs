﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagementView;
using System;

namespace ProjectManagementView.Migrations
{
    [DbContext(typeof(ProjectManagementViewContext))]
    [Migration("20170929123820_Views")]
    partial class Views
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

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

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Bug", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<Guid?>("NfrId");

                    b.Property<Guid?>("SprintId");

                    b.Property<int>("Status");

                    b.Property<Guid?>("TaskId");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("NfrId");

                    b.HasIndex("SprintId");

                    b.HasIndex("TaskId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Nfr", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<Guid?>("SprintId");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.ToTable("Nfrs");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProjectId");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Subtask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<Guid?>("SprintId");

                    b.Property<int>("Status");

                    b.Property<Guid?>("TaskId");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("TaskId");

                    b.ToTable("Subtasks");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<Guid?>("SprintId");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<Guid?>("ProjectId");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Bug", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Nfr")
                        .WithMany("Bugs")
                        .HasForeignKey("NfrId");

                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Bugs")
                        .HasForeignKey("SprintId");

                    b.HasOne("ProjectManagementView.Storage.Models.Task")
                        .WithMany("Bugs")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Nfr", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Nfrs")
                        .HasForeignKey("SprintId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Sprint", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Subtask", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Subtasks")
                        .HasForeignKey("SprintId");

                    b.HasOne("ProjectManagementView.Storage.Models.Task")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.Task", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Sprint")
                        .WithMany("Tasks")
                        .HasForeignKey("SprintId");
                });

            modelBuilder.Entity("ProjectManagementView.Storage.Models.User", b =>
                {
                    b.HasOne("ProjectManagementView.Storage.Models.Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
