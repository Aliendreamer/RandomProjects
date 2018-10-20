namespace IRunes.Services.Contracts
{
    using Domain;

    public interface IUserService
    {
        User GetUser(User user);

        bool RegisterUser(User user);
    }
}