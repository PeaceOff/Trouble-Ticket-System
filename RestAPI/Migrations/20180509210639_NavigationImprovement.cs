using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RestAPI.Migrations
{
    public partial class NavigationImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondaryTicket_Ticket_TicketID",
                table: "SecondaryTicket");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Ticket",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TicketID",
                table: "SecondaryTicket",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SecondaryTicket",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_SecondaryTicket_TicketID",
                table: "SecondaryTicket",
                newName: "IX_SecondaryTicket_TicketId");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "SecondaryTicket",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryTicket_Ticket_TicketId",
                table: "SecondaryTicket",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondaryTicket_Ticket_TicketId",
                table: "SecondaryTicket");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ticket",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "SecondaryTicket",
                newName: "TicketID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SecondaryTicket",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_SecondaryTicket_TicketId",
                table: "SecondaryTicket",
                newName: "IX_SecondaryTicket_TicketID");

            migrationBuilder.AlterColumn<int>(
                name: "TicketID",
                table: "SecondaryTicket",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryTicket_Ticket_TicketID",
                table: "SecondaryTicket",
                column: "TicketID",
                principalTable: "Ticket",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
