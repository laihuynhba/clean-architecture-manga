namespace IntegrationTests.EntityFrameworkTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Customers;
    using Domain.Customers.ValueObjects;
    using Domain.Security;
    using Domain.Security.ValueObjects;
    using Infrastructure.EntityFrameworkDataAccess;
    using Infrastructure.EntityFrameworkDataAccess.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public sealed class CustomerRepositoryTests
    {
        [Fact]
        public async Task Add_ChangesDatabase()
        {
            var options = new DbContextOptionsBuilder<MangaContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            var factory = new EntityFactory();

            var customer = factory.NewCustomer(
                new SSN("198608177955"),
                new Name("Ivan Paulovich"));

            var user = factory.NewUser(
                customer.Id,
                new ExternalUserId("github/ivanpaulovich"),
                new Name("Ivan Paulovich"));

            using (var context = new MangaContext(options))
            {
                context.Database.EnsureCreated();

                var userRepository = new UserRepository(context);
                await userRepository.Add(user)
                    .ConfigureAwait(false);

                var customerRepository = new CustomerRepository(context);
                await customerRepository.Add(customer)
                    .ConfigureAwait(false);

                Assert.Equal(2, context.Customers.Count());
            }
        }

        [Fact]
        public async Task Get_ReturnsCustomer()
        {
            var options = new DbContextOptionsBuilder<MangaContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            ICustomer customer = null;

            using (var context = new MangaContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new CustomerRepository(context);
                customer = await repository.GetBy(SeedData.DefaultCustomerId)
                    .ConfigureAwait(false);

                Assert.NotNull(customer);
            }
        }
    }
}
