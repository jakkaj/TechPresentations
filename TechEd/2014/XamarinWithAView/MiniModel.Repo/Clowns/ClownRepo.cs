using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniCore.Repo;
using MiniModel.Contract.Repo;
using MiniModel.Entity;

namespace MiniModel.Repo.Clowns
{
    public class ClownRepo : BasicRepo<Clown>, IClownRepo
    {
        public ClownRepo() : base("SomeAPIPath")
        {

        }

        public async override Task<Clown> Get(string extra = null)
        {
            await Task.Delay(2000);
            return new Clown {Name = "It", IsNightmarish = true};
        }

        public async override Task<List<Clown>> GetList(string extra = null)
        {
            await Task.Delay(4000);

            var l = new List<Clown>();

            for (int i = 0; i < 10; i++)
            {
                l.Add(new Clown {Name = "Clown" + i, IsNightmarish = i % 2 == 0});
            }

            return l;
        }
    }
}
