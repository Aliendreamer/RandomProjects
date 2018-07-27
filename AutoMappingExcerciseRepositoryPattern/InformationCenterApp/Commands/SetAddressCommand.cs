namespace InformationCenterApp.Commands
{
    using System;
    using System.Linq;
    using Information.Data;

    public class SetAddressCommand:ICommand
    {
        public SetAddressCommand(UnitOfWork db, string[] info)
        {
            this.Db = db;
            this.Info = info;
        }

        public string[] Info { get; set; }
        public UnitOfWork Db { get; set; }
       

        public void Execute()
        {
            int id = int.Parse(this.Info[0]);
            string[] address = this.Info.Skip(1).ToArray();
            string addressToSet = string.Join(" ", address);

            Db.Employees.SetAddress(id,addressToSet);
 
            Db.Complete();

            Console.WriteLine("Sucessfull changed address of employee with Id: {0}",id);
        }
    }
}
