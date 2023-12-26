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
    public interface IButtonsPostsService
    {
        ButtonsPostsBL Add(ButtonsPostsBL bpost);
        ButtonsPostsBL Delete(int post);
        ButtonsPostsBL Update(ButtonsPostsBL bpost);

        IEnumerable<ButtonsPostsBL> GetAll();
        ButtonsPostsBL GetByPost(int post);
    }

    public class ButtonsPostsService : IButtonsPostsService
    {
        private readonly IButtonsPostsRepository _buttonsPostsRepository;
        private readonly IMapper _mapper;

        public ButtonsPostsService(IButtonsPostsRepository buttonsPostsRepository, IMapper mapper)
        {
            _buttonsPostsRepository = buttonsPostsRepository;
            _mapper = mapper;
        }

        private bool IsExist(ButtonsPostsBL bpost)
        {
            return _buttonsPostsRepository.GetAll().FirstOrDefault(elem =>
                    elem.Post == bpost.Post) != null;
        }

        private bool IsNotExist(int post)
        {
            return _buttonsPostsRepository.GetByPost(post) == null;
        }

        public ButtonsPostsBL Add(ButtonsPostsBL bpost)
        {
            if (IsExist(bpost))
                throw new Exception("Post with this number already exists");

            return _mapper.Map<ButtonsPostsBL>(_buttonsPostsRepository.Add(_mapper.Map<ButtonsPosts>(bpost)));
        }

        public ButtonsPostsBL Delete(int post)
        {
            return _mapper.Map<ButtonsPostsBL>(_buttonsPostsRepository.Delete(post));
        }

        public IEnumerable<ButtonsPostsBL> GetAll()
        {
            return _mapper.Map<IEnumerable<ButtonsPostsBL>>(_buttonsPostsRepository.GetAll());
        }

        public ButtonsPostsBL Update(ButtonsPostsBL bpost)
        {
            if (IsNotExist(bpost.Post))
                throw new Exception("Post with this number does not exist.");

            return _mapper.Map<ButtonsPostsBL>(_buttonsPostsRepository.Update(_mapper.Map<ButtonsPosts>(bpost)));
        }

        public ButtonsPostsBL GetByPost(int post)
        {
            return _mapper.Map<ButtonsPostsBL>(_buttonsPostsRepository.GetByPost(post));
        }
    }
}
