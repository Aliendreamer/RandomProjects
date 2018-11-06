namespace Panda.Services.Interfaces
{
    using SIS.Framework.Security;
    using Infrastructure.ViewModels.InputModels;

    public interface IUserService
    {
        IdentityUser Login(LogingInputViewModel model);

        IdentityUser Register(RegisterInputViewModel model);
    }
}