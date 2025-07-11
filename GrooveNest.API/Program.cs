﻿using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Repository.Repositories;
using GrooveNest.Service.Interfaces;
using GrooveNest.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Domain.DTOs.LoginDTOs;

var builder = WebApplication.CreateBuilder(args);


// Retrieve the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ✅ Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:5173", "http://192.168.100.209:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });



// Register repositories and services
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));


builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<UserRoleRepository>();
builder.Services.AddScoped<ArtistRepository>();
builder.Services.AddScoped<ArtistApplicationRepository>();
builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<PlaylistRepository>();
builder.Services.AddScoped<ArtistRepository>();
builder.Services.AddScoped<TrackRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<TrackGenreRepository>();
builder.Services.AddScoped<LikeRepository>();
builder.Services.AddScoped<PlaylistTrackRepository>();
builder.Services.AddScoped<RatingRepository>();
builder.Services.AddScoped<CommentRepository>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IArtistApplicationRepository, ArtistApplicationRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ITrackGenreRepository, TrackGenreRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IPlaylistTrackRepository, PlaylistTrackRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserRoleService>();
builder.Services.AddScoped<ArtistApplicationService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<PlaylistService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<TrackService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<TrackGenreService>();
builder.Services.AddScoped<LikeService>();
builder.Services.AddScoped<PlaylistTrackService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<JwtTokenService>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IArtistApplicationService, ArtistApplicationService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ITrackGenreService, TrackGenreService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IPlaylistTrackService, PlaylistTrackService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAuthenticationService<LoginDto, UserAuthResponseDto>, AuthenticationService>();



var app = builder.Build();


// Seed roles at application startup
using (var scope = app.Services.CreateScope())
{
    var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
    await roleService.SeedRolesAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Add this to serve files from wwwroot
app.UseStaticFiles();

// ✅ CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
