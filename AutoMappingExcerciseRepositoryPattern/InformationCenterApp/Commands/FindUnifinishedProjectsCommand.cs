namespace InformationCenterApp.Commands
{
    using System;
    using System.Text;
    using Information.Data;

    public class FindUnifinishedProjectsCommand:ICommand
    {
        public FindUnifinishedProjectsCommand(UnitOfWork db, string[] info)
        {
            Db = db;
            Info = info;
        }

        public UnitOfWork Db { get; set; }
        public string[] Info { get; set; }


        public void Execute()
        {
           var projects= Db.Projects.GetUnfinishedProjects();

            StringBuilder sb=new StringBuilder();

            foreach (var p in projects)
            {
                sb.AppendLine($"{p.Manager} {p.StartDate} {p.Name} {p.Goal}");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
