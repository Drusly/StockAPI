using MQTTnet.Server;
using Karluna.Business.ServerSide.TCU.Servers.MQTT.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Business.Domain.Mqtt
{
    public  class MqttManagement
    {
        public static MqttManagement Create()
        {
            return new MqttManagement();
        }

        //public MqttServerModel ConfigureMqttServer()
        //{
        //    MqttServerModel server = new MqttServerModel();
        //    try
        //    {
        //        Broker broker = new Broker();
        //        server.mqttServer = broker.CreateMqttServer();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
            
        //    return server;
        //}
    }
}
