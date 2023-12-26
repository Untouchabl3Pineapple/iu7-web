using TestsCore.Builders;

namespace TestsCore.ObjectMothers
{
    public class ButtonsEventsOM
    {
        public ButtonsEventsBuilder CreateButtonsEvents()
        {
            return new ButtonsEventsBuilder()
                        .withID(1)
                        .withButtonColor("RED")
                        .withNumber(3);
        }

        public List<ButtonsEventsBuilder> CreateRange()
        {
            return new List<ButtonsEventsBuilder> {
                        new ButtonsEventsBuilder().withID(1)
                                                  .withButtonColor("RED")
                                                  .withNumber(3),
                        new ButtonsEventsBuilder().withID(2)
                                                  .withButtonColor("GREEN")
                                                  .withNumber(3) };
        }
    }
}
