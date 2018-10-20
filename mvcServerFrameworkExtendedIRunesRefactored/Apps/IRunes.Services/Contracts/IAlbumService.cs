namespace IRunes.Services.Contracts
{
    using Domain;
    using System.Collections.Generic;

    public interface IAlbumService
    {
        IEnumerable<Album> GetAllAlbums();

        IDictionary<string, string> GetAlbumDetails(string id);

        IDictionary<string, string> CreateAlbumDetails(string id);

        Album CreateAlbum(Album album);

        string GetAlbumsAsString(List<Album> albums);
    }
}