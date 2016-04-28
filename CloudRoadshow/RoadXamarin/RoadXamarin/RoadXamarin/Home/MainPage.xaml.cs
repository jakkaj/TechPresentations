using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace RoadXamarin.Home
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var result = await MarkdownGetter.GetMarkdown(ThisUrl.Text);

            var modelResult = JsonConvert.DeserializeObject<UrlModel>(result);

            MyBrowser.Source = new HtmlWebViewSource {Html = modelResult.Result};
        }
    }
}
