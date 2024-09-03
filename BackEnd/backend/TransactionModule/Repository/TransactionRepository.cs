using backend.Context;
using backend.TransactionModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.TransactionModule.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        public List<Transaction> GetTransactions()
        {
            return _context.Transactions.OrderBy(p => p.id).ToList();
        }
        
        public Transaction GetTransactionById(long id)
        {
            return _context.Transactions.Where(a => a.id == id).FirstOrDefault();
        }

        public string CreateTransaction(Transaction Transaction, string user)
        {
            try
            {
                Transaction.active = true;
                Transaction.created_date = DateTime.Now;
                Transaction.created_user = user;

                _context.Transactions.Add(Transaction);
                _context.SaveChanges(); 

                return "Insert Transaction Success!";
            }
            catch (Exception ex)
            {
                return $"Insert Transaction Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateTransactionById(Transaction Transaction, string user)
        {
            try
            {
                // Fetch the existing Transaction from the database
                var existingTransaction = GetTransactionById(Transaction.id);
                if (existingTransaction == null)
                {
                    // Transaction with the given ID does not exist
                    return "Transaction doesnt exists!";
                }

                // Update properties
                existingTransaction.transaction_no = Transaction.transaction_no;
                existingTransaction.total_amount = Transaction.total_amount;
                existingTransaction.active = Transaction.active;
                existingTransaction.updated_user = user;
                existingTransaction.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.Transactions.Update(existingTransaction);
                _context.SaveChanges();
                return "Update Transaction Success!";
            }
            catch (Exception ex)
            {
                return $"Update Transaction Failed with Error :{ex.InnerException}"; // Failure
            }

        }

        public string DeleteTransactionById(long id)
        {
            try
            {

                // Fetch the existing Transaction from the database
                var existingTransaction = GetTransactionById(id);
                if (existingTransaction == null)
                {
                    // Transaction with the given ID does not exist
                    return "Transaction doesnt exists!";
                }

                // Mark the entity for deletion
                _context.Transactions.Remove(existingTransaction);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete Transaction Success!";
            }
            catch (Exception ex)
            {
                return $"Delete Transaction Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
