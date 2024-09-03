using backend.TransactionDetailModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.TransactionDetailModule.Repository
{
    public interface ITransactionDetailRepository
    {
        List<TransactionDetailDTO> GetTransactionDetails();
        TransactionDetail GetTransactionDetailById(long id);
        string CreateTransactionDetail(TransactionDetail TransactionDetail, string user);
        string UpdateTransactionDetailById(TransactionDetail TransactionDetail, string user);
        string DeleteTransactionDetailById(long id);
    }
}
