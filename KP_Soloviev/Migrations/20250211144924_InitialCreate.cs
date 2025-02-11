using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KP_Soloviev.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    AnimeID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Genre = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    AgeRating = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Seasons = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Anime__AF82110AD8650DA8", x => x.AnimeID);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false),
                    RatingType = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    NameOfList = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    Criteria = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rating__FCCDF85C978E8F6E", x => x.RatingID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserMail = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    UserPass = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    UserRole = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACB8E63C48", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Anime_Rating",
                columns: table => new
                {
                    AnimeID = table.Column<int>(type: "int", nullable: false),
                    RatingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Anime_Ra__704ECE8F4F08FAE0", x => new { x.AnimeID, x.RatingID });
                    table.ForeignKey(
                        name: "FK__Anime_Rat__Anime__49C3F6B7",
                        column: x => x.AnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID");
                    table.ForeignKey(
                        name: "FK__Anime_Rat__Ratin__4AB81AF0",
                        column: x => x.RatingID,
                        principalTable: "Rating",
                        principalColumn: "RatingID");
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    FavouriteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfList = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favourit__5944B57AAFBF0408", x => x.FavouriteID);
                    table.ForeignKey(
                        name: "FK_Favourites_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: false),
                    AnimeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Author = table.Column<string>(type: "nchar(255)", fixedLength: true, maxLength: 255, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__74BC79AE5CFE2927", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK__Reviews__AnimeID__6EF57B66",
                        column: x => x.AnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID");
                    table.ForeignKey(
                        name: "FK__Reviews__UserID__6FE99F9F",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Anime_Favourites",
                columns: table => new
                {
                    AnimeID = table.Column<int>(type: "int", nullable: false),
                    FavouriteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Anime_Fa__1A165A5D6258B4DF", x => new { x.AnimeID, x.FavouriteID });
                    table.ForeignKey(
                        name: "FK__Anime_Fav__Anime__5FB337D6",
                        column: x => x.AnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID");
                    table.ForeignKey(
                        name: "FK__Anime_Fav__Favou__60A75C0F",
                        column: x => x.FavouriteID,
                        principalTable: "Favourites",
                        principalColumn: "FavouriteID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anime_Favourites_FavouriteID",
                table: "Anime_Favourites",
                column: "FavouriteID");

            migrationBuilder.CreateIndex(
                name: "IX_Anime_Rating_RatingID",
                table: "Anime_Rating",
                column: "RatingID");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserID",
                table: "Favourites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AnimeID",
                table: "Reviews",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anime_Favourites");

            migrationBuilder.DropTable(
                name: "Anime_Rating");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Anime");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
