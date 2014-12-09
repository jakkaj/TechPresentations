

using System;
using System.Collections.Generic;
using System.Windows.Input;
using MiniModel.Contract.Service;
using MiniModel.Entity;
using MiniModel.Model.Workflow;
using MobileApp.Views.Step_2;
using Xamarin.Forms;
using XamlingCore.Portable.Data.Extensions;
using XamlingCore.Portable.View.ViewModel;

namespace MobileApp.Views.Home
{
    public class HomeViewModel : XViewModel
    {
        private readonly IWorkflowService _wfService;
        public string MainText { get; set; }

        private List<XViewModel> _items;

        public ICommand NextPageCommand { get; set; }



        public HomeViewModel(IWorkflowService wfService)
        {
            wfService.SetupFlows();
            _wfService = wfService;
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

        async void _onNextPage()
        {
            var p = new Person { Id = Guid.NewGuid(), Name = "Bob Peringtonton2", Age = 40 };
            await p.Set(); 
            await p.StartWorkflow(FlowNames.UploadPerson);

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
