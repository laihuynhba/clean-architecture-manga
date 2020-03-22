namespace WebApi.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using Domain.Accounts;
    using Domain.Accounts.ValueObjects;

    /// <summary>
    ///     Account Details.
    /// </summary>
    public sealed class AccountDetailsModel
    {
        public AccountDetailsModel(IAccount account)
        {
            this.AccountId = account.Id.ToGuid();
            this.CurrentBalance = account.GetCurrentBalance().ToDecimal();
        }

        /// <summary>
        ///     Gets account ID.
        /// </summary>
        [Required]
        public Guid AccountId { get; }

        /// <summary>
        ///     Gets current Balance.
        /// </summary>
        [Required]
        public decimal CurrentBalance { get; }
    }
}
