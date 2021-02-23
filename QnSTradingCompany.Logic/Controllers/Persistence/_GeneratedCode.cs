//@QnSGeneratedCode
namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
    sealed partial class UserController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Account.IUser, Entities.Persistence.Account.User>
    {
        static UserController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal UserController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal UserController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
    [Logic.Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    sealed partial class RoleController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Account.IRole, Entities.Persistence.Account.Role>
    {
        static RoleController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal RoleController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal RoleController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
    [Logic.Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    sealed partial class LoginSessionController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Account.ILoginSession, Entities.Persistence.Account.LoginSession>
    {
        static LoginSessionController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal LoginSessionController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal LoginSessionController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
    [Logic.Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    sealed partial class IdentityXRoleController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Account.IIdentityXRole, Entities.Persistence.Account.IdentityXRole>
    {
        static IdentityXRoleController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal IdentityXRoleController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal IdentityXRoleController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
    [Logic.Modules.Security.Authorize("SysAdmin", "AppAdmin")]
    sealed partial class IdentityController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Account.IIdentity, Entities.Persistence.Account.Identity>
    {
        static IdentityController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal IdentityController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal IdentityController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
    sealed partial class ActionLogController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Account.IActionLog, Entities.Persistence.Account.ActionLog>
    {
        static ActionLogController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal ActionLogController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal ActionLogController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.App
{
    sealed partial class OrderController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.App.IOrder, Entities.Persistence.App.Order>
    {
        static OrderController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal OrderController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal OrderController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.App
{
    sealed partial class ConditionController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.App.ICondition, Entities.Persistence.App.Condition>
    {
        static ConditionController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal ConditionController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal ConditionController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Configuration
{
    sealed partial class SettingController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Configuration.ISetting, Entities.Persistence.Configuration.Setting>
    {
        static SettingController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal SettingController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal SettingController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Data
{
    sealed partial class BinaryDataController : GenericPersistenceControllerWithRun<QnSTradingCompany.Contracts.Persistence.Data.IBinaryData, Entities.Persistence.Data.BinaryData>
    {
        static BinaryDataController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal BinaryDataController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal BinaryDataController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.Language
{
    sealed partial class TranslationController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.Language.ITranslation, Entities.Persistence.Language.Translation>
    {
        static TranslationController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal TranslationController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal TranslationController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.MasterData
{
    sealed partial class ProductController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.MasterData.IProduct, Entities.Persistence.MasterData.Product>
    {
        static ProductController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal ProductController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal ProductController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Persistence.MasterData
{
    sealed partial class CustomerController : GenericPersistenceController<QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer, Entities.Persistence.MasterData.Customer>
    {
        static CustomerController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        internal CustomerController(DataContext.IContext context):base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        internal CustomerController(ControllerObject controller):base(controller)
        {
            Constructing();
            Constructed();
        }
    }
}
