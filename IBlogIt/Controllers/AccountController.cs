using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBlogIt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IBlogIt.Models.Account;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IBlogIt.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.LoginState = "Registerd";
            if (User.Identity.IsAuthenticated)
                return View("AlreadyLogged");
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registeration)
        {
            if (!ModelState.IsValid)
                return View(registeration);

            var newUser = new IdentityUser
            {
                Email = registeration.EmailAddress,
                UserName = registeration.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, registeration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }
                return View();
            }

            return RedirectToAction("Login");                  
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.LoginState = "Logined in";
            if (User.Identity.IsAuthenticated)
                return View("AlreadyLogged");
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _signInManager.PasswordSignInAsync(
                login.UserName, login.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login Error!");
                return View();
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task <IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl);
        }

    }
}
