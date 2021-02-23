//@QnSGeneratedCode
namespace QnSTradingCompany.Logic.Controllers.Business.Account
{
    sealed partial class IdentityUserController : GenericOneToAnotherController<QnSTradingCompany.Contracts.Business.Account.IIdentityUser, Entities.Business.Account.IdentityUser, QnSTradingCompany.Contracts.Persistence.Account.IIdentity, QnSTradingCompany.Logic.Entities.Persistence.Account.Identity, QnSTradingCompany.Contracts.Persistence.Account.IUser, QnSTradingCompany.Logic.Entities.Persistence.Account.User>
    {
        static IdentityUserController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public IdentityUserController(DataContext.IContext context):base(context)
        {
            Constructing();
            oneEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController(this);
            anotherEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.UserController(this);
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public IdentityUserController(ControllerObject controller):base(controller)
        {
            Constructing();
            oneEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController(this);
            anotherEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.UserController(this);
            Constructed();
        }
        private QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController oneEntityController = null;
        protected override GenericController<QnSTradingCompany.Contracts.Persistence.Account.IIdentity, QnSTradingCompany.Logic.Entities.Persistence.Account.Identity> OneEntityController
        {
            get => oneEntityController;
            set => oneEntityController = value as QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController;
        }
        private QnSTradingCompany.Logic.Controllers.Persistence.Account.UserController anotherEntityController = null;
        protected override GenericController<QnSTradingCompany.Contracts.Persistence.Account.IUser, QnSTradingCompany.Logic.Entities.Persistence.Account.User> AnotherEntityController
        {
            get => anotherEntityController;
            set => anotherEntityController = value as QnSTradingCompany.Logic.Controllers.Persistence.Account.UserController;
        }
    }
}
namespace QnSTradingCompany.Logic.Controllers.Business.Account
{
    sealed partial class AppAccessController : GenericOneToManyController<QnSTradingCompany.Contracts.Business.Account.IAppAccess, Entities.Business.Account.AppAccess, QnSTradingCompany.Contracts.Persistence.Account.IIdentity, QnSTradingCompany.Logic.Entities.Persistence.Account.Identity, QnSTradingCompany.Contracts.Persistence.Account.IRole, QnSTradingCompany.Logic.Entities.Persistence.Account.Role>
    {
        static AppAccessController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public AppAccessController(DataContext.IContext context):base(context)
        {
            Constructing();
            oneEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController(this);
            manyEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.RoleController(this);
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public AppAccessController(ControllerObject controller):base(controller)
        {
            Constructing();
            oneEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController(this);
            manyEntityController = new QnSTradingCompany.Logic.Controllers.Persistence.Account.RoleController(this);
            Constructed();
        }
        private QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController oneEntityController = null;
        protected override GenericController<QnSTradingCompany.Contracts.Persistence.Account.IIdentity, QnSTradingCompany.Logic.Entities.Persistence.Account.Identity> OneEntityController
        {
            get => oneEntityController;
            set => oneEntityController = value as QnSTradingCompany.Logic.Controllers.Persistence.Account.IdentityController;
        }
        private QnSTradingCompany.Logic.Controllers.Persistence.Account.RoleController manyEntityController = null;
        protected override GenericController<QnSTradingCompany.Contracts.Persistence.Account.IRole, QnSTradingCompany.Logic.Entities.Persistence.Account.Role> ManyEntityController
        {
            get => manyEntityController;
            set => manyEntityController = value as QnSTradingCompany.Logic.Controllers.Persistence.Account.RoleController;
        }
    }
}
