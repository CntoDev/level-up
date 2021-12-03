﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Roster.Infrastructure.Storage;

namespace Roster.Web.Migrations.RosterDb
{
    [DbContext(typeof(RosterDbContext))]
    [Migration("20211203185731_PromoteMember")]
    partial class PromoteMember
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Roster.Core.Domain.ApplicationForm", b =>
                {
                    b.Property<string>("Nickname")
                        .HasColumnType("text")
                        .HasColumnName("Nickname");

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<string>("BiNickname")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GithubNickname")
                        .HasColumnType("text");

                    b.Property<string>("InterviewerComment")
                        .HasColumnType("text");

                    b.Property<string>("SteamId")
                        .HasColumnType("text");

                    b.Property<string>("TeamspeakId")
                        .HasColumnType("text");

                    b.HasKey("Nickname");

                    b.ToTable("ApplicationForms");
                });

            modelBuilder.Entity("Roster.Core.Domain.Dlc", b =>
                {
                    b.Property<string>("DlcName")
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("DlcName");

                    b.ToTable("Dlcs");
                });

            modelBuilder.Entity("Roster.Core.Domain.Member", b =>
                {
                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<string>("BiNickname")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DiscordId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GithubNickname")
                        .HasColumnType("text");

                    b.Property<string>("Gmail")
                        .HasColumnType("text");

                    b.Property<int?>("RankId")
                        .HasColumnType("integer")
                        .HasColumnName("RankId");

                    b.Property<string>("SteamId")
                        .HasColumnType("text");

                    b.Property<string>("TeamspeakId")
                        .HasColumnType("text");

                    b.Property<bool>("_emailVerified")
                        .HasColumnType("boolean")
                        .HasColumnName("EmailVerified");

                    b.Property<string>("_verificationCode")
                        .HasColumnType("text")
                        .HasColumnName("VerificationCode");

                    b.Property<DateTime?>("_verificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("VerificationTime");

                    b.HasKey("Nickname");

                    b.HasIndex("Email");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Roster.Core.Domain.Rank", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("Roster.Core.Domain.ApplicationForm", b =>
                {
                    b.OwnsOne("Roster.Core.Domain.DiscordId", "DiscordId", b1 =>
                        {
                            b1.Property<string>("ApplicationFormNickname")
                                .HasColumnType("text");

                            b1.Property<string>("Id")
                                .HasColumnType("text")
                                .HasColumnName("DiscordId");

                            b1.HasKey("ApplicationFormNickname");

                            b1.ToTable("ApplicationForms");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationFormNickname");
                        });

                    b.OwnsOne("Roster.Core.Domain.EmailAddress", "Email", b1 =>
                        {
                            b1.Property<string>("ApplicationFormNickname")
                                .HasColumnType("text");

                            b1.Property<string>("Email")
                                .HasColumnType("text")
                                .HasColumnName("Email");

                            b1.HasKey("ApplicationFormNickname");

                            b1.ToTable("ApplicationForms");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationFormNickname");
                        });

                    b.OwnsOne("Roster.Core.Domain.EmailAddress", "Gmail", b1 =>
                        {
                            b1.Property<string>("ApplicationFormNickname")
                                .HasColumnType("text");

                            b1.Property<string>("Email")
                                .HasColumnType("text")
                                .HasColumnName("Gmail");

                            b1.HasKey("ApplicationFormNickname");

                            b1.ToTable("ApplicationForms");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationFormNickname");
                        });

                    b.OwnsMany("Roster.Core.Domain.OwnedDlc", "OwnedDlcs", b1 =>
                        {
                            b1.Property<string>("ApplicationFormNickname")
                                .HasColumnType("text");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.HasKey("ApplicationFormNickname", "Id");

                            b1.ToTable("OwnedDlc");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationFormNickname");
                        });

                    b.Navigation("DiscordId");

                    b.Navigation("Email");

                    b.Navigation("Gmail");

                    b.Navigation("OwnedDlcs");
                });
#pragma warning restore 612, 618
        }
    }
}
