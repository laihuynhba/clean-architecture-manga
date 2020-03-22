// <copyright file="User.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Domain.Security
{
    using Customers.ValueObjects;
    using ValueObjects;

    /// <inheritdoc />
    public abstract class User : IUser
    {
        /// <inheritdoc />
        public ExternalUserId ExternalUserId { get; }

        /// <inheritdoc />
        public Name? Name { get; }

        /// <inheritdoc />
        public CustomerId? CustomerId { get; private set; }

        /// <inheritdoc />
        public void Assign(CustomerId customerId)
        {
            this.CustomerId = customerId;
        }
    }
}
