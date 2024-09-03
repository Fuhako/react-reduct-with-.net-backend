using backend.Context;
using backend.TransactionModule.Model;
using backend.TransactionModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.TransactionModule.Controller
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _TransactionRepository;
        public TransactionController(ITransactionRepository TransactionRepository, ApplicationDbContext context) 
        {
            _TransactionRepository = TransactionRepository;
        }

        [Route("api/[controller]/GetTransaction")]
        [HttpGet]
        public IActionResult GetTransaction()
        {
            var result = _TransactionRepository.GetTransactions();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/[controller]/GetTransactionById")]
        [HttpGet]
        public IActionResult GetTransactionById(int id)
        {
            var result = _TransactionRepository.GetTransactionById(id);
            if(result == null)
            {
                return NotFound("Transaction not found!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/CreateTransaction")]
        [HttpPost]
        public IActionResult CreateTransaction(Transaction Transaction)
        {
            if (Transaction == null)
            {
                return BadRequest("Transaction cannot be null");
            }

            var result = _TransactionRepository.CreateTransaction(Transaction, Transaction.created_user);
            if (result == null)
            {
                return NotFound("Create Transaction Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/UpdateTransactionById")]
        [HttpPut]
        public IActionResult UpdateTransactionById(Transaction Transaction)
        {
            if (Transaction == null)
            {
                return BadRequest("Transaction cannot be null");
            }

            var result = _TransactionRepository.UpdateTransactionById(Transaction, Transaction.created_user);
            if (result == null)
            {
                return NotFound("Update Transaction Failed!");
            }
                
            return Ok(result);
        }

        [Route("api/[controller]/DeleteTransactionById")]
        [HttpDelete]
        public IActionResult DeleteTransactionById([FromQuery] long id)
        {
            if (id == 0)
            {
                return BadRequest("Id Transaction cannot be null");
            }

            var result = _TransactionRepository.DeleteTransactionById(id);
            if (result == null)
            {
                return NotFound("Delete Transaction Failed!");
            }

            return Ok(result);
        }
    }
}
