namespace Pandora.BackEnd.Common
{
    public static class ExceptionNames
    {
        public const string UNAUTHORIZED_EXCEPTION = "UnauthorizedException";
        public const string BL_VALIDATION_EXCEPTION = "BLValidationException";
        public const string VALIDATION_EXCEPTION = "ValidationException";
        public const string DATA_EXCEPTION = "DataException";
        public const string GENERAL_EXCEPTION = "GeneralException";
    }

    public static class RequestHeadersKeys
    {
        public const string USER_IDENTITY_NAME = "UserIdentityName";
        public const string USER_HOSTNAME = "ClientUserHostName";
        public const string CORRELATION_ID = "Correlation-ID";
        public const string AUTHORIZATION_TOKEN = "Authorization-Token";
    }

    public static class KeyAndNameConst
    {
        public const string VALIDATION_MESSAGES_KEY = "ValidationMessages";
    }

    public class ValidationResourceMessage
    {
        public string Key { get; set; }
        public string ResourceType { get; set; }
        public string ResourceKey { get; set; }
        public string UnlocalizedMessage { get; set; }
        public string[] ResourceFormatValues { get; set; }
    }
}
