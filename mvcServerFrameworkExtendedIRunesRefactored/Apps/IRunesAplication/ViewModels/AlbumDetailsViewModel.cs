namespace IRunesApplication.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using IRunes.Domain;

    public class AlbumDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        //link for the cover of album
        public string Cover { get; set; }

        //TODO: check if it compute correctly!
        public decimal Price => this.AlbumTracks.Sum(t => t.Track.Price) * 0.87m;

        public ICollection<UserAlbum> AlbumUsers { get; set; }

        public ICollection<AlbumTrack> AlbumTracks { get; set; }
    }
}