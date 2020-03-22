namespace UnitTests.PresenterTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Application.Boundaries.Register;
    using Domain.Accounts;
    using Domain.Accounts.ValueObjects;
    using Domain.Customers.ValueObjects;
    using Domain.Security.ValueObjects;
    using Infrastructure.InMemoryDataAccess;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.UseCases.V1.Register;
    using Xunit;
    using Account = Infrastructure.InMemoryDataAccess.Account;

    public sealed class RegisterPresenterTests
    {
        [Fact]
        public void GivenValidData_Handle_WritesOkObjectResult()
        {
            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                new SSN("198608178888"),
                new Name("Ivan Paulovich"),
                Array.Empty<AccountId>());

            var account = new Account(
                new AccountId(Guid.NewGuid()),
                customer.Id,
                Array.Empty<Credit>(),
                Array.Empty<Debit>()
                );

            var user = new User(
                customer.Id,
                new ExternalUserId("github/ivanpaulovich"),
                new Name("Ivan Paulovich"));

            var registerOutput = new RegisterOutput(
                user,
                customer,
                new List<IAccount>() { account });

            var sut = new RegisterPresenter();
            sut.Standard(registerOutput);

            var actual = Assert.IsType<CreatedAtRouteResult>(sut.ViewModel);
            Assert.Equal((int)HttpStatusCode.Created, actual.StatusCode);

            var actualValue = (RegisterResponse)actual.Value;
            Assert.Equal(customer.Id.ToGuid(), actualValue.Customer.CustomerId);
        }
    }
}
