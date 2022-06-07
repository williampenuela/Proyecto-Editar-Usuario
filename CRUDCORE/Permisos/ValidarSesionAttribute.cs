using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDCORE.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {

        public int Acceso { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = filterContext.HttpContext.Session.GetInt32("idUsuario");
            filterContext.HttpContext.Session.Clear();
            if (id == null)
            {
                filterContext.Result = new RedirectResult("~/Mantenedor/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
