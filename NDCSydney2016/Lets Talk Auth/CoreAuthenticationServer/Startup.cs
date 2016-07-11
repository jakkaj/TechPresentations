using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CoreAuthenticationServer.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace CoreAuthenticationServer
{
    public class Startup
    {
        // The ACR claim is used to indicate which policy was executed
        public static string AcrClaimType = "http://schemas.microsoft.com/claims/authnclassreference";
        public static string PolicyKey = "b2cpolicy";
        public static string OIDCMetadataSuffix = "/.well-known/openid-configuration";

        // App config settings
        private static string clientId;
        private static string aadInstance;
        private static string tenant ;
        private static string redirectUri;

        // B2C policy identifiers
        public static string SignUpPolicyId;
        public static string SignInPolicyId;
        public static string ProfilePolicyId;


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

          
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });


            aadInstance = Configuration["ADSettings:AadInstance"];
            tenant = Configuration["ADSettings:Tenant"];
            OIDCMetadataSuffix = Configuration["ADSettings:OIDCMetadataSuffix"];
            SignUpPolicyId = Configuration["ADSettings:SignUpPolicyId"];
            SignInPolicyId = Configuration["ADSettings:SignInPolicyId"];
            ProfilePolicyId = Configuration["ADSettings:UserProfilePolicyId"];
            clientId = Configuration["ADSettings:ClientId"];

            var configMetadataUrl = String.Format(CultureInfo.InvariantCulture, aadInstance, 
                tenant, "/v2.0", OIDCMetadataSuffix,
                SignInPolicyId);
            

            services.Configure<OpenIdConnectOptions>(options =>
            {
                options.AutomaticAuthenticate = true;
                options.SignInScheme = "Cookies";
                options.ClientId = clientId;
                options.Configuration = new OpenIdConnectConfiguration();
                
                options.ResponseType = "id_token token";
                options.AuthenticationScheme = "oidc";
                options.CallbackPath = "/signin-oidc";
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(configMetadataUrl,
                    new OpenIdConnectConfigurationRetriever());


                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");

                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        // Get the ClaimsIdentity
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        if (identity != null)
                        {
                            // Add the Name ClaimType. This is required if we want User.Identity.Name to actually return something!
                            if (!context.Principal.HasClaim(c => c.Type == ClaimTypes.Name) &&
                                            identity.HasClaim(c => c.Type == "name"))
                                identity.AddClaim(new Claim(ClaimTypes.Name, identity.FindFirst("name").Value));
                        }

                        return Task.FromResult(0);
                    }

                };

            });

            // Add framework services.
            services.AddMvc(x =>
            {
                //x.Filters.Add(new AuthorizeFilter(
                //    new AuthorizationPolicy(
                //        requirements: new List<RolesAuthorizationRequirement>()
                //        {
                //            new RolesAuthorizationRequirement(
                //                new List<string>() { "User" })
                //        },

                //        authenticationSchemes: new List<string>() { "Cookies", "oidc" })));
            });

            services.AddOptions();
            services.Configure<AdSettings>(Configuration.GetSection("ADSettings"));


          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            IOptions<AdSettings> adSettings)
        {

            //  var settings = adSettings.Value;

            //  aadInstance = settings.AadInstance;
            //  tenant = settings.Tenant;
            //  OIDCMetadataSuffix = settings.OIDCMetadataSuffix;
            //  SignUpPolicyId = settings.SignUpPolicyId;
            //  SignInPolicyId = settings.SignInPolicyId;
            //  ProfilePolicyId = settings.ProfilePolicyId;

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
               AutomaticAuthenticate = true,
                CookieName = "MyApp",
                CookieSecure = CookieSecurePolicy.Always,
                AuthenticationScheme = "Cookies"

            });

            //  JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            ////  var config = new ConfigurationManager<OpenIdConnectConfiguration>();

            //  var configMetadataUrl = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant, "/v2.0", OIDCMetadataSuffix,
            //      new string[] {SignUpPolicyId, SignInPolicyId, ProfilePolicyId});

            //  var r = new OpenIdConnectConfigurationRetriever();

            //  var config = new ConfigurationManager<OpenIdConnectConfiguration>(configMetadataUrl, r);


            //  var connectOptions = new OpenIdConnectOptions()
            //  {
            //      AutomaticAuthenticate = true,
            //    // Authority= configMetadataUrl,
            //      ClientId = settings.ClientId,
            //      ResponseType = "id_token",
            //      AuthenticationScheme = "oidc",
            //      CallbackPath = "/signin-oidc",
            //      ConfigurationManager = config,

            //  };

            //  connectOptions.Scope.Add("openid");
            //  connectOptions.Scope.Add("profile");
            //  connectOptions.Scope.Add("email");

            //  connectOptions.Events = new OpenIdConnectEvents()
            //  {


            //      //OnTicketReceived = async y =>
            //      //{
            //      //    return base.

            //      //    //var identity = y.Principal.Identity as ClaimsIdentity;

            //      //    //var subject = identity.Claims.FirstOrDefault(z => z.Type == "sub");

            //      //    //// Do something with subject like lookup in local users DB.

            //      //    //var newIdentity = new ClaimsIdentity(
            //      //    //    y.Options.SignInScheme,
            //      //    //    "given_name",
            //      //    //    "role");

            //      //    //// Do some stuff to `newIdentity` like adding claims.

            //      //    //// Create a new ticket with `newIdentity`.

            //      //    ////.AuthenticationTicket = new AuthenticationTicket(
            //      //    ////    new ClaimsPrincipal(newIdentity),
            //      //    ////    y.AuthenticationTicket.Properties,
            //      //    ////    y.AuthenticationTicket.AuthenticationScheme);

            //      //    //await Task.FromResult(0);
            //      //},
            //      //OnTokenValidated = async y =>
            //      //{



            //      //}


            //      //OnAuthenticationValidated = async y =>
            //      //{
            //      //    var identity = y.AuthenticationTicket.Principal.Identity as ClaimsIdentity;

            //      //    var subject = identity.Claims.FirstOrDefault(z => z.Type == "sub");

            //      //    // Do something with subject like lookup in local users DB.

            //      //    var newIdentity = new ClaimsIdentity(
            //      //        y.AuthenticationTicket.AuthenticationScheme,
            //      //        "given_name",
            //      //        "role");

            //      //    // Do some stuff to `newIdentity` like adding claims.

            //      //    // Create a new ticket with `newIdentity`.
            //      //    x.AuthenticationTicket = new AuthenticationTicket(
            //      //        new ClaimsPrincipal(newIdentity),
            //      //        y.AuthenticationTicket.Properties,
            //      //        y.AuthenticationTicket.AuthenticationScheme);

            //      //    await Task.FromResult(0);
            //      //}
            //  };
            var options = app.ApplicationServices.GetRequiredService<IOptions<OpenIdConnectOptions>>();
            app.UseOpenIdConnectAuthentication(options.Value);


            //OpenIdConnectAuthenticationOptions options = new OpenIdConnectAuthenticationOptions
            //{
            //    // These are standard OpenID Connect parameters, with values pulled from web.config
            //    ClientId = clientId,
            //    RedirectUri = redirectUri,
            //    PostLogoutRedirectUri = redirectUri,
            //    Notifications = new OpenIdConnectAuthenticationNotifications
            //    {
            //        AuthenticationFailed = AuthenticationFailed,
            //        RedirectToIdentityProvider = OnRedirectToIdentityProvider,
            //        AuthorizationCodeReceived = AuthorizationCodeReceived,
            //        SecurityTokenReceived = SecurityTokenReceived,
            //        SecurityTokenValidated = SecurityTokenValidated


            //    },
            //    Scope = "openid",
            //    ResponseType = "id_token",

            //    // The PolicyConfigurationManager takes care of getting the correct Azure AD authentication
            //    // endpoints from the OpenID Connect metadata endpoint. It is included in the PolicyAuthHelpers folder.
            //    // The first parameter is the metadata URL of your B2C directory.
            //    // The second parameter is an array of the policies that your app will use.
            //    ConfigurationManager = new PolicyConfigurationManager(
            //       String.Format(CultureInfo.InvariantCulture, aadInstance, tenant, "/v2.0", OIDCMetadataSuffix),
            //       new string[] { SignUpPolicyId, SignInPolicyId, ProfilePolicyId }),

            //    // This piece is optional. It is used to display the user's name in the navigation bar.
            //    TokenValidationParameters = new TokenValidationParameters
            //    {
            //        NameClaimType = "name",
            //    },
            //};
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        

        //private async Task OnRedirectToIdentityProvider(RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        //{
        //    PolicyConfigurationManager mgr = notification.Options.ConfigurationManager as PolicyConfigurationManager;
        //    if (notification.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
        //    {
        //        OpenIdConnectConfiguration config = await mgr.GetConfigurationByPolicyAsync(CancellationToken.None, notification.OwinContext.Authentication.AuthenticationResponseRevoke.Properties.Dictionary[Startup.PolicyKey]);
        //        notification.ProtocolMessage.IssuerAddress = config.EndSessionEndpoint;
        //    }
        //    else
        //    {
        //        OpenIdConnectConfiguration config = await mgr.GetConfigurationByPolicyAsync(CancellationToken.None, notification.OwinContext.Authentication.AuthenticationResponseChallenge.Properties.Dictionary[Startup.PolicyKey]);
        //        notification.ProtocolMessage.IssuerAddress = config.AuthorizationEndpoint;
        //    }
        //}
    }
}
