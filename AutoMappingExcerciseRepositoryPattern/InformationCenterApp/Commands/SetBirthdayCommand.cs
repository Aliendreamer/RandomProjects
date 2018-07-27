namespace InformationCenterApp.Commands
{
    using System;
    using System.Globalization;
    using Information.Data;
 
    public class SetBirthdayCommand:ICommand
    {
        public SetBirthdayCommand(UnitOfWork db, string[] info)
        {
            this.Db = db;
            this.Info = info;
        }

        public string[] Info { get; set; }
        public UnitOfWork Db { get; set; }

        public void Execute()
        {
            string format = "dd-MM-yyyy";
            DateTime birthday=DateTime.ParseExact(this.Info[2],format,CultureInfo.InvariantCulture);
            int id = int.Parse(this.Info[0]);

            
            Db.Employees.SetBirthday(id,birthday);
            Db.Complete();
            Console.WriteLine("Sucessfuly passed the changes");

        }
    }
}
