namespace IRunes.Domain
{
    public class AlbumTrack
    {
        public string AlbumId { get; set; }
        public Album Album { get; set; }


        public string TrackId { get; set; }
        public Track Track { get; set; }
    }
}
