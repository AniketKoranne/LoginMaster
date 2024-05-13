using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using LoginMaster.App_Data;
using LoginMaster.Models;

namespace LoginMaster.Controllers
{
    public class UserController : Controller
    {
        private AppDbContext db = new AppDbContext(); // Assuming you have a DbContext

        // GET: /User/Index
        public ActionResult Index()
        {
            return View();
        }

        // POST: /User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (db.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View("Index", model);
                }

                // Hash password
                var passwordSalt = GenerateSalt();
                var passwordHash = HashPassword(model.Password, passwordSalt);

                // Create new user
                var user = new User
                {
                    UserName = model.UserName,
                    Phone = model.Phone,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Organization = model.Organization,
                    Department = model.Department,
                    IsActive = true // Set account as active
                };

                db.Users.Add(user);
                db.SaveChanges();

                // Redirect to login after successful registration
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        // GET: /User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user != null && VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    // Successful login, implement your authentication mechanism
                    // For example, set authentication cookie or JWT token
                    return Json(new { success = true });                    
                }
                else
                {
                    return Json(new { success = false });
                }
            }

            return View("Index", model);
        }

        // Helper methods for password hashing and salt generation
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, saltedPassword, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, saltBytes.Length, passwordBytes.Length);
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string savedHash, string salt)
        {
            var hashedPassword = HashPassword(password, salt);
            return hashedPassword == savedHash;
        }
    }
}
