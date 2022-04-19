using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modal
{
    public class OperationLog
    {
        public int Id { get; set; }
        public String EventInfo { get; set; }
        public DateTime? Timestamp { get; set; }
        public String Message { get; set; }
        public String User { get; set; }
        public String Ip { get; set; }
        public String Level { get; set; }
        public String Exception { get; set; }
        public String Properties { get; set; }

        [NotMapped]
        public String EventId
        {
            get
            {
                if (String.IsNullOrEmpty(this.EventInfo))
                    return "";
                else
                {
                    JObject obj = JObject.Parse(this.EventInfo);
                    return obj.Value<String>("Id");
                }
            }
            set
            { }
        }

        [NotMapped]
        public String EventName
        {
            get
            {
                if (String.IsNullOrEmpty(this.EventInfo))
                    return "";
                else
                {
                    JObject obj = JObject.Parse(this.EventInfo);
                    return obj.Value<String>("Name");
                }
            }
            set
            { }
        }
    }
}
