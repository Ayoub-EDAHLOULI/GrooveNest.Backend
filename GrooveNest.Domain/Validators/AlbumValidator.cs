using GrooveNest.Domain.DTOs.AlbumDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Domain.Validators
{
    public static class AlbumValidator
    {
        public static ApiResponse<AlbumResponseDto>? ValidateCreate(AlbumCreateDto albumCreateDto)
        {
            var requiredFields = new Dictionary<string, string?>
            {
                { nameof(albumCreateDto.Title), albumCreateDto.Title },
                { nameof(albumCreateDto.ReleaseDate), albumCreateDto.ReleaseDate.ToString() },
            };
            foreach (var field in requiredFields)
            {
                if (StringValidator.IsNullOrWhiteSpace(field.Value))
                {
                    return ApiResponse<AlbumResponseDto>.ErrorResponse($"{field.Key} is required.");
                }
            }
            if (albumCreateDto.Title.Length < 3)
            {
                return ApiResponse<AlbumResponseDto>.ErrorResponse("Title must be at least 3 characters long.");
            }
            return null;
        }
    }
}
