//@QnSCodeCopy
//MdStart
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Modules.Exception
{
    public static class ErrorMessages
    {
        private static Dictionary<ErrorIdentity, string> Messages { get; } = new Dictionary<ErrorIdentity, string>();
        static ErrorMessages()
        {
            Messages.Add(ErrorIdentity.InvalidLogin, "Invalid login attempt.");
            Messages.Add(ErrorIdentity.InvalidEmailSyntax, "Invalid email syntax.");
            Messages.Add(ErrorIdentity.InvalidPasswordSyntax, "Invalid password syntax.");
            Messages.Add(ErrorIdentity.InvalidPassword, "Invalid password.");
            Messages.Add(ErrorIdentity.AuthorizationNotLoggedIn, "You are not logged in.");
            Messages.Add(ErrorIdentity.AuthorizationInvalidToken, "Invalid authorization token.");
            Messages.Add(ErrorIdentity.AuthorizationTimeOut, "Login time out.");
            Messages.Add(ErrorIdentity.AuthorizationNotAuthorized, "You are not authorized to access this function.");
            Messages.Add(ErrorIdentity.InvalidFilename, "Invalid filename.");
            Messages.Add(ErrorIdentity.DuplicatedFilename, "A file with the same name exists.");
            Messages.Add(ErrorIdentity.EntityNotFound, "An entity with the id '{0}' could not be found.");
        }

        public static string GetMessage(ErrorIdentity identity)
        {
            string defaultMessage = $"Error [{(int)identity}]: {identity}";

            return GetMessage(identity, defaultMessage);
        }
        public static string GetMessage(ErrorIdentity identity, string defaultMessage)
        {
            string result = defaultMessage;

            if (Messages.ContainsKey(identity))
            {
                result = Messages[identity];
            }
            return result;
        }
        public static string GetMessage(ErrorIdentity identity, params object[] args)
        {
            string result;

            if (Messages.ContainsKey(identity))
            {
                result = string.Format(Messages[identity], args);
            }
            else
            {
                result = $"Unknow error identintity [{identity}]";
            }
            return result;
        }
    }
}
//MdEnd
