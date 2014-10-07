using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniCore.Contract;
using MiniModel.Contract.Repo;
using MiniModel.Entity;

namespace MiniModel.Service.Clowns
{
    public class ClownsService : IClownsService
    {
        private readonly IEntityCache _entityCache;
        private readonly IClownRepo _clownRepo;

        public ClownsService(IEntityCache entityCache, IClownRepo clownRepo)
        {
            _entityCache = entityCache;
            _clownRepo = clownRepo;
        }

        public async Task<List<Clown>> GetClowns()
        {
            var clowns = await _entityCache.GetEntity<List<Clown>>
                ("ClownsList", () => _clownRepo.GetList(), TimeSpan.FromMinutes(1), true, true);

            return clowns;
        } 
    }
}
