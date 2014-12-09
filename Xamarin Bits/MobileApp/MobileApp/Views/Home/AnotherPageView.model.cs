using System;
using System.Threading.Tasks;
using XamlingCore.Portable.View.ViewModel;

namespace MobileApp.Views.Home
{
    public class AnotherPageViewModel : XViewModel
    {
        private string _mainText;

        public AnotherPageViewModel()
        {
            _showTime();
        }

        async void _showTime()
        {
            while (true)
            {
                MainText = DateTime.Now.ToString();
                await Task.Delay(1000);
            }
        }

        public string MainText
        {
            get { return _mainText; }
            set
            {
                _mainText = value;
                OnPropertyChanged();
            }
        }
    }
}
