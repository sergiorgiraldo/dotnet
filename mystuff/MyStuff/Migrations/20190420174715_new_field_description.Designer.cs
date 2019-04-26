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
    [Migration("20190420174715_new_field_description")]
    partial class new_field_description
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("MyStuff.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuyerEmail");

                    b.Property<string>("BuyerName");

                    b.Property<string>("BuyerPhone");

                    b.Property<DateTime>("DateCreation");

                    b.Property<DateTime>("DateSell");

                    b.Property<string>("Description");

                    b.Property<string>("Image1");

                    b.Property<string>("Image2");

                    b.Property<string>("Image3");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}