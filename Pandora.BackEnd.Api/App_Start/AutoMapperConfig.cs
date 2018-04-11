using Pandora.BackEnd.Business.DTOs;

namespace ATPSistema.Api.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            DTOConfig.Execute();
        }
    }
}