﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Repositories;

namespace ToDoList.Migrations
{
    [DbContext(typeof(ToDoListContext))]
    [Migration("20210417080706_linkentities")]
    partial class linkentities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("ToDoList.Database.ToDoListEntity", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UsersUserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemID");

                    b.HasIndex("UsersUserID");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("ToDoList.Database.ToDoUsersEntity", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToDoList.Database.ToDoListEntity", b =>
                {
                    b.HasOne("ToDoList.Database.ToDoUsersEntity", "Users")
                        .WithMany("Lists")
                        .HasForeignKey("UsersUserID");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ToDoList.Database.ToDoUsersEntity", b =>
                {
                    b.Navigation("Lists");
                });
#pragma warning restore 612, 618
        }
    }
}
