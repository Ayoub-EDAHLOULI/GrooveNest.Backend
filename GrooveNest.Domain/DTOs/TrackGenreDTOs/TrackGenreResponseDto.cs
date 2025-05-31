namespace GrooveNest.Domain.DTOs.TrackGenreDTOs
{
    public class TrackGenreResponseDto
    {
        public Guid TrackId { get; set; }
        public Guid GenreId { get; set; }
        public string? TrackName { get; set; }
        public string? GenreName { get; set; }
        public string? ArtistName { get; set; }
        public string? AlbumName { get; set; }
    }
}
