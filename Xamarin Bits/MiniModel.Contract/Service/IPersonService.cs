using System;
using System.Threading.Tasks;
using MiniModel.Entity;

namespace MiniModel.Contract.Service
{
    public interface IPersonService
    {
        Task<Person> Load(Guid id);
        Task<Person> Save(Person p);
    }
}