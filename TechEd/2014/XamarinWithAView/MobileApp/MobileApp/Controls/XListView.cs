using Xamarin.Forms;

namespace MobileApp.Controls
{
    public class XListView : ListView
    {
        public XListView()
        {
            ItemTemplate = new DataTemplate(typeof(DynamicContentCell));
        }
    }
}
