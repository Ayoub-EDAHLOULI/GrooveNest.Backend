using GrooveNest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<TrackGenre> TrackGenres { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ************ Entity Configuration ************ //

            var userEntity = modelBuilder.Entity<User>();
            var artistEntity = modelBuilder.Entity<Artist>();
            var albumEntity = modelBuilder.Entity<Album>();
            var trackEntity = modelBuilder.Entity<Track>();
            var playlistEntity = modelBuilder.Entity<Playlist>();
            var genreEntity = modelBuilder.Entity<Genre>();
            var commentEntity = modelBuilder.Entity<Comment>();
            var likeEntity = modelBuilder.Entity<Like>();
            var roleEntity = modelBuilder.Entity<Role>();
            var ratingEntity = modelBuilder.Entity<Rating>();


            // -------------------------------------------------- //
            // ------------ ** User Configuration ** ------------ //
            // -------------------------------------------------- //

            userEntity.HasKey(u => u.Id);

            userEntity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            userEntity.HasIndex(u => u.UserName).IsUnique();
            userEntity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            userEntity.HasIndex(u => u.Email).IsUnique();
            userEntity.Property(u => u.Password).IsRequired().HasMaxLength(100);
            userEntity.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");



            // ---------------------------------------------------- //
            // ------------ ** Artist Configuration ** ------------ //
            // ---------------------------------------------------- //

            artistEntity.HasKey(a => a.Id);

            artistEntity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            artistEntity.Property(a => a.Bio).HasMaxLength(500);
            artistEntity.Property(a => a.AvatarUrl).HasMaxLength(200);



            // --------------------------------------------------- //
            // ------------ ** Album Configuration ** ------------ //
            // --------------------------------------------------- //

            albumEntity.HasKey(a => a.Id);

            albumEntity.Property(a => a.Title).IsRequired().HasMaxLength(100);
            albumEntity.Property(a => a.ReleaseDate).IsRequired();
            albumEntity.Property(a => a.CoverUrl).HasMaxLength(200);



            // --------------------------------------------------- //
            // ------------ ** Track Configuration ** ------------ //
            // --------------------------------------------------- //

            trackEntity.HasKey(t => t.Id);

            trackEntity.Property(t => t.Title).IsRequired().HasMaxLength(100);
            trackEntity.Property(t => t.DurationSec).IsRequired();
            trackEntity.Property(t => t.AudioUrl).IsRequired().HasMaxLength(200);
            trackEntity.Property(t => t.TrackNumber).IsRequired();



            // ------------------------------------------------------ //
            // ------------ ** Playlist Configuration ** ------------ //
            // ------------------------------------------------------ //

            playlistEntity.HasKey(p => p.Id);

            playlistEntity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            playlistEntity.Property(p => p.IsPublic).IsRequired();
            playlistEntity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");



            // --------------------------------------------------- //
            // ------------ ** Genre Configuration ** ------------ //
            // --------------------------------------------------- //

            genreEntity.HasKey(g => g.Id);

            genreEntity.Property(g => g.Name).IsRequired().HasMaxLength(50);
            genreEntity.HasIndex(g => g.Name).IsUnique();



            // ----------------------------------------------------- //
            // ------------ ** Comment Configuration ** ------------ //
            // ----------------------------------------------------- //

            commentEntity.HasKey(c => c.Id);

            commentEntity.Property(c => c.Content).IsRequired().HasMaxLength(500);
            commentEntity.Property(c => c.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");



            // -------------------------------------------------- //
            // ------------ ** Like Configuration ** ------------ //
            // -------------------------------------------------- //

            likeEntity.Property(l => l.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");



            // -------------------------------------------------- //
            // ------------ ** Role Configuration ** ------------ //
            // -------------------------------------------------- //

            roleEntity.HasKey(r => r.Id);

            roleEntity.Property(r => r.Name).IsRequired().HasMaxLength(50);



            // ---------------------------------------------------- //
            // ------------ ** Rating Configuration ** ------------ //
            // ---------------------------------------------------- //

            ratingEntity.HasKey(r => r.Id);

            ratingEntity.Property(r => r.Stars).IsRequired();
            ratingEntity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");





            // ************ One to One Relationship Configuration ************ //


            // -------------------------------------------------- //
            // -------------- ** User - Artist ** --------------- //
            // -------------------------------------------------- //

            userEntity
                .HasOne(u => u.Artist)
                .WithOne(a => a.User)
                .HasForeignKey<Artist>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);




            // ************ One to Many Relationship Configuration ************ //


            // --------------------------------------------------------------------------- //
            // --------------------- One-to-Many Relationship User → Playlists ----------- //
            // --------------------------------------------------------------------------- //

            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);


            // ---------------------------------------------------------------------------- //
            // --------------------- One-to-Many Relationship User → Comments ------------- //
            // ---------------------------------------------------------------------------- //

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // -------------------------------------------------------------------------- //
            // --------------------- One-to-Many Relationship User → Rating ------------- //
            // -------------------------------------------------------------------------- //

            modelBuilder.Entity<User>()
                .HasMany(u => u.Ratings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // --------------------------------------------------------------------------- //
            // --------------------- One-to-Many Relationship Artist → Album ------------- //
            // --------------------------------------------------------------------------- //

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Albums)
                .WithOne(al => al.Artist)
                .HasForeignKey(al => al.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);


            // -------------------------------------------------------------------------- //
            // --------------------- One-to-Many Relationship Album → Track ------------- //
            // -------------------------------------------------------------------------- //

            modelBuilder.Entity<Album>()
                .HasMany(al => al.Tracks)
                .WithOne(t => t.Album)
                .HasForeignKey(t => t.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);


            // ---------------------------------------------------------------------------- //
            // --------------------- One-to-Many Relationship Track → Comment ------------- //
            // ---------------------------------------------------------------------------- //

            modelBuilder.Entity<Track>()
                .HasMany(t => t.Comments)
                .WithOne(c => c.Track)
                .HasForeignKey(c => c.TrackId)
                .OnDelete(DeleteBehavior.Cascade);


            // --------------------------------------------------------------------------- //
            // --------------------- One-to-Many Relationship Track → Rating ------------- //
            // --------------------------------------------------------------------------- //

            modelBuilder.Entity<Track>()
                .HasMany(t => t.Ratings)
                .WithOne(r => r.Track)
                .HasForeignKey(r => r.TrackId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
