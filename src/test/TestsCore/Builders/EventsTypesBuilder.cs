using System.ComponentModel.DataAnnotations;
using db_cp.Models;
using db_cp.ModelsBL;

namespace TestsCore.Builders
{
    public class EventsTypesBuilder
    {
        public int ID;
        public string EventType;

        public EventsTypesBuilder() { }

        public EventsTypesBuilder withID(int id)
        {
            this.ID = id;
            return this;
        }

        public EventsTypesBuilder withEventType(string eventType)
        {
            this.EventType = eventType;
            return this;
        }

        public EventsTypes build()
        {
            return new EventsTypes { ID = ID, EventType = EventType };
        }

        public EventsTypesBL buildBL()
        {
            return new EventsTypesBL { ID = ID, EventType = EventType };
        }
    }
}
