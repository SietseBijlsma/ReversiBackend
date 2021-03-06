﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiRestApi.Models.Databse;

namespace ReversiRestApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210217103847_AddSizeToGames")]
    partial class AddSizeToGames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReversiRestApi.Models.Databse.GameModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Board")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MoveCount")
                        .HasColumnType("int");

                    b.Property<int>("Moving")
                        .HasColumnType("int");

                    b.Property<int>("Player1Color")
                        .HasColumnType("int");

                    b.Property<string>("Player1Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Player2Color")
                        .HasColumnType("int");

                    b.Property<string>("Player2Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Winner")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
