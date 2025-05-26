using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController(PlaylistService playlistService) : ControllerBase
    {
        private readonly PlaylistService _playlistService = playlistService;
    }
}
