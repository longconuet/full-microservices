﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_OrderDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<string>(type: "varchar(50)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(250)", nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(250)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddress = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
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
