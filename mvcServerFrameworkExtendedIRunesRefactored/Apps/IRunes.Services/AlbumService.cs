namespace IRunes.Services
{
    using Data;
    using Domain;
    using System;
    using Contracts;
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

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

        // we gonna return it as string so we can just pass it to the view data
        public IDictionary<string, string> GetAlbumDetails(string id)
        {
            var album = this.Context.Albums.Include(x => x.AlbumTracks).ThenInclude(x => x.Track).FirstOrDefault(x => x.Id == id);
            var albumDetails = CreateAlbumDetails(album);

            return albumDetails;
        }

        //it works finally don't use StringBuilder fot this ! god take me a while to figure it out of desperation
        private string ExtractAlbumTrackList(Album album)
        {
            if (!album.AlbumTracks.Any()) return IRunesConstants.NoTracksCurrently;

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
            //TODO: short work around for now as binding model has problem with calculated properties it seems

            decimal price = album?.Price ?? 0.00m;
            string albumName = album.Name;
            string albumCover = album.Cover;
            var albumTracks = ExtractAlbumTrackList(album);

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

        public Album CreateAlbum(Album album)
        {
            album.Cover = WebUtility.UrlDecode(album.Cover);

            var albumExists = this.Context.Albums.Any(x => x.Name == album.Name);

            if (albumExists)
            {
                return null;
            }

            this.Context.Albums.Add(album);
            this.Context.SaveChanges();

            return album;
        }

        public string GetAlbumsAsString(List<Album> albums)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var album in albums)
            {
                string htmlTemplate = $"<a href=" + "/albums/details" + $"?id={album.Id}>{album.Name}</a><br/>";

                sb.AppendLine(htmlTemplate);
                sb.Append(Environment.NewLine);
            }

            if (!albums.Any())
            {
                string htmlTemplate = $"<a href=" + "/" + ">" + $"{IRunesConstants.NoAlbumConstant}" + "</a><br/>";
                sb.AppendLine(htmlTemplate);
            }
            return sb.ToString();
        }

        public IDictionary<string, string> CreateAlbumDetails(string id)
        {
            var album = this.Context.Albums.Find(id);
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
    }
}