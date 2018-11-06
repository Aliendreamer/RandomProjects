namespace Exam.App
{
    using SIS.Framework;

    public class Launcher
    {
        private static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}