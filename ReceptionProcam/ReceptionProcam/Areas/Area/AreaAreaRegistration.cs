using System.Web.Mvc;

namespace ReceptionProcam.Areas.Area
{
    public class AreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Area";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Area_default",
                "Area/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                 name: "Areas",
                 url: "Area/{controller}/{action}/{id}",
                 defaults: new { area = "Areas", controller = "Welcome", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "ReceptionProcam.Areas.Area.Controllers" }
             );
            
        }
    }
}