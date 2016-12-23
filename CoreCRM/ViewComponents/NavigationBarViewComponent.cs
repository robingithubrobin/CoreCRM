using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCRM.Data;
using CoreCRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CoreCRM.ViewComponents
{
    public class NavigationBarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public NavigationBarViewComponent(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
			// if (_signInManager.IsSignedIn(HttpContext.User)) {
			//     wait FillModel();
			// }
            await Task.Delay(10);

			return View();
        }

        // private async Task FillModel(NavigationBarViewModel model)
        // {
        //     var user = await _userManager.GetUserAsync(HttpContext.User);
        //     if (user == null) return;

        //     model.UserName = user.UserName;

        //     _context.Entry(user).Reference(u => u.Profile).Load();
        //     model.UserAvatar = user.Profile.Avatar;

        //     _context.Entry(user).Reference(u => u.CustomUi);
        //     model.MainItems = GetMainMenu(user.CustomUi);
        //     model.MoreItems = GetMoreMenu(user.CustomUi);
        //     model.ShortcutMenu = GetShortcutMenu(user.CustomUi);
        //     model.UserMenu = GetUserMenu(user.CustomUi);

        //     model.MessageNum = GetMessageNum(user);
        //     model.ScheduleNum = GetScheduleNum(user);
        //     model.TaskNum = GetTaskNum(user);

        //     var descriptor = (ControllerActionDescriptor)ViewContext.ActionDescriptor;
        //     model.CurrentActionName = descriptor.ActionName;
        //     model.CurrentControllerName = descriptor.ControllerName;
        // }

        // private IEnumerable<NavigationBarItem> GetUserMenu(CustomUi customUi)
        // {
        //     var menuItems = new List<NavigationBarItem>();
        //     menuItems.Add(new NavigationBarItem() {
        //         Text = "测试用户",
        //         AreaName = "",
        //         ActionName = "Index",
        //         ControllerName = "Home"
        //     });

        //     return menuItems;
        // }

        // private IEnumerable<NavigationBarItem> GetShortcutMenu(CustomUi customUi)
        // {
        //     var menuItems = new List<NavigationBarItem>();
        //     menuItems.Add(new NavigationBarItem() {
        //         Text = "测试",
        //         AreaName = "",
        //         ActionName = "Index",
        //         ControllerName = "Home"
        //     });

        //     return menuItems;
        // }

        // private IEnumerable<NavigationBarItem> GetMoreMenu(CustomUi customUi)
        // {
        //     var menuItems = new List<NavigationBarItem>();
        //     menuItems.Add(new NavigationBarItem() {
        //         Text = "商机",
        //         AreaName = "",
        //         ActionName = "Index",
        //         ControllerName = "Home"
        //     });

        //     return menuItems;
        // }

        // private IEnumerable<NavigationBarItem> GetMainMenu(CustomUi customUi)
        // {
        //     var menuItems = new List<NavigationBarItem>();
        //     menuItems.Add(new NavigationBarItem() {
        //         Text = "线索",
        //         AreaName = "",
        //         ActionName = "Index",
        //         ControllerName = "Home"
        //     });
        //     menuItems.Add(new NavigationBarItem() {
        //         Text = "客户",
        //         AreaName = "",
        //         ActionName = "Index",
        //         ControllerName = "Home"
        //     });
        //     menuItems.Add(new NavigationBarItem() {
        //         Text = "商机",
        //         AreaName = "",
        //         ActionName = "Index",
        //         ControllerName = "Home"
        //     });

        //     return menuItems;
        // }

        // private int GetTaskNum(ApplicationUser user)
        // {
        //     return 0;
        // }

        // private int GetScheduleNum(ApplicationUser user)
        // {
        //     return 0;
        // }

        // private int GetMessageNum(ApplicationUser user)
        // {
        //     return 0;
        // }

    }
}
