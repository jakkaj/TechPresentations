using Autofac;
using MiniCore.Cache;
using MiniCore.Contract;
using MiniCore.Serialiser;
using MiniCore.Storage.XamlingCore.Portable.Data.Repos.Base;

namespace MiniCore.Glue
{
    public class MiniCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocalStorageFileRepo>().As<ILocalStorageFileRepo>().SingleInstance();
            builder.RegisterType<JsonNETEntitySerialiser>().As<IEntitySerialiser>().SingleInstance();
            builder.RegisterType<EntityCache>().As<IEntityCache>().SingleInstance();

            base.Load(builder);
        }
    }
}
