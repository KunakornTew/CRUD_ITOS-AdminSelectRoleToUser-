using ITOS_LEARN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ITOS_LEARN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ITOS_LEARN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("User");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Login", "Account");
            }

            var currentUser = _context.Users.FirstOrDefault(u => u.Username == username);
            bool isConfirmed = currentUser?.IsConfirmed ?? false;

            ViewData["IsConfirmed"] = isConfirmed;  // ส่งค่า isConfirmed ไปยัง View

            return View();
        }

        // หน้า Admin ดูรายชื่อผู้ใช้ที่รอการอนุมัติ
        public async Task<IActionResult> AdminApproval()
        {
            var role = HttpContext.Session.GetString("Role");

            // ตรวจสอบว่า User มีสิทธิ์เป็น Admin หรือไม่
            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home"); // ถ้าไม่ใช่ Admin ให้กลับไปหน้า Home
            }

            // ดึงข้อมูลผู้ใช้ที่มี Role = "User" และยังไม่ได้รับการยืนยัน (IsConfirmed = false)
            var usersWaitingForApproval = await _context.Users
                .Where(u => u.Role != "Admin") // กรองผู้ใช้ที่เป็น User และยังไม่ได้ยืนยัน
                .ToListAsync();

            return View(usersWaitingForApproval); // ส่งข้อมูลไปยัง View
        }
    }
  }
