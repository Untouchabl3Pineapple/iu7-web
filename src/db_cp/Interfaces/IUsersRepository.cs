using System;
using db_cp.Models;
using System.Collections.Generic;

namespace db_cp.Interfaces
{
    public interface IUsersRepository
    {
        Users Add(Users user);
        Users Update(Users user);
        Users Delete(string login);

        IEnumerable<Users> GetAll();
        Users GetByLogin(string login);
    }
}
