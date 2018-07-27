namespace InformationCenterApp.Commands
{
    using System;
    using System.Text;
    using Information.Data;

    public class FindProjectByManagerIdCommand:ICommand
    {
        public FindProjectByManagerIdCommand(UnitOfWork db, string[] info)
        {
            Db = db;
            Info = info;
        }

        public UnitOfWork Db { get; set; }
        public string[] Info { get; set; }


        public void Execute()
        {
            int id = int.Parse(this.Info[0]);

            var projects = Db.Projects.GetProjetsByManagerId(id);

            StringBuilder sb = new StringBuilder();

            foreach (var p in projects)
            {
                sb.AppendLine($"{p.Manager.FirstName} {p.Manager.LastName} {p.StartDate.Date} {p.Name} {p.Goal}");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
