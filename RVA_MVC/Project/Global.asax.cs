using Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dapper;

namespace Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            InicijalizacijaZaProlaz();
            DodelaProviderSingleton.SetDodelaProvider(new DodelaProvider(new DodelaLekara()));
            SubjectSingleton.SetSubject(new ConcreteSubject());
            //ConnectionString.Write("Server=127.0.0.1;Port=3306;Database=rva;user id=rvaprojekat;password=rvaprojekat");
            KorisnikStrategy.SetStrategy(new KorisnikProvider());
            ObavestenjeSingleton.SetObavestenje(new ObavestenjeProvider());
            Bootstrapper.Initialise();
        }

        private void InicijalizacijaZaProlaz()
        {
            var read = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Podaci.txt").Replace("\n", "");
            foreach (var item in read.Split(';'))
            {
                if(!string.IsNullOrWhiteSpace(item))
                    using (IDbConnection c = new MySqlConnection(ConnectionString.Read()))
                    {
                        c.Execute(item);
                    }
            }
        }
    }
}
