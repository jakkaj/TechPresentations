using System.Globalization;
using System.Resources;
using Autofac;
using MiniModel.Model.Service;
using MobileApp.Views.Home;
using XamlingCore.iOS.Glue;
using XamlingCore.Platform.Shared.Glue;
using XamlingCore.Portable.Contract.Downloaders;
using XamlingCore.Portable.Glue.Glue;
using XamlingCore.Portable.Glue.Locale;
using XamlingCore.Portable.Service.Localisation;

namespace MobileApp.iOS.Glue
{
    public class ProjectGlue : iOSGlue
    {
        public override void Init()
        {
            base.Init();

           

            XCoreAutoRegistration.RegisterAssembly(Builder, typeof(HomeViewModel), false);
            XCoreAutoRegistration.RegisterAssembly(Builder, typeof(ProjectGlue));
           
            XLocale.CultureInfo = CultureInfo.CurrentCulture;//new CultureInfo("de");//
          

            var modelAssmbly = typeof(SampleService).Assembly;

            Builder.RegisterModule(new XAutofacModule(modelAssmbly, "Service"));
            Builder.RegisterModule(new XAutofacModule(modelAssmbly, "Repo"));




            //Samples for more advanced wireup 
            // Builder.RegisterAssemblyTypes(typeof(HomeWelcomeViewModel).Assembly).Where(_ => _.BaseType == typeof(TripodViewModel)).PropertiesAutowired();
           

         

            Container = Builder.Build();


        }


    }
}