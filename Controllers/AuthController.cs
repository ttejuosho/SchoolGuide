using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGuide
{
    public class AuthController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Auth/SignIn")]
        [Route("/SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Auth/SignUp")]
        [Route("/SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Auth/SignIn")]
        public async Task<IActionResult> SignIn(SigninModel signinData, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (signinData != null)
                {
                    var result = await signInManager.PasswordSignInAsync(signinData.Email, signinData.Password, signinData.RememberMe, false).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                            return LocalRedirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }
            return View(signinData);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Auth/SignUp")]
        public async Task<IActionResult> SignUp(SignupModel signupData)
        {
            if (ModelState.IsValid)
            {
                if (signupData != null)
                {
                    var user = new IdentityUser { UserName = signupData.Email, Email = signupData.Email };
                    var result = await userManager.CreateAsync(user, signupData.Password).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(signupData);
        }

        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction("Index", "Home");
        }
    }
}
