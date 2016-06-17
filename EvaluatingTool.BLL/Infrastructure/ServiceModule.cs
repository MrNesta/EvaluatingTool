using EvaluatingTool.DAL.Repositories;
using EvaluatingTool.DAL.Interfaces;
using Ninject.Modules;

namespace EvaluatingTool.BLL.Infrastructure
{
    public class ServiceModule: NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
