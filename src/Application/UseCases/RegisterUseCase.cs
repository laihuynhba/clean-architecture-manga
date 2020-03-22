// <copyright file="Register.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Application.UseCases
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boundaries.Register;
    using Domain.Accounts;
    using Domain.Customers;
    using Domain.Customers.ValueObjects;
    using Domain.Security;
    using Domain.Security.Services;
    using Services;

    /// <summary>
    ///     Register
    ///     <see href="https://github.com/ivanpaulovich/clean-architecture-manga/wiki/Domain-Driven-Design-Patterns#use-case">
    ///         Use
    ///         Case Domain-Driven Design Pattern
    ///     </see>
    ///     .
    /// </summary>
    public sealed class RegisterUseCase : IUseCase
    {
        private readonly AccountService _accountService;
        private readonly CustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOutputPort _outputPort;
        private readonly SecurityService _securityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterUseCase" /> class.
        /// </summary>
        /// <param name="userService">User Service.</param>
        /// <param name="customerService">Customer Service.</param>
        /// <param name="accountService">Account Service.</param>
        /// <param name="securityService">Security Service.</param>
        /// <param name="outputPort">Output Port.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public RegisterUseCase(
            IUserService userService,
            CustomerService customerService,
            AccountService accountService,
            SecurityService securityService,
            IOutputPort outputPort,
            IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository)
        {
            this._userService = userService;
            this._customerService = customerService;
            this._accountService = accountService;
            this._securityService = securityService;
            this._outputPort = outputPort;
            this._unitOfWork = unitOfWork;
            this._customerRepository = customerRepository;
            this._accountRepository = accountRepository;
        }

        /// <summary>
        ///     Executes the Use Case.
        /// </summary>
        /// <param name="input">Input Message.</param>
        /// <returns>Task.</returns>
        public async Task Execute(RegisterInput input)
        {
            if (input is null)
            {
                this._outputPort.WriteError(Messages.InputIsNull);
                return;
            }

            var user = this._userService.GetUser();

            if (await this.VerifyCustomerAlreadyRegistered(user))
            {
                return;
            }

            var customer = await this._customerService
                .CreateCustomer(input.SSN, user.Name.Value)
                .ConfigureAwait(false);

            var account = await this._accountService
                .OpenCheckingAccount(customer.Id, input.InitialAmount)
                .ConfigureAwait(false);

            await this._securityService
                .CreateUserCredentials(user, customer.Id)
                .ConfigureAwait(false);

            customer.Assign(account.Id);

            await this._unitOfWork.Save()
                .ConfigureAwait(false);

            this.BuildOutput(user, customer, new List<IAccount> { account });
        }

        private async Task<bool> VerifyCustomerAlreadyRegistered(IUser user)
        {
            if (!(user.CustomerId is CustomerId customerId))
            {
                return false;
            }

            if (!await this._customerService.IsCustomerRegistered(customerId)
                .ConfigureAwait(false))
            {
                return false;
            }

            var existingCustomer = await this._customerRepository.GetBy(customerId)
                .ConfigureAwait(false);
            var existingAccounts = await this._accountRepository.GetBy(customerId)
                .ConfigureAwait(false);

            var output = new RegisterOutput(
                user,
                existingCustomer,
                existingAccounts);

            this._outputPort.HandleAlreadyRegisteredCustomer(output);
            return true;

        }

        private void BuildOutput(
            IUser user,
            ICustomer customer,
            IList<IAccount> account)
        {
            var output = new RegisterOutput(
                user,
                customer,
                account);
            this._outputPort.Standard(output);
        }
    }
}
