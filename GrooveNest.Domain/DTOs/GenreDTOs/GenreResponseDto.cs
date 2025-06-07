namespace GrooveNest.Domain.DTOs.GenreDTOs
{
    public class GenreResponseDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public int TrackCount { get; set; }
    }
}
