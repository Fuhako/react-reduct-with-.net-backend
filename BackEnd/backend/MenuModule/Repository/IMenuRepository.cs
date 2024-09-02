using backend.MenuModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.MenuModule.Repository
{
    public interface IMenuRepository
    {
        List<Menu> GetMenus();
        Menu GetMenuById(long id);
        string CreateMenu(Menu Menu, string user);
        string UpdateMenuById(Menu Menu, string user);
        string DeleteMenuById(long id);
    }
}
