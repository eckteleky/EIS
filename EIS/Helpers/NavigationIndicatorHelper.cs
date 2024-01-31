using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIS.Helpers
{
    public static class NavigationIndicatorHelper
    {
        public static string MakeActiveClass(this IUrlHelper urlHelper, string controller, string action)
        {
            try
            {
                string result = "active";
                string controllerName = "";
                if (urlHelper.ActionContext.RouteData.Values["controller"] != null)
                {
                    controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                }
                else
                {
                    //if (string.IsNullOrEmpty(controllerName)) return null;
                    return null;
                }
                string methodName = "";
                if (urlHelper.ActionContext.RouteData.Values["action"] != null)
                {
                    methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();
                }
                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    if (methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public static string MakeActiveClass(this IUrlHelper urlHelper, string controller, string action, string id)
        {
            try
            {
                string result = "active";
                string controllerName = "";
                if (urlHelper.ActionContext.RouteData.Values["controller"] != null)
                {
                    controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                }
                else
                {
                    //if (string.IsNullOrEmpty(controllerName)) return null;
                    return null;
                }
                string methodName = "";
                if (urlHelper.ActionContext.RouteData.Values["action"] != null)
                {
                    methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();
                }
                string idName = "";
                if (urlHelper.ActionContext.RouteData.Values["id"] != null)
                {
                    idName = urlHelper.ActionContext.RouteData.Values["id"].ToString();
                }
                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    if (methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                    {
                        if (idName.Equals(id, StringComparison.OrdinalIgnoreCase))
                        {
                            return result;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}