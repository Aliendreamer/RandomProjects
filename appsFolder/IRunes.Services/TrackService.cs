namespace IRunes.Services
{
    using System;
    using System.Linq;
    using System.Net;
    using Contracts;
    using Data;
    using Domain;
    using SIS.HTTP.Requests;

    public class TrackService:ITrackService
    {
        public IRunesDbContext Context { get; set; }

        public TrackService()
        {
            this.Context=new IRunesDbContext();
        }

        public string CreateTrack(IHttpRequest request)
        {
            string trackName = request.FormData[IRunesConstants.TrackFormNameHolder].ToString();
            string trackLink= WebUtility.UrlDecode(request.FormData[IRunesConstants.TrackUrlFormHolder].ToString());
            decimal price = decimal.Parse(request.FormData[IRunesConstants.TrackPriceFormHolder].ToString());
            string albumIdhiddenInTheHtml = request.FormData[IRunesConstants.AlbumPlaceHolderTrackCreateForm].ToString();   
         
            var track = new Track
            {
                Name = trackName,
                Link =trackLink,
                Price = price
            };

            bool trackExists = this.Context.Tracks.Any(x => x.Name == track.Name);

            if (!trackExists)
            {
                this.Context.Tracks.Add(track);
                this.Context.SaveChanges();
            }

            var trackAlbum = new AlbumTrack
            {
                AlbumId= this.Context.Albums.Find(albumIdhiddenInTheHtml).Id,
                TrackId =this.Context.Tracks.First(x=>x.Name==track.Name).Id
            };
            bool trackAlbumExist =
                this.Context.AlbumsTracks.Any(x => x.AlbumId == trackAlbum.AlbumId && x.TrackId == trackAlbum.TrackId);

            if (!trackAlbumExist)
            {
                this.Context.Add(trackAlbum);
                this.Context.SaveChanges();
            }

            return albumIdhiddenInTheHtml;

        }


        public Track Details(string trackId)
        {
            var track = this.Context.Tracks.Find(trackId);          

            return track;
        }
    }
}
