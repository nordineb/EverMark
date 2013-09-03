using Elmah;
using System.Collections.Generic;
using System.ComponentModel.Design;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EvernoteMvcExample.ElmahConfig), "Start")]

namespace EvernoteMvcExample
{
    public static class ElmahConfig
    {
        public static void Start()
        {
            ServiceCenter.Current = CreateServiceProviderQueryHandler(ServiceCenter.Current);
        }

        private static ServiceProviderQueryHandler CreateServiceProviderQueryHandler(ServiceProviderQueryHandler sp)
        {
            return context =>
            {
                var container = new ServiceContainer(sp(context));

                var config = new Dictionary<string, string>();
                config["LogId"] = Configuration.ElmahLogId;
                var log = new Elmah.Io.ErrorLog(config);

                container.AddService(typeof(ErrorLog), log);
                return container;
            };
        }
    }
}
