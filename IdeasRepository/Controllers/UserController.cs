using IdeasRepository.Models;
using IdeasRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IdeasRepository.Controllers
{
    [HandleError]
    [Authorize]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService) 
        {
            this.userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginData loginData)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Message = "Not valid data!";
                return ShowLoginPage();
            }
            if (!userService.Login(loginData))
            {
                ViewBag.Message = "Such user is not found! Please, check your input.";
                return ShowLoginPage();    
            }
            FormsAuthentication.SetAuthCookie(loginData.Email, false);
            return RedirectToAction("GetUserRecords", "Record");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ShowLoginPage");
        }

        public ActionResult Error() 
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User user) 
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Not valid user's data!";
                return ShowRegisterPage();
            }
            if (userService.IsUserExists(user)) 
            {
                ViewBag.Message = "Such is already exists";
                return ShowRegisterPage();
            }
            RegisterUser(user);
            return RedirectToAction("GetUserRecords","Record");
        }

        [AllowAnonymous]
        public ActionResult ShowLoginPage()
        {
            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult ShowRegisterPage()
        {
            return View("Register");
        }
        
        private void RegisterUser(User user) 
        {
            string role = user.Type.ToString();
            Membership.CreateUser(user.Email, user.Password);
            Roles.AddUserToRole(user.Email, role);
            userService.Save(user);
            FormsAuthentication.SetAuthCookie(user.Email, false);
        }

    }
}
