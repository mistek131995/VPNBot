using Core.Model.User;

namespace RepositoryUnitTests.Common.Entities
{
    public static class UserTemplate
    {
        public static User NewUser = new User("newUser", "newUser@test.com", "password", UserSost.NotActive);
        public static User ActiveUser = new User("activeUser", "activeUser@test.ru", "password", UserSost.Active);

        public static UserConnection Connection1 = new UserConnection(1, 443, "ntwrk", "vless", "hz", "key", "google", "google", "adsfse", DateTime.MinValue, ConnectionType.Free);
    }
}
