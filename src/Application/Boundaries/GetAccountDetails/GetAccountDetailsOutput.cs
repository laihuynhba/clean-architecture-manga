// <copyright file="GetAccountDetailsOutput.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Application.Boundaries.GetAccountDetails
{
    using Domain.Accounts;

    /// <summary>
    ///     Get Account Details Output Message.
    /// </summary>
    public sealed class GetAccountDetailsOutput
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GetAccountDetailsOutput" /> class.
        /// </summary>
        /// <param name="account">Account object.</param>
        public GetAccountDetailsOutput(IAccount account)
        {
            this.Account = account;
        }

        /// <summary>
        ///     Gets the Account.
        /// </summary>
        public IAccount Account { get; }
    }
}
