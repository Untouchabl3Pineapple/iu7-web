using TestsCore.Builders;

namespace TestsCore.ObjectMothers
{
    public class EventsOM
    {
        public EventsBuilder CreateEvents()
        {
            return new EventsBuilder()
                        .withID(1)
                        .withButtonEventID(1)
                        .withDetectingTime(DateTime.UtcNow);
        }

        public List<EventsBuilder> CreateRange()
        {
            return new List<EventsBuilder> {
                        new EventsBuilder().withID(1)
                                           .withButtonEventID(1)
                                           .withDetectingTime(DateTime.UtcNow),
                        new EventsBuilder().withID(2)
                                           .withButtonEventID(2)
                                           .withDetectingTime(DateTime.UtcNow)
            };
        }
    }
}