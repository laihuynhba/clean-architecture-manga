// <copyright file="GetCustomerDetailsOutput.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Application.Boundaries.GetCustomerDetails
{
    using System;
    using Domain.Customers;

    /// <summary>
    ///     Gets Customer Details Output Message.
    /// </summary>
    public sealed class GetCustomerDetailsOutput
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GetCustomerDetailsOutput" /> class.
        /// </summary>
        /// <param name="customer">Customer object.</param>
        public GetCustomerDetailsOutput(ICustomer customer)
        {
            this.Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }

        /// <summary>
        ///     Gets the Customer.
        /// </summary>
        public ICustomer Customer { get; }
    }
}
