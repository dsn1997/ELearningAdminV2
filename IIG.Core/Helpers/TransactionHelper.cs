using System.Transactions;

namespace IIG.Core.Helper
{
    public static class TransactionHelper
    {
        public static TransactionScope CreateTransactionScope()
        {
            TransactionOptions transactionOption = new()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(300000)
            };

            return new TransactionScope(TransactionScopeOption.RequiresNew, transactionOption, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
