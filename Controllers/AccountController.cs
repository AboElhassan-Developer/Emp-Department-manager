using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectMVC.Models;
using ProjectMVC.ViewModel;

namespace ProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> SaveRegister
            (RegisterUserViewModel UserViewModel)
        {

            if(ModelState.IsValid)
            {
                //Mapping
                ApplicationUser appUser = new ApplicationUser();
                appUser.Address=UserViewModel.Address;
                appUser.UserName=UserViewModel.UserName;
                appUser.PasswordHash = UserViewModel.Password;


                //SaveDatabase
              IdentityResult result= 
                    await userManager.CreateAsync(appUser,UserViewModel.Password);
                if(result.Succeeded)
                {
                    //assign to Role
                    await userManager.AddToRoleAsync(appUser, "Admin");
                    //Cookie
                  await signInManager.SignInAsync(appUser, false);
                    return RedirectToAction("Index", "Department");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }


            }

            return View("Register",UserViewModel);


        }

        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//requests.form[_requests]
        public async Task <IActionResult> SaveLogin(LoginUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //Check found

               ApplicationUser appUser=
                    await userManager.FindByNameAsync(userViewModel.UserName);
                if(appUser != null)
                {
                    bool found=
                        await userManager.CheckPasswordAsync(appUser, userViewModel.Password);
               
                if(found==true)
                    {
                       await signInManager.SignInAsync(appUser, userViewModel.RememberMe);
                        return RedirectToAction("Index", "Department");
                    }
                }
                ModelState.AddModelError("", "Username OR Password Wrong");
                //create cookie
            }
            return View("Login",userViewModel);

        }

        public async Task <IActionResult> SignOut()
        {
          await  signInManager.SignOutAsync();

           
            TempData["Message"] = "You have been logged out successfully.";

            
            return RedirectToAction("Login", "Account");
        }
    }
}
