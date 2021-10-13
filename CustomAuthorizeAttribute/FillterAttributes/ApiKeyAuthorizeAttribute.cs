using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthorizeAttribute.FillterAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiKeyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>  
        /// This will Authorize User  
        /// </summary>  
        /// <returns></returns>  
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var checkHeader = context.HttpContext.Request.Headers.ContainsKey("HeadLine");
            if (!checkHeader)
            {
                context.Result = new JsonResult("NotAuthorized")
                {
                    Value = new
                    {
                        Status = "Error",
                        Message = "Invalid Token"
                    }
                };
            }
        }
    }
}
