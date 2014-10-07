using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCore.Contract
{
    public interface ILocalStorageFileRepo
    {
        Task<bool> Delete(string fileName);

        Task<bool> Set<T>(T entity, string fileName)
            where T : class, new();

        Task<T> Get<T>(string fileName)
            where T : class, new();

        Task<List<T>> GetAll<T>(string folderName)
            where T : class, new();

        Task DeleteAll(string folderName);
    }
}
