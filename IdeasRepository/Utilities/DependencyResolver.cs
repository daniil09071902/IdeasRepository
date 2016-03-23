using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using System.Web.Mvc;
using IdeasRepository.Services;
using IdeasRepository.Controllers;
using IdeasRepository.Models;

namespace IdeasRepository.Utilities
{
    public class DependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public DependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();

        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUserService>().To<UserService>().WhenInjectedInto<UserController>();
            kernel.Bind<IRecordService>().To<RecordService>().WhenInjectedInto<RecordController>();
        }
    }
}