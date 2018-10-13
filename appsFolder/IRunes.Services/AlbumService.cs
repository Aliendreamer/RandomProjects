namespace IRunes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Contracts;
    using Data;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using SIS.HTTP.Requests;

    public class AlbumService : IAlbumService
    {
        protected IRunesDbContext Context { get; set; }

        public AlbumService()
        {
            this.Context = new IRunesDbContext();
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            var albums = this.Context.Albums.Include(x => x.AlbumTracks).ToArray();

            return albums;
        }

        // we gonna return it as string so we can just pass it to the ViewBag
        public IDictionary<string, string> GetAlbumDetails(string id)
        {
            var album = this.Context.Albums.Include(x => x.AlbumTracks).ThenInclude(x => x.Track).FirstOrDefault(x => x.Id == id);
            var albumDetails = CreateAlbumDetails(album);

            return albumDetails;
        }

        //it works finaly dont use stringbuilder fot this ! god take me a while to figure it out of desperation
        private string ExtractAlbumTrackList(Album album)
        {
            if (!album.AlbumTracks.Any()) return "No tracks in the album";

            string trackInfo = "<ol>";
            foreach (var track in album.AlbumTracks)
            {
                trackInfo += ("<li><a href=" + $"/tracks/details?albumId={album.Id}" +
                            $"&trackId={track.Track.Id}>" + $"{track.Track.Name}" + "</a></li>");
            }

            trackInfo += "</ol>";

            return trackInfo;
        }

        private IDictionary<string, string> CreateAlbumDetails(Album album)
        {
            decimal price = album?.Price ?? 0.00m;
            string albumName = album.Name;
            string albumCover = album.Cover;
            string albumTracks = ExtractAlbumTrackList(album);

            var albumInfo = new Dictionary<string, string>
            {
                {
                    IRunesConstants.AlbumDetailsViewCoverHolder,albumCover
                },
                {
                    IRunesConstants.AlbumDetailsViewPriceHolder,price.ToString("f2")
                },
                {
                    IRunesConstants.AlbumDetailsViewNameHolder,albumName
                },
                {
                    IRunesConstants.AlbumDetailsViewTracksHolder,albumTracks
                },
                {
                    IRunesConstants.AlbumPlaceHolderTrackCreateForm,album.Id
                }
            };

            return albumInfo;
        }

        public Album CreateAlbum(IHttpRequest request)
        {
            string albumName = request.FormData[IRunesConstants.AlbumNameForm].ToString();
            string albumCover = WebUtility.UrlDecode(request.FormData[IRunesConstants.AlbumCoverForm].ToString());

            Album album = new Album
            {
                Name = albumName,
                Cover = albumCover
            };
            var albumExists = this.Context.Albums.Any(x => x.Name == album.Name);

            if (albumExists)
            {
                return null;
            }

            this.Context.Albums.Add(album);
            this.Context.SaveChanges();

            return album;
        }

        public string GetAlbumsAsString(IEnumerable<Album> albums)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var album in albums)
            {
                string htmlTemplate = $"<a href=" + "/albums/details" + $"?id={album.Id}>{album.Name}</a><br/>";

                sb.AppendLine(htmlTemplate);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}