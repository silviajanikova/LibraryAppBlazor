using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAppBlazor.Migrations
{
    /// <inheritdoc />
    public partial class Add_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckOutRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOutRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOutRecord_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckOutRecord_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "IsAvailable", "Title" },
                values: new object[,]
                {
                    { new Guid("55cab9d8-1f84-44b7-ab3c-259b7a923f9d"), "George Orwell", true, "1984" },
                    { new Guid("6e5fb9df-a805-4c99-8546-12a910a03165"), "Harper Lee", true, "Zabiť vtáča robáka" },
                    { new Guid("8aa2f6bf-9660-413c-9197-914eedc68646"), "J.K.Rowling", true, "Harry Potter a Kameň mudrcov" },
                    { new Guid("a86e7bdc-89b2-4e2e-8f04-7d4b44a6612b"), "Jane Austen", true, "Pýcha a predsudok" },
                    { new Guid("fc55b64f-7b79-4331-912a-37c11ea1064a"), "F. Scott Fitzgerald", true, "Veľký Gatsby" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "DateOfBirth", "Firstname", "IdentityCardNumber", "Surname" },
                values: new object[,]
                {
                    { new Guid("1d4db229-b8ad-4439-adc8-648d8e809a29"), new DateOnly(1976, 1, 17), "Zuzana", "789456123", "Horváthová" },
                    { new Guid("2bc56910-78b1-4c7a-b95e-5e4051b4e5a3"), new DateOnly(1995, 12, 3), "Marta", "321654987", "Ďuricová" },
                    { new Guid("892000ab-1040-4a2e-80f6-658cb4d426c6"), new DateOnly(1990, 6, 25), "Eva", "987654321", "Svobodová" },
                    { new Guid("93a4c59b-01b4-49ea-a384-ce2436f565d3"), new DateOnly(1982, 8, 8), "Peter", "456123789", "Kováč" },
                    { new Guid("b9d34651-6e55-4985-8390-7d80c0e27090"), new DateOnly(1985, 3, 12), "Ján", "123456789", "Novák" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckOutRecord_BookId",
                table: "CheckOutRecord",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOutRecord_MemberId",
                table: "CheckOutRecord",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckOutRecord");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
