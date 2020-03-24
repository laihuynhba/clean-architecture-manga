namespace Infrastructure.InMemoryDataAccess
{
    using Domain.Customers.ValueObjects;
    using Domain.Security.ValueObjects;

    public sealed class User : Domain.Security.User
    {
        /// <inheritdoc />
        public override ExternalUserId ExternalUserId { get; }

        /// <inheritdoc />
        public override Name? Name { get; }

        /// <inheritdoc />
        public override CustomerId? CustomerId { get; protected set; }

        public User(CustomerId? customerId, ExternalUserId externalUserId, Name? name)
        {
            this.CustomerId = customerId;
            this.ExternalUserId = externalUserId;
            this.Name = name;
        }
    }
}
