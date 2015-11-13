using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlingCore.Portable.Contract.Downloaders;
using XamlingCore.Portable.Contract.Repos.Base;
using XamlingCore.Portable.Model.Response;
using YouCore.Contract.Services;
using YouCore.Entity.IFTTT;

namespace YouCore.Model.Services
{
    public class IFTTTService : IIFTTTService
    {
        private readonly IXWebRepo<string> _pushRepo;

        public IFTTTService(IXWebRepo<string> pushRepo)
        {
            _pushRepo = pushRepo;
        }

        public async Task<XResult<bool>> RouteRequest(IFTTTPushRequest request)
        {
            var newTarget = new IFTTTPush
            {
                Value1 = request.Value1,
                Value2 = request.Value2
            };

            var result = await _pushRepo.PostResult(newTarget, request.Url);

            if (!result.IsSuccessCode)
            {
                return XResult<bool>.GetFailed(result.Result);
            }
            return new XResult<bool>(true);
        }
    }
}
