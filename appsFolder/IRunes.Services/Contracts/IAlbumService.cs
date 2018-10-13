namespace IRunes.Services.Contracts
{
    using System.Collections.Generic;
    using Domain;
    using SIS.HTTP.Requests;

    public interface IAlbumService
    {
        IEnumerable<Album> GetAllAlbums();

        IDictionary<string, string> GetAlbumDetails(string id);

        Album CreateAlbum(IHttpRequest request);

        string GetAlbumsAsString(IEnumerable<Album> albums);
    }
}