using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASOG_OBRAS.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string route)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var pageRoute = routeData.Values["page"].ToString();

            return route == pageRoute ? "active" : "";
        }
    }

}
// el siguiente link es la solucion para el problema de la navegación
// https://www.nuomiphp.com/eplan/en/242639.html

