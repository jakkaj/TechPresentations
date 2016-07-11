namespace CoreAuthenticationServer.Model.Entity
{
    public class AdSettings
    {
        public string AcrClaimType { get; set; }
        public string PolicyKey { get; set; }
        public string OIDCMetadataSuffix { get; set; }
        public string ClientId { get; set; }
        public string AadInstance { get; set; }
        public string Tenant { get; set; }
        public string RedirectUri { get; set; }
        public string SignUpPolicyId { get; set; }
        public string SignInPolicyId { get; set; }
        public string ProfilePolicyId { get; set; }


    }
}
