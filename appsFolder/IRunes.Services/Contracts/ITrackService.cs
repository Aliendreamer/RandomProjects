namespace IRunes.Services.Contracts
{

    using Domain;
    using SIS.HTTP.Requests;

    public interface ITrackService
    {
        string CreateTrack(IHttpRequest request);

        Track Details(string trackId);
    }
}