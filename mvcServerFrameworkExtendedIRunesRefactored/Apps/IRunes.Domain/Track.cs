namespace IRunes.Domain
{
    using System.Collections.Generic;

    public class Track
    {
        public Track()
        {
            this.TrackAlbums=new HashSet<AlbumTrack>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        //link for the track video
        public string Link { get; set; }

        public decimal Price { get; set; }

        public ICollection<AlbumTrack> TrackAlbums { get; set; }
    }
}
