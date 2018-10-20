namespace IRunes.Services
{
    using Data;
    using Domain;
    using Contracts;
    using System.Net;
    using System.Linq;

    public class TrackService : ITrackService
    {
        public IRunesDbContext Context { get; set; }

        public TrackService()
        {
            this.Context = new IRunesDbContext();
        }

        public string CreateTrack(Track track, string albumId)
        {
            track.Link = WebUtility.UrlDecode(track.Link);
            //decimal price = decimal.Parse(track.Price);
            string albumIdhiddenInTheHtml = albumId;

            bool trackExists = this.Context.Tracks.Any(x => x.Name == track.Name);

            if (!trackExists)
            {
                this.Context.Tracks.Add(track);
                this.Context.SaveChanges();
            }

            var trackAlbum = new AlbumTrack
            {
                AlbumId = this.Context.Albums.Find(albumIdhiddenInTheHtml).Id,
                TrackId = this.Context.Tracks.First(x => x.Name == track.Name).Id
            };
            bool trackAlbumExist =
                this.Context.AlbumsTracks.Any(x => x.AlbumId == trackAlbum.AlbumId && x.TrackId == trackAlbum.TrackId);

            if (!trackAlbumExist)
            {
                this.Context.Add(trackAlbum);
                this.Context.SaveChanges();
            }

            return track.Id;
        }

        public Track Details(string trackId)
        {
            var track = this.Context.Tracks.Find(trackId);

            return track;
        }
    }
}