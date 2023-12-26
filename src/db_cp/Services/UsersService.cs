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
    public interface IUsersService
    {
        UsersBL Add(UsersBL user);
        UsersBL Delete(string login);
        UsersBL Update(UsersBL user);

        IEnumerable<UsersBL> GetAll();
        UsersBL GetByLogin(string login);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        private bool IsExist(UsersBL user)
        {
            return _usersRepository.GetAll().FirstOrDefault(elem =>
                    elem.Login == user.Login) != null;
        }

        private bool IsNotExist(string login)
        {
            return _usersRepository.GetByLogin(login) == null;
        }

        public UsersBL Add(UsersBL user)
        {
            if (IsExist(user))
                throw new Exception("User with this login already exists");

            return _mapper.Map<UsersBL>(_usersRepository.Add(_mapper.Map<Users>(user)));
        }

        public UsersBL Delete(string login)
        {
            return _mapper.Map<UsersBL>(_usersRepository.Delete(login));
        }

        public IEnumerable<UsersBL> GetAll()
        {
            return _mapper.Map<IEnumerable<UsersBL>>(_usersRepository.GetAll());
        }

        public UsersBL GetByLogin(string login)
        {
            return _mapper.Map<UsersBL>(_usersRepository.GetByLogin(login));
        }

        public UsersBL Update(UsersBL user)
        {
            if (IsNotExist(user.Login))
                throw new Exception("User with this login does not exist");

            return _mapper.Map<UsersBL>(_usersRepository.Update(_mapper.Map<Users>(user)));
        }
    }
}
