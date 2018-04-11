using System;
using System.Collections.Generic;

namespace Pandora.BackEnd.Business
{
    public class BLResponse<T>
    {
        public T Data { get; set; }

        public int Count { get; set; }

        public bool HasErrors { get; set; }

        public string ErrorCode { get; set; }

        public List<string> Errors { get; set; }

        public Exception Exception { get; set; }

        public BLResponse()
        {
            this.Errors = new List<string>();
            this.HasErrors = false;
        }
    }
}
