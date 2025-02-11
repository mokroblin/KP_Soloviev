﻿// <auto-generated />
using System;
using KP_Soloviev.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KP_Soloviev.Migrations
{
    [DbContext(typeof(Anime_CollectionContext))]
    [Migration("20250211144924_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KP_Soloviev.Models.Anime", b =>
                {
                    b.Property<int>("AnimeID")
                        .HasColumnType("int");

                    b.Property<int>("AgeRating")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nchar(500)")
                        .IsFixedLength();

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<byte[]>("Photo")
                        .HasMaxLength(1)
                        .HasColumnType("varbinary(1)");

                    b.Property<int>("Seasons")
                        .HasColumnType("int");

                    b.Property<int>("Series")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.HasKey("AnimeID")
                        .HasName("PK__Anime__AF82110AD8650DA8");

                    b.ToTable("Anime", (string)null);
                });

            modelBuilder.Entity("KP_Soloviev.Models.Anime_Favourite", b =>
                {
                    b.Property<int>("AnimeID")
                        .HasColumnType("int");

                    b.Property<int>("FavouriteID")
                        .HasColumnType("int");

                    b.HasKey("AnimeID", "FavouriteID")
                        .HasName("PK__Anime_Fa__1A165A5D6258B4DF");

                    b.HasIndex("FavouriteID");

                    b.ToTable("Anime_Favourites");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Anime_Rating", b =>
                {
                    b.Property<int>("AnimeID")
                        .HasColumnType("int");

                    b.Property<int>("RatingID")
                        .HasColumnType("int");

                    b.HasKey("AnimeID", "RatingID")
                        .HasName("PK__Anime_Ra__704ECE8F4F08FAE0");

                    b.HasIndex("RatingID");

                    b.ToTable("Anime_Rating", (string)null);
                });

            modelBuilder.Entity("KP_Soloviev.Models.Favourite", b =>
                {
                    b.Property<int>("FavouriteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavouriteID"));

                    b.Property<DateOnly>("CreationDate")
                        .HasColumnType("date");

                    b.Property<string>("NameOfList")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FavouriteID")
                        .HasName("PK__Favourit__5944B57AAFBF0408");

                    b.HasIndex("UserID");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Rating", b =>
                {
                    b.Property<int>("RatingID")
                        .HasColumnType("int");

                    b.Property<string>("Criteria")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<string>("NameOfList")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<string>("RatingType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<DateOnly>("UpdateDate")
                        .HasColumnType("date");

                    b.HasKey("RatingID")
                        .HasName("PK__Rating__FCCDF85C978E8F6E");

                    b.ToTable("Rating", (string)null);
                });

            modelBuilder.Entity("KP_Soloviev.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewID"));

                    b.Property<int>("AnimeID")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nchar(500)")
                        .IsFixedLength();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ReviewID")
                        .HasName("PK__Reviews__74BC79AE5CFE2927");

                    b.HasIndex("AnimeID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("KP_Soloviev.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("UserMail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<string>("UserPass")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.HasKey("UserID")
                        .HasName("PK__Users__1788CCACB8E63C48");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Anime_Favourite", b =>
                {
                    b.HasOne("KP_Soloviev.Models.Anime", "Anime")
                        .WithMany("Anime_Favourites")
                        .HasForeignKey("AnimeID")
                        .IsRequired()
                        .HasConstraintName("FK__Anime_Fav__Anime__5FB337D6");

                    b.HasOne("KP_Soloviev.Models.Favourite", "Favourite")
                        .WithMany("Anime_Favourites")
                        .HasForeignKey("FavouriteID")
                        .IsRequired()
                        .HasConstraintName("FK__Anime_Fav__Favou__60A75C0F");

                    b.Navigation("Anime");

                    b.Navigation("Favourite");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Anime_Rating", b =>
                {
                    b.HasOne("KP_Soloviev.Models.Anime", "Anime")
                        .WithMany("Anime_Ratings")
                        .HasForeignKey("AnimeID")
                        .IsRequired()
                        .HasConstraintName("FK__Anime_Rat__Anime__49C3F6B7");

                    b.HasOne("KP_Soloviev.Models.Rating", "Rating")
                        .WithMany("Anime_Ratings")
                        .HasForeignKey("RatingID")
                        .IsRequired()
                        .HasConstraintName("FK__Anime_Rat__Ratin__4AB81AF0");

                    b.Navigation("Anime");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Favourite", b =>
                {
                    b.HasOne("KP_Soloviev.Models.User", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserID")
                        .IsRequired()
                        .HasConstraintName("FK_Favourites_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Review", b =>
                {
                    b.HasOne("KP_Soloviev.Models.Anime", "Anime")
                        .WithMany("Reviews")
                        .HasForeignKey("AnimeID")
                        .IsRequired()
                        .HasConstraintName("FK__Reviews__AnimeID__6EF57B66");

                    b.HasOne("KP_Soloviev.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .IsRequired()
                        .HasConstraintName("FK__Reviews__UserID__6FE99F9F");

                    b.Navigation("Anime");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Anime", b =>
                {
                    b.Navigation("Anime_Favourites");

                    b.Navigation("Anime_Ratings");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Favourite", b =>
                {
                    b.Navigation("Anime_Favourites");
                });

            modelBuilder.Entity("KP_Soloviev.Models.Rating", b =>
                {
                    b.Navigation("Anime_Ratings");
                });

            modelBuilder.Entity("KP_Soloviev.Models.User", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
