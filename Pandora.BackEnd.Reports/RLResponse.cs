using System.Collections.Generic;

namespace Pandora.BackEnd.Reports
{
    public class RLResponse
    {
        public byte[] Report { get; set; }

        public int Count { get { return Errors.Count; } }

        public bool HasErrors { get; set; }

        public string ErrorCode { get; set; }

        public List<string> Errors { get; set; }

        public RLResponse()
        {
            this.Errors = new List<string>();
            this.HasErrors = false;
        }
    }
}
