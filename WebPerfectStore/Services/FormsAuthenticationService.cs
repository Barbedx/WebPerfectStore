using System;
using System.Web.Security;
using WebPerfectStore.Intefaces;

namespace WebPerfectStore.Services
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (userName == null) throw new ArgumentException("Value Cannot be null or empty.", "username");
            FormsAuthentication.SetAuthCookie( userName,createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}