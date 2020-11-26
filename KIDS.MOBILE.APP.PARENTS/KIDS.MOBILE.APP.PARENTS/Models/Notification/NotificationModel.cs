using KIDS.MOBILE.APP.PARENTS.Configurations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace KIDS.MOBILE.APP.PARENTS.Models.Notification
{
    public class NotificationModel
    {
        private string _date;
        private string _picture;

        [JsonProperty("Picture")]
        public string Picture
        {
            get => AppConstants.UriBaseWebForm + _picture;
            set => _picture = value;
        }

        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("Date")]
        public string Date
        {
            get => DateTime.Parse(_date).ToString("dd/MM/yyyy");
            set => _date = value;
        }

        [JsonProperty("Views")]
        public string Views { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}