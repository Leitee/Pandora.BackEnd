using Pandora.BackEnd.Data.Contracts;
using System;

namespace Pandora.BackEnd.Business.Concrets
{
    public abstract class ServicesBase
    {
        protected IApplicationUow Uow { get; set; }

        protected void HandleSVCException<T>(ref BLResponse<T> pResponse, Exception pEx)
        {
            pResponse.HasErrors = true;
            pResponse.Errors.Add("Error at Business Service");
            pResponse.Errors.Add(pEx.Message);
            if (pEx.InnerException != null)
                pResponse.Errors.Add(pEx.InnerException.Message);
        }

        protected void HandleSVCException<T>(ref BLResponse<T> pResponse, params string[] pErrors)
        {
            pResponse.HasErrors = true;
            pResponse.Errors.Add("Error at Business Service");
            pResponse.Errors.AddRange(pErrors);            
        }
    }
}
