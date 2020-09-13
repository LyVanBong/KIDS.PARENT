using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KIDS.MOBILE.APP.PARENTS.Models.Notification
{
    public class NotificationModel
    {
        [JsonProperty("Picture")]
        public string Picture { get; set; }
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("Date")]
        public string Date { get; set; }
        [JsonProperty("Views")]
        public string Views { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}