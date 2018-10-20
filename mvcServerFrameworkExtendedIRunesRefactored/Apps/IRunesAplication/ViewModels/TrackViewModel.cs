namespace IRunesApplication.ViewModels
{
    using IRunes.Domain;
    using System.Collections.Generic;

    public class TrackViewModel
    {
        public string Id { get; set; }

        public string trackName { get; set; }

        //link for the track video
        public string trackLink { get; set; }

        public decimal trackPrice { get; set; }

        public ICollection<AlbumTrack> TrackAlbums { get; set; }
    }
}