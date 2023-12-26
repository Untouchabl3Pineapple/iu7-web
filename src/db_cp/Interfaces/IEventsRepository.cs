using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.Interfaces
{
    public interface IEventsRepository
    {
        Events Add(Events eevent);
        Events Update(Events eevent);
        Events Delete(int id);

        IEnumerable<Events> GetAll();
        Events GetByID(int id);
    }
}
