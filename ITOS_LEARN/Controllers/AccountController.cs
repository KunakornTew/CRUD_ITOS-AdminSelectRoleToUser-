using Microsoft.AspNetCore.Mvc;
using ITOS_LEARN.Models;
using ITOS_LEARN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ITOS_LEARN.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            passwordHasher = new PasswordHasher<User>();
            _context = context;
        }

        // หน้า Login GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // หน้า Login POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Email == model.Email);

                if (user != null)
                {
                    var result = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        // เก็บข้อมูลลงใน session
                        HttpContext.Session.SetString("User", user.Username);
                        HttpContext.Session.SetString("Role", user.Role);
                        HttpContext.Session.SetString("IsConfirmed", user.IsConfirmed.ToString());

                        var login = new Login
                        {
                            Username = user.Username,
                            LoginTime = DateTime.Now,
                            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
                        };

                        _context.Logins.Add(login);
                        await _context.SaveChangesAsync();

                        // สร้าง Claims สำหรับผู้ใช้
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("IsConfirmed", user.IsConfirmed.ToString())
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        // Sign-in ผู้ใช้
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        // ตรวจสอบ Role และสถานะการยืนยัน
                        if (user.Role == "Admin")
                        {
                            return RedirectToAction("Index", "People");
                        }
                        else if (user.Role == "User" && user.IsConfirmed)
                        {
                            return RedirectToAction("Index", "People");
                        }
                        else if (user.Role == "User" && !user.IsConfirmed)
                        {
                            return RedirectToAction("Index", "Waiting");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username, password, or email.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username, password, or email.");
                }
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        // หน้า Register GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var existingEmail = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                // เปลี่ยนจาก model เป็น user
                if (user.Role == "Admin")
                {
                    user.IsConfirmed = true;
                }
                else
                {
                    user.IsConfirmed = false;
                }

                // แฮชรหัสผ่าน
                var hashPassword = passwordHasher.HashPassword(user, user.Password);
                user.Password = hashPassword;

                // บันทึกข้อมูลผู้ใช้ใหม่
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // อัปเดต Permission ที่ UserId เป็น null
                var userId = user.User_ID;

                // เฉพาะ Permission ที่ RoleId ของผู้ใช้ตรงกับ RoleId ของ Permission ที่มี UserId เป็น null
                var permissions = _context.Permissions
                    .Where(p => p.UserId == null && p.RoleId == user.Role)
                    .ToList();

                foreach (var permission in permissions)
                {
                    permission.UserId = userId;
                }

                // บันทึกการเปลี่ยนแปลงของ Permission
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // เก็บชื่อผู้ใช้จาก session ก่อน
            var username = HttpContext.Session.GetString("User");

            if (!string.IsNullOrEmpty(username))
            {
                // เพิ่ม LogoutTime ในฐานข้อมูล
                var login = await _context.Logins
                    .Where(l => l.Username == username && l.LogoutTime == null)  // ตรวจสอบว่า LogoutTime ยังเป็น null
                    .OrderByDescending(l => l.LoginTime)
                    .FirstOrDefaultAsync();

                if (login != null)
                {
                    login.LogoutTime = DateTime.Now;
                    await _context.SaveChangesAsync();  // บันทึกการเปลี่ยนแปลง
                }

                // ลบข้อมูลใน session ที่เก็บ user
                HttpContext.Session.Remove("User");
            }

            // พาผู้ใช้กลับไปหน้า Login
            return RedirectToAction("Login", "Account");
        }
    }
}
