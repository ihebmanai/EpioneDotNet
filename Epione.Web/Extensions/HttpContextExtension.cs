using Epione.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epione.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static user GetUser(this HttpContextBase context)
        {
            return (user)context.Session["IUser"];
        }
    }
}