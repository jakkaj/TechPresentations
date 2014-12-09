using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.iOS.Step_1;

namespace MiniModel.Model.Service
{
    public class SampleService : ISampleService
    {
        private readonly IAlertDisplayer _alertDisplayer;

        public SampleService(IAlertDisplayer alertDisplayer)
        {
            _alertDisplayer = alertDisplayer;
        }

        public async Task<bool> DoSomeWork()
        {
            await Task.Delay(2000);

            _alertDisplayer.DisplayAlert("Cool", "Nice message dude");

            return true;
        } 
    }
}
