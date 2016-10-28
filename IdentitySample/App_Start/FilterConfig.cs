using IdentitySample.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.App_Start
{
    public static class FilterConfig
    {
        public static void Configure(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorResponseFilter());
        }
    }
}