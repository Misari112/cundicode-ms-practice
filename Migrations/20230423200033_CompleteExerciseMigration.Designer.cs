﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ms_practice.Data;

#nullable disable

namespace ms_practice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230423200033_CompleteExerciseMigration")]
    partial class CompleteExerciseMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ms_practice.Models.CompleteProgrammingExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgrammingExerciseId")
                        .HasColumnType("int");

                    b.Property<string>("Script")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammingExerciseId");

                    b.ToTable("CompleteExercises");
                });

            modelBuilder.Entity("ms_practice.Models.ProgrammingExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DifficultyLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Examples")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FunctionSignature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hints")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemoryLimit")
                        .HasColumnType("int");

                    b.Property<string>("SolutionTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestCases")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TimeLimit")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visibility")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("ms_practice.Models.CompleteProgrammingExercise", b =>
                {
                    b.HasOne("ms_practice.Models.ProgrammingExercise", "ProgrammingExercise")
                        .WithMany()
                        .HasForeignKey("ProgrammingExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgrammingExercise");
                });
#pragma warning restore 612, 618
        }
    }
}
