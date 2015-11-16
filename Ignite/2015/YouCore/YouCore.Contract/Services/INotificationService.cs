using System.Threading.Tasks;
using XamlingCore.Portable.Model.Response;

namespace YouCore.NET.Service
{
    public interface INotificationService
    {
        Task<XResult<bool>> SendWindowsNative(string message, string title);
    }
}