using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace db_cp.DTO
{
    public class EventsBaseDTO
    {
        public int ButtonEvent_ID { get; set; }
        public int? EventType_ID { get; set; }
        public string? EventDescription { get; set; }
        public DateTime DetectingTime { get; set; }
        public DateTime? FixingTime { get; set; }
        public DateTime? TimeUpdate { get; set; }
        public string? User_Login { get; set; }
    }

    public class EventsDTO : EventsBaseDTO
    {
        public int ID { get; set; }
    }

    public class EventsUpdateDTO
    {
        public int EventType_ID { get; set; }
        public string EventDescription { get; set; }
    }
}
