using System.Collections.Generic;

namespace Pandora.BackEnd.Business
{
    public class BLResponse<T>
    {
        public T Data { get; set; }

        public int Count { get { return Errors.Count; } }

        public bool HasErrors { get; set; }

        public string ErrorCode { get; set; }

        public List<string> Errors { get; set; }

        public BLResponse()
        {
            Errors = new List<string>();
            HasErrors = false;
        }
    }
}
