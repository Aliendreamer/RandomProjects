namespace IRunesApplication.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using ViewModels;
    using System.Linq;
    using IRunes.Domain;
    using IRunes.Services;
    using IRunes.Services.Contracts;
    using SIS.Framework.Attributes.Methods;
    using SIS.Framework.Services.Contracts;
    using SIS.Framework.ActionsResults.Base;

    public class AlbumsController : BaseController
    {
        protected IAlbumService AlbumService { get; }

        public AlbumsController(IUserCookieService userCookieService, IAlbumService albumService)
            : base(userCookieService)
        {
            this.AlbumService = albumService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var albums = this.AlbumService.GetAllAlbums().ToList();
            var albumsAsString = this.AlbumService.GetAlbumsAsString(albums);
            this.ViewModel.Data[IRunesConstants.AlbumAllPlaceholder] = albumsAsString;
            this.SettingViewsBasedOnAccess();
            return this.View();
        }

        // get album/create
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        //post album/create
        [HttpPost]
        public IActionResult Create(AlbumCreateViewModel model)
        {
            var album = Mapper.Map<Album>(model);
            var createdAlbum = this.AlbumService.CreateAlbum(album);
            var albumViewModel = Mapper.Map<AlbumDetailsViewModel>(createdAlbum);
            return this.Details(albumViewModel);
        }

        //get
        [HttpGet]
        public IActionResult Details(AlbumDetailsViewModel albumViewModel)
        {
            if (albumViewModel.Id == null)
            {
                var result = this.AlbumService.CreateAlbumDetails(this.Request
                    .QueryData[IRunesConstants.AlbumIdFromTrackCreateFormThroughRequestQeury].ToString());

                SetttingViewDataForAlbumDetails(result);
                return this.View();
            }
            var albumDetails = this.AlbumService.GetAlbumDetails(albumViewModel.Id);

            SetttingViewDataForAlbumDetails(albumDetails);

            return this.View();
        }

        private void SetttingViewDataForAlbumDetails(IDictionary<string, string> data)
        {
            if (data[IRunesConstants.AlbumDetailsViewTracksHolder] == IRunesConstants.NoTracksCurrently)
            {
                this.ViewModel.Data[IRunesConstants.DisplayInAlbumDetails] = "none";

                this.ViewModel.Data[IRunesConstants.DisplayAnyTracks] = "none";
            }
            else
            {
                this.ViewModel.Data[IRunesConstants.DisplayInAlbumDetails] = "inline";

                this.ViewModel.Data[IRunesConstants.DisplayAnyTracks] = "inline";
            }

            foreach (var d in data)
            {
                this.ViewModel.Data[d.Key] = d.Value;
            }
            this.SettingViewsBasedOnAccess();
        }
    }
}