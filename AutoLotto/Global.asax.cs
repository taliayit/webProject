using AutoLotto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AutoLotto
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                db.Workouts.RemoveRange(db.Workouts);
                db.SaveChanges();

                for(int i = 1; i <= 15; i++)
                {
                    db.Workouts.Add(new Workout()
                    {
                        Id = i,
                        ImageName = i + ".jpg",
                        Description = "" + i
                    });
                }       
                db.SaveChanges();
            }

        }
    }
}
