using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Kpmg.Blue.Common.Services.Interfaces
{
    public interface IUnitsOfWork
    {
        Task<TResult> CommitAsync<TResult>(Func<Task<TResult>> transaction);
        Task CommitAsync(Func<Task> transaction);
        Task<TResult> CommitAsync<TResult>(Func<Task<TResult>> transaction, TransactionScopeAsyncFlowOption option);
        Task CommitAsync(Func<Task> transaction, TransactionScopeAsyncFlowOption option);
    }
}
