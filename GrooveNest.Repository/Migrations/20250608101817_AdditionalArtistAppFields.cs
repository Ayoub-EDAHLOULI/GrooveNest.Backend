using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrooveNest.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalArtistAppFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ArtistApplications",
                newName: "StageName");

            migrationBuilder.AddColumn<string>(
                name: "ArtistBio",
                table: "ArtistApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "ArtistApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MusicGenres",
                table: "ArtistApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "SampleTrackLinks",
                table: "ArtistApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "ArtistApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YouTubeUrl",
                table: "ArtistApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistBio",
                table: "ArtistApplications");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "ArtistApplications");

            migrationBuilder.DropColumn(
                name: "MusicGenres",
                table: "ArtistApplications");

            migrationBuilder.DropColumn(
                name: "SampleTrackLinks",
                table: "ArtistApplications");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "ArtistApplications");

            migrationBuilder.DropColumn(
                name: "YouTubeUrl",
                table: "ArtistApplications");

            migrationBuilder.RenameColumn(
                name: "StageName",
                table: "ArtistApplications",
                newName: "Message");
        }
    }
}
