using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GomelRectorCouncil.Data;

namespace GomelRectorCouncil.Migrations
{
    [DbContext(typeof(CouncilDbContext))]
    [Migration("20170503183929_testMigration")]
    partial class testMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3");

            modelBuilder.Entity("GomelRectorCouncil.Models.Achievement", b =>
                {
                    b.Property<int>("AchievementId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IndicatorId");

                    b.Property<float>("IndicatorValue");

                    b.Property<int>("UnivercityId");

                    b.HasKey("AchievementId");

                    b.HasIndex("IndicatorId");

                    b.HasIndex("UnivercityId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Chairperson", b =>
                {
                    b.Property<int>("ChairpersonId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AppointmentDate");

                    b.Property<int>("RectorID");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime?>("StopDate");

                    b.HasKey("ChairpersonId");

                    b.HasIndex("RectorID");

                    b.ToTable("Chairpersons");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Document", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChairpersonId");

                    b.Property<string>("DocumentDescription");

                    b.Property<string>("DocumentName");

                    b.Property<string>("DocumentURL");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("RegistrationNumber");

                    b.HasKey("DocumentId");

                    b.HasIndex("ChairpersonId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Indicator", b =>
                {
                    b.Property<int>("IndicatorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IndicatorDescription");

                    b.Property<byte>("IndicatorId1");

                    b.Property<byte?>("IndicatorId2");

                    b.Property<byte?>("IndicatorId3");

                    b.Property<string>("IndicatorName");

                    b.Property<int?>("IndicatorType");

                    b.Property<int>("Year");

                    b.HasKey("IndicatorId");

                    b.ToTable("Indicators");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Rector", b =>
                {
                    b.Property<int>("RectorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("UniversityId");

                    b.HasKey("RectorId");

                    b.ToTable("Rectors");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.University", b =>
                {
                    b.Property<int>("UniversityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RectorId");

                    b.Property<string>("UniversityName");

                    b.HasKey("UniversityId");

                    b.HasIndex("RectorId")
                        .IsUnique();

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Achievement", b =>
                {
                    b.HasOne("GomelRectorCouncil.Models.Indicator", "Indicator")
                        .WithMany("Achievements")
                        .HasForeignKey("IndicatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GomelRectorCouncil.Models.University", "Univercity")
                        .WithMany("Achievements")
                        .HasForeignKey("UnivercityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Chairperson", b =>
                {
                    b.HasOne("GomelRectorCouncil.Models.Rector", "Rector")
                        .WithMany()
                        .HasForeignKey("RectorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Document", b =>
                {
                    b.HasOne("GomelRectorCouncil.Models.Chairperson", "Chairperson")
                        .WithMany("Documents")
                        .HasForeignKey("ChairpersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.University", b =>
                {
                    b.HasOne("GomelRectorCouncil.Models.Rector", "Rector")
                        .WithOne("University")
                        .HasForeignKey("GomelRectorCouncil.Models.University", "RectorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
