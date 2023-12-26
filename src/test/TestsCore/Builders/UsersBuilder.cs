using System.ComponentModel.DataAnnotations;
using db_cp.Models;
using db_cp.ModelsBL;

namespace TestsCore.Builders
{
    public class UsersBuilder
    {
        public string Login;
        public string Password;
        public string Permission;
        public string Name;
        public string Surname;
        public string MiddleName;

        public UsersBuilder() { }

        public UsersBuilder withLogin(string login)
        {
            this.Login = login;
            return this;
        }

        public UsersBuilder withPassword(string password)
        {
            this.Password = password;
            return this;
        }

        public UsersBuilder withPermission(string permission)
        {
            this.Permission = permission;
            return this;
        }

        public UsersBuilder withName(string name)
        {
            this.Name = name;
            return this;
        }

        public UsersBuilder withSurname(string surname)
        {
            this.Surname = surname;
            return this;
        }

        public UsersBuilder withMiddleName(string middleName)
        {
            this.MiddleName = middleName;
            return this;
        }

        public Users build()
        {
            return new Users
            {
                Login = Login,
                Password = Password,
                Permission = Permission,
                Name = Name,
                Surname = Surname,
                MiddleName = MiddleName
            };
        }

        public UsersBL buildBL()
        {
            return new UsersBL
            {
                Login = Login,
                Password = Password,
                Permission = Permission,
                Name = Name,
                Surname = Surname,
                MiddleName = MiddleName
            };
        }
    }
}
