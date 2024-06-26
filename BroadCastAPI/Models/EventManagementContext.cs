﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BroadCastAPI.Models;

public partial class EventManagementContext : DbContext
{
    public EventManagementContext()
    {
    }

    public EventManagementContext(DbContextOptions<EventManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EventContent> EventContents { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=CEI2103\\SQLEXPRESS;Initial Catalog=EventManagement;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3214EC07C87BE600");

            entity.ToTable("Admin");

            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(250);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__72ABC2CC9E16B24D");

            entity.ToTable("Department");

            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("Department_Name");
        });

        modelBuilder.Entity<EventContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventCon__3214EC07949187DF");

            entity.ToTable("EventContent");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Particip__3214EC0732C8F66C");

            entity.Property(e => e.DateOfRegistration)
                .HasPrecision(6)
                .HasColumnName("Date_Of_Registration");
            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.Designation).HasMaxLength(20);
            entity.Property(e => e.ParticipantEmail)
                .HasMaxLength(250)
                .HasColumnName("Participant_Email");
            entity.Property(e => e.ParticipantName)
                .HasMaxLength(100)
                .HasColumnName("Participant_Name");

            entity.HasOne(d => d.Dept).WithMany(p => p.Participants)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Participant_Dept_Id");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__32D1F423A2D0D470");

            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.StaffEmail)
                .HasMaxLength(250)
                .HasColumnName("Staff_Email");
            entity.Property(e => e.StaffName)
                .HasMaxLength(100)
                .HasColumnName("Staff_name");
            entity.Property(e => e.SubId).HasColumnName("Sub_Id");

            entity.HasOne(d => d.Dept).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Staff_Dept_Id");

            entity.HasOne(d => d.Sub).WithMany(p => p.Staff)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Staff_Sub_Id");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StuId).HasName("PK__Student__DD8D49E1DF151293");

            entity.ToTable("Student");

            entity.Property(e => e.StuId).HasColumnName("Stu_Id");
            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.StuEmail)
                .HasMaxLength(250)
                .HasColumnName("Stu_Email");
            entity.Property(e => e.StudentName)
                .HasMaxLength(100)
                .HasColumnName("Student_name");

            entity.HasOne(d => d.Dept).WithMany(p => p.Students)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Dept_Id");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubId).HasName("PK__Subjects__DFB18CCD338F0A55");

            entity.Property(e => e.SubId).HasColumnName("Sub_Id");
            entity.Property(e => e.SubName)
                .HasMaxLength(100)
                .HasColumnName("Sub_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
