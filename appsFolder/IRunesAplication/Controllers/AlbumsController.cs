namespace IRunesAplication.Controllers
{
    using IRunes.Services;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.WebServer.Results;

    public class AlbumsController : BaseController
    {
        public IHttpResponse All()
        {
            var albums = this.AlbumService.GetAllAlbums();
            var resultString = this.AlbumService.GetAlbumsAsString(albums);

            this.ViewBag[IRunesConstants.AlbumAllPlaceholer] = resultString != string.Empty ? resultString : IRunesConstants.NoAlbumConstant;
            return View();
        }

        // get album/create
        public IHttpResponse Create()
        {
            return View();
        }

        //post album/create
        public IHttpResponse Create(IHttpRequest request)
        {
            var album = this.AlbumService.CreateAlbum(request);

            if (album == null)
            {
                return new RedirectResult("/create");
            }

            return this.Details(album.Id);
        }

        //get
        // tupleKey is album info!!!
        // tuple value is track info!!!
        public IHttpResponse Details(string id)
        {
            var albumDetails = this.AlbumService.GetAlbumDetails(id);

            foreach (var albumDetail in albumDetails)
            {
                this.ViewBag.Add(albumDetail.Key, albumDetail.Value);
            }

            return View();
        }
    }
}