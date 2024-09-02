using backend.MenuAccessModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.MenuAccessModule.Repository
{
    public interface IMenuAccessRepository
    {
        List<MenuAccess> GetMenuAccess();
        MenuAccess GetMenuAccessById(long id);
        List<MenuAccess> GetMenuAccessByRoleId(long roleid);
        string CreateMenuAccess(MenuAccess MenuAccess, string user);
        string UpdateMenuAccessById(MenuAccess MenuAccess, string user);
        string DeleteMenuAccessById(long id);
    }
}
