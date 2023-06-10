﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VolksmondAPI.Data;

#nullable disable

namespace VolksmondAPI.Migrations
{
    [DbContext(typeof(VolksmondAPIContext))]
    partial class VolksmondAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VolksmondAPI.Models.Citizen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Citizen");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CitizenId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.ToTable("Problem");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Referendum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProblemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VotingEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VotingStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.ToTable("Referendum");
                });

            modelBuilder.Entity("VolksmondAPI.Models.ReferendumVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CitizenId")
                        .HasColumnType("int");

                    b.Property<int>("ReferendumId")
                        .HasColumnType("int");

                    b.Property<int>("SolutionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReferendumId");

                    b.ToTable("ReferendumVote");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CitizenId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPinned")
                        .HasColumnType("bit");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int>("SolutionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReplyId");

                    b.HasIndex("SolutionId");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("VolksmondAPI.Models.ReplyVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CitizenId")
                        .HasColumnType("int");

                    b.Property<int?>("Reply")
                        .HasColumnType("int");

                    b.Property<int>("ReplyId")
                        .HasColumnType("int");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Reply");

                    b.ToTable("ReplyVote");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Solution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CitizenId")
                        .HasColumnType("int");

                    b.Property<int>("ProblemId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("ProblemId");

                    b.ToTable("Solution");
                });

            modelBuilder.Entity("VolksmondAPI.Models.SolutionVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CitizenId")
                        .HasColumnType("int");

                    b.Property<int>("SolutionId")
                        .HasColumnType("int");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SolutionId");

                    b.ToTable("SolutionVote");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Problem", b =>
                {
                    b.HasOne("VolksmondAPI.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Referendum", b =>
                {
                    b.HasOne("VolksmondAPI.Models.Problem", null)
                        .WithMany("Referendums")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VolksmondAPI.Models.ReferendumVote", b =>
                {
                    b.HasOne("VolksmondAPI.Models.Referendum", null)
                        .WithMany("Votes")
                        .HasForeignKey("ReferendumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VolksmondAPI.Models.Reply", b =>
                {
                    b.HasOne("VolksmondAPI.Models.Reply", null)
                        .WithMany("Replies")
                        .HasForeignKey("ReplyId");

                    b.HasOne("VolksmondAPI.Models.Solution", null)
                        .WithMany("Replies")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VolksmondAPI.Models.ReplyVote", b =>
                {
                    b.HasOne("VolksmondAPI.Models.Reply", null)
                        .WithMany("Votes")
                        .HasForeignKey("Reply");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Solution", b =>
                {
                    b.HasOne("VolksmondAPI.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VolksmondAPI.Models.Problem", null)
                        .WithMany("Solutions")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("VolksmondAPI.Models.SolutionVote", b =>
                {
                    b.HasOne("VolksmondAPI.Models.Solution", null)
                        .WithMany("Votes")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VolksmondAPI.Models.Problem", b =>
                {
                    b.Navigation("Referendums");

                    b.Navigation("Solutions");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Referendum", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Reply", b =>
                {
                    b.Navigation("Replies");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("VolksmondAPI.Models.Solution", b =>
                {
                    b.Navigation("Replies");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
