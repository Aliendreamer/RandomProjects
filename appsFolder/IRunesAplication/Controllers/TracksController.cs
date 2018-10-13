namespace IRunesAplication.Controllers
{
    using IRunes.Services;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;

    public class TracksController : BaseController
    {
        //get create
        public IHttpResponse Create(string toString)
        {
            //TODO keeping those i think i botched the details method code! check it after work !!!before the lecture!
            // string backToAlbum = $"<a href =" + $"/albums/details/?id={toString}" + "> Back To Album</a>";
            //this.ViewBag[IRunesConstants.BackToAlbumTrackCreateForm] = backToAlbum;
            this.ViewBag[IRunesConstants.TrackDetailsViewAlbumPathHolder] = $"/albums/details?id={toString}";
            this.ViewBag[IRunesConstants.AlbumPlaceHolderTrackCreateForm] = toString;
            return View();
        }

        //post create!
        public IHttpResponse Create(IHttpRequest request)
        {
            string albumid = this.TrackService.CreateTrack(request);

            var response = this.Create(albumid);

            return response;
        }

        public IHttpResponse Details(string albumId, string trackId)
        {
            var track = this.TrackService.Details(trackId);

            string backToAlbum = albumId;
            this.ViewBag[IRunesConstants.TrackDetailsViewTrackNameHolder] = track.Name;
            this.ViewBag[IRunesConstants.TrackDetailsViewTracPriceHolder] = track.Price.ToString("f2");
            this.ViewBag[IRunesConstants.TrackDetailsViewTrackUrlHolder] = track.Link.Replace("watch?v=", "embed/");
            this.ViewBag[IRunesConstants.TrackUrlSongNameHolder] = track.Name;
            this.ViewBag[IRunesConstants.TrackDetailsViewAlbumPathHolder] = backToAlbum;
            return View();
        }
    }
}