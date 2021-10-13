using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CustomAuthorizeAttribute.FillterAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] // it means allow this can be anotation on the class  or directly on the method as well
    public class ApiKeyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>  
        /// This will Authorize User  
        /// Ref: https://www.c-sharpcorner.com/article/how-to-override-customauthorization-class-in-net-core/
        /// </summary>  
        /// <returns></returns>  
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var checkHeader = context.HttpContext.Request.Headers.ContainsKey("Authorization");
            if (!checkHeader)
            {
                context.Result = new JsonResult("NotAuthorized")
                {
                    Value = new
                    {
                        Status = "Error",
                        Message = "Invalid Header"
                    }
                };
            }
            else
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString();
                if(token != "hello-kitty")
                {
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
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
}
