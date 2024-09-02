using backend.Context;
using backend.MenuModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.MenuModule.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        public List<Menu> GetMenus()
        {
            return _context.Menu.OrderBy(p => p.id).ToList();
        }
        
        public Menu GetMenuById(long id)
        {
            return _context.Menu.Where(a => a.id == id).FirstOrDefault();
        }

        public string CreateMenu(Menu Menu, string user)
        {
            try
            {
                Menu.active = true;
                Menu.created_date = DateTime.Now;
                Menu.created_user = user;

                _context.Menu.Add(Menu);
                _context.SaveChanges(); 

                return "Insert Menu Success!";
            }
            catch (Exception ex)
            {
                return $"Insert Menu Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateMenuById(Menu Menu, string user)
        {
            try
            {
                // Fetch the existing Menu from the database
                var existingMenu = GetMenuById(Menu.id);
                if (existingMenu == null)
                {
                    // Menu with the given ID does not exist
                    return "Menu doesnt exists!";
                }

                // Update properties
                existingMenu.name = Menu.name;
                existingMenu.active = Menu.active;
                existingMenu.updated_user = user;
                existingMenu.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.Menu.Update(existingMenu);
                _context.SaveChanges();
                return "Update Menu Success!";
            }
            catch (Exception ex)
            {
                return $"Update Menu Failed with Error :{ex.InnerException}"; // Failure
            }

        }

        public string DeleteMenuById(long id)
        {
            try
            {

                // Fetch the existing Menu from the database
                var existingMenu = GetMenuById(id);
                if (existingMenu == null)
                {
                    // Menu with the given ID does not exist
                    return "Menu doesnt exists!";
                }

                // Mark the entity for deletion
                _context.Menu.Remove(existingMenu);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete Menu Success!";
            }
            catch (Exception ex)
            {
                return $"Delete Menu Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
