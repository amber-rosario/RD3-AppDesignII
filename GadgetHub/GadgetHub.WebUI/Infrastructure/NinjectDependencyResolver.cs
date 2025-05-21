using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Ninject;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using Moq;
using GadgetHub.Domain.Concrete;
using System.Configuration;
using GadgetHub.WebUI.Infrastructure.Abstract;
using GadgetHub.WebUI.Infrastructure.Concrete;

namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            mykernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type myserviceType)
        {
            return mykernel.TryGet(myserviceType);
        }

        public IEnumerable<object> GetServices(Type myserviceType)
        {
            return mykernel.GetAll(myserviceType);
        }

        private void AddBindings()
        {

            mykernel.Bind<IGadgetRepository>().To<EFGadgetRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse
                (ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            mykernel.Bind<IOrderProcessor>()
                            .To<EmailOrderProcessor>()
                            .WithConstructorArgument("settings", emailSettings);

            // Authentication
            mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }        
    }
}