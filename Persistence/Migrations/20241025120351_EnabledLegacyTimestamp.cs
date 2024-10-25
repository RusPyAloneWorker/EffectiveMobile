using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnabledLegacyTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("679858f7-48fb-4d5c-99a1-4bf9907d91b1"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8bc64465-470f-4d25-99e2-d2c881f46a26"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryTime",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DeliveryTime", "District", "Weight" },
                values: new object[,]
                {
                    { new Guid("49dd4992-3fee-4039-aef1-b85d90cf4c35"), new DateTime(2024, 9, 23, 3, 56, 33, 0, DateTimeKind.Unspecified), "Москва, Южная 25", 35m },
                    { new Guid("7cd6d7c8-d4a7-4060-9d64-d64fe283f3c7"), new DateTime(2024, 12, 13, 21, 56, 13, 0, DateTimeKind.Unspecified), "Москва, Новороссийская 20", 35m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("49dd4992-3fee-4039-aef1-b85d90cf4c35"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7cd6d7c8-d4a7-4060-9d64-d64fe283f3c7"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DeliveryTime", "District", "Weight" },
                values: new object[,]
                {
                    { new Guid("679858f7-48fb-4d5c-99a1-4bf9907d91b1"), new DateTime(2024, 9, 23, 3, 56, 33, 0, DateTimeKind.Unspecified), "Москва, Южная 25", 35m },
                    { new Guid("8bc64465-470f-4d25-99e2-d2c881f46a26"), new DateTime(2024, 12, 13, 21, 56, 13, 0, DateTimeKind.Unspecified), "Москва, Новороссийская 20", 35m }
                });
        }
    }
}
