using System.Collections.Generic;
using System.Threading.Tasks;
using MiniModel.Entity;

namespace MiniModel.Contract.Repo
{
    public interface IClownsService
    {
        Task<List<Clown>> GetClowns();
    }
}