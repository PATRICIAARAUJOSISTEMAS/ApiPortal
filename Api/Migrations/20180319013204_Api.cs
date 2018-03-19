using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Api : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Users");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    IsFailure = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Registration = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(maxLength: 30, nullable: false),
                    Quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    IsFailure = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Registration = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    IsFailure = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Registration = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IsFailure = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Registration = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    NickName = table.Column<string>(maxLength: 100, nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }
    }
}