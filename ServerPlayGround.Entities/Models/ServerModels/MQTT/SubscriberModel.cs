using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPlayGround.Entities.Models.ServerModels.MQTT
{
    public class SubscriberModel
    {
        public string ClientId {  get; set; }
        public string Topic {  get; set; }
        public int? RetryInterval { get; set; }
    }
}
