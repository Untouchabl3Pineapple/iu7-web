using db_cp.Models;
using db_cp.ModelsBL;

namespace TestsCore.Builders
{
    public class ButtonsEventsBuilder
    {
        public int ID;
        public string ButtonColor;
        public short Number;

        public ButtonsEventsBuilder() { }

        public ButtonsEventsBuilder withID(int id)
        {
            this.ID = id;
            return this;
        }

        public ButtonsEventsBuilder withButtonColor(string buttonColor)
        {
            this.ButtonColor = buttonColor;
            return this;
        }

        public ButtonsEventsBuilder withNumber(short number)
        {
            this.Number = number;
            return this;
        }

        public ButtonsEvents build()
        {
            return new ButtonsEvents { ID = ID, ButtonColor = ButtonColor, Number = Number };
        }

        public ButtonsEventsBL buildBL()
        {
            return new ButtonsEventsBL { ID = ID, ButtonColor = ButtonColor, Number = Number };
        }
    }
}
