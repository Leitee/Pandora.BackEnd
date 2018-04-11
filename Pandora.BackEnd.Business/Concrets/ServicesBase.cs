using Pandora.BackEnd.Data.Contracts;

namespace Pandora.BackEnd.Business.Concrets
{
    public abstract class ServicesBase
    {
        protected IApplicationUow Uow { get; set; }
    }
}
