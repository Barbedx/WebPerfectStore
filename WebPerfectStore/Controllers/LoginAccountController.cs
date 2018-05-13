using System.Web.Mvc;
using WebPerfectStore.Models;

namespace WebPerfectStore.Controllers
{

    [Authorize]
    public class LoginAccountController : BaseController
    {
        public ActionResult LogOn()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult LogOn(string userName, string password, string returnUrl)
        //{
        //    if (!ValidateLogOn(userName, password))
        //        return View();
        //    web_Agents user = Membership.GetUser() as web_Agents;
        //    FormsAuthentication.SetAuthCookie(user.Login, false);
        //    if (!String.IsNullOrEmpty(returnUrl) && returnUrl != "/")
        //        return Redirect(returnUrl);
        //    else
        //        return RedirectToAction("Index", "Home");
        //}


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid
                && MembershipService.ValidateUser(model.Login, model.Password))
            {
                FormsService.SignIn(model.Login, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl) &&
                    returnUrl.Length > 1 &&
                    returnUrl.StartsWith("/") &&
                    !returnUrl.StartsWith("//") &&
                    !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Логин или пароль неверный.");
            return View(model);
        }


        public ActionResult LogOff()
        {
            FormsService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //private bool ValidateLogOn(string userName, string password)
        //{
        //    if (String.IsNullOrEmpty(userName))
        //    {
        //        ModelState.AddModelError("username", "You must specify a username.");
        //    }
        //    if (String.IsNullOrEmpty(password))
        //    {
        //        ModelState.AddModelError("password", "You must specify a password.");
        //    }
        //    //}
        //    //if (!provider.ValidateUser(userName, password))
        //    //{
        //    //    ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
        //    //}
        //    return ModelState.IsValid;
        //}
    }
}