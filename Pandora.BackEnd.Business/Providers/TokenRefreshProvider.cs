using Microsoft.Owin.Security.Infrastructure;
using System;

namespace Pandora.BackEnd.Bussines.Providers
{
    public class TokenRefreshProvider : AuthenticationTokenProvider
    {
        public TokenRefreshProvider() { }

        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddMinutes(720));
            context.SetToken(context.SerializeTicket());
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}