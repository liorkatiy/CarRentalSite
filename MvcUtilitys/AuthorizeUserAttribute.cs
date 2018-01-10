using Entity;
using System;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
namespace MvcUtilitys
{
    /// <summary>
    /// Check If The User Is Logged And the Authorazion
    /// </summary>
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Lowest Premission Required To Enter
        /// </summary>
        public Premission Premission { get; set; }

        /// <summary>
        /// Inverse The Premission Only Users Lower Can Enter
        /// </summary>
        public bool Inverse { get; set; }

        UserAuth state;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            state = httpContext.Session.VerifyPremission(Premission);
            return (state == UserAuth.yes) != Inverse;
        }

        //set the return url
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (state == UserAuth.noLogin)
            {
                //user isnt login redirect to login page
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "Login" },
                    { "returnUrl", filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) }
                });
            }
            else //user is logged and is unauthorize
                filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}