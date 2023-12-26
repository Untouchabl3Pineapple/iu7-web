using System;
using db_cp.Models;
using db_cp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using db_cp.ModelsBL;
using AutoMapper;
using db_cp.Repository;

namespace db_cp.Services
{
    public interface IButtonsEventsService
    {
        ButtonsEventsBL Add(ButtonsEventsBL bevent);
        ButtonsEventsBL Delete(int id);
        ButtonsEventsBL Update(ButtonsEventsBL bevent);

        IEnumerable<ButtonsEventsBL> GetAll();
        ButtonsEventsBL GetByID(int id);
    }

    public class ButtonsEventsService : IButtonsEventsService
    {
        private readonly IButtonsEventsRepository _buttonsEventsRepository;
        private readonly IMapper _mapper;

        public ButtonsEventsService(IButtonsEventsRepository buttonsEventsRepository, IMapper mapper)
        {
            _buttonsEventsRepository = buttonsEventsRepository;
            _mapper = mapper;
        }

        private bool IsExist(ButtonsEventsBL bevent)
        {
            return _buttonsEventsRepository.GetAll().FirstOrDefault(elem =>
                    elem.ID == bevent.ID) != null;
        }

        private bool IsNotExist(int id)
        {
            return _buttonsEventsRepository.GetByID(id) == null;
        }

        public ButtonsEventsBL Add(ButtonsEventsBL bevent)
        {
            if (IsExist(bevent))
                throw new Exception("An event with this ID already exists");

            return _mapper.Map<ButtonsEventsBL>(_buttonsEventsRepository.Add(_mapper.Map<ButtonsEvents>(bevent)));
        }

        public ButtonsEventsBL Delete(int id)
        {
            return _mapper.Map<ButtonsEventsBL>(_buttonsEventsRepository.Delete(id));
        }

        public IEnumerable<ButtonsEventsBL> GetAll()
        {
            return _mapper.Map<IEnumerable<ButtonsEventsBL>>(_buttonsEventsRepository.GetAll());
        }

        public ButtonsEventsBL Update(ButtonsEventsBL bevent)
        {
            if (IsNotExist(bevent.ID))
                throw new Exception("There is no such event");

            return _mapper.Map<ButtonsEventsBL>(_buttonsEventsRepository.Update(_mapper.Map<ButtonsEvents>(bevent)));
        }

        public ButtonsEventsBL GetByID(int id)
        {
            return _mapper.Map<ButtonsEventsBL>(_buttonsEventsRepository.GetByID(id));
        }
    }
}
