﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManagerAPI;

#nullable disable

namespace TaskManagerAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240319112458_UpdatedUsers")]
    partial class UpdatedUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskManagerAPI.Entities.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CommentId");

                    b.HasIndex("IssueId");

                    b.HasIndex("UserLogin");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TaskManagerAPI.Entities.Issue", b =>
                {
                    b.Property<Guid>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IssueId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserLogin");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("TaskManagerAPI.Entities.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskManagerAPI.Entities.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Login");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Login = "user1",
                            Name = "User1",
                            Password = "434F30F6A76EF55ECAE03197BEC4A3F14CA29DE9979E3A84D6B6A93857B6BAE4:FEFF4E79FB775715E64947DD1B5A4822:50000:SHA256",
                            Role = 0
                        },
                        new
                        {
                            Login = "user2",
                            Name = "User2",
                            Password = "FBAAD6BBE5DFCF68420FA28FB05BEB6B2BE0563D59314A18E2E440F4D25E2429:9BCB8CA1DB63344126C5A316DA1459C4:50000:SHA256",
                            Role = 1
                        },
                        new
                        {
                            Login = "user3",
                            Name = "User3",
                            Password = "4A7353495B9C92C235A2BAFFBBE368125953FF25AB6513B2D610D0AFAA437244:03170422FDDAB4ECA63D08A92A59D564:50000:SHA256",
                            Role = 2
                        });
                });

            modelBuilder.Entity("TaskManagerAPI.Entities.Comment", b =>
                {
                    b.HasOne("TaskManagerAPI.Entities.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagerAPI.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagerAPI.Entities.Issue", b =>
                {
                    b.HasOne("TaskManagerAPI.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagerAPI.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
