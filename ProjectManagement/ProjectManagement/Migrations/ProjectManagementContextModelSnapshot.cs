﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectManagement;

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

            modelBuilder.Entity("ProjectManagement.Project.Model.Project", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.ToTable("Project");
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
        }
    }
}
