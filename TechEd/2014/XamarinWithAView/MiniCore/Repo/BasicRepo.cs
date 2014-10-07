using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniCore.Contract;

namespace MiniCore.Repo
{
    public abstract class BasicRepo<T> :IBasicRepo<T>
        where T : class, new()
    {
        private readonly string _service;

        protected BasicRepo(string service)
        {
            _service = service;
        }

        public Task<T> Post<TRequest>(TRequest entity, string extra = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> Post(string serialisedData, string extra = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string extra = null)
        {
            throw new NotImplementedException();
        }

        public abstract Task<T> Get(string extra = null);
        
        public Task<T> Put<TRequest>(TRequest entity, string extra = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> Put(string serialisedData, string extra = null)
        {
            throw new NotImplementedException();
        }

        public abstract Task<List<T>> GetList(string extra = null);
        

        public Task<List<T>> PostList(string serialisedData, string extra = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> Upload(byte[] data, string extra, string method)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> PostList<TRequest>(TRequest requestEntity, string extra = null, string verb = "POST")
        {
            throw new NotImplementedException();
        }
    }
}
