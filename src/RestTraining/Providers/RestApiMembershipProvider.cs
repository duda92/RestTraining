using System;
using System.Linq;
using System.Web.Security;
using RestTraining.Common.Authorization;
using System.Collections.Generic;

namespace RestTraining.Api.Providers
{
    public class RestApiMembershipProvider : MembershipProvider
    {
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            // TODO: Implement this method
            totalRecords = 0;
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            // TODO: Implement this method
            totalRecords = 0;
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            // TODO: Implement this method
            totalRecords = 0;
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public override string ApplicationName
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public override MembershipUser CreateUser(string username,
            string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved,
            object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            return usersPasswords.Any(x => x.Key == username && x.Value == password);
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public static List<KeyValuePair<string, string>> usersPasswords = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string> ("client1", "client1"),
            new KeyValuePair<string, string> ("testClient1", "testClient1"),
        };

    }

}