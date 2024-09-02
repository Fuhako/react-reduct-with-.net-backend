using backend.Context;
using backend.MenuModule.Model;
using backend.MenuModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.MenuModule.Controller
{
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _MenuRepository;
        public MenuController(IMenuRepository MenuRepository, ApplicationDbContext context) 
        {
            _MenuRepository = MenuRepository;
        }

        [Route("api/[controller]/GetMenu")]
        [HttpGet]
        public IActionResult GetMenu()
        {
            var result = _MenuRepository.GetMenus();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/[controller]/GetMenuById")]
        [HttpGet]
        public IActionResult GetMenuById(int id)
        {
            var result = _MenuRepository.GetMenuById(id);
            if(result == null)
            {
                return NotFound("Menu not found!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/CreateMenu")]
        [HttpPost]
        public IActionResult CreateMenu(Menu Menu)
        {
            if (Menu == null)
            {
                return BadRequest("Menu cannot be null");
            }

            var result = _MenuRepository.CreateMenu(Menu, Menu.created_user);
            if (result == null)
            {
                return NotFound("Create Menu Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/UpdateMenuById")]
        [HttpPut]
        public IActionResult UpdateMenuById(Menu Menu)
        {
            if (Menu == null)
            {
                return BadRequest("Menu cannot be null");
            }

            var result = _MenuRepository.UpdateMenuById(Menu, Menu.created_user);
            if (result == null)
            {
                return NotFound("Update Menu Failed!");
            }
                
            return Ok(result);
        }

        [Route("api/[controller]/DeleteMenuById")]
        [HttpDelete]
        public IActionResult DeleteMenuById([FromQuery] long id)
        {
            if (id == 0)
            {
                return BadRequest("Id Menu cannot be null");
            }

            var result = _MenuRepository.DeleteMenuById(id);
            if (result == null)
            {
                return NotFound("Delete Menu Failed!");
            }

            return Ok(result);
        }
    }
}
