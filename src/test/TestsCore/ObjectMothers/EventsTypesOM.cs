using TestsCore.Builders;

namespace TestsCore.ObjectMothers
{
    public class EventsTypesOM
    {
        public EventsTypesBuilder CreateEventsTypes()
        {
            return new EventsTypesBuilder()
                        .withID(1)
                        .withEventType("HEELP");
        }

        public List<EventsTypesBuilder> CreateRange()
        {
            return new List<EventsTypesBuilder> {
                        new EventsTypesBuilder().withID(1)
                                                .withEventType("Help me"),
                        new EventsTypesBuilder().withID(2)
                                                .withEventType("OMG") };
        }
    }
}