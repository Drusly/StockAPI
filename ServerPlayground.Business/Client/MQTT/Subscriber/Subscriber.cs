using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using ServerPlayGround.Entities.Models.ServerModels.MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Business.Client.MQTT.Subscriber
{
    public class Subscriber : IHostedService
    {
        MqttFactory _mqttFactory;
        IMqttClient _mqttClient;
        MqttClientOptions _options;

        public Subscriber()
        {
            _mqttFactory = new MqttFactory();
            _options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 14444)
                .WithClientId("2")
                .Build();
            _mqttClient = _mqttFactory.CreateMqttClient();
            _mqttClient.ApplicationMessageReceivedAsync += MessageReceive;
            _mqttClient.DisconnectedAsync += Reconnect;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ConnectClientAsync(_options);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        public Task MessageReceive(MqttApplicationMessageReceivedEventArgs e)
        {
            var text = System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine("Subscriber: " + text);
            return Task.CompletedTask;
        }

        public async Task<Task> ConnectClientAsync(MqttClientOptions options)
        {
            MqttClientConnectResult conres = new MqttClientConnectResult();
            try
            {
                conres = await _mqttClient.ConnectAsync(options);
                var subres = await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("TCU").Build());
            }
            catch (Exception ex) 
            { 
            
            }
            if (conres.ResultCode == MqttClientConnectResultCode.Success)
                return Task.CompletedTask;
            else
                return Task.FromResult(conres);
        }

        public async Task Reconnect(MqttClientDisconnectedEventArgs e)
        {
            await _mqttClient.ReconnectAsync();
        }
    }
}
