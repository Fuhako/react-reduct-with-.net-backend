using backend.Context;
using backend.TransactionDetailModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.TransactionDetailModule.Repository
{
    public class TransactionDetailRepository : ITransactionDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionDetailRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        //public List<TransactionDetail> GetTransactionDetails()
        //{
        //    return _context.TransactionDetails.OrderBy(p => p.id).ToList();
        //}

        public List<TransactionDetailDTO> GetTransactionDetails()
        {
            var query = from a in _context.TransactionDetails
                        join b in _context.Transactions on a.transaction_id equals b.id
                        join c in _context.ProductVariant on a.product_variant_id equals c.id
                        join d in _context.Products on c.product_id equals d.id
                        join e in _context.ProductCategory on d.product_category_id equals e.id
                        select new TransactionDetailDTO
                        {
                            TransactionId = a.transaction_id,
                            TransactionNo = b.transaction_no,
                            Category = e.name,
                            ProductName = c.name,
                            Qty = a.qty,
                            Subtotal = a.subtotal
                        };

            return query.ToList();
        }

        public TransactionDetail GetTransactionDetailById(long id)
        {
            return _context.TransactionDetails.Where(a => a.id == id).FirstOrDefault();
        }

        public string CreateTransactionDetail(TransactionDetail TransactionDetail, string user)
        {
            try
            {
                TransactionDetail.active = true;
                TransactionDetail.created_date = DateTime.Now;
                TransactionDetail.created_user = user;

                _context.TransactionDetails.Add(TransactionDetail);
                _context.SaveChanges(); 

                return "Insert TransactionDetail Success!";
            }
            catch (Exception ex)
            {
                return $"Insert TransactionDetail Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateTransactionDetailById(TransactionDetail TransactionDetail, string user)
        {
            try
            {
                // Fetch the existing TransactionDetail from the database
                var existingTransactionDetail = GetTransactionDetailById(TransactionDetail.id);
                if (existingTransactionDetail == null)
                {
                    // TransactionDetail with the given ID does not exist
                    return "TransactionDetail doesnt exists!";
                }

                // Update properties
                existingTransactionDetail.transaction_id = TransactionDetail.transaction_id;
                existingTransactionDetail.product_variant_id = TransactionDetail.product_variant_id;
                existingTransactionDetail.active = TransactionDetail.active;
                existingTransactionDetail.updated_user = user;
                existingTransactionDetail.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.TransactionDetails.Update(existingTransactionDetail);
                _context.SaveChanges();
                return "Update TransactionDetail Success!";
            }
            catch (Exception ex)
            {
                return $"Update TransactionDetail Failed with Error :{ex.InnerException}"; // Failure
            }

        }

        public string DeleteTransactionDetailById(long id)
        {
            try
            {

                // Fetch the existing TransactionDetail from the database
                var existingTransactionDetail = GetTransactionDetailById(id);
                if (existingTransactionDetail == null)
                {
                    // TransactionDetail with the given ID does not exist
                    return "TransactionDetail doesnt exists!";
                }

                // Mark the entity for deletion
                _context.TransactionDetails.Remove(existingTransactionDetail);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete TransactionDetail Success!";
            }
            catch (Exception ex)
            {
                return $"Delete TransactionDetail Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
