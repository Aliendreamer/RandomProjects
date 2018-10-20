namespace IRunesApplication.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using ViewModels;
    using IRunes.Domain;
    using IRunes.Services;
    using IRunes.Services.Contracts;
    using SIS.Framework.Attributes.Methods;
    using SIS.Framework.ActionsResults.Base;
    using SIS.Framework.Services.Contracts;

    public class TracksController : BaseController
    {
        public ITrackService TrackService { get; }

        public TracksController(ITrackService trackService, IUserCookieService userCookieService) : base(userCookieService)
        {
            this.TrackService = trackService;
        }

        //get create
        [HttpGet]
        public IActionResult Create()
        {
            this.ViewModel.Data[IRunesConstants.AlbumPlaceHolderTrackCreateForm] =
                this.Request.QueryData[IRunesConstants.AlbumIdFromTrackCreateFormThroughRequestQeury];
            return this.View();
        }

        //post create!
        [HttpPost]
        public IActionResult Create(TrackViewModel model)
        {
            var track = Mapper.Map<Track>(model);

            string albumId = this.Request.QueryData[IRunesConstants.AlbumIdFromTrackCreateFormThroughRequestQeury].ToString();

            var trackId = this.TrackService.CreateTrack(track, albumId);

            var trackDetails = new TrackDetailsViewModel
            {
                trackId = trackId,
                albumId = albumId
            };
            return this.Details(trackDetails);
        }

        [HttpGet]
        public IActionResult Details(TrackDetailsViewModel model)
        {
            var track = this.TrackService.Details(model.trackId);
            SetttingViewDataForTrackDetails(track, model.albumId);
            this.SettingViewsBasedOnAccess();
            return this.View();
        }

        private void SetttingViewDataForTrackDetails(Track track, string albumId)
        {
            this.ViewModel.Data[IRunesConstants.TrackDetailsViewTrackNameHolder] = track.Name;
            this.ViewModel.Data[IRunesConstants.TrackDetailsViewTrackUrlHolder] = track.Link.Replace("watch?v=", "embed/");
            this.ViewModel.Data[IRunesConstants.TrackDetailsViewTrackPriceHolder] = track.Price;
            this.ViewModel.Data[IRunesConstants.TrackDetailsViewAlbumPathHolder] = albumId;

            this.SettingViewsBasedOnAccess();
        }
    }
}