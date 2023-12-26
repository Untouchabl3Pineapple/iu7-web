using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using db_cp.ModelsBL;
using AutoMapper;
using db_cp.Repository;
using Microsoft.Extensions.Hosting;

namespace db_cp.Services
{
    public interface IEventsService
    {
        EventsBL Add(EventsBL eevent);
        EventsBL Delete(int id);
        EventsBL Update(EventsBL eevent);

        IEnumerable<EventsBL> GetAll();
        EventsBL GetByID(int id);
    }

    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;

        public EventsService(IEventsRepository eventsRepository, IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        private bool IsExist(EventsBL eevent)
        {
            return _eventsRepository.GetAll().FirstOrDefault(elem =>
                    elem.ID == eevent.ID) != null;
        }

        private bool IsNotExist(int id)
        {
            return _eventsRepository.GetByID(id) == null;
        }

        public EventsBL Add(EventsBL eevent)
        {
            if (IsExist(eevent))
                throw new Exception("An event with this ID already exists");

            return _mapper.Map<EventsBL>(_eventsRepository.Add(_mapper.Map<Events>(eevent)));
        }

        public EventsBL Delete(int id)
        {
            return _mapper.Map<EventsBL>(_eventsRepository.Delete(id));
        }

        public IEnumerable<EventsBL> GetAll()
        {
            return _mapper.Map<IEnumerable<EventsBL>>(_eventsRepository.GetAll());
        }

        public EventsBL Update(EventsBL eevent)
        {
            if (IsNotExist(eevent.ID))
                throw new Exception("Event with this ID does not exist");

            return _mapper.Map<EventsBL>(_eventsRepository.Update(_mapper.Map<Events>(eevent)));
        }

        public EventsBL GetByID(int id)
        {
            return _mapper.Map<EventsBL>(_eventsRepository.GetByID(id));
        }
    }
}
