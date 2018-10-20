namespace IRunes.Services.Contracts
{
    using Domain;

    public interface ITrackService
    {
        string CreateTrack(Track track, string id);

        Track Details(string trackId);
    }
}