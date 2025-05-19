namespace GrooveNest.Domain.Entities
{
    public class TrackGenre
    {
        public int TrackId { get; set; }
        public Track Track { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
