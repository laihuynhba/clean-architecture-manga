namespace Infrastructure.InMemoryDataAccess
{
    using Domain.Customers.ValueObjects;
    using Domain.Security.ValueObjects;

    public sealed class User : Domain.Security.User
    {
        /// <inheritdoc />
        public ExternalUserId ExternalUserId { get; set; }

        /// <inheritdoc />
        public Name? Name { get; set; }

        /// <inheritdoc />
        public CustomerId? CustomerId { get; private set; }

        public User(CustomerId? customerId, ExternalUserId externalUserId, Name? name)
        {
            this.CustomerId = customerId;
            this.ExternalUserId = externalUserId;
            this.Name = name;
        }
    }
}
