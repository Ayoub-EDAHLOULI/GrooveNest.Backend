using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrooveNest.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndCreatedAtToTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Tracks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Tracks");
        }
    }
}
