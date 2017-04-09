using System.Collections.Generic;

namespace ParcelLoader.Models
{
    public class RequestData
    {
        public List<RequestParams> RequestParams { get; set; }
    }

    public class RequestParams
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}