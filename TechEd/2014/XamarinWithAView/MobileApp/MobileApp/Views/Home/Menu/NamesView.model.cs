using System.Collections.ObjectModel;
using MobileApp.Entity;
using MobileApp.Views.Home.Menu.List;
using MobileFramework.Shared.Base;

namespace MobileApp.Views.Home.Menu
{
    public class NamesViewModel : ViewModel
    {
        public ObservableCollection<ListItemViewModel> People { get; set; }

        private ListItemViewModel _selectedItem;

        public NamesViewModel()
        {
            People = new ObservableCollection<ListItemViewModel>();
        }

        public override void OnInitiated()
        {
            _init();
        }

        void _init()
        {
            for (var i = 0; i < 10; i++)
            {
                var p = new Person { Name = "Person " + i, Age = i };
                var vm = Create<ListItemViewModel>(_ => _.Item = p);

                People.Add(vm);
            }
        }

        void _onSelected()
        {
            var vm = SelectedItem;
        }

        public ListItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                _onSelected();
            }
        }

    }
}
