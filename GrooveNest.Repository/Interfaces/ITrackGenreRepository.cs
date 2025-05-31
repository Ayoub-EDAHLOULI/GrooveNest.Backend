using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface ITrackGenreRepository
    {
        Task<IEnumerable<TrackGenre>> GetAllAsync();
        Task<TrackGenre?> GetByIdAsync(Guid trackId);
        Task AddAsync(TrackGenre entity);
        Task DeleteAsync(TrackGenre entity);
        Task<List<Genre>> GetGenresByTrackIdAsync(Guid trackId);
        Task<List<Track>> GetTracksByGenreIdAsync(Guid genreId);
    }
}
