using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniModel.Entity;
using MobileFramework.Shared.Base;

namespace MobileApp.Views.Home.Clowns.List
{
    public class ClownsListViewModel : ItemViewModel<Clown>
    {
        public ClownsListViewModel()
        {
            Title = "MenuOpt";
        }
    }
}
