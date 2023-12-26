using TestsCore.Builders;

namespace TestsCore.ObjectMothers
{
    public class UsersOM
    {
        public UsersBuilder CreateUsers()
        {
            return new UsersBuilder()
                        .withLogin("admin")
                        .withPassword("admin")
                        .withPermission("admin")
                        .withName("ilya")
                        .withSurname("artemev")
                        .withMiddleName("olegovich");
        }

        public List<UsersBuilder> CreateRange()
        {
            return new List<UsersBuilder> {
                        new UsersBuilder().withLogin("user1")
                                          .withPassword("user")
                                          .withPermission("foreman")
                                          .withName("gadzhi")
                                          .withSurname("artemev")
                                          .withMiddleName("olegovich"),
                        new UsersBuilder().withLogin("user2")
                                          .withPassword("user")
                                          .withPermission("foreman")
                                          .withName("kirill")
                                          .withSurname("artemev")
                                          .withMiddleName("olegovich")
            };
        }
    }
}