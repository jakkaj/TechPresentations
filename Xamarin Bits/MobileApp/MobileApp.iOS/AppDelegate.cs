using System.Globalization;
using Autofac;
using MobileApp.iOS.BasicForm;
using MobileApp.iOS.Glue;
using MobileApp.Views.Home;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using XamlingCore.iOS;
using XamlingCore.Portable.Glue.Locale;
using XamlingCore.XamarinThings.Content.Navigation;
using XamlingCore.XamarinThings.Frame;

namespace MobileApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        private XiOSCore<XRootFrame, XNavigationPageTypedViewModel<HomeViewModel>, ProjectGlue> xCore;

        private ProjectGlue _glue;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            XLocale.CultureInfo = new CultureInfo(NSLocale.PreferredLanguages[0]);

            Forms.Init();

            //_glue = new ProjectGlue();
            // _glue.Init();
            // var page = _glue.Container.Resolve<BasePage>();

            // window = new UIWindow(UIScreen.MainScreen.Bounds);

            // window.RootViewController = page.CreateViewController();
            // window.MakeKeyAndVisible();






            //boot using standard navigation page 
            xCore = new XiOSCore<XRootFrame, XNavigationPageTypedViewModel<HomeViewModel>, ProjectGlue>();
            xCore.Init();
            // var c = XLocale.CultureInfo;


            ////boot using master detail setup
            //xCore = new XiOSCore<XRootFrame, RootMasterDetailViewModel, ProjectGlue>();
            //xCore.Init();

            return true;
        }
    }
}
