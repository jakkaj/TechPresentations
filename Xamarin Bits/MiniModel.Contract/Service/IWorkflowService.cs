using System.Threading.Tasks;

namespace MiniModel.Contract.Service
{
    public interface IWorkflowService
    {
        Task SetupFlows();
    }
}