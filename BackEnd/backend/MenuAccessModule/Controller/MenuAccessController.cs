using backend.Context;
using backend.MenuAccessModule.Model;
using backend.MenuAccessModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.MenuAccessModule.Controller
{
    [ApiController]
    public class MenuAccessController : ControllerBase
    {
        private readonly IMenuAccessRepository _MenuAccessRepository;
        public MenuAccessController(IMenuAccessRepository MenuAccessRepository, ApplicationDbContext context) 
        {
            _MenuAccessRepository = MenuAccessRepository;
        }

        [Route("api/[controller]/GetMenuAccess")]
        [HttpGet]
        public IActionResult GetMenuAccess()
        {
            var result = _MenuAccessRepository.GetMenuAccess();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/[controller]/GetMenuAccessById")]
        [HttpGet]
        public IActionResult GetMenuAccessById(int id)
        {
            var result = _MenuAccessRepository.GetMenuAccessById(id);
            if(result == null)
            {
                return NotFound("MenuAccess not found!");
            }

            return Ok(result);
        }
        
        [Route("api/[controller]/GetMenuAccessByRoleId")]
        [HttpGet]
        public IActionResult GetMenuAccessByRoleId(int roleid)
        {
            var result = _MenuAccessRepository.GetMenuAccessByRoleId(roleid);
            if(result == null)
            {
                return NotFound("MenuAccess not found!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/CreateMenuAccess")]
        [HttpPost]
        public IActionResult CreateMenuAccess(MenuAccess MenuAccess)
        {
            if (MenuAccess == null)
            {
                return BadRequest("MenuAccess cannot be null");
            }

            var result = _MenuAccessRepository.CreateMenuAccess(MenuAccess, MenuAccess.created_user);
            if (result == null)
            {
                return NotFound("Create MenuAccess Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/UpdateMenuAccessById")]
        [HttpPut]
        public IActionResult UpdateMenuAccessById(MenuAccess MenuAccess)
        {
            if (MenuAccess == null)
            {
                return BadRequest("MenuAccess cannot be null");
            }

            var result = _MenuAccessRepository.UpdateMenuAccessById(MenuAccess, MenuAccess.created_user);
            if (result == null)
            {
                return NotFound("Update MenuAccess Failed!");
            }
                
            return Ok(result);
        }

        [Route("api/[controller]/DeleteMenuAccessById")]
        [HttpDelete]
        public IActionResult DeleteMenuAccessById([FromQuery] long id)
        {
            if (id == 0)
            {
                return BadRequest("Id MenuAccess cannot be null");
            }

            var result = _MenuAccessRepository.DeleteMenuAccessById(id);
            if (result == null)
            {
                return NotFound("Delete MenuAccess Failed!");
            }

            return Ok(result);
        }
    }
}
