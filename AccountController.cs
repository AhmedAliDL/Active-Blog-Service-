using Active_Blog_Service.Models;
using Active_Blog_Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Data;
namespace Active_Blog_Service.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager ,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerUserViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new User();
                user.FName = registerUserViewModel.FName;
                user.LName = registerUserViewModel.LName;
                user.Email = registerUserViewModel.Email;
                user.PhoneNumber = registerUserViewModel.Phone;
                user.PasswordHash = registerUserViewModel.Password;
                user.Address = registerUserViewModel.Address;

           
                if (registerUserViewModel.ImageFile != null && registerUserViewModel.ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"); // Ensure this folder exists
                    var fileName = Path.GetFileName(registerUserViewModel.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await registerUserViewModel.ImageFile.CopyToAsync(stream);
                    }

                    // Save the image path to the user model
                    user.Image = $"/images/{fileName}"; // Store relative path for later use
                }

                user.UserName = registerUserViewModel.Email;

                var result = await _userManager.CreateAsync(user,
                    registerUserViewModel.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,true);
                    
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                    ModelState.AddModelError("Password",error.Description);
            }
            return View(registerUserViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUserViewModel)
        {
            if(ModelState.IsValid)
            {
                var userModel = await _userManager.FindByEmailAsync(loginUserViewModel.Email);
               

                if (userModel != null)
                {
                    //create cookie
                    var result = await _userManager.CheckPasswordAsync(userModel,loginUserViewModel.Password);
                    if (result)
                    {
                        await _signInManager.SignInAsync(userModel, loginUserViewModel.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("Password", "Password is not correct");
                }
                ModelState.AddModelError("Email", "Email is not found");
            }
            return View(loginUserViewModel);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AccountEditViewModel accountEditViewModel)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
               user.Email = accountEditViewModel.Email?? user.Email;
               user.Address = accountEditViewModel.Address?? user.Address;
               user.PhoneNumber = accountEditViewModel.Phone?? user.PhoneNumber;
               user.FName = accountEditViewModel.FName?? user.FName;
               user.LName = accountEditViewModel.LName?? user.LName;
               user.PasswordHash = accountEditViewModel.Password?? user.PasswordHash;

                if (accountEditViewModel.ImageFile != null && accountEditViewModel.ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"); // Ensure this folder exists
                    var fileName = Path.GetFileName(accountEditViewModel.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await accountEditViewModel.ImageFile.CopyToAsync(stream);
                    }
                    var oldImagePath = @$"wwwroot{user.Image}";
                    System.IO.File.Delete(oldImagePath);
                    // Save the image path to the user model
                    user.Image = $"/images/{fileName}"; // Store relative path for later use
                }
                user.UserName = accountEditViewModel.Email ?? user.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(accountEditViewModel);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
