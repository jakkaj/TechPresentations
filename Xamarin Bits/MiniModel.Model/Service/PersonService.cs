using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniModel.Contract.Service;
using MiniModel.Entity;
using XamlingCore.Portable.Contract.Entities;

namespace MiniModel.Model.Service
{
    public class PersonService : IPersonService
    {
        private readonly IEntityManager<Person> _personManager;

        public PersonService(IEntityManager<Person> personManager)
        {
            _personManager = personManager;
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
