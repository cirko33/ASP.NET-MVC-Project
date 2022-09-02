using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using Classes;
using Project.Controllers;

namespace Project
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IKorisnikProvider, KorisnikProvider>();
            container.RegisterType<IBolnicaProvider, BolnicaProvider>();
            container.RegisterType<IDodelaProvider, DodelaProvider>();
            container.RegisterType<ILekarProvider, LekarProvider>();
            container.RegisterType<IObavestenjeProvider, ObavestenjeProvider>();
            container.RegisterType<IPacijentProvider, PacijentProvider>();
            container.RegisterType<IPregledProvider, PregledProvider>();
            container.RegisterType<ITerminProvider, TerminProvider>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IController, AdminController>("Admin");
            container.RegisterType<IController, AuthController>("Auth");
            //container.RegisterType<IController, HomeController>("Home");
            container.RegisterType<IController, LekarController>("Lekar");
            container.RegisterType<IController, PacijentController>("Pacijent");
            // e.g. container.RegisterType<ITestService, TestService>();            

            return container;
        }
    }
}