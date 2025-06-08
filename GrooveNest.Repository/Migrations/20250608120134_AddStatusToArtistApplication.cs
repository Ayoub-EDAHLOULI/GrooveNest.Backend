using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrooveNest.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToArtistApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateReviewed",
                table: "ArtistApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "ArtistApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ArtistApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateReviewed",
                table: "ArtistApplications");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "ArtistApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ArtistApplications");
        }
    }
}
