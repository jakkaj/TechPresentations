using System.Threading.Tasks;
using XamlingCore.Portable.Model.Response;
using YouCore.Entity.IFTTT;

namespace YouCore.Contract.Services
{
    public interface IIFTTTService
    {
        Task<XResult<bool>> RouteRequest(IFTTTPushRequest request);
    }
}