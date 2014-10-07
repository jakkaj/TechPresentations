using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MobileFramework.Shared.Base;
using MobileFramework.Shared.Contract;

namespace MobileFramework.Shared.Factory
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly ILifetimeScope _scope;

        public ViewModelFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ViewModel Create<T>(Action<T> initialisedCallback = null)
            where T : ViewModel
        {
            var obj = _scope.Resolve<T>();

            if (initialisedCallback != null)
            {
                initialisedCallback(obj);
            }

            obj.OnInitiated();

            return obj;
        }
    }
}
