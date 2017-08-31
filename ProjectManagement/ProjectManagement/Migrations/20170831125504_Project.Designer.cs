using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectManagement;

namespace ProjectManagement.Migrations
{
    [DbContext(typeof(ProjectManagementContext))]
    [Migration("20170831125504_Project")]
    partial class Project
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });
        }
    }
}
