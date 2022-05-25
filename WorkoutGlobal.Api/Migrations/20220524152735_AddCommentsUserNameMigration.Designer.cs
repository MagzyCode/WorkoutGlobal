﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutGlobal.Api.Context;

#nullable disable

namespace WorkoutGlobal.Api.Migrations
{
    [DbContext(typeof(WorkoutGlobalContext))]
    [Migration("20220524152735_AddCommentsUserNameMigration")]
    partial class AddCommentsUserNameMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f4a4ce79-c6b3-4e12-9c98-ff07b5030752",
                            ConcurrencyStamp = "4b9a57f1-a6fb-4e3a-a582-e59341c017f2",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1",
                            ConcurrencyStamp = "a50f22dc-b940-472d-8b2b-72befeac7c4d",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "4f4d7080-beee-4a97-be65-2ffccde5eb72",
                            ConcurrencyStamp = "f86475a7-f5e4-4518-af60-fd76d3c030ce",
                            Name = "Trainer",
                            NormalizedName = "TRAINER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                            RoleId = "6abe6f33-ae4b-4430-8f14-493dc9a5a9d1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CommentatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CommentsBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CommentatorId");

                    b.HasIndex("CommentsBlockId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.CommentsBlock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommentedVideoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CommentedVideoId")
                        .IsUnique();

                    b.ToTable("CommentsBlocks");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.CourseVideos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VideoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("VideoId");

                    b.ToTable("CourseVideos");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderSize")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderedProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderedProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Context")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PostCreationTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("CostPerProduct")
                        .HasColumnType("float");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductSupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductSupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.ProductSuppliers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResidencePlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductSuppliers");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Stockroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Stockrooms");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClassificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("IsStatusVerify")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResidencePlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int>("SportsActivity")
                        .HasColumnType("int");

                    b.Property<string>("UserCredentialsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserCredentialsId")
                        .IsUnique()
                        .HasFilter("[UserCredentialsId] IS NOT NULL");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.UserCredentials", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "b5b84fd7-5366-44eb-9d1b-408c6a4a8926",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "132e5090-52a5-4686-b855-80d62973ad1c",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "21c9b9e74e5071de6d6c872ccae5af4deb3b42563cd649a3179a5780163b6238",
                            PasswordSalt = "46da4fb783d806ab",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3cf3aa6c-1e4f-47f3-98b3-2178374a09f1",
                            TwoFactorEnabled = false,
                            UserName = "MagzyCode"
                        });
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Video", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.UserCredentials", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.UserCredentials", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutGlobal.Api.Models.UserCredentials", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.UserCredentials", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Comment", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.User", "Commentator")
                        .WithMany("Comments")
                        .HasForeignKey("CommentatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WorkoutGlobal.Api.Models.CommentsBlock", "CommentsBlock")
                        .WithMany("Comments")
                        .HasForeignKey("CommentsBlockId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Commentator");

                    b.Navigation("CommentsBlock");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.CommentsBlock", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.Video", "CommentedVideo")
                        .WithOne("CommentsBlock")
                        .HasForeignKey("WorkoutGlobal.Api.Models.CommentsBlock", "CommentedVideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommentedVideo");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Course", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.User", "Creator")
                        .WithMany("Courses")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.CourseVideos", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.Course", "Course")
                        .WithMany("CourseVideos")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WorkoutGlobal.Api.Models.Video", "Video")
                        .WithMany("VideoCourses")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Order", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.User", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutGlobal.Api.Models.Product", "OrderedProduct")
                        .WithMany("Orders")
                        .HasForeignKey("OrderedProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("OrderedProduct");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Post", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.User", "Creator")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Product", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.ProductSuppliers", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("ProductSupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Stockroom", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.Product", "Product")
                        .WithOne("Stockroom")
                        .HasForeignKey("WorkoutGlobal.Api.Models.Stockroom", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.User", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.UserCredentials", "UserCredentials")
                        .WithOne("User")
                        .HasForeignKey("WorkoutGlobal.Api.Models.User", "UserCredentialsId");

                    b.Navigation("UserCredentials");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Video", b =>
                {
                    b.HasOne("WorkoutGlobal.Api.Models.User", "User")
                        .WithMany("Videos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.CommentsBlock", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Course", b =>
                {
                    b.Navigation("CourseVideos");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Product", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Stockroom");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.ProductSuppliers", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Courses");

                    b.Navigation("Orders");

                    b.Navigation("Posts");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.UserCredentials", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkoutGlobal.Api.Models.Video", b =>
                {
                    b.Navigation("CommentsBlock");

                    b.Navigation("VideoCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
