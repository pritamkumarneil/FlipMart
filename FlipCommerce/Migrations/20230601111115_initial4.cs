﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlipCommerce.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryAddress_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId",
                principalTable: "DeliveryAddress",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryAddress_DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                table: "Orders");
        }
    }
}
