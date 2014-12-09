using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniModel.Contract.Service;
using MiniModel.Entity;
using XamlingCore.Portable.Contract.Entities;
using XamlingCore.Portable.Data.Extensions;
using XamlingCore.Portable.Workflow.Stage;

namespace MiniModel.Model.Service
{
    public class PersonService : IPersonService
    {
        private readonly IEntityManager<Person> _personManager;

        public PersonService(IEntityManager<Person> personManager)
        {
            _personManager = personManager;
        }

        public async Task<XStageResult> PrepareForUpload(Guid id)
        {
            await Task.Delay(2000);
            var p = await _personManager.Get(id);

            if (p == null)
            {
                return new XStageResult(false, id, "Could not find person");
            }

            p.Name += "_wf";
            
            await p.Set();

            return new XStageResult(true, id);
        }

        public async Task<XStageResult> DoUpload(Guid id)
        {
            await Task.Delay(2000);

            var p = await _personManager.Get(id);

            if (p == null)
            {
                return new XStageResult(false, id, "Could not find person");
            }

            p.Name += "_wf";

            await p.Set();

            return new XStageResult(true, id);
        }

        public async Task<Person> Load(Guid id)
        {
            return await _personManager.Get(id);
        }

        public async Task<Person> Save(Person p)
        {
            return await _personManager.Set(p);
        }
    }
}
