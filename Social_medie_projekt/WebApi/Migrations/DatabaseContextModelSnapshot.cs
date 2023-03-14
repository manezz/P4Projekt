﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Database;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Database.Entities.Like", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Like");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            PostId = 1
                        },
                        new
                        {
                            UserId = 1,
                            PostId = 2
                        },
                        new
                        {
                            UserId = 2,
                            PostId = 1
                        },
                        new
                        {
                            UserId = 2,
                            PostId = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Login", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("LoginId");

                    b.ToTable("Login");

                    b.HasData(
                        new
                        {
                            LoginId = 1,
                            Email = "Test1@mail.dk",
                            Password = "password",
                            Role = 0
                        },
                        new
                        {
                            LoginId = 2,
                            Email = "Test2@mail.dk",
                            Password = "password",
                            Role = 1
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Date = new DateTime(2023, 3, 14, 8, 53, 33, 217, DateTimeKind.Local).AddTicks(516),
                            Desc = "tadnawdnada",
                            Title = "testestestest",
                            UserId = 1
                        },
                        new
                        {
                            PostId = 2,
                            Date = new DateTime(2023, 3, 14, 8, 53, 33, 217, DateTimeKind.Local).AddTicks(520),
                            Desc = "Woooooo!",
                            Title = "Test!",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostLikes", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.ToTable("PostLikes");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Likes = 2
                        },
                        new
                        {
                            PostId = 2,
                            Likes = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            TagId = 1
                        },
                        new
                        {
                            PostId = 1,
                            TagId = 2
                        },
                        new
                        {
                            PostId = 1,
                            TagId = 3
                        },
                        new
                        {
                            PostId = 2,
                            TagId = 3
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("TagId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tag");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            Name = "sax"
                        },
                        new
                        {
                            TagId = 2,
                            Name = "fax"
                        },
                        new
                        {
                            TagId = 3,
                            Name = "howdy"
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<int>("LoginId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("UserId");

                    b.HasIndex("LoginId")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Created = new DateTime(2023, 3, 14, 8, 53, 33, 217, DateTimeKind.Local).AddTicks(369),
                            LoginId = 1,
                            UserName = "tester 1"
                        },
                        new
                        {
                            UserId = 2,
                            Created = new DateTime(2023, 3, 14, 8, 53, 33, 217, DateTimeKind.Local).AddTicks(495),
                            LoginId = 2,
                            UserName = "222test222"
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Like", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Database.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("WebApi.Database.Entities.Like", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Post", b =>
                {
                    b.HasOne("WebApi.Database.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostLikes", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Post", "Post")
                        .WithOne("PostLikes")
                        .HasForeignKey("WebApi.Database.Entities.PostLikes", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostTag", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Database.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Login", "Login")
                        .WithOne("User")
                        .HasForeignKey("WebApi.Database.Entities.User", "LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Login", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Post", b =>
                {
                    b.Navigation("PostLikes");
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
