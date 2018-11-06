namespace Panda.Services
{
    using Data;

    public abstract class BaseService
    {
        public PandaDb Db { get; set; }

        protected BaseService()
        {
            this.Db = new PandaDb();
        }
    }
}