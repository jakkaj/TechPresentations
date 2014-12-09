using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniModel.Contract.Service;
using XamlingCore.Portable.Workflow.Flow;

namespace MiniModel.Model.Workflow
{
    public class WorkflowService : IWorkflowService
    {
        private readonly XWorkflowHub _hub;
        private readonly IPersonService _personService;

        public WorkflowService(XWorkflowHub hub, IPersonService personService)
        {
            _hub = hub;
            _personService = personService;
        }

        public async Task SetupFlows()
        {
            await _setupAWorkflow();
        }

        async Task _setupAWorkflow()
        {
            await _hub.AddFlow(FlowNames.UploadPerson, "Uploading Person")
                .AddStage(FlowStages.PrepareForUpload, "Preparing for upload", "Failed to prepare",
                    _personService.PrepareForUpload, false, false)
                .AddStage(FlowStages.DoUpload, "Doing the upload", "Upload failed", _personService.DoUpload, false, true,
                    2)
                .Complete();
        }
    }

    public class FlowNames
    {
        public const string UploadPerson = "UploadPerson";
    }

    public class FlowStages
    {
        public const string PrepareForUpload = "PrepareForUpload";
        public const string DoUpload = "DoUpload";
    }
}
