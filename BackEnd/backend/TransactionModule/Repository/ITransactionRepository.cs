using backend.TransactionModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.TransactionModule.Repository
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
        Transaction GetTransactionById(long id);
        string CreateTransaction(Transaction Transaction, string user);
        string UpdateTransactionById(Transaction Transaction, string user);
        string DeleteTransactionById(long id);
    }
}
