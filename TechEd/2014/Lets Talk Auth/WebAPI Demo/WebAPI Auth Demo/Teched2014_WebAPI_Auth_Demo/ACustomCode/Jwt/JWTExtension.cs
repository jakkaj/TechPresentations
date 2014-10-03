
using Microsoft.Owin.Extensions;
using Owin;

namespace Teched2014_WebAPI_Auth_Demo.ACustomCode.Jwt
{
    public static class JWTExtension
    {
        public static IAppBuilder UseJwtToken(this IAppBuilder app, JWTConfig config)
        {
            app.Use(typeof (JWTMiddleware), config);
            app.UseStageMarker(PipelineStage.Authenticate);
            return app;
        }
    }
}