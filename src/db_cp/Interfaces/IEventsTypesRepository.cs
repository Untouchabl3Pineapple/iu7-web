using System;
using db_cp.Models;
using System.Collections.Generic;

namespace db_cp.Interfaces
{
    public interface IEventsTypesRepository
    {
        EventsTypes Add(EventsTypes eventtype);
        EventsTypes Update(EventsTypes eventtype);
        EventsTypes Delete(int id);

        IEnumerable<EventsTypes> GetAll();
        EventsTypes GetByID(int id);
        EventsTypes GetByEventType(string eventtype);
    }
}
