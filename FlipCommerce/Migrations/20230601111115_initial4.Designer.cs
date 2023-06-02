﻿// <auto-generated />
using System;
using FlipCommerce.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlipCommerce.Migrations
{
    [DbContext(typeof(FlipCommerceDbContext))]
    [Migration("20230601111115_initial4")]
    partial class initial4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FlipCommerce.Model.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CardNo")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidTill")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("cardType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("cvv")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardNo")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("FlipCommerce.Model.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CartTotal")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("FlipCommerce.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MobNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmailId")
                        .IsUnique();

                    b.HasIndex("MobNo")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FlipCommerce.Model.DeliveryAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Landmark")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MobNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("address1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("flat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("DeliveryAddress");
                });

            modelBuilder.Entity("FlipCommerce.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("RequiredQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("FlipCommerce.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CardUsed")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("DeliveryAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderNo")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("OrderValue")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryAddressId");

                    b.HasIndex("OrderNo")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FlipCommerce.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("productStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FlipCommerce.Model.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ProductId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("FlipCommerce.Model.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MobNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmailId")
                        .IsUnique();

                    b.HasIndex("MobNo")
                        .IsUnique();

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("FlipCommerce.Model.Card", b =>
                {
                    b.HasOne("FlipCommerce.Model.Customer", "custmer")
                        .WithMany("Cards")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("custmer");
                });

            modelBuilder.Entity("FlipCommerce.Model.Cart", b =>
                {
                    b.HasOne("FlipCommerce.Model.Customer", "customer")
                        .WithOne("cart")
                        .HasForeignKey("FlipCommerce.Model.Cart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("FlipCommerce.Model.DeliveryAddress", b =>
                {
                    b.HasOne("FlipCommerce.Model.Customer", "customer")
                        .WithMany("addresses")
                        .HasForeignKey("CustomerId");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("FlipCommerce.Model.Item", b =>
                {
                    b.HasOne("FlipCommerce.Model.Cart", "cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId");

                    b.HasOne("FlipCommerce.Model.Order", "order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("FlipCommerce.Model.Product", "product")
                        .WithMany("Items")
                        .HasForeignKey("ProductId");

                    b.Navigation("cart");

                    b.Navigation("order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("FlipCommerce.Model.Order", b =>
                {
                    b.HasOne("FlipCommerce.Model.Customer", "customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlipCommerce.Model.DeliveryAddress", "address")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryAddressId");

                    b.Navigation("address");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("FlipCommerce.Model.Product", b =>
                {
                    b.HasOne("FlipCommerce.Model.Seller", "seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("seller");
                });

            modelBuilder.Entity("FlipCommerce.Model.ProductImage", b =>
                {
                    b.HasOne("FlipCommerce.Model.Product", "product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("FlipCommerce.Model.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("FlipCommerce.Model.Customer", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Orders");

                    b.Navigation("addresses");

                    b.Navigation("cart")
                        .IsRequired();
                });

            modelBuilder.Entity("FlipCommerce.Model.DeliveryAddress", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FlipCommerce.Model.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("FlipCommerce.Model.Product", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("FlipCommerce.Model.Seller", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}