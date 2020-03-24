// <copyright file="User.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Infrastructure.EntityFrameworkDataAccess.Entities
{
    using Domain.Customers.ValueObjects;
    using Domain.Security.ValueObjects;

    /// <summary>
    ///     User.
    /// </summary>
    public sealed class User : Domain.Security.User
    {
        /// <inheritdoc />
        public override ExternalUserId ExternalUserId {  get; }

        /// <inheritdoc />
        public override Name? Name { get; }

        /// <inheritdoc />
        public override CustomerId? CustomerId { get; protected set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="customerId">CustomerId.</param>
        /// <param name="externalUserId">External User Id.</param>
        public User(CustomerId? customerId, ExternalUserId externalUserId, Name? name)
        {
            this.CustomerId = customerId;
            this.ExternalUserId = externalUserId;
            this.Name = name;
        }

        private User()
        {
        }
    }
}
