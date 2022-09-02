﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using websearch.app;

#nullable disable

namespace websearch.app.Migrations
{
    [DbContext(typeof(FrontierDbContext))]
    partial class FrontierDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("websearch.app.WebSearch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Query")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WebSearches");
                });

            modelBuilder.Entity("websearch.app.WebSearchResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WebSearchResultSetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WebSearchResultSetId");

                    b.ToTable("WebSearchResults");
                });

            modelBuilder.Entity("websearch.app.WebSearchResultSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("RelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WebSearchId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WebSearchId");

                    b.ToTable("WebSearchResultSets");
                });

            modelBuilder.Entity("websearch.app.WebSearchResult", b =>
                {
                    b.HasOne("websearch.app.WebSearchResultSet", null)
                        .WithMany("WebSearchResults")
                        .HasForeignKey("WebSearchResultSetId");
                });

            modelBuilder.Entity("websearch.app.WebSearchResultSet", b =>
                {
                    b.HasOne("websearch.app.WebSearch", "WebSearch")
                        .WithMany()
                        .HasForeignKey("WebSearchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WebSearch");
                });

            modelBuilder.Entity("websearch.app.WebSearchResultSet", b =>
                {
                    b.Navigation("WebSearchResults");
                });
#pragma warning restore 612, 618
        }
    }
}
