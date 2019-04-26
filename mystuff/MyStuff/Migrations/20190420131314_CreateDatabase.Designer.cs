﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyStuff.Data;

namespace MyStuff.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20190420131314_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("MyStuff.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("MyStuff.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuyerEmail");

                    b.Property<string>("BuyerName");

                    b.Property<string>("BuyerPhone");

                    b.Property<DateTime>("DateCreation");

                    b.Property<DateTime>("DateSell");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MyStuff.Models.Image", b =>
                {
                    b.HasOne("MyStuff.Models.Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
