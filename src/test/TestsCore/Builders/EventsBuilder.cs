using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using db_cp.Models;
using db_cp.ModelsBL;

namespace TestsCore.Builders
{
    public class EventsBuilder
    {
        public int ID;
        public int ButtonEvent_ID;
        public int? EventType_ID;
        public string? EventDescription;
        public DateTime DetectingTime;
        public DateTime? FixingTime;
        public DateTime? TimeUpdate;
        public string? User_Login;

        public EventsBuilder() { }

        public EventsBuilder withID(int id)
        {
            this.ID = id;
            return this;
        }

        public EventsBuilder withButtonEventID(int buttonEventID)
        {
            this.ButtonEvent_ID = buttonEventID;
            return this;
        }

        public EventsBuilder withEventTypeID(int eventTypeID)
        {
            this.EventType_ID = eventTypeID;
            return this;
        }

        public EventsBuilder withEventDescription(string eventDescription)
        {
            this.EventDescription = eventDescription;
            return this;
        }

        public EventsBuilder withDetectingTime(DateTime detectingTime)
        {
            this.DetectingTime = detectingTime;
            return this;
        }

        public EventsBuilder withFixingTime(DateTime fixingTime)
        {
            this.FixingTime = fixingTime;
            return this;
        }

        public EventsBuilder withTimeUpdate(DateTime timeUpdate)
        {
            this.TimeUpdate = timeUpdate;
            return this;
        }

        public EventsBuilder withUserLogin(string userLogin)
        {
            this.User_Login = userLogin;
            return this;
        }

        public Events build()
        {
            return new Events
            {
                ID = ID,
                ButtonEvent_ID = ButtonEvent_ID,
                EventType_ID = EventType_ID ?? null,
                EventDescription = EventDescription ?? null,
                DetectingTime = DetectingTime,
                FixingTime = FixingTime ?? null,
                TimeUpdate = TimeUpdate ?? null,
                User_Login = User_Login ?? null
            };
        }

        public EventsBL buildBL()
        {
            return new EventsBL
            {
                ID = ID,
                ButtonEvent_ID = ButtonEvent_ID,
                EventType_ID = EventType_ID ?? null,
                EventDescription = EventDescription ?? null,
                DetectingTime = DetectingTime,
                FixingTime = FixingTime ?? null,
                TimeUpdate = TimeUpdate ?? null,
                User_Login = User_Login ?? null
            };
        }
    }
}
