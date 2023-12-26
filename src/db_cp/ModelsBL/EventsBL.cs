using System;

namespace db_cp.ModelsBL
{
    public class EventsBL
    {
        public int ID { get; set; }
        public int ButtonEvent_ID { get; set; }
        public int? EventType_ID { get; set; }
        public string? EventDescription { get; set; }
        public DateTime DetectingTime { get; set; }
        public DateTime? FixingTime { get; set; }
        public DateTime? TimeUpdate { get; set; }
        public string? User_Login { get; set; }
    }
}
