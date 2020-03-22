namespace Infrastructure.ExternalAuthentication
{
    using Domain.Customers.ValueObjects;
    using Domain.Security;
    using Domain.Security.Services;
    using Domain.Security.ValueObjects;
    using InMemoryDataAccess;

    public sealed class TestUserService : IUserService
    {
        private readonly IUserFactory _userFactory;

        public TestUserService(IUserFactory userFactory)
        {
            this._userFactory = userFactory;
        }

        public IUser GetUser()
        {
            var customerId = new CustomerId(MangaContext.DefaultCustomerId.ToGuid());
            var externalUserId = new ExternalUserId("github/ivanpaulovich");
            var name = new Name("Ivan Paulovich");

            var user = this._userFactory
                .NewUser(customerId, externalUserId, name);
            return user;
        }
    }
}
