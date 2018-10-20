
namespace IRunes.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Album
    {
        public Album()
        {
            this.AlbumTracks=new HashSet<AlbumTrack>();
            this.AlbumUsers=new HashSet<UserAlbum>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        //link for the cover of album
        public string Cover { get; set; }

        [NotMapped]
        //TODO: check if it compute correctly!
        public decimal? Price => this.AlbumTracks.Sum(t => t.Track.Price) * 0.87m;

        public ICollection<UserAlbum> AlbumUsers { get; set; }

        public ICollection<AlbumTrack> AlbumTracks{ get; set; }
    }
}
