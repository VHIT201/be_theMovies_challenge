using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Kpmg.Blue.Common.Services.Interfaces;

namespace Kpmg.Blue.Common.Services
{
    public class UnitsOfWork : IUnitsOfWork
    {
        public async Task<TResult> CommitAsync<TResult>(Func<Task<TResult>> transaction)
        {
            return await CommitAsync(transaction, TransactionScopeAsyncFlowOption.Enabled);
        }

        public async Task CommitAsync(Func<Task> transaction)
        {
            await CommitAsync(transaction, TransactionScopeAsyncFlowOption.Enabled);
        }

        public async Task<TResult> CommitAsync<TResult>(Func<Task<TResult>> transaction, TransactionScopeAsyncFlowOption option)
        {
            TResult result;

            using (var transactionScope = new TransactionScope(option))
            {
                result = await transaction();
                transactionScope.Complete();
            }
            return result;
        }

        public async Task CommitAsync(Func<Task> transaction, TransactionScopeAsyncFlowOption option)
        {
            using var transactionScope = new TransactionScope(option);
            await transaction();
            transactionScope.Complete();
        }
    }
}
