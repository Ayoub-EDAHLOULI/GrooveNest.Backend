namespace GrooveNest.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //
        public ICollection<TrackGenre> TrackGenres { get; set; } = [];
    }
}
