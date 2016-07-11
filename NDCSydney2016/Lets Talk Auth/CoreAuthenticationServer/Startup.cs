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
using Microsoft.AspNetCore.Mvc;
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
        private static string tenant;
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            });

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});

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
                //options.AutomaticChallenge = true;

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
                //options.Scope.Add("profile");
                //options.Scope.Add("email");

                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    NameClaimType = "name",
                //};

                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        // Get the ClaimsIdentity
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        if (identity != null)
                        {
                            var oid = identity.Claims.FirstOrDefault(
                                _ => _.Type == "oid").Value;

                            var claim = identity.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);

                            if (claim != null)
                            {
                                identity.RemoveClaim(claim);
                            }

                            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, oid));

                            // await context.HttpContext.Authentication.SignInAsync("Cookies", context.Principal);
                            // //await context.HttpContext.Authentication.SignInAsync("oidc", context.Principal);

                            // context.HttpContext.User = context.Principal;

                            // var c = context.Request.HttpContext;
                            // //identity.Label = "Testing 1232";

                            // var newIdentity = new ClaimsIdentity(
                            //context.Ticket.AuthenticationScheme,
                            //"given_name",
                            //"role");

                            //      context.Ticket = new AuthenticationTicket(
                            //new ClaimsPrincipal(newIdentity),
                            //context.Ticket.Properties,
                            //"Cookies");

                            //var oid = identity.Claims.FirstOrDefault(
                            //    _ => _.Type == "oid").Value;

                            //var claim = identity.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);
                            //if (claim != null)
                            //{
                            //    identity.RemoveClaim(claim);
                            //}

                            //var claimsub = identity.Claims.FirstOrDefault(_ => _.Type == "sub");
                            //if (claimsub != null)
                            //{
                            //    identity.RemoveClaim(claim);
                            //}

                            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, oid));
                            //identity.AddClaim(new Claim("sub", oid));

                            //identity.AddClaim(new Claim(ClaimTypes.Name, "Jordan knight"));
                            //identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                            //var c = "1";

                            // Add the Name ClaimType. This is required if we want User.Identity.Name to actually return something!
                            //if (!context.Principal.HasClaim(c => c.Type == ClaimTypes.Name) &&
                            //                identity.HasClaim(c => c.Type == "name"))
                            //    identity.AddClaim(new Claim(ClaimTypes.Name, identity.FindFirst("name").Value));
                            //if (!context.Principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) &&
                            //                identity.HasClaim(c => c.Type == "name"))
                            //    identity.AddClaim(new Claim(ClaimTypes.Name, identity.FindFirst("name").Value));
                            //context.HttpContext.User = context.Principal;
                            //await context.HttpContext.Authentication.SignInAsync("Cookies", context.Principal);
                        }

                        return Task.FromResult(0);


                    },
                    OnTokenValidated = async context =>
                    {
                        var t = context.SecurityToken;
                    },


                };

            });

            // Add framework services.
            services.AddMvc();
            //.AddMvc(x =>
            //{
            //    x.Filters.Add(new XAuthorizeFilter(
            //        new AuthorizationPolicy(
            //            requirements: new List<RolesAuthorizationRequirement>()
            //            {
            //                new RolesAuthorizationRequirement(
            //                    new List<string>() { "User" })
            //            },
            //            authenticationSchemes: new List<string>() { "Cookies", "oidc" })));
            //});

            //services.AddAuthorization(options =>
            //{

            //    //var policies = options.DefaultPolicy;

            //    //options.AddPolicy("all", policy => policy.RequireClaim(ClaimTypes.NameIdentifier));
            //    //options.DefaultPolicy = options.GetPolicy("all");
            //    //options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("objectId"));
            //});



            //  services.AddIdentity<>()
            services.AddOptions();
            services.Configure<AdSettings>(Configuration.GetSection("ADSettings"));




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IOptions<AdSettings> adSettings)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            var options = app.ApplicationServices.GetRequiredService<IOptions<OpenIdConnectOptions>>();
            app.UseOpenIdConnectAuthentication(options.Value);

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
