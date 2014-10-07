using System;
using MobileFramework.Shared.Base;

namespace MobileFramework.Shared.Contract
{
    public interface IViewModelFactory
    {
        ViewModel Create<T>(Action<T> initialisedCallback = null)
            where T : ViewModel;
    }
}