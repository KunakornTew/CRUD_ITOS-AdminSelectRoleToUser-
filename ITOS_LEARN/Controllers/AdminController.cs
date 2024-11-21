using Microsoft.AspNetCore.Mvc;
using ITOS_LEARN.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ITOS_LEARN.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmAndChangeRole(int userId, string newRole)
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name);

            if (currentUser == null || currentUser.Role != "Admin")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.User_ID == userId);
            if (user != null)
            {
                user.Role = newRole;
                user.IsConfirmed = true;

                _context.SaveChanges();

                // อัพเดท Claims และ Sign-in ใหม่
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            }

            return RedirectToAction("AdminApproval", "Home");  // ถ้าไม่มี Referer จะกลับไปที่หน้า UserList
        }
    }
}



