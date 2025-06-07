using GrooveNest.Domain.DTOs.ArtistDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;
using Microsoft.AspNetCore.Http;

namespace GrooveNest.Service.Services
{
    public class ArtistService(IArtistRepository artistRepository, IUserService userService) : IArtistService
    {
        private readonly IArtistRepository _artistRepository = artistRepository;
        private readonly IUserService _userRepository = userService;


        // -------------------------------------------------------------------------- //
        // ------------------------ GetAllArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<ArtistResponseDto>>> GetAllArtistsAsync()
        {
            var artists = await _artistRepository.GetAllAsync();
            if (artists == null || !artists.Any())
            {
                return ApiResponse<List<ArtistResponseDto>>.ErrorResponse("No artists found.");
            }

            var artistDtos = await Task.WhenAll(artists.Select(async a =>
            {
                var userResponse = a.UserId.HasValue ? await _userRepository.GetUserByIdAsync(a.UserId.Value) : null;
                var user = userResponse?.Data; // Extract the UserResponseDto from ApiResponse<UserResponseDto>

                return new ArtistResponseDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Bio = a.Bio,
                    UserName = user?.UserName ?? "Unknown",
                    ProfilePictureUrl = a.AvatarUrl
                };
            }));

            return ApiResponse<List<ArtistResponseDto>>.SuccessResponse(artistDtos.ToList(), "Artists retrieved successfully.");
        }


        // ------------------------------------------------------------------------------------ //
        // ------------------------ GetAllPaginatedArtistsAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------ // 
        public async Task<ApiResponse<object>> GetAllPaginatedArtistsAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var artists = await _artistRepository.GetAllAsync();
            if (artists == null || !artists.Any())
            {
                return ApiResponse<object>.ErrorResponse("No artists found.");
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                artists = artists.Where(a => a.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalCount = artists.Count();
            var artistDtos = await Task.WhenAll(artists.Select(async a =>
            {
                var userResponse = a.UserId.HasValue ? await _userRepository.GetUserByIdAsync(a.UserId.Value) : null;
                var user = userResponse?.Data; // Extract the UserResponseDto from ApiResponse<UserResponseDto>
                return new ArtistResponseDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Bio = a.Bio,
                    UserName = user?.UserName ?? "Unknown"
                };
            }));

            var paginatedArtists = artistDtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                PaginatedArtists = paginatedArtists,
                TotalArtists = totalCount,
            };

