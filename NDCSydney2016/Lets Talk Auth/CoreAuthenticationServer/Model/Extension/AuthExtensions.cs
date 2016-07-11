using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CoreAuthenticationServer.Model.Extension
{
    public static class AuthExtensions
    {
        public static void AddB2CAuth(this IServiceCollection services, IConfigurationSection configuration)
        {
            var aadInstance = configuration["AadInstance"];
            var tenant = configuration["Tenant"];
            var oidcMetadataSuffix = configuration["OIDCMetadataSuffix"];
            var signInPolicyId = configuration["SignInPolicyId"];
            var clientId = configuration["ClientId"];

            var configMetadataUrl = String.Format(CultureInfo.InvariantCulture, aadInstance,
                tenant, "/v2.0", oidcMetadataSuffix,
                signInPolicyId);


            services.Configure<OpenIdConnectOptions>(options =>
            {
                options.AutomaticAuthenticate = true;

                options.SignInScheme = "Cookies";
                options.ClientId = clientId;
                options.Configuration = new OpenIdConnectConfiguration();

                options.ResponseType = "id_token";
                options.AuthenticationScheme = "oidc";
                options.CallbackPath = "/signin-oidc";
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(configMetadataUrl,
                    new OpenIdConnectConfigurationRetriever());

                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;

                options.Scope.Add("openid");

                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        // Get the ClaimsIdentity
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        //you could do stuff here to things. 
                        return Task.FromResult(0);
                    }
                };

            });
        }
    }
}
