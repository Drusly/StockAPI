using MQTTnet.Client;
using Karluna.API;
using Karluna.Business.Client.MQTT.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Test.Server.Test.Mqtt.Test
{
    public class MqttTest
    {
        [Fact]
        public async Task MqttSubscriberConnectionTest()
        {
            Utils.Server.Server.StartTestServer();
            Subscriber subscriber = new Subscriber();
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 14444)
                .WithClientId(Guid.NewGuid().ToString())
                .Build();
            var result = await subscriber.ConnectClientAsync(options);
            Assert.Equal(Task.CompletedTask, result);
        }
    }
}
