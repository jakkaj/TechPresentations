using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCore.Contract
{
    public interface ICacheInfo
    {
        Guid CacheId { get; set; }
        DateTime CacheDateStamp { get; set; }
        bool FromCache { get; set; }
    }
}
