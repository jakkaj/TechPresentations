using System.Threading.Tasks;

namespace MiniModel.Model.Service
{
    public interface ISampleService
    {
        Task<bool> DoSomeWork();
    }
}