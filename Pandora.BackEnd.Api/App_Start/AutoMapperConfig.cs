using Pandora.BackEnd.Business.DTO;
using Pandora.BackEnd.Business.DRO;

namespace ATPSistema.Api.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            DTOConfig.Execute();
            DROConfig.Execute();
        }
    }
}