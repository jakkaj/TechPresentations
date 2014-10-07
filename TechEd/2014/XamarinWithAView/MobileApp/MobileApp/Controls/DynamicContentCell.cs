using System;
using Autofac;
using MobileApp.Glue;
using MobileFramework.Shared.Contract;
using Xamarin.Forms;

namespace MobileApp.Controls
{
    public class DynamicContentCell : ViewCell
    {
        protected override void OnBindingContextChanged()
        {
            var vm = BindingContext;

            //resolve a view for this bad boy
            var resolver = ContainerHost.Container.Resolve<IViewResolver>();

            var v = resolver.ResolveView(vm);

            if (v == null)
            {
                throw new InvalidOperationException("Could not find view for this view model: " + vm.GetType().FullName);
            }

            View = v;

            base.OnBindingContextChanged();
        }
    }
}
