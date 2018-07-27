namespace InformationCenterApp.Commands
{
    using Information.Data;

    public interface ICommand
    {
        UnitOfWork Db { get; set; }
        string[] Info { get; set; }
       
        void Execute();
    }
}