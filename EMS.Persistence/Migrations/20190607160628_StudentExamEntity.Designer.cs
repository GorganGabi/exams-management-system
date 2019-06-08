﻿// <auto-generated />
using System;
using EMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EMS.Persistence.Migrations
{
    [DbContext(typeof(EMSContext))]
    [Migration("20190607160628_StudentExamEntity")]
    partial class StudentExamEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EMS.Domain.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ProfessorId");

                    b.Property<int>("Semester");

                    b.Property<int>("StudentYear");

                    b.Property<string>("Title");

                    b.Property<string>("UniversityYear");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EMS.Domain.Entities.Professor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("EMS.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FatherInitial");

                    b.Property<string>("Group");

                    b.Property<string>("Name");

                    b.Property<string>("RegistrationNumber");

                    b.Property<Guid>("UserId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EMS.Domain.Entities.StudentCourse", b =>
                {
                    b.Property<Guid>("StudentId");

                    b.Property<Guid>("CourseId");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("EMS.Domain.Entities.StudentExam", b =>
                {
                    b.Property<Guid>("StudentId");

                    b.Property<Guid>("ExamId");

                    b.Property<string>("Checked");

                    b.Property<Guid>("Id");

                    b.HasKey("StudentId", "ExamId");

                    b.HasIndex("ExamId");

                    b.ToTable("StudentExam");
                });

            modelBuilder.Entity("EMS.Domain.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CourseId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Room");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("EMS.Domain.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ExamId");

                    b.Property<bool>("IsConfirmed");

                    b.Property<Guid>("StudentId");

                    b.Property<float>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("EMS.Domain.Course", b =>
                {
                    b.HasOne("EMS.Domain.Entities.Professor", "Professor")
                        .WithOne("Course")
                        .HasForeignKey("EMS.Domain.Course", "ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Entities.StudentCourse", b =>
                {
                    b.HasOne("EMS.Domain.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Entities.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Entities.StudentExam", b =>
                {
                    b.HasOne("EMS.Domain.Exam", "Exam")
                        .WithMany("StudentExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Entities.Student", "Student")
                        .WithMany("StudentExams")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Exam", b =>
                {
                    b.HasOne("EMS.Domain.Course", "Course")
                        .WithMany("Exams")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Grade", b =>
                {
                    b.HasOne("EMS.Domain.Exam", "Exam")
                        .WithMany("Grades")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
