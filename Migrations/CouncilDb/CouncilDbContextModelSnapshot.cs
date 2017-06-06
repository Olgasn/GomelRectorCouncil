using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;

namespace GomelRectorCouncil.Migrations.CouncilDb
{
    [DbContext(typeof(CouncilDbContext))]
    partial class CouncilDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.4");

            modelBuilder.Entity("GomelRectorCouncil.Models.Achievement", b =>
                {
                    b.Property<int>("AchievementId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IndicatorId");

                    b.Property<float>("IndicatorValue");

                    b.Property<int>("UnivercityId");

                    b.Property<int>("Year");

                    b.HasKey("AchievementId");

                    b.HasIndex("IndicatorId");

                    b.HasIndex("UnivercityId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Chairperson", b =>
                {
                    b.Property<int>("ChairpersonId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RectorId");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime?>("StopDate");

                    b.HasKey("ChairpersonId");

                    b.HasIndex("RectorId");

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

                    b.Property<int>("IndicatorType");

                    b.Property<string>("IndicatorUnit");

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

                    b.Property<string>("Photo");

                    b.Property<int>("UniversityId");

                    b.HasKey("RectorId");

                    b.HasIndex("UniversityId")
                        .IsUnique();

                    b.ToTable("Rectors");
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.University", b =>
                {
                    b.Property<int>("UniversityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Logo");

                    b.Property<string>("UniversityName");

                    b.Property<string>("Website");

                    b.HasKey("UniversityId");

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
                        .HasForeignKey("RectorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Document", b =>
                {
                    b.HasOne("GomelRectorCouncil.Models.Chairperson", "Chairperson")
                        .WithMany("Documents")
                        .HasForeignKey("ChairpersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GomelRectorCouncil.Models.Rector", b =>
                {
                    b.HasOne("GomelRectorCouncil.Models.University", "University")
                        .WithOne("Rector")
                        .HasForeignKey("GomelRectorCouncil.Models.Rector", "UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
