using MiniCore.Contract;
using Newtonsoft.Json;

namespace MiniCore.Serialiser
{
    public class JsonNETEntitySerialiser : IEntitySerialiser
    {
        public JsonSerializerSettings Settings { get; set; }

        public T Deserialise<T>(string entity) where T : class
        {
            if (Settings != null)
            {
                return JsonConvert.DeserializeObject<T>(entity, Settings);
            }

            var result = JsonConvert.DeserializeObject<T>(entity);

            return result;


        }

        public string Serialise<T>(T entity)
        {
            if (Settings != null)
            {
                return JsonConvert.SerializeObject(entity, Formatting.None, Settings);
            }
            return JsonConvert.SerializeObject(entity, Formatting.None);
        }
    }
}
