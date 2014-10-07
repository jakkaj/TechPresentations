using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCore.Contract
{
    public interface IBasicRepo<TEntity> where TEntity : class, new()
    {
        Task<TEntity> Post<TRequest>(TRequest entity, string extra = null);
        Task<TEntity> Post(string serialisedData, string extra = null);

        Task<bool> Delete(string extra = null);
        Task<TEntity> Get(string extra = null);

        Task<TEntity> Put<TRequest>(TRequest entity, string extra = null);
        Task<TEntity> Put(string serialisedData, string extra = null);

        Task<List<TEntity>> GetList(string extra = null);
        Task<List<TEntity>> PostList(string serialisedData, string extra = null);
        
        Task<TEntity> Upload(byte[] data, string extra, string method);
        Task<List<TEntity>> PostList<TRequest>(TRequest requestEntity, string extra = null, string verb = "POST");
    }
}
