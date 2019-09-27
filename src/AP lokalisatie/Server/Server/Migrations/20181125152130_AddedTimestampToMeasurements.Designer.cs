﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

namespace Server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20181125152130_AddedTimestampToMeasurements")]
    partial class AddedTimestampToMeasurements
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Server.Models.Anchor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Mac");

                    b.Property<long?>("MapId");

                    b.Property<int?>("UserId");

                    b.Property<int>("XPos");

                    b.Property<int>("YPos");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("UserId");

                    b.ToTable("Anchors");
                });

            modelBuilder.Entity("Server.Models.Map", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Height");

                    b.Property<string>("Picture");

                    b.Property<int?>("UserId");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("Server.Models.Measurement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Distance");

                    b.Property<string>("Mac_Anchor");

                    b.Property<string>("Mac_Tag");

                    b.Property<string>("Unix_Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Server.Models.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Mac");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Server.Models.Anchor", b =>
                {
                    b.HasOne("Server.Models.Map", "Map")
                        .WithMany("Anchors")
                        .HasForeignKey("MapId");

                    b.HasOne("Server.Models.User", "User")
                        .WithMany("Anchors")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Server.Models.Map", b =>
                {
                    b.HasOne("Server.Models.User")
                        .WithMany("Maps")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Server.Models.Tag", b =>
                {
                    b.HasOne("Server.Models.User", "User")
                        .WithMany("Tags")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
