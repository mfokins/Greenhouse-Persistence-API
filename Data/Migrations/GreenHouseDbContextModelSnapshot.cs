﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(GreenHouseDbContext))]
    partial class GreenHouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Data.Models.Greenhouse", b =>
                {
                    b.Property<string>("GreenHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.HasKey("GreenHouseId");

                    b.ToTable("Greenhouses");
                });

            modelBuilder.Entity("Data.Models.Measurements.DioxideCarbonMeasurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Co2Measurement")
                        .HasColumnType("int");

                    b.Property<string>("GreenHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GreenHouseId");

                    b.ToTable("DioxideCarbonMeasurement");
                });

            modelBuilder.Entity("Data.Models.Measurements.HumidityMeasurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GreenHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Humidity")
                        .HasColumnType("float");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GreenHouseId");

                    b.ToTable("HumidityMeasurement");
                });

            modelBuilder.Entity("Data.Models.Measurements.MoistureMeasurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Moisture")
                        .HasColumnType("float");

                    b.Property<int?>("PotId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PotId");

                    b.ToTable("MoistureMeasurement");
                });

            modelBuilder.Entity("Data.Models.Measurements.TemperatureMeasurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GreenHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GreenHouseId");

                    b.ToTable("TemperatureMeasurement");
                });

            modelBuilder.Entity("Data.Models.Pot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GreenHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MoistureSensorId")
                        .HasColumnType("int");

                    b.Property<int>("MoistureSensorStatusId")
                        .HasColumnType("int");

                    b.Property<int>("MoistureThresholdId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GreenHouseId");

                    b.HasIndex("MoistureSensorStatusId");

                    b.HasIndex("MoistureThresholdId");

                    b.ToTable("Pot");
                });

            modelBuilder.Entity("Data.Models.SensorStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GreenHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsWorking")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GreenHouseId");

                    b.ToTable("SensorStatus");
                });

            modelBuilder.Entity("Data.Models.Threshold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GreenHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("HigherThreshold")
                        .HasColumnType("float");

                    b.Property<double>("LowerThreshold")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GreenHouseId");

                    b.ToTable("Threshold");
                });

            modelBuilder.Entity("Data.Models.Measurements.DioxideCarbonMeasurement", b =>
                {
                    b.HasOne("Data.Models.Greenhouse", null)
                        .WithMany("DioxideCarbonMeasurements")
                        .HasForeignKey("GreenHouseId");
                });

            modelBuilder.Entity("Data.Models.Measurements.HumidityMeasurement", b =>
                {
                    b.HasOne("Data.Models.Greenhouse", null)
                        .WithMany("HumidityMeasurements")
                        .HasForeignKey("GreenHouseId");
                });

            modelBuilder.Entity("Data.Models.Measurements.MoistureMeasurement", b =>
                {
                    b.HasOne("Data.Models.Pot", null)
                        .WithMany("MoistureMeasurements")
                        .HasForeignKey("PotId");
                });

            modelBuilder.Entity("Data.Models.Measurements.TemperatureMeasurement", b =>
                {
                    b.HasOne("Data.Models.Greenhouse", null)
                        .WithMany("TemperatureMesurments")
                        .HasForeignKey("GreenHouseId");
                });

            modelBuilder.Entity("Data.Models.Pot", b =>
                {
                    b.HasOne("Data.Models.Greenhouse", null)
                        .WithMany("Pots")
                        .HasForeignKey("GreenHouseId");

                    b.HasOne("Data.Models.SensorStatus", "MoistureSensorStatus")
                        .WithMany()
                        .HasForeignKey("MoistureSensorStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Threshold", "MoistureThreshold")
                        .WithMany()
                        .HasForeignKey("MoistureThresholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MoistureSensorStatus");

                    b.Navigation("MoistureThreshold");
                });

            modelBuilder.Entity("Data.Models.SensorStatus", b =>
                {
                    b.HasOne("Data.Models.Greenhouse", null)
                        .WithMany("SensorStatuses")
                        .HasForeignKey("GreenHouseId");
                });

            modelBuilder.Entity("Data.Models.Threshold", b =>
                {
                    b.HasOne("Data.Models.Greenhouse", null)
                        .WithMany("Thresholds")
                        .HasForeignKey("GreenHouseId");
                });

            modelBuilder.Entity("Data.Models.Greenhouse", b =>
                {
                    b.Navigation("DioxideCarbonMeasurements");

                    b.Navigation("HumidityMeasurements");

                    b.Navigation("Pots");

                    b.Navigation("SensorStatuses");

                    b.Navigation("TemperatureMesurments");

                    b.Navigation("Thresholds");
                });

            modelBuilder.Entity("Data.Models.Pot", b =>
                {
                    b.Navigation("MoistureMeasurements");
                });
#pragma warning restore 612, 618
        }
    }
}
