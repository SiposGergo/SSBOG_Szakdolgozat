using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Controllers
{
    public class MyController:Controller
    {
        protected int GetLoggedInUserId()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (userId == null || userId == "" || !Int32.TryParse(userId, out int id))
            {
                throw new ApplicationException("Felhasználó nem található!");
            }
            return id;
        }
    }
}
