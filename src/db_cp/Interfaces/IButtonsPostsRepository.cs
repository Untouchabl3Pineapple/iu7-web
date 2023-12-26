using System.Collections.Generic;
using db_cp.Models;

namespace db_cp.Interfaces
{
    public interface IButtonsPostsRepository
    {
        ButtonsPosts Add(ButtonsPosts bpost);
        ButtonsPosts Update(ButtonsPosts bpost);
        ButtonsPosts Delete(int post);

        IEnumerable<ButtonsPosts> GetAll();
        ButtonsPosts GetByPost(int post);
    }
}
