// <copyright file="GetCustomerDetails.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Application.UseCases
{
    using System.Threading.Tasks;
    using Boundaries.GetCustomerDetails;
    using Domain.Customers;
    using Domain.Customers.ValueObjects;
    using Domain.Security;
    using Domain.Security.Services;

    /// <summary>
    ///     Get Customer Details
    ///     <see href="https://github.com/ivanpaulovich/clean-architecture-manga/wiki/Domain-Driven-Design-Patterns#use-case">
    ///         Use
    ///         Case Domain-Driven Design Pattern
    ///     </see>
    ///     .
    /// </summary>
    public sealed class GetCustomerDetailsUseCase : IUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOutputPort _outputPort;
        private readonly IUserService _userService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GetCustomerDetailsUseCase" /> class.
        /// </summary>
        /// <param name="userService">User Service.</param>
        /// <param name="outputPort">Output Port.</param>
        /// <param name="customerRepository">Customer Repository.</param>
        public GetCustomerDetailsUseCase(
            IUserService userService,
            IOutputPort outputPort,
            ICustomerRepository customerRepository)
        {
            this._userService = userService;
            this._outputPort = outputPort;
            this._customerRepository = customerRepository;
        }

        /// <summary>
        ///     Executes the Use Case.
        /// </summary>
        /// <param name="input">Input Message.</param>
        /// <returns>Task.</returns>
        public async Task Execute(GetCustomerDetailsInput input)
        {
            if (input is null)
            {
                this._outputPort.WriteError(Messages.InputIsNull);
                return;
            }

            IUser user = this._userService.GetUser();

            ICustomer customer;

            if (user.CustomerId is CustomerId customerId)
            {
                try
                {
                    customer = await this._customerRepository
                        .GetBy(customerId)
                        .ConfigureAwait(false);
                }
                catch (CustomerNotFoundException ex)
                {
                    this._outputPort.NotFound(ex.Message);
                    return;
                }
            }
            else
            {
                this._outputPort.NotFound(Messages.CustomerDoesNotExist);
                return;
            }

            this.BuildOutput(customer);
        }

        private void BuildOutput(ICustomer customer)
        {
            var output = new GetCustomerDetailsOutput(customer);
            this._outputPort.Standard(output);
        }
    }
}
