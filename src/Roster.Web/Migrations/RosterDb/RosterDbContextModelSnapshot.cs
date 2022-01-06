﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Roster.Infrastructure.Storage;

#nullable disable

namespace Roster.Web.Migrations.RosterDb
{
    [DbContext(typeof(RosterDbContext))]
    partial class RosterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Roster.Core.Domain.ApplicationForm", b =>
                {
                    b.Property<string>("Nickname")
                        .HasColumnType("text")
                        .HasColumnName("Nickname");

                    b.Property<bool?>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<string>("BiNickname")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GithubNickname")
                        .HasColumnType("text");

                    b.Property<string>("InterviewerComment")
                        .HasColumnType("text");

                    b.Property<int>("PreferredPronouns")
                        .HasColumnType("integer");

                    b.Property<string>("SteamId")
                        .HasColumnType("text");

                    b.Property<string>("TeamspeakId")
                        .HasColumnType("text");

                    b.Property<string>("TimeZone")
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

            modelBuilder.Entity("Roster.Core.Domain.EventState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventType")
                        .HasColumnType("text");

                    b.Property<string>("Json")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EventStates");
                });

            modelBuilder.Entity("Roster.Core.Domain.Member", b =>
                {
                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<string>("BiNickname")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Discharged")
                        .HasColumnType("boolean");

                    b.Property<string>("DiscordId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GithubNickname")
                        .HasColumnType("text");

                    b.Property<string>("Gmail")
                        .HasColumnType("text");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("timestamp with time zone");

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
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("VerificationTime");

                    b.HasKey("Nickname");

                    b.HasIndex("Email");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Roster.Core.Domain.Rank", b =>
                {
                    b.Property<int>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("_id"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("_id"), 1L, null, null, null, null, null);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("_id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("Roster.Core.Domain.ApplicationForm", b =>
                {
                    b.OwnsOne("Roster.Core.Domain.EmailAddress", "Email", b1 =>
                        {
                            b1.Property<string>("ApplicationFormNickname")
                                .HasColumnType("text");

                            b1.Property<string>("Email")
                                .IsRequired()
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
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Gmail");

                            b1.HasKey("ApplicationFormNickname");

                            b1.ToTable("ApplicationForms");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationFormNickname");
                        });

                    b.OwnsOne("Roster.Core.Domain.DiscordId", "DiscordId", b1 =>
                        {
                            b1.Property<string>("ApplicationFormNickname")
                                .HasColumnType("text");

                            b1.Property<string>("Id")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("DiscordId");

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
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

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

            modelBuilder.Entity("Roster.Core.Domain.Member", b =>
                {
                    b.OwnsMany("Roster.Core.Domain.MemberDischarge", "MemberDischarges", b1 =>
                        {
                            b1.Property<string>("MemberNickname")
                                .HasColumnType("text");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Comment")
                                .HasColumnType("text");

                            b1.Property<DateTime>("DateOfDischarge")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("DischargePath")
                                .HasColumnType("integer");

                            b1.Property<bool>("IsAlumni")
                                .HasColumnType("boolean");

                            b1.HasKey("MemberNickname", "Id");

                            b1.ToTable("MemberDischarge");

                            b1.WithOwner()
                                .HasForeignKey("MemberNickname");
                        });

                    b.Navigation("MemberDischarges");
                });
#pragma warning restore 612, 618
        }
    }
}
