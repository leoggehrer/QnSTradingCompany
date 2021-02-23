//@QnSGeneratedCode
namespace QnSTradingCompany.BlazorApp.Models.Business.Account
{
    partial class IdentityUser : OneToAnotherModel<QnSTradingCompany.Contracts.Persistence.Account.IIdentity, QnSTradingCompany.BlazorApp.Models.Persistence.Account.Identity, QnSTradingCompany.Contracts.Persistence.Account.IUser, QnSTradingCompany.BlazorApp.Models.Persistence.Account.User>
    {
    }
}
namespace QnSTradingCompany.BlazorApp.Models.Business.Account
{
    using System;
    public partial class IdentityUser : QnSTradingCompany.Contracts.Business.Account.IIdentityUser
    {
        static IdentityUser()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public IdentityUser()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public void CopyProperties(QnSTradingCompany.Contracts.Business.Account.IIdentityUser other)
        {
            if (other == null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                OneItem.CopyProperties(other.OneItem);
                AnotherItem.CopyProperties(other.AnotherItem);
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QnSTradingCompany.Contracts.Business.Account.IIdentityUser other, ref bool handled);
        partial void AfterCopyProperties(QnSTradingCompany.Contracts.Business.Account.IIdentityUser other);
        public static Business.Account.IdentityUser Create()
        {
            BeforeCreate();
            var result = new Business.Account.IdentityUser();
            AfterCreate(result);
            return result;
        }
        public static Business.Account.IdentityUser Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new Business.Account.IdentityUser();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static Business.Account.IdentityUser Create(QnSTradingCompany.Contracts.Business.Account.IIdentityUser other)
        {
            BeforeCreate(other);
            var result = new Business.Account.IdentityUser();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(Business.Account.IdentityUser instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(Business.Account.IdentityUser instance, object other);
        static partial void BeforeCreate(QnSTradingCompany.Contracts.Business.Account.IIdentityUser other);
        static partial void AfterCreate(Business.Account.IdentityUser instance, QnSTradingCompany.Contracts.Business.Account.IIdentityUser other);
    }
}
namespace QnSTradingCompany.BlazorApp.Models.Business.Account
{
    partial class AppAccess : OneToManyModel<QnSTradingCompany.Contracts.Persistence.Account.IIdentity, QnSTradingCompany.BlazorApp.Models.Persistence.Account.Identity, QnSTradingCompany.Contracts.Persistence.Account.IRole, QnSTradingCompany.BlazorApp.Models.Persistence.Account.Role>
    {
    }
}
namespace QnSTradingCompany.BlazorApp.Models.Business.Account
{
    using System;
    public partial class AppAccess
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(36)]
        public System.String Guid
        {
            get => OneModel.Guid;
        }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(128)]
        public System.String Name
        {
            get => OneModel.Name;
            set => OneModel.Name = value;
        }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(128)]
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public System.String Email
        {
            get => OneModel.Email;
            set => OneModel.Email = value;
        }
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public System.String Password
        {
            get => OneModel.Password;
            set => OneModel.Password = value;
        }
        public System.Int32 TimeOutInMinutes
        {
            get => OneModel.TimeOutInMinutes;
            set => OneModel.TimeOutInMinutes = value;
        }
        public System.Boolean EnableJwtAuth
        {
            get => OneModel.EnableJwtAuth;
            set => OneModel.EnableJwtAuth = value;
        }
        public System.Int32 AccessFailedCount
        {
            get => OneModel.AccessFailedCount;
            set => OneModel.AccessFailedCount = value;
        }
        public QnSTradingCompany.Contracts.Modules.Common.State State
        {
            get => OneModel.State;
            set => OneModel.State = value;
        }
    }
}
namespace QnSTradingCompany.BlazorApp.Models.Business.Account
{
    using System;
    public partial class AppAccess : QnSTradingCompany.Contracts.Business.Account.IAppAccess
    {
        static AppAccess()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public AppAccess()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public void CopyProperties(QnSTradingCompany.Contracts.Business.Account.IAppAccess other)
        {
            if (other == null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                OneItem.CopyProperties(other.OneItem);
                ClearManyItems();
                foreach (var item in other.ManyItems)
                {
                    AddManyItem(item);
                }
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QnSTradingCompany.Contracts.Business.Account.IAppAccess other, ref bool handled);
        partial void AfterCopyProperties(QnSTradingCompany.Contracts.Business.Account.IAppAccess other);
        public static Business.Account.AppAccess Create()
        {
            BeforeCreate();
            var result = new Business.Account.AppAccess();
            AfterCreate(result);
            return result;
        }
        public static Business.Account.AppAccess Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new Business.Account.AppAccess();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static Business.Account.AppAccess Create(QnSTradingCompany.Contracts.Business.Account.IAppAccess other)
        {
            BeforeCreate(other);
            var result = new Business.Account.AppAccess();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(Business.Account.AppAccess instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(Business.Account.AppAccess instance, object other);
        static partial void BeforeCreate(QnSTradingCompany.Contracts.Business.Account.IAppAccess other);
        static partial void AfterCreate(Business.Account.AppAccess instance, QnSTradingCompany.Contracts.Business.Account.IAppAccess other);
    }
}
