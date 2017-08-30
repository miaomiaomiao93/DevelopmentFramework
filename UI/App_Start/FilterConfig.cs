﻿using System.Web;
using System.Web.Mvc;
using UI.Services;

namespace UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionAttribute());
        }
    }
}