using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Repository.Repositories;
using GrooveNest.Service.Interfaces;
using GrooveNest.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Retrieve the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register repositories and services
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));


builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<UserRoleRepository>();
builder.Services.AddScoped<ArtistRepository>();
builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<PlaylistRepository>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserRoleService>();
builder.Services.AddScoped<ArtistApplicationService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<PlaylistService>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IArtistApplicationService, ArtistApplicationService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();



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

app.UseAuthorization();

app.MapControllers();

app.Run();
