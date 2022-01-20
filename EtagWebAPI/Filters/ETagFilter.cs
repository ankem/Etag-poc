using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using EtagWebAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EtagWebAPI.Filters
{
    public class ETagFilter : IResourceFilter
    {
        private readonly IEtagService _service;
        public ETagFilter(IEtagService etagService)
        {
            this._service = etagService;
        }
       
       

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var requestedEtag = FilterContextUtility.GetEtagFromRequestHeader(context);
            if (requestedEtag!=null && FilterContextUtility.IsEtagEnabledOnEndpoint(context))
            {
                var requestedUri = FilterContextUtility.GetRequestPath(context);
                var requestedHttpMethod = FilterContextUtility.GetRequestedMethod(context);
                string storedEtag = _service.GetEtag(requestedUri, requestedHttpMethod);
                if(storedEtag == requestedEtag)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = 304
                    };
                }
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }


    }
}
