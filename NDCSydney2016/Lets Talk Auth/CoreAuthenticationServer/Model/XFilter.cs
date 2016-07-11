using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CoreAuthenticationServer.Model
{
    /// <summary>
    /// An implementation of <see cref="IAsyncAuthorizationFilter"/> which applies a specific
    /// <see cref="AuthorizationPolicy"/>. MVC recognizes the <see cref="AuthorizeAttribute"/> and adds an instance of
    /// this filter to the associated action or controller.
    /// </summary>
    public class XAuthorizeFilter : IAsyncAuthorizationFilter
    {
        /// <summary>
        /// Initialize a new <see cref="AuthorizeFilter"/> instance.
        /// </summary>
        /// <param name="policy">Authorization policy to be used.</param>
        public XAuthorizeFilter(AuthorizationPolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }
            Policy = policy;
        }

        /// <summary>
        /// Initialize a new <see cref="AuthorizeFilter"/> instance.
        /// </summary>
        /// <param name="policyProvider">The <see cref="IAuthorizationPolicyProvider"/> to use to resolve policy names.</param>
        /// <param name="authorizeData">The <see cref="IAuthorizeData"/> to combine into an <see cref="IAuthorizeData"/>.</param>
        public XAuthorizeFilter(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authorizeData)
        {
            if (policyProvider == null)
            {
                throw new ArgumentNullException(nameof(policyProvider));
            }
            if (authorizeData == null)
            {
                throw new ArgumentNullException(nameof(authorizeData));
            }

            PolicyProvider = policyProvider;
            AuthorizeData = authorizeData;
        }

        /// <summary>
        /// The <see cref="IAuthorizationPolicyProvider"/> to use to resolve policy names.
        /// </summary>
        public IAuthorizationPolicyProvider PolicyProvider { get; }

        /// <summary>
        /// The <see cref="IAuthorizeData"/> to combine into an <see cref="IAuthorizeData"/>.
        /// </summary>
        public IEnumerable<IAuthorizeData> AuthorizeData { get; }

        /// <summary>
        /// Gets the authorization policy to be used.  If null, the policy will be constructed via
        /// AuthorizePolicy.CombineAsync(PolicyProvider, AuthorizeData)
        /// </summary>
        public AuthorizationPolicy Policy { get; private set; }

        /// <inheritdoc />
        public virtual async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var effectivePolicy = Policy ?? await AuthorizationPolicy.CombineAsync(PolicyProvider, AuthorizeData);
            if (effectivePolicy == null)
            {
                return;
            }

            // Build a ClaimsPrincipal with the Policy's required authentication types
            if (effectivePolicy.AuthenticationSchemes != null && effectivePolicy.AuthenticationSchemes.Any())
            {
                ClaimsPrincipal newPrincipal = null;
                foreach (var scheme in effectivePolicy.AuthenticationSchemes)
                {
                    var u = context.HttpContext.Authentication;
                    var u2 = context.HttpContext.User;
                    var result = await context.HttpContext.Authentication.AuthenticateAsync(scheme);
                    if (result != null)
                    {
                        newPrincipal = result;
                    }
                }
                // If all schemes failed authentication, provide a default identity anyways
                if (newPrincipal == null)
                {
                    newPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
                }
                context.HttpContext.User = newPrincipal;
            }

            // Allow Anonymous skips all authorization
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            var httpContext = context.HttpContext;
            var authService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            // Note: Default Anonymous User is new ClaimsPrincipal(new ClaimsIdentity())
            if (!await authService.AuthorizeAsync(httpContext.User, context, effectivePolicy))
            {
                context.Result = new ChallengeResult(effectivePolicy.AuthenticationSchemes.ToArray());
            }
        }
    }
}