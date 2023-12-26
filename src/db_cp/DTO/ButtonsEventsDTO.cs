namespace db_cp.DTO
{
    public class ButtonsEventsBaseDTO
    {
        public string ButtonColor { get; set; }
        public short Number { get; set; }
    }

    public class ButtonsEventsDTO: ButtonsEventsBaseDTO
    {
        public int ID { get; set; }
    }
}
