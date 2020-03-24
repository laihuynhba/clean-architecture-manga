namespace Infrastructure.InMemoryDataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Accounts;
    using Domain.Accounts.Credits;
    using Domain.Accounts.Debits;
    using Domain.Accounts.ValueObjects;
    using Domain.Customers.ValueObjects;

    public sealed class AccountRepository : IAccountRepository
    {
        private readonly MangaContext _context;

        public AccountRepository(MangaContext context)
        {
            this._context = context;
        }

        public async Task<IList<IAccount>> GetBy(CustomerId customerId)
        {
            var accounts = this._context.Accounts
                .Where(e => e.CustomerId.Equals(customerId))
                .Select(e => (IAccount)e)
                .ToList();

            return await Task.FromResult(accounts)
                .ConfigureAwait(false);
        }

        public async Task Add(IAccount account, ICredit credit)
        {
            this._context
                .Accounts
                .Add((InMemoryDataAccess.Account)account);

            this._context
                .Credits
                .Add((InMemoryDataAccess.Credit)credit);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task Delete(IAccount account)
        {
            var accountOld = this._context.Accounts
                .SingleOrDefault(e => e.Id.Equals(account.Id));

            this._context.Accounts.Remove(accountOld);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task<IAccount> GetAccount(AccountId accountId)
        {
            var account = this._context.Accounts
                .SingleOrDefault(e => e.Id.Equals(accountId));

            if (account is null)
            {
                throw new AccountNotFoundException($"The account {accountId} does not exist or is not processed yet.");
            }

            return await Task.FromResult<Account>(account)
                .ConfigureAwait(false);
        }

        public async Task Update(IAccount account, ICredit credit)
        {
            Account accountOld = this._context
                .Accounts
                .SingleOrDefault(e => e.Id.Equals(account.Id));

            accountOld = (Account)account;
            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task Update(IAccount account, IDebit debit)
        {
            Account accountOld = this._context.Accounts
                .SingleOrDefault(e => e.Id.Equals(account.Id));

            accountOld = (Account)account;
            await Task.CompletedTask
                .ConfigureAwait(false);
        }
    }
}
