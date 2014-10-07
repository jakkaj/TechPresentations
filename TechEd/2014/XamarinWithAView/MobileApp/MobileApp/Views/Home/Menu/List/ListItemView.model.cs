using MobileApp.Entity;
using MobileFramework.Shared.Base;

namespace MobileApp.Views.Home.Menu.List
{
    public class ListItemViewModel : ItemViewModel<Person>
    {
        public ListItemViewModel()
        {
            Title = "MenuOpt";
        }
    }
}
