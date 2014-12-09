using System;
using System.Collections.Generic;
using System.Net.Mime.MediaTypeNames;
using System.Text;
using Autofac;
using MiniModel.Model.Service;
using MobileApp.Views.Step_2;
using Xamarin.Forms;

namespace MobileApp.iOS.BasicForm
{
    public class BasePage : ContentPage
    {
        private readonly ISampleService _sampleService;
        private readonly ILifetimeScope _scope;

        public BasePage(ISampleService sampleService, ILifetimeScope scope)
        {
            _sampleService = sampleService;
            _scope = scope;
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
            var content = _scope.Resolve<SecondViewModel>();

            var t = content.GetType();

            var typeName = t.FullName.Replace("ViewModel", "View");
            
            var nextToType = Type.GetType(string.Format("{0}, {1}", typeName, t.Assembly.FullName));

            if (_scope.IsRegistered(nextToType))
            {
                var tView = _scope.Resolve(nextToType) as View;
                tView.BindingContext = content;
                Content = tView;
            }
        }
    }
}
