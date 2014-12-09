using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileFramework.Shared.Contract
{
    public interface IViewResolver
    {
        Page Resolve(object content);
        View ResolveView(object content);
    }
}
