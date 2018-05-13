using System;
using System.Web.Security;
using WebPerfectStore.Intefaces;

namespace WebPerfectStore.Services
{
    public class AccountMembershipService : IMembershipService
    {

        private readonly MembershipProvider _provider;

        public AccountMembershipService() : this(null)
        {

        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength => _provider.MinRequiredPasswordLength;

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(string userName, string password)
        {
           if( userName == null) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (password == null) throw new ArgumentException("Value cannot be null or empty.", "userName");
            return _provider.ValidateUser(userName, password);
        }
    }
}