using System.Web;


namespace PrototypeWebBlockchain.Functions.Filters
{
    public class AuthorizeUser : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Hash"] == null)
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller", "Member"},
                    {"Action", "Login"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}