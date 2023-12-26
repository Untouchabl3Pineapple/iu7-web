namespace db_cp.DTO
{
    public class EventsTypesBaseDTO
    {
        public string EventType { get; set; }
    }

    public class EventsTypesDTO: EventsTypesBaseDTO
    {
        public int ID { get; set; }
    }
}
