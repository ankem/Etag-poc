namespace EtagWebAPI.Models
{
    public class EtagCache
    {
        public long Id { get; set; }
        public string Uri { get; set; }
        public string HttpMethod { get; set; }

        public string Etag { get; set; }
    }
}
