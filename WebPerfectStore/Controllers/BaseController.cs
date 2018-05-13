using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebPerfectStore.Intefaces;
using WebPerfectStore.Models;
using WebPerfectStore.Services;

namespace WebPerfectStore.Controllers
{
    public class BaseController : Controller
    {
        public AdminMemberProvider Provider = (AdminMemberProvider)Membership.Provider;
        protected Na_project db = new Na_project();
        private Agent _currentAgent;
        public Agent CurrentUser
        {
            get
            {
                if (_currentAgent == null)
                {

                    var login = User.Identity.GetUserName();
                    _currentAgent= db.Agents.FirstOrDefault (x => x.Login == login);
                }
                return _currentAgent;
            }
        }
        public async Task<Agent> GetCurrentUserAsync()
        {
            var login = User.Identity.GetUserName();
            return await db.Agents.FirstOrDefaultAsync(x => x.Login == login);

        }

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null)
                FormsService = new FormsAuthenticationService();

            if (MembershipService == null)
                MembershipService = new AccountMembershipService();

            base.Initialize(requestContext);
        }

    }
}