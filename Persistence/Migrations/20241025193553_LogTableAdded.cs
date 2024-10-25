using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LogTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("49dd4992-3fee-4039-aef1-b85d90cf4c35"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7cd6d7c8-d4a7-4060-9d64-d64fe283f3c7"));

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    InnerException = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DeliveryTime", "District", "Weight" },
                values: new object[,]
                {
                    { new Guid("48b0b57b-dc7a-4331-becd-e1f14eea6c99"), new DateTime(2024, 12, 13, 21, 56, 13, 0, DateTimeKind.Unspecified), "Москва, Новороссийская 20", 35m },
                    { new Guid("ab3f538c-dd53-4d1e-9643-5a9639294c0f"), new DateTime(2024, 9, 23, 3, 56, 33, 0, DateTimeKind.Unspecified), "Москва, Южная 25", 35m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("48b0b57b-dc7a-4331-becd-e1f14eea6c99"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ab3f538c-dd53-4d1e-9643-5a9639294c0f"));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DeliveryTime", "District", "Weight" },
                values: new object[,]
                {
                    { new Guid("49dd4992-3fee-4039-aef1-b85d90cf4c35"), new DateTime(2024, 9, 23, 3, 56, 33, 0, DateTimeKind.Unspecified), "Москва, Южная 25", 35m },
                    { new Guid("7cd6d7c8-d4a7-4060-9d64-d64fe283f3c7"), new DateTime(2024, 12, 13, 21, 56, 13, 0, DateTimeKind.Unspecified), "Москва, Новороссийская 20", 35m }
                });
        }
    }
}
