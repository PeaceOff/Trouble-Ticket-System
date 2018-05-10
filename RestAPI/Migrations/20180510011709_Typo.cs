using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RestAPI.Migrations
{
    public partial class Typo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tittle",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Tittle",
                table: "SecondaryTicket");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SecondaryTicket",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SecondaryTicket");

            migrationBuilder.AddColumn<string>(
                name: "Tittle",
                table: "Ticket",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tittle",
                table: "SecondaryTicket",
                nullable: true);
        }
    }
}
