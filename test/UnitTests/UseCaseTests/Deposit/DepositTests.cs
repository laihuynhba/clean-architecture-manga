namespace UnitTests.UseCaseTests.Deposit
{
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Boundaries.Deposit;
    using Application.UseCases;
    using Domain.Accounts.ValueObjects;
    using Infrastructure.InMemoryDataAccess;
    using Infrastructure.InMemoryDataAccess.Presenters;
    using TestFixtures;
    using Xunit;

    public sealed class DepositTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public DepositTests(StandardFixture fixture)
        {
            this._fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(PositiveDataSetup))]
        public async Task Deposit_ChangesBalance(decimal amount)
        {
            var presenter = new DepositPresenter();
            var sut = new DepositUseCase(
                this._fixture.AccountService,
                presenter,
                this._fixture.AccountRepository,
                this._fixture.UnitOfWork);

            await sut.Execute(
                new DepositInput(
                    MangaContext.DefaultAccountId,
                    new PositiveMoney(amount)));

            var output = presenter.Deposits.Last();
            var actualDebit = Assert.IsType<Debit>(output.Transaction);
            Assert.Equal(amount, actualDebit.Amount.ToMoney().ToDecimal());
        }

        [Theory]
        [ClassData(typeof(NegativeDataSetup))]
        public async Task Deposit_ShouldNot_ChangesBalance_WhenNegative(decimal amount)
        {
            var presenter = new DepositPresenter();
            var sut = new DepositUseCase(
                this._fixture.AccountService,
                presenter,
                this._fixture.AccountRepository,
                this._fixture.UnitOfWork);

            await Assert.ThrowsAsync<MoneyShouldBePositiveException>(() =>
                sut.Execute(
                    new DepositInput(
                        MangaContext.DefaultAccountId,
                        new PositiveMoney(amount))));
        }
    }
}
