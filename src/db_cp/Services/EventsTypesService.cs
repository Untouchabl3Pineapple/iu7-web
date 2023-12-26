using System;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using db_cp.Models;
using db_cp.ModelsBL;
using AutoMapper;
using db_cp.Repository;

namespace db_cp.Services
{
    public interface IEventsTypesService
    {
        EventsTypesBL Add(EventsTypesBL eventtype);
        EventsTypesBL Delete(int id);
        EventsTypesBL Update(EventsTypesBL eventtype);

        IEnumerable<EventsTypesBL> GetAll();
        EventsTypesBL GetByID(int id);
        EventsTypesBL GetByEventType(string eventtype);
    }

    public class EventsTypesService : IEventsTypesService
    {
        private readonly IEventsTypesRepository _eventsTypesRepository;
        private readonly IMapper _mapper;

        public EventsTypesService(IEventsTypesRepository eventsTypesRepository, IMapper mapper)
        {
            _eventsTypesRepository = eventsTypesRepository;
            _mapper = mapper;
        }

        private bool IsExist(EventsTypesBL eventtype)
        {
            return _eventsTypesRepository.GetAll().FirstOrDefault(elem =>
                    elem.ID == eventtype.ID) != null;
        }

        private bool IsNotExist(int id)
        {
            return _eventsTypesRepository.GetByID(id) == null;
        }

        public EventsTypesBL Add(EventsTypesBL eventtype)
        {
            if (IsExist(eventtype))
                throw new Exception("An event type with this ID already exists");

            return _mapper.Map<EventsTypesBL>(_eventsTypesRepository.Add(_mapper.Map<EventsTypes>(eventtype)));
        }

        public EventsTypesBL Delete(int id)
        {
            return _mapper.Map<EventsTypesBL>(_eventsTypesRepository.Delete(id));
        }

        public IEnumerable<EventsTypesBL> GetAll()
        {
            return _mapper.Map<IEnumerable<EventsTypesBL>>(_eventsTypesRepository.GetAll());
        }

        public EventsTypesBL Update(EventsTypesBL eventtype)
        {
            if (IsNotExist(eventtype.ID))
                throw new Exception("Event type with this ID does not exist");

            return _mapper.Map<EventsTypesBL>(_eventsTypesRepository.Update(_mapper.Map<EventsTypes>(eventtype)));
        }

        public EventsTypesBL GetByID(int id)
        {
            return _mapper.Map<EventsTypesBL>(_eventsTypesRepository.GetByID(id));
        }

        public EventsTypesBL GetByEventType(string eventtype)
        {
            return _mapper.Map<EventsTypesBL>(_eventsTypesRepository.GetByEventType(eventtype));
        }
    }
}
