using EtagWebAPI.Models;
namespace EtagWebAPI.Utils
{
    public class EtagService : IEtagService
    {
        private readonly EtagContext _context;

        public EtagService(EtagContext context)
        {
            this._context = context;
        }

        public string GetEtag(string uri, string method) => _context?.EtagItems?
            .Where(etag => etag.Uri == uri && etag.HttpMethod == method)?
            .FirstOrDefault()?.Etag;

        public string FindOrCreateEtag(string uri, string method) {
            var existingEtag = GetEtag(uri, method);
            if (existingEtag != null)
            {
                return existingEtag;
            }else
            {
                var eTagString = Guid.NewGuid().ToString();
                EtagCache newEtag = new();
                newEtag.Uri = uri;
                newEtag.HttpMethod = method;
                newEtag.Etag = eTagString;
                _context.EtagItems.Add(newEtag);
                _context.SaveChanges();
                return eTagString;
            }
            
        }
    }
}
