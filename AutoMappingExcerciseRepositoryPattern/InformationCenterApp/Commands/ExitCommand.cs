namespace InformationCenterApp.Commands
{
    using System;
    using Information.Data;

    public class ExitCommand:ICommand
    {
      
        public string[] Info { get; set; }
        public UnitOfWork Db { get; set; }

        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}
