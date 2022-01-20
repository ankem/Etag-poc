namespace EtagWebAPI.Utils
{
    public interface IEtagService
    {
        public string GetEtag(String uri, String method);
        public string FindOrCreateEtag(string uri, string method);
    }
}
