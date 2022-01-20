using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using EtagWebAPI.Filters;

namespace EtagWebAPI.Utils
{
    public class FilterContextUtility
    {
        public static bool IsEtagEnabledOnEndpoint(FilterContext context)
        {
            var actionDescription = context.ActionDescriptor as ControllerActionDescriptor;
            return actionDescription.MethodInfo?.GetCustomAttributes(inherit: true)?.Any(a => a.GetType().Equals(typeof(EtagAttribute))) ?? false;
        }

        public static string? GetEtagFromRequestHeader(FilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            return context.HttpContext.Request.Headers.ETag;
        }

        public static string GetRequestPath(FilterContext context)
        {
            return context.HttpContext.Request.Path.Value;
        }

        public static string GetRequestedMethod(FilterContext context)
        {
            return context.HttpContext.Request.Method;
        }
    }
}
