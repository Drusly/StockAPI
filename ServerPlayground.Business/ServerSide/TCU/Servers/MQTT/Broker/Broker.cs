using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karluna.Business.ServerSide.TCU.Servers.MQTT.Broker
{
    public class Broker : IBroker
    {
        MqttServer _mqttServer;

        public MqttServerOptions CreateMqttServer()
        {
            MqttServerOptionsBuilder builder = new MqttServerOptionsBuilder();
            var options = builder.WithConnectionBacklog(100)
                    .WithDefaultEndpointPort(14444)
                    .Build();
            return options;
        }

        public Task interceptingPublishEvent(InterceptingPublishEventArgs e)
        {
            Console.WriteLine("Broker: " + System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload));
            if (e.ApplicationMessage.Topic == "TCU")
                e.ProcessPublish = true;
            else
                e.ProcessPublish = false;

            return Task.CompletedTask;
        }

        public Task interceptingSubscriptionEvent(InterceptingSubscriptionEventArgs e)
        {
            if (e.TopicFilter.Topic == "TCU")
                e.ProcessSubscription = true;
            else
                e.ProcessSubscription = false;

            return Task.CompletedTask;
        }

        //public Task StartAsync(CancellationToken cancellationToken)
        //{

        //    try
        //    {

        //        _mqttServer = new MqttFactory().CreateMqttServer(options);
        //        //_mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedHandlerDelegate(e => {
        //        //    Console.WriteLine(e.ClientId.ToString());
        //        //});
        //        _mqttServer.InterceptingPublishAsync += interceptingPublishEvent;
        //        _mqttServer.StartAsync();
        //        Console.WriteLine(_mqttServer.IsStarted.ToString());
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    _mqttServer.StopAsync();
        //    return Task.CompletedTask;
        //}
    }
}
