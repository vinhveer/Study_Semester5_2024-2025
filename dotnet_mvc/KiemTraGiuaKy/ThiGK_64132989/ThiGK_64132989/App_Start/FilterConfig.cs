﻿using System.Web;
using System.Web.Mvc;

namespace ThiGK_64132989
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
