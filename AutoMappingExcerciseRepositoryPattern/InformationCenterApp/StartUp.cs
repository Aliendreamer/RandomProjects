namespace InformationCenterApp
{
    using AutoMapper;
    using Information.Data;

    public class StartUp
    {
        static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<InformationDataProfile>());

            Engine engine=new Engine();
            engine.Run();

        }
    }
}
