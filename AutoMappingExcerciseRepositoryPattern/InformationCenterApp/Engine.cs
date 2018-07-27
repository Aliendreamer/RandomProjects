namespace InformationCenterApp
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Commands;
    using Information.Data;

    public class Engine
    {
        public Engine()
        {
            this.Db=new UnitOfWork(new InformationDbContext());
            this.CommandParser=new CommandParser(Db);
        }
        private CommandParser CommandParser { get; set; }
        private UnitOfWork Db { get; set; }
        public void Run()
        {            
                while (true)
                {
                 
                    //Db.Seed();
                    Console.WriteLine(AvaiableCommandsList());
                    Console.WriteLine("Input next command:");
                    string input = Console.ReadLine();
                    this.CommandParser.ParseCommand(input);
                    
                }
            
            // ReSharper disable once FunctionNeverReturns
        }

        private string AvaiableCommandsList()
        {

            var commandTypes = Assembly.GetCallingAssembly().GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ICommand))).Select(x=>x.Name.Substring(0,x.Name.Length-7)).ToArray();

            const string info = "Avaiable commands:";

            string result=string.Format("{0} {1}",info,string.Join(" ", commandTypes));

            return result;
        }
    }
}
