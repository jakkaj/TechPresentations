using System;
using System.Collections.Generic;
using System.Net.Mime.MediaTypeNames;
using System.Text;
using MiniModel.Model.Service;
using Xamarin.Forms;

namespace MobileApp.iOS.BasicForm
{
    public class BasePage : ContentPage
    {
        private readonly ISampleService _sampleService;

        public BasePage(ISampleService sampleService)
        {
            _sampleService = sampleService;
            _init();
        }

        void _init()
        {
            var b = new Button
            {
                Text = "Click me!"
            };
            b.Clicked += B_Clicked;

            Content = b;
        }

        private void B_Clicked(object sender, EventArgs e)
        {
            _sampleService.DoSomeWork();
        }
    }
}
