using EvaluatingTool.BLL.Interfaces;
using EvaluatingTool.BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EvaluatingTool.WEB.Util
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBinding();
        }

        private void AddBinding()
        {
            kernel.Bind<ITestService>().To<TestService>();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}