            return ApiResponse<object>.SuccessResponse(response, "Paginated artists retrieved successfully.");
        }


        // --------------------------------------------------------------------------- //
        // ------------------------ GetArtistByIdAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 
        public async Task<ApiResponse<ArtistResponseDto>> GetArtistByIdAsync(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return ApiResponse<ArtistResponseDto>.ErrorResponse("Artist not found.");
            }
            var userResponse = artist.UserId.HasValue ? await _userRepository.GetUserByIdAsync(artist.UserId.Value) : null;
            var user = userResponse?.Data; // Extract the UserResponseDto from ApiResponse<UserResponseDto>
            var artistDto = new ArtistResponseDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Bio = artist.Bio,
                UserName = user?.UserName ?? "Unknown"
            };
            return ApiResponse<ArtistResponseDto>.SuccessResponse(artistDto, "Artist retrieved successfully.");
        }



        // -------------------------------------------------------------------------- //
        // ------------------------ CreateArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<ArtistResponseDto>> CreateArtistAsync(ArtistCreateDto artistCreateDto)
        {
            if (string.IsNullOrWhiteSpace(artistCreateDto.Name))
            {
                return ApiResponse<ArtistResponseDto>.ErrorResponse("Artist name is required.");
            }

            var trimmedName = StringValidator.TrimOrEmpty(artistCreateDto.Name);
            var trimmedbio = StringValidator.TrimOrEmpty(artistCreateDto.Bio);

            var existingArtist = await _artistRepository.GetArtistByName(trimmedName);
            if (existingArtist != null)
            {
                return ApiResponse<ArtistResponseDto>.ErrorResponse("An artist with this name already exists.");
            }

            // Validate file size and type for avatar
            if (artistCreateDto.Avatar != null && artistCreateDto.Avatar.Length > 5 * 1024 * 1024) // 5 MB limit
            {
                return ApiResponse<ArtistResponseDto>.ErrorResponse("Avatar file size exceeds the limit of 5 MB.");
            }

            if (artistCreateDto.Avatar != null && !artistCreateDto.Avatar.ContentType.StartsWith("image/"))
            {
                return ApiResponse<ArtistResponseDto>.ErrorResponse("Avatar must be an image file.");
            }

            string? avatarUrl = null;
            try
            {
                avatarUrl = await SaveAvatarAsync(artistCreateDto.Avatar);
            }
            catch (Exception ex)
            {
                return ApiResponse<ArtistResponseDto>.ErrorResponse($"Error saving avatar: {ex.Message}");
            }

            var artist = new Artist
            {
                Id = Guid.NewGuid(),
                Name = trimmedName,
                Bio = trimmedbio,
                AvatarUrl = avatarUrl,
                UserId = artistCreateDto.UserId
            };

            await _artistRepository.AddAsync(artist);

            var userResponse = artist.UserId.HasValue ? await _userRepository.GetUserByIdAsync(artist.UserId.Value) : null;
            var user = userResponse?.Data;

            var artistDto = new ArtistResponseDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Bio = artist.Bio,
                UserName = user?.UserName ?? "Unknown"
            };

            return ApiResponse<ArtistResponseDto>.SuccessResponse(artistDto, "Artist created successfully.");
        }



        // -------------------------------------------------------------------------- //
        // ------------------------ UpdateArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<ArtistResponseDto>> UpdateArtistAsync(Guid id, ArtistUpdateDto artistUpdateDto)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return ApiResponse<ArtistResponseDto>.ErrorResponse("Artist not found.");
            }

            // Update name if provided
            if (!string.IsNullOrWhiteSpace(artistUpdateDto.Name))
            {
                var trimmedName = StringValidator.TrimOrEmpty(artistUpdateDto.Name);
                var existingArtistByName = await _artistRepository.GetArtistByName(trimmedName);
                if (existingArtistByName != null && existingArtistByName.Id != id)
                {
                    return ApiResponse<ArtistResponseDto>.ErrorResponse("An artist with this name already exists.");
                }
                artist.Name = trimmedName;
            }

            // Update bio if provided
            if (!string.IsNullOrWhiteSpace(artistUpdateDto.Bio))
            {
                artist.Bio = StringValidator.TrimOrEmpty(artistUpdateDto.Bio);
            }

            // Update avatar if provided
            if (artistUpdateDto.Avatar != null && artistUpdateDto.Avatar.Length > 0)
            {
                // Validate file size and type for avatar
                if (artistUpdateDto.Avatar.Length > 5 * 1024 * 1024) // 5 MB limit
                {
                    return ApiResponse<ArtistResponseDto>.ErrorResponse("Avatar file size exceeds the limit of 5 MB.");
                }

                if (!artistUpdateDto.Avatar.ContentType.StartsWith("image/"))
                {
                    return ApiResponse<ArtistResponseDto>.ErrorResponse("Avatar must be an image file.");
                }

                string? newAvatarUrl = null;

                try
                {
                    newAvatarUrl = await SaveAvatarAsync(artistUpdateDto.Avatar);
                }
                catch (Exception ex)
                {
                    return ApiResponse<ArtistResponseDto>.ErrorResponse($"Error saving avatar: {ex.Message}");
                }

                if (newAvatarUrl != null)
                {
                    artist.AvatarUrl = newAvatarUrl;
                }
            }

            await _artistRepository.UpdateAsync(artist);

            var userResponse = artist.UserId.HasValue ? await _userRepository.GetUserByIdAsync(artist.UserId.Value) : null;
            var user = userResponse?.Data;

            var artistDto = new ArtistResponseDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Bio = artist.Bio,
                UserName = user?.UserName ?? "Unknown"
            };

            return ApiResponse<ArtistResponseDto>.SuccessResponse(artistDto, "Artist updated successfully.");
        }



        // -------------------------------------------------------------------------- //
        // ------------------------ DeleteArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<string>> DeleteArtistAsync(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return ApiResponse<string>.ErrorResponse("Artist not found.");
            }
            // Optionally, delete the avatar file if it exists
            if (!string.IsNullOrEmpty(artist.AvatarUrl))
            {
                var avatarPath = Path.Combine("wwwroot", artist.AvatarUrl.TrimStart('/'));

                if (File.Exists(avatarPath))
                {
                    try
                    {
                        File.Delete(avatarPath);
                    }
                    catch (Exception ex)
                    {
                        return ApiResponse<string>.ErrorResponse($"Error deleting avatar file: {ex.Message}");
                    }
                }
            }
            await _artistRepository.DeleteAsync(artist);
            return ApiResponse<string>.SuccessResponse(string.Empty, "Artist deleted successfully.");
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ SaveAvatarAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 
        private static async Task<string?> SaveAvatarAsync(IFormFile? avatarFile)
        {
            if (avatarFile == null || avatarFile.Length == 0)
                return null;

            var uploadsFolder = Path.Combine("wwwroot", "uploads", "avatars");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(avatarFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await avatarFile.CopyToAsync(stream);
            }

            return $"/uploads/avatars/{uniqueFileName}";
        }

    }
}
