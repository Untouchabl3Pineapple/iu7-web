namespace db_cp.DTO
{
    public class UsersDTO
    {
        public string Login { get; set; }
        public string Permission { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
    }

    public class NewUsersDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Permission { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
    }

    public class UsersLoginDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class UsersUpdatePermDTO
    {
        public string Permission { get; set; }
    }
}

