﻿namespace IRunes.Data
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class IRunesDbContext : DbContext
    {
        public IRunesDbContext(DbContextOptions options) : base(options)
        {
        }

        public IRunesDbContext()
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<AlbumTrack> AlbumsTracks { get; set; }

        public DbSet<UserAlbum> UsersAlbums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connectionString = @"Server=(LocalDb)\.;Database=IRunesDb;Integrated Security=True";

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAlbum>()
                .HasKey(x => new { x.AlbumId, x.UserId });

            builder.Entity<User>()
                .HasMany(x => x.UserAlbums)
                .WithOne(x => x.User);

            builder.Entity<Album>()
                .HasMany(x => x.AlbumUsers)
                .WithOne(x => x.Album);

            builder.Entity<AlbumTrack>()
                .HasKey(x => new { x.AlbumId, x.TrackId });

            builder.Entity<Album>()
                .HasMany(x => x.AlbumTracks)
                .WithOne(x => x.Album);

            builder.Entity<Track>()
                .HasMany(x => x.TrackAlbums)
                .WithOne(x => x.Track);
        }
    }
}