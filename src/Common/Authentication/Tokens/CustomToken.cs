using System;

namespace Authentication.Tokens
{
    public class CustomToken
    {
        public string Token { get; internal set; }
        public DateTime Expiration { get; internal set; }
    }
}