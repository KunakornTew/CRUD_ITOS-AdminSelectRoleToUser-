using ITOS_LEARN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _requiredRole;
    private readonly ApplicationDbContext _context;

    public CustomAuthorizeAttribute(string requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        var username = context.HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(username))
        {
            // ถ้าไม่มีชื่อผู้ใช้ ให้ไปหน้า AccessDenied
            context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            return;
        }

        // ดึงข้อมูลจากฐานข้อมูล
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null || user.Role != _requiredRole)
        {
            // ถ้าไม่พบผู้ใช้หรือ Role ไม่ตรง ให้ไปหน้า AccessDenied
            context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
        }
    }
}
