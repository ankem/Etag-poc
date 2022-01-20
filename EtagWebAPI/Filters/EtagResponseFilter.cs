using Microsoft.AspNetCore.Mvc.Filters;
using EtagWebAPI.Utils;

namespace EtagWebAPI.Filters
{

    public class EtagResponseFilter : IActionFilter
    {
        private readonly IEtagService _service;
        public EtagResponseFilter(IEtagService etagService)
        {
            this._service = etagService;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (FilterContextUtility.IsEtagEnabledOnEndpoint(context))
            {
                var requestedUri = FilterContextUtility.GetRequestPath(context);
                var requestedHttpMethod = FilterContextUtility.GetRequestedMethod(context);
                context.HttpContext.Response.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.ETag, this._service.FindOrCreateEtag(requestedUri, requestedHttpMethod));
            }
        }

        
    }
}
