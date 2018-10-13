namespace IRunes.Services.Contracts
{
    using Domain;
    using SIS.HTTP.Requests;

    public interface IUserService
    {
        User GetUser(string name,string password);

        bool RegisterUser(IHttpRequest request);

        //TODO: i don't remember why i put it here? investigate!
        bool CheckUserExist(User user);
    }
}