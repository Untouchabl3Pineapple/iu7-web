using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.Interfaces
{
    public interface IButtonsEventsRepository
    {
        ButtonsEvents Add(ButtonsEvents bevent);
        ButtonsEvents Update(ButtonsEvents bevent);
        ButtonsEvents Delete(int id);

        IEnumerable<ButtonsEvents> GetAll();
        ButtonsEvents GetByID(int id);
    }
}
