namespace NitecoTest.Configs
{
    public class JwtConfig : IJwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TimeLife { get; set; } //for second
        public string Secret { get; set; }

        public JwtConfig(string secret, string issuer, string audience, int timeLife)
        {
            this.Secret = secret;
            this.Issuer = issuer;
            this.Audience = audience;
            this.TimeLife = timeLife;
        }
        public JwtConfig GetJwtConfig()
        {
            return new JwtConfig(Secret, Issuer, Audience, TimeLife);
        }

        public void Dispose()
        {
            //NOT THING TO DO HERE
        }
    }
}
