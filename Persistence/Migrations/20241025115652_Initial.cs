using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    District = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DeliveryTime", "District", "Weight" },
                values: new object[,]
                {
                    { new Guid("679858f7-48fb-4d5c-99a1-4bf9907d91b1"), new DateTime(2024, 9, 23, 3, 56, 33, 0, DateTimeKind.Unspecified), "Москва, Южная 25", 35m },
                    { new Guid("8bc64465-470f-4d25-99e2-d2c881f46a26"), new DateTime(2024, 12, 13, 21, 56, 13, 0, DateTimeKind.Unspecified), "Москва, Новороссийская 20", 35m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
