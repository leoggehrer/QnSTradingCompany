//@QnSGeneratedCode
namespace QnSTradingCompany.Adapters
{
    public static partial class Factory
    {
        public static Contracts.Client.IAdapterAccess<I> Create<I>()
        {
            Contracts.Client.IAdapterAccess<I> result = null;
            if (Adapter == AdapterType.Controller)
            {
                if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.IProduct))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.IProduct>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Language.ITranslation))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Language.ITranslation>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Data.IBinaryData))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Data.IBinaryData>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Configuration.ISetting))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Configuration.ISetting>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.ICondition))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.App.ICondition>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.IOrder))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.App.IOrder>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IRole))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Account.IRole>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IUser))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Account.IUser>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IAppAccess))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Business.Account.IAppAccess>() as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IIdentityUser))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Business.Account.IIdentityUser>() as Contracts.Client.IAdapterAccess<I>;
                }
            }
            else if (Adapter == AdapterType.Service)
            {
                if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer, Transfer.Persistence.MasterData.Customer>(BaseUri, "Customers") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.IProduct))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.IProduct, Transfer.Persistence.MasterData.Product>(BaseUri, "Products") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Language.ITranslation))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Language.ITranslation, Transfer.Persistence.Language.Translation>(BaseUri, "Translations") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Data.IBinaryData))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Data.IBinaryData, Transfer.Persistence.Data.BinaryData>(BaseUri, "BinaryDatas") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Configuration.ISetting))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Configuration.ISetting, Transfer.Persistence.Configuration.Setting>(BaseUri, "Settings") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.ICondition))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.App.ICondition, Transfer.Persistence.App.Condition>(BaseUri, "Conditions") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.IOrder))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.App.IOrder, Transfer.Persistence.App.Order>(BaseUri, "Orders") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IRole))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(BaseUri, "Roles") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IUser))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Account.IUser, Transfer.Persistence.Account.User>(BaseUri, "Users") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IAppAccess))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IIdentityUser))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Business.Account.IIdentityUser, Transfer.Business.Account.IdentityUser>(BaseUri, "IdentityUsers") as Contracts.Client.IAdapterAccess<I>;
                }
            }
            return result;
        }
        public static Contracts.Client.IAdapterAccess<I> Create<I>(string sessionToken)
        {
            Contracts.Client.IAdapterAccess<I> result = null;
            if (Adapter == AdapterType.Controller)
            {
                if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.IProduct))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.IProduct>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Language.ITranslation))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Language.ITranslation>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Data.IBinaryData))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Data.IBinaryData>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Configuration.ISetting))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Configuration.ISetting>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.ICondition))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.App.ICondition>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.IOrder))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.App.IOrder>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IRole))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Account.IRole>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IUser))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Persistence.Account.IUser>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IAppAccess))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Business.Account.IAppAccess>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IIdentityUser))
                {
                    result = new Controller.GenericControllerAdapter<QnSTradingCompany.Contracts.Business.Account.IIdentityUser>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
                }
            }
            else if (Adapter == AdapterType.Service)
            {
                if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer, Transfer.Persistence.MasterData.Customer>(sessionToken, BaseUri, "Customers") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.IProduct))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.MasterData.IProduct, Transfer.Persistence.MasterData.Product>(sessionToken, BaseUri, "Products") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Language.ITranslation))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Language.ITranslation, Transfer.Persistence.Language.Translation>(sessionToken, BaseUri, "Translations") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Data.IBinaryData))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Data.IBinaryData, Transfer.Persistence.Data.BinaryData>(sessionToken, BaseUri, "BinaryDatas") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Configuration.ISetting))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Configuration.ISetting, Transfer.Persistence.Configuration.Setting>(sessionToken, BaseUri, "Settings") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.ICondition))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.App.ICondition, Transfer.Persistence.App.Condition>(sessionToken, BaseUri, "Conditions") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.IOrder))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.App.IOrder, Transfer.Persistence.App.Order>(sessionToken, BaseUri, "Orders") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IRole))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(sessionToken, BaseUri, "Roles") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IUser))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Persistence.Account.IUser, Transfer.Persistence.Account.User>(sessionToken, BaseUri, "Users") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IAppAccess))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(sessionToken, BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
                }
                else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Business.Account.IIdentityUser))
                {
                    result = new Service.GenericServiceAdapter<QnSTradingCompany.Contracts.Business.Account.IIdentityUser, Transfer.Business.Account.IdentityUser>(sessionToken, BaseUri, "IdentityUsers") as Contracts.Client.IAdapterAccess<I>;
                }
            }
            return result;
        }
    }
}
