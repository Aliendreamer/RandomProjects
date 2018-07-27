namespace InformationCenterApp
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Commands;
    using Information.Data;


    public class CommandParser
    {
        public CommandParser(UnitOfWork db)
        {
            Db = db;
        }

        private ICommand Command { get; set; }

        protected UnitOfWork Db { get; set; }


        public void ParseCommand(string commandInfo)
        {
            string[] tokens = commandInfo.Split(new[] { ' ', '<', '>', ':' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string commandName = tokens[0].ToLower();

            string[] inputInfo = tokens.Skip(1).ToArray();

            Type classInfo = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(x => x.Name.ToLower().StartsWith(commandName));

            var constructorParams = classInfo?.GetConstructors().FirstOrDefault()?.GetParameters();

            if (constructorParams?.Length<1)
            {
                this.Command = (ICommand)Activator.CreateInstance(classInfo, null);
            }
            else
            {
                this.Command = (ICommand)Activator.CreateInstance(classInfo,this.Db,inputInfo);

            }

            Execute();
        }

        private void Execute()
        {
            this.Command.Execute();
        }
    }
}
