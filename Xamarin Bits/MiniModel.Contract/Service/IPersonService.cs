using System;
using System.Threading.Tasks;
using MiniModel.Entity;
using XamlingCore.Portable.Workflow.Stage;

namespace MiniModel.Contract.Service
{
    public interface IPersonService
    {
        Task<Person> Load(Guid id);
        Task<Person> Save(Person p);
        Task<XStageResult> PrepareForUpload(Guid id);
        Task<XStageResult> DoUpload(Guid id);
    }
}