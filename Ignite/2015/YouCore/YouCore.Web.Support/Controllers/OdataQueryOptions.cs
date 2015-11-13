using System.Net;
using System.Net.Http;
using System.Web.Http.OData.Query;
using Microsoft.Data.OData;

namespace YouCore.Web.Support.Controllers
{
    public static class OdataQueryOptions
    {
        public static HttpResponseMessage ValidateODataQueryOptionsForPaging(HttpRequestMessage request, ODataQueryOptions opts)
        {
            var settings = new ODataValidationSettings()
            {
                AllowedQueryOptions = AllowedQueryOptions.Top | AllowedQueryOptions.Skip,
                MaxTop = 50
            };

            try
            {
                opts.Validate(settings);
            }

            catch (ODataException ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Filter not allowed: " + ex.Message);
            }

            return null;

        }
    }
}
