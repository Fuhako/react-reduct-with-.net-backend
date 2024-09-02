using backend.Context;
using backend.MenuAccessModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.MenuAccessModule.Repository
{
    public class MenuAccessRepository : IMenuAccessRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuAccessRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        public List<MenuAccess> GetMenuAccess()
        {
            return _context.MenuAccess.OrderBy(p => p.id).ToList();
        }
        
        public MenuAccess GetMenuAccessById(long id)
        {
            return _context.MenuAccess.Where(a => a.id == id).FirstOrDefault();
        }
        public List<MenuAccess> GetMenuAccessByRoleId(long roleid)
        {
            return _context.MenuAccess.Where(a => a.role_id == roleid).ToList();
        }
        public string CreateMenuAccess(MenuAccess MenuAccess, string user)
        {
            try
            {
                MenuAccess.active = true;
                MenuAccess.created_date = DateTime.Now;
                MenuAccess.created_user = user;

                _context.MenuAccess.Add(MenuAccess);
                _context.SaveChanges(); 

                return "Insert MenuAccess Success!";
            }
            catch (Exception ex)
            {
                return $"Insert MenuAccess Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateMenuAccessById(MenuAccess MenuAccess, string user)
        {
            try
            {
                // Fetch the existing MenuAccess from the database
                var existingMenuAccess = GetMenuAccessById(MenuAccess.id);
                if (existingMenuAccess == null)
                {
                    // MenuAccess with the given ID does not exist
                    return "MenuAccess doesnt exists!";
                }

                // Update properties
                existingMenuAccess.menu_id = MenuAccess.menu_id;
                existingMenuAccess.role_id = MenuAccess.role_id;
                existingMenuAccess.active = MenuAccess.active;
                existingMenuAccess.updated_user = user;
                existingMenuAccess.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.MenuAccess.Update(existingMenuAccess);
                _context.SaveChanges();
                return "Update MenuAccess Success!";
            }
            catch (Exception ex)
            {
                return $"Update MenuAccess Failed with Error :{ex.InnerException}"; // Failure
            }

        }


        public string DeleteMenuAccessById(long id)
        {
            try
            {

                // Fetch the existing MenuAccess from the database
                var existingMenuAccess = GetMenuAccessById(id);
                if (existingMenuAccess == null)
                {
                    // MenuAccess with the given ID does not exist
                    return "MenuAccess doesnt exists!";
                }

                // Mark the entity for deletion
                _context.MenuAccess.Remove(existingMenuAccess);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete MenuAccess Success!";
            }
            catch (Exception ex)
            {
                return $"Delete MenuAccess Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
