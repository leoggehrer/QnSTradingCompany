//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using QnSTradingCompany.Logic.Controllers;
using QnSTradingCompany.Logic.Entities.Persistence.Account;
using QnSTradingCompany.Logic.Modules.Account;
using QnSTradingCompany.Logic.Modules.Exception;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Modules.Security
{
    internal static partial class Authorization
    {
        static Authorization()
        {
            ClassConstructing();
            if (SystemAuthorizationToken.IsNullOrEmpty())
            {
                SystemAuthorizationToken = Guid.NewGuid().ToString();
            }
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        internal static int DefaultTimeOutInMinutes { get; private set; } = 90;
        internal static int DefaultTimeOutInSeconds => DefaultTimeOutInMinutes * 60;
        internal static string SystemAuthorizationToken { get; set; }

        internal static async Task CheckAuthorizationAsync(string sessionToken, MethodBase methodeBase, AccessType accessType)
        {
            sessionToken.CheckArgument(nameof(sessionToken));
            methodeBase.CheckArgument(nameof(methodeBase));

            bool handled = false;

            BeforeCheckAuthorization(sessionToken, methodeBase, accessType, ref handled);
            if (handled == false)
            {
                await CheckAuthorizationInternalAsync(sessionToken, methodeBase, accessType).ConfigureAwait(false);
            }
            AfterCheckAuthorization(sessionToken, methodeBase, accessType);
        }

        private static async Task CheckAuthorizationInternalAsync(string sessionToken, MethodBase methodBase, AccessType accessType)
        {
            var originalMethodBase = methodBase.GetAsyncOriginal();

            if (sessionToken.IsNullOrEmpty())
            {
                var authorization = originalMethodBase.GetCustomAttribute<AuthorizeAttribute>();
                var isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                    throw new AuthorizationException(ErrorType.NotLogedIn);
            }
            else if (sessionToken.Equals(SystemAuthorizationToken) == false)
            {
                var authorization = originalMethodBase.GetCustomAttribute<AuthorizeAttribute>();
                bool isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    var curSession = await AccountManager.QueryAliveSessionAsync(sessionToken).ConfigureAwait(false);

                    if (curSession == null)
                        throw new AuthorizationException(ErrorType.InvalidSessionToken);

                    if (curSession.IsTimeout)
                        throw new AuthorizationException(ErrorType.AuthorizationTimeOut);

                    var isAuthorized = authorization.Roles.Any() == false
                                       || curSession.Roles.Any(lr => authorization.Roles.Contains(lr.Designation));

                    if (isAuthorized == false)
                        throw new AuthorizationException(ErrorType.NotAuthorized);

                    curSession.LastAccess = DateTime.Now;

                    var handled = false;

                    BeforeLogging(originalMethodBase, accessType, ref handled);
                    if (handled == false)
                    {
                        await LoggingAsync(curSession.IdentityId, originalMethodBase.DeclaringType.Name, originalMethodBase.Name, string.Empty).ConfigureAwait(false);
                    }
                    AfterLogging(originalMethodBase, accessType);
                }
            }
        }

        static partial void BeforeCheckAuthorization(string sessionToken, MethodBase methodeBase, AccessType accessType, ref bool handled);
        static partial void AfterCheckAuthorization(string sessionToken, MethodBase methodeBase, AccessType accessType);

        static partial void BeforeLogging(MethodBase methodeBase, AccessType accessType, ref bool handled);
        static partial void AfterLogging(MethodBase methodeBase, AccessType accessType);

        internal static async Task CheckAuthorizationAsync(string sessionToken, Type instanceType, MethodBase methodeBase, AccessType accessType)
        {
            bool handled = false;

            BeforeCheckAuthorization(sessionToken, instanceType, methodeBase, accessType, ref handled);
            if (handled == false)
            {
                await CheckAuthorizationInternalAsync(sessionToken, instanceType, methodeBase, accessType).ConfigureAwait(false);
            }
            AfterCheckAuthorization(sessionToken, instanceType, methodeBase, accessType);
        }

        private static async Task CheckAuthorizationInternalAsync(string sessionToken, Type instanceType, MethodBase methodBase, AccessType accessType)
        {
            static AuthorizeAttribute GetClassAuthorization(Type classType)
            {
                var runType = classType;
                var result = default(AuthorizeAttribute);

                do
                {
                    result = runType.GetCustomAttribute<AuthorizeAttribute>();
                    runType = runType.BaseType;
                } while (result == null && runType != null);
                return result;
            }

            var originalMethodBase = methodBase.GetAsyncOriginal();

            if (sessionToken.IsNullOrEmpty())
            {
                var authorization = originalMethodBase.GetCustomAttribute<AuthorizeAttribute>()
                                  ?? GetClassAuthorization(instanceType);
                var isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    throw new AuthorizationException(ErrorType.NotLogedIn);
                }
            }
            else if (sessionToken.Equals(SystemAuthorizationToken) == false)
            {
                var authorization = originalMethodBase.GetCustomAttribute<AuthorizeAttribute>()
                                    ?? GetClassAuthorization(instanceType);
                var isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    var curSession = await AccountManager.QueryAliveSessionAsync(sessionToken).ConfigureAwait(false);

                    if (curSession == null)
                        throw new AuthorizationException(ErrorType.InvalidSessionToken);

                    if (curSession.IsTimeout)
                        throw new AuthorizationException(ErrorType.AuthorizationTimeOut);

                    bool isAuthorized = authorization.Roles.Any() == false
                                        || curSession.Roles.Any(lr => authorization.Roles.Contains(lr.Designation));

                    if (isAuthorized == false)
                        throw new AuthorizationException(ErrorType.NotAuthorized);

                    curSession.LastAccess = DateTime.Now;
                    var handled = false;

                    BeforeLogging(originalMethodBase, accessType, ref handled);
                    if (handled == false)
                    {
                        await LoggingAsync(curSession.IdentityId, instanceType.Name, originalMethodBase.Name, string.Empty).ConfigureAwait(false);
                    }
                    AfterLogging(originalMethodBase, accessType);
                }
            }

        }

        static partial void BeforeCheckAuthorization(string sessionToken, Type instanceType, MethodBase methodeBase, AccessType accessType, ref bool handled);
        static partial void AfterCheckAuthorization(string sessionToken, Type instanceType, MethodBase methodeBase, AccessType accessType);


        private static Task LoggingAsync(int identityId, string subject, string action, string info)
        {
            return Task.Run(async () =>
            {
                using var actionLogCtrl = new Logic.Controllers.Persistence.Account.ActionLogController(Factory.CreateContext())
                {
                    SessionToken = SystemAuthorizationToken
                };
                var entity = new ActionLog
                {
                    IdentityId = identityId,
                    Time = DateTime.Now,
                    Subject = subject,
                    Action = action,
                    Info = info
                };
                await actionLogCtrl.InsertAsync(entity).ConfigureAwait(false);
                await actionLogCtrl.SaveChangesAsync().ConfigureAwait(false);
            });
        }
    }
}
//MdEnd
