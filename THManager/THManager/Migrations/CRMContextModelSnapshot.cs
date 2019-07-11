﻿// <auto-generated />
using System;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jobs.Migrations
{
    [DbContext(typeof(CRMContext))]
    partial class CRMContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("THManager.Models.AccountTradeHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Account");

                    b.Property<decimal>("DesiredDollarQuantity")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("DesiredPercentProfit")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("DesiredProfit")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("DesiredQuantity")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("DollarQuantity")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("Pair");

                    b.Property<decimal>("PercentProfit")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("Profit")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("Side");

                    b.Property<string>("SignalStr");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.ToTable("AccountTradeHistories");
                });

            modelBuilder.Entity("THManager.Models.ExchangeKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ExchangeKeys");
                });

            modelBuilder.Entity("THManager.Models.IgnoreIds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.ToTable("IgnoreIds");
                });

            modelBuilder.Entity("THManager.Models.WrongOrders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.ToTable("WrongOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
