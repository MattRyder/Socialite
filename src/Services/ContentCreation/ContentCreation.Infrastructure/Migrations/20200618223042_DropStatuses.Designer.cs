﻿// <auto-generated />
using System;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContentCreation.Infrastructure.Migrations
{
    [DbContext(typeof(ContentCreationDbContext))]
    [Migration("20200618223042_DropStatuses")]
    partial class DropStatuses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AlbumId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Caption")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(140) CHARACTER SET utf8mb4")
                        .HasMaxLength(140);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.PostState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("PostStates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "drafted"
                        },
                        new
                        {
                            Id = 2,
                            Name = "published"
                        });
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Photo", b =>
                {
                    b.HasOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Album", null)
                        .WithMany("Photos")
                        .HasForeignKey("AlbumId");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.Post", b =>
                {
                    b.HasOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.PostState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate.UserProfile", b =>
                {
                    b.OwnsOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate.EmailAddress", "EmailAddress", b1 =>
                        {
                            b1.Property<Guid>("UserProfileId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasColumnName("EmailAddress")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("UserProfileId");

                            b1.ToTable("UserProfiles");

                            b1.WithOwner()
                                .HasForeignKey("UserProfileId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
