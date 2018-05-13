using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using WebPerfectStore.Models;

namespace WebPerfectStore
{
    public class AdminMemberProvider : MembershipProvider
    {


        public override bool EnablePasswordRetrieval { get { throw new NotImplementedException(); } }

        public override bool EnablePasswordReset { get { throw new NotImplementedException(); } }

        public override bool RequiresQuestionAndAnswer { get { throw new NotImplementedException(); } }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts { get { throw new NotImplementedException(); } }

        public override int PasswordAttemptWindow { get { throw new NotImplementedException(); } }

        public override bool RequiresUniqueEmail { get { return false; } }

        public override MembershipPasswordFormat PasswordFormat { get { throw new NotImplementedException(); } }

        public override int MinRequiredPasswordLength { get { return 4; } }

        public override int MinRequiredNonAlphanumericCharacters { get { throw new NotImplementedException(); } }

        public override string PasswordStrengthRegularExpression { get { throw new NotImplementedException(); } }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            var userContext = new Na_project();
            var user = userContext.Agents.SingleOrDefault(x => x.Login == login);
            if (user != null)
            {
            return    new MembershipUser("AdminMemberProvider", user.Name, user.FId, user.Login, 
                    string.Empty, string.Empty, true, false, DateTime.MinValue, 
                    DateTime.MinValue, DateTime.MinValue, DateTime.Now, DateTime.Now);

                
            }
            return null;

        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        public AdminMemberProvider() : this(null) { }

        private Na_project _db;
        public AdminMemberProvider(Na_project database) : base()
        {
            _db = database ?? new Na_project();
        }

        public Agent CurrentUser { get; set; }
        public Agent CreateUser(string name, string pass)
        {
            return null;
        }
        /// <summary>
        /// Procuses an MD5 hash string of the password
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <returns>MD5 Hash string</returns>
        protected byte[] EncryptPassword(string password)
        {
            //we use codepage 1252 because that is what sql server uses
            HashAlgorithm algoritm = new MD5CryptoServiceProvider();
            return algoritm.ComputeHash(Encoding.ASCII.GetBytes(password));
            //byte[] pwdBytes = Encoding.GetEncoding(1252).GetBytes(password);
            //byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(pwdBytes);
            //return Encoding.GetEncoding(1252).GetString(hashBytes);
        }


        public override bool ValidateUser(string username, string password)
        {

            if (string.IsNullOrEmpty(password.Trim())) return false;

            var hash = EncryptPassword(password);
            Agent user = _db.Agents.FirstOrDefault(x => x.Login == username);
            if (user == null) return false;
            if (hash.SequenceEqual(user.Password))
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }
    }
}