namespace GrooveNest.Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //
        public ICollection<TrackGenre> TrackGenres { get; set; } = [];
    }
}
