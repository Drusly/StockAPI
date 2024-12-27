using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.AspNetCore;
using Karluna.API;
using Karluna.Business.Client.MQTT.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Test.Utils.Server
{
    public static class Server
    {
        public static void StartTestServer()
        {
            using var host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(
                    o =>
                    {
                        o.ListenAnyIP(14444, l => l.UseMqtt()); // MQTT pipeline
                        o.ListenAnyIP(5000); // Default HTTP pipeline
                    });

                webBuilder.UseStartup<StartUp>();
            }).ConfigureServices(services => services.AddHostedService<Subscriber>()).Build().StartAsync();
        }
    }
}
