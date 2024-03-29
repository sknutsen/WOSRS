﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WOSRSTest.Server.Data;

#nullable disable

namespace WOSRSTest.Server.Migrations.DataMigrations
{
    [DbContext(typeof(DataDbContext))]
    partial class DataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("WOSRS.Shared.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category_name");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("CategoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("item_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("ItemId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("item_name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("ItemId");

                    b.ToTable("items");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.ItemCategory", b =>
                {
                    b.Property<int>("ItemCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("item_category_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("ItemCategoryId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer")
                        .HasColumnName("item_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("ItemCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ItemId");

                    b.ToTable("item_categories");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.ScheduledItem", b =>
                {
                    b.Property<int>("ScheduledItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("scheduled_item_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("ScheduledItemId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean")
                        .HasColumnName("is_complete");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer")
                        .HasColumnName("item_id");

                    b.Property<int?>("ScheduleGroup")
                        .HasColumnType("integer")
                        .HasColumnName("schedule_group");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("ScheduledItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("scheduled_items");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.Settings", b =>
                {
                    b.Property<int>("SettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("settings_id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("SettingsId"));

                    b.Property<int?>("OrderType")
                        .HasColumnType("integer")
                        .HasColumnName("order_type");

                    b.Property<bool>("PointSystem")
                        .HasColumnType("boolean")
                        .HasColumnName("point_system");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("SettingsId");

                    b.ToTable("settings");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.ItemCategory", b =>
                {
                    b.HasOne("WOSRS.Shared.Models.Category", "Category")
                        .WithMany("ItemCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WOSRS.Shared.Models.Item", "Item")
                        .WithMany("ItemCategories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.ScheduledItem", b =>
                {
                    b.HasOne("WOSRS.Shared.Models.Item", "Item")
                        .WithMany("ScheduledItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.Category", b =>
                {
                    b.Navigation("ItemCategories");
                });

            modelBuilder.Entity("WOSRS.Shared.Models.Item", b =>
                {
                    b.Navigation("ItemCategories");

                    b.Navigation("ScheduledItems");
                });
#pragma warning restore 612, 618
        }
    }
}
