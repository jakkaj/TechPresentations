using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MiniModel.Service.Services;

namespace MiniModel.Service.Glue
{
    public class ModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().AsImplementedInterfaces().SingleInstance();
            base.Load(builder);
        }
    }
}
