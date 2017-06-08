using MVCProject.Models.DataBaseProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //[Authorize]
        public PartialViewResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    User u;
                    using (var ctx = new InternertMarkertEntitesContainer())
                    {
                        u = ctx.Users.Find(int.Parse(User.Identity.Name));
                    }
                    if (u == null)
                        throw new Exception();
                    return PartialView("_UserPage", u);
                }
                catch (Exception)
                {
                    return PartialView("_LoginPage");
                }
            }
            else
                return PartialView("_LoginPage");
        }

        public ActionResult LoginPage()
        {
            return PartialView("_LoginPage");
        }

        public ActionResult UserPage()
        {
            return PartialView("_UserPage");
        }

        public ActionResult Add()
        {
            User user = null;

            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (var ctx = new InternertMarkertEntitesContainer())
                    {
                        user = ctx.Users.Find(int.Parse(User.Identity.Name));
                    }
                }
                catch (Exception) { }
            }

                return View(user);
        }
        [HttpPost]
        public ActionResult Add(User newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var ctx = new InternertMarkertEntitesContainer())
                    {
                        if (User.Identity.IsAuthenticated)// update user
                        {
                            User oldUser = ctx.Users.Find(int.Parse(User.Identity.Name));// get user to update
                            UpdateModel(oldUser, newUser);
                        }
                        else// add user
                        {
                            if (ctx.Users.Where(x => x.UserName == newUser.UserName).Count() == 0)
                                ctx.Users.Add(newUser);
                        }
                        ctx.SaveChanges();
                    }
                    Login(newUser);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Add", newUser);
                }
            }
            catch (Exception e)
            {
               return View("Add", newUser);
            }
        }

        private void UpdateModel(User oldUser, User newUser)
        {
            oldUser.BirthDate = newUser.BirthDate;
            oldUser.Email = newUser.Email;
            oldUser.FirstName = newUser.FirstName;
            oldUser.LastName = newUser.LastName;
            oldUser.Id = newUser.Id;
        }

        public ActionResult Login(User logUser)
        {
            ModelState.Add("loggin", new System.Web.Mvc.ModelState());
            if (ModelState.IsValidField("UserName")& ModelState.IsValidField("Password"))
            {
                try
                {
                    using (var ctx = new InternertMarkertEntitesContainer())
                    {
                        // verific the user
                        var user = ctx.Users
                            .Where(x => x.UserName == logUser.UserName & x.Password == logUser.Password)
                            .FirstOrDefault();
                        if (user != null)
                            System.Web.Security.FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                        else
                            ModelState.AddModelError("Loggin","Not valid Username or Password");
                    }
                }
                catch (Exception) { } 
            }
            else
            {
                ModelState.AddModelError("Loggin", "User name or passoword not valid, must be under 50 letter");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}