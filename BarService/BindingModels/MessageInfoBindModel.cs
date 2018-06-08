using System;
using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class MessageInfoBindModel
    {
        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        public string FromMailAddress { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public DateTime DateDelivery { get; set; }
    }
}