using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCore.Contract
{
    public interface IEntitySerialiser
    {
        T Deserialise<T>(string entity)
            where T : class;

        string Serialise<T>(T entity);
    }
}
