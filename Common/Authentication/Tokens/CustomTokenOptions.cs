namespace Authentication.Tokens
{
    public class CustomTokenOptions
    {
        public string Issuer { get; set; } 
        public string Audience { get; set; } 
        public string SigningKey { get; set; }
        public int ExpirationLength { get; set; } 
    }
}