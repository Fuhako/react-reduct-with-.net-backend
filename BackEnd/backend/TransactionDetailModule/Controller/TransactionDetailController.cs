using backend.Context;
using backend.TransactionDetailModule.Model;
using backend.TransactionDetailModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.TransactionDetailModule.Controller
{
    [ApiController]
    public class TransactionDetailController : ControllerBase
    {
        private readonly ITransactionDetailRepository _TransactionDetailRepository;
        public TransactionDetailController(ITransactionDetailRepository TransactionDetailRepository, ApplicationDbContext context) 
        {
            _TransactionDetailRepository = TransactionDetailRepository;
        }

        [Route("api/[controller]/GetTransactionDetail")]
        [HttpGet]
        public IActionResult GetTransactionDetail()
        {
            var result = _TransactionDetailRepository.GetTransactionDetails();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/[controller]/GetTransactionDetailById")]
        [HttpGet]
        public IActionResult GetTransactionDetailById(int id)
        {
            var result = _TransactionDetailRepository.GetTransactionDetailById(id);
            if(result == null)
            {
                return NotFound("TransactionDetail not found!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/CreateTransactionDetail")]
        [HttpPost]
        public IActionResult CreateTransactionDetail(TransactionDetail TransactionDetail)
        {
            if (TransactionDetail == null)
            {
                return BadRequest("TransactionDetail cannot be null");
            }

            var result = _TransactionDetailRepository.CreateTransactionDetail(TransactionDetail, TransactionDetail.created_user);
            if (result == null)
            {
                return NotFound("Create TransactionDetail Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/UpdateTransactionDetailById")]
        [HttpPut]
        public IActionResult UpdateTransactionDetailById(TransactionDetail TransactionDetail)
        {
            if (TransactionDetail == null)
            {
                return BadRequest("TransactionDetail cannot be null");
            }

            var result = _TransactionDetailRepository.UpdateTransactionDetailById(TransactionDetail, TransactionDetail.created_user);
            if (result == null)
            {
                return NotFound("Update TransactionDetail Failed!");
            }
                
            return Ok(result);
        }

        [Route("api/[controller]/DeleteTransactionDetailById")]
        [HttpDelete]
        public IActionResult DeleteTransactionDetailById([FromQuery] long id)
        {
            if (id == 0)
            {
                return BadRequest("Id TransactionDetail cannot be null");
            }

            var result = _TransactionDetailRepository.DeleteTransactionDetailById(id);
            if (result == null)
            {
                return NotFound("Delete TransactionDetail Failed!");
            }

            return Ok(result);
        }
    }
}
