using System.Collections.ObjectModel;
using MiniModel.Contract.Repo;
using MobileApp.Views.Home.Clowns.List;
using MobileApp.Views.Home.Menu.List;
using MobileFramework.Shared.Base;

namespace MobileApp.Views.Home.Clowns
{
    public class ClownsViewModel : ViewModel
    {
        private readonly IClownsService _clownService;

        public ObservableCollection<ClownsListViewModel> ClownsList { get; set; }

        private ClownsListViewModel _selectedItem;

        public ClownsViewModel(IClownsService clownService)
        {
            _clownService = clownService;
            ClownsList = new ObservableCollection<ClownsListViewModel>();
        }

        public override void OnInitiated()
        {
            _init();
            base.OnInitiated();
        }

        async void _init()
        {
            var clowns = await _clownService.GetClowns();
            foreach (var item in clowns)
            {
                var i = item;
                var vm = Create<ClownsListViewModel>(_ => _.Item = i);
                ClownsList.Add(vm);
            }
        }
    }
}
