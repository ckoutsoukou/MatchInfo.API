﻿// <auto-generated />
using System;
using MatchInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MatchInfo.API.Migrations
{
    [DbContext(typeof(MatchInfoContext))]
    [Migration("20230628065909_InitialDBCreate")]
    partial class InitialDBCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MatchInfo.API.Entities.Match", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("MatchDateTime")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Sport")
                        .HasColumnType("tinyint");

                    b.Property<string>("TeamA")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TeamB")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.HasIndex("MatchDateTime", "TeamA", "TeamB", "Sport")
                        .IsUnique();

                    b.ToTable("Matches");

                    b.HasCheckConstraint("CK_Matches_Sport", "[Sport] = 1 OR [Sport] = 2");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "OSFP-PAO",
                            MatchDateTime = new DateTime(2023, 7, 30, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Sport = (byte)1,
                            TeamA = "OSFP",
                            TeamB = "PAO"
                        },
                        new
                        {
                            ID = 2,
                            Description = "AEK-PAO",
                            MatchDateTime = new DateTime(2023, 6, 29, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Sport = (byte)2,
                            TeamA = "AEK",
                            TeamB = "PAO"
                        });
                });

            modelBuilder.Entity("MatchInfo.API.Entities.MatchOdd", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<double>("Odd")
                        .HasColumnType("float");

                    b.Property<string>("Specifier")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.HasIndex("MatchId", "Specifier")
                        .IsUnique();

                    b.ToTable("MatchOdds");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            MatchId = 1,
                            Odd = 1.5,
                            Specifier = "X"
                        },
                        new
                        {
                            ID = 2,
                            MatchId = 2,
                            Odd = 2.2999999999999998,
                            Specifier = "1"
                        },
                        new
                        {
                            ID = 3,
                            MatchId = 2,
                            Odd = 3.1000000000000001,
                            Specifier = "2"
                        });
                });

            modelBuilder.Entity("MatchInfo.API.Entities.MatchOdd", b =>
                {
                    b.HasOne("MatchInfo.API.Entities.Match", "Match")
                        .WithMany("MatchOdds")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("MatchInfo.API.Entities.Match", b =>
                {
                    b.Navigation("MatchOdds");
                });
#pragma warning restore 612, 618
        }
    }
}
