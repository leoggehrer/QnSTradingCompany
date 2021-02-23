//@QnSCodeCopy
//MdStart

using QnSTradingCompany.Contracts.Client;

namespace QnSTradingCompany.Logic
{
    public static partial class Factory
    {
        static Factory()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public enum PersistenceType
        {
            Db,
            //Csv,
            //Ser,
        }
        public static PersistenceType Persistence { get; set; } = Factory.PersistenceType.Db;
        internal static DataContext.IContext CreateContext()
        {
            DataContext.IContext result = null;

            if (Persistence == PersistenceType.Db)
            {
                result = new DataContext.Db.QnSTradingCompanyDbContext();
            }
            return result;
        }

        public static IAccountManager CreateAccountManager() => new Modules.Account.AccountManagerWrapper();
    }
}
//MdEnd
