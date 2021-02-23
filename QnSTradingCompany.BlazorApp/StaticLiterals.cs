//@QnSCodeCopy
namespace QnSTradingCompany.BlazorApp
{
    public sealed partial class StaticLiterals
    {
        static StaticLiterals()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        #region General
        public static int[] DefaultPageSizes { get; private set; } = new int[] { 25, 50, 100, 200 };
        public static string ListFor => nameof(ListFor);
        public static string SelectFor => nameof(SelectFor);
        #endregion

        #region Session
        public static string AppStartedTimeKey => nameof(AppStartedTimeKey);
        public static string AuthorizationSessionKey => nameof(AuthorizationSessionKey);
        public static string SessionHistoryKey => nameof(SessionHistoryKey);
        public static string BeforeLoginPageKey => nameof(BeforeLoginPageKey);
        #endregion

        #region Pages
        public static string LoginPage => "Login";
        #endregion Pages
    }
}
