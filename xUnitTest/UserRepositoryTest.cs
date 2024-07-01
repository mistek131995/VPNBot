using Core.Model.User;
using Infrastructure.Common;
using Infrastructure.Database;
using RepositoryUnitTests.Common.Entities;
using xUnitTest.Common;

namespace xUnitTest
{
    public class UserRepositoryTest
    {
        private Context context;

        public UserRepositoryTest()
        {
            context = new MockSqliteContext().CreateContext();
        }

        [Fact]
        public async Task AddUserTest()
        {
            var repositoryPrvider = new RepositoryProvider(context);
            await repositoryPrvider.UserRepository.AddAsync(UserTemplate.NewUser);
            await repositoryPrvider.UserRepository.AddAsync(UserTemplate.ActiveUser);

            var users = await repositoryPrvider.UserRepository.GetAllAsync();

            Assert.True(users.Count > 0);
        }

        [Fact]
        public async Task GetAllUser()
        {
            var repositoryPrvider = new RepositoryProvider(context);
            await repositoryPrvider.UserRepository.AddAsync(UserTemplate.NewUser);
            await repositoryPrvider.UserRepository.AddAsync(UserTemplate.ActiveUser);

            var users = await repositoryPrvider.UserRepository.GetAllAsync();

            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task GetUserById()
        {
            var repositoryPrvider = new RepositoryProvider(context);
            await repositoryPrvider.UserRepository.AddAsync(UserTemplate.NewUser);
            await repositoryPrvider.UserRepository.AddAsync(UserTemplate.ActiveUser);

            var user = await repositoryPrvider.UserRepository.GetByIdAsync(1);

            Assert.NotNull(user);
        }

        [Fact]
        public async Task AddUserPaymentTest()
        {
            var repositoryPrvider = new RepositoryProvider(context);
            await repositoryPrvider.AccessPositionRepository.AddAsync(AccessPositionTemplate.AccessPosition1);
            await repositoryPrvider.UserRepository.AddAsync(UserTemplate.NewUser);


            var user = await repositoryPrvider.UserRepository.GetByIdAsync(1);
            user.Payments.Add(new Payment(1, 100, DateTime.Now, PaymentState.Completed, null, Guid.NewGuid(), PaymentMethod.YouKassa));
            user = await repositoryPrvider.UserRepository.UpdateAsync(user);


            Assert.NotEmpty(user.Payments);
        }

        [Fact]
        public async Task AddUserVpnConnection()
        {
            var repositoryPrvider = new RepositoryProvider(context);
            await repositoryPrvider.LocationRepository.AddAsync(VpnLocationTemplate.LocationWithServer);
            var user = await repositoryPrvider.UserRepository.AddAsync(UserTemplate.NewUser);

            var test = repositoryPrvider.LocationRepository.GetAllAsync();

            user.UserConnections.Add(UserTemplate.Connection1);
            user = await repositoryPrvider.UserRepository.UpdateAsync(user);

            Assert.NotEmpty(user.UserConnections);
        }
    }
}
