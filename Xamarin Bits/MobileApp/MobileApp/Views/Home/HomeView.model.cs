

using System.Collections.Generic;
using System.Windows.Input;
using MobileApp.Views.Step_2;
using Xamarin.Forms;
using XamlingCore.Portable.View.ViewModel;

namespace MobileApp.Views.Home
{
    public class HomeViewModel : XViewModel
    {
        public string MainText { get; set; }

        private List<XViewModel> _items;

        public ICommand NextPageCommand { get; set; }

        

        public HomeViewModel()
        {
            MainText = "Jordan was ere.";
            NextPageCommand = new Command(_onNextPage);
        }

        public override void OnInitialise()
        {

            var l = new List<XViewModel>();

            l.Add(CreateContentModel<FirstViewModel>());
            l.Add(CreateContentModel<SecondViewModel>());

            Items = l;

            base.OnInitialise();
        }

        void _onNextPage()
        {
            NavigateTo<AnotherPageViewModel>();
        }

        public List<XViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
    }
}
