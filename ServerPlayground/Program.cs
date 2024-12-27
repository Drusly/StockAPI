using Karluna.API;
using MQTTnet.AspNetCore;
using Karluna.Business.Client.MQTT.Subscriber;
using Microsoft.Extensions.Options;
using System.Net;

/*For generic Host and create mqtt port communication*/
var builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseKestrel(
        o =>
        {
            //o.ListenAnyIP(14444, l => l.UseMqtt()); // MQTT pipeline
            o.ListenAnyIP(5000); // Default HTTP pipeline
            o.Listen(IPAddress.Loopback, 5000, listenOptions =>
            {
                listenOptions.UseHttps("C:\\nginx-1.26.2\\ssl\\__karluna_com_tr-crt.pfx", "__karluna_com_tr-crt");
            });
        });

    webBuilder.UseIIS();
    webBuilder.UseStartup<StartUp>();
}).ConfigureServices(services => services.AddHostedService<Subscriber>()).Build(); //

builder.Run();
