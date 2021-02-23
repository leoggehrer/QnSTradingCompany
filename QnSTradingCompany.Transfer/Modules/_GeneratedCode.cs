//@QnSGeneratedCode
namespace QnSTradingCompany.Transfer.Modules.Account
{
    partial class Logon : ModuleModel
    {
    }
}
namespace QnSTradingCompany.Transfer.Modules.Account
{
    using System;
    public partial class Logon : QnSTradingCompany.Contracts.Modules.Account.ILogon
    {
        static Logon()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public Logon()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public System.String Email
        {
            get;
            set;
        }
        public System.String Password
        {
            get;
            set;
        }
        public System.String OptionalInfo
        {
            get;
            set;
        }
        public void CopyProperties(QnSTradingCompany.Contracts.Modules.Account.ILogon other)
        {
            if (other == null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Email = other.Email;
                Password = other.Password;
                OptionalInfo = other.OptionalInfo;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QnSTradingCompany.Contracts.Modules.Account.ILogon other, ref bool handled);
        partial void AfterCopyProperties(QnSTradingCompany.Contracts.Modules.Account.ILogon other);
        public static Modules.Account.Logon Create()
        {
            BeforeCreate();
            var result = new Modules.Account.Logon();
            AfterCreate(result);
            return result;
        }
        public static Modules.Account.Logon Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new Modules.Account.Logon();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static Modules.Account.Logon Create(QnSTradingCompany.Contracts.Modules.Account.ILogon other)
        {
            BeforeCreate(other);
            var result = new Modules.Account.Logon();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(Modules.Account.Logon instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(Modules.Account.Logon instance, object other);
        static partial void BeforeCreate(QnSTradingCompany.Contracts.Modules.Account.ILogon other);
        static partial void AfterCreate(Modules.Account.Logon instance, QnSTradingCompany.Contracts.Modules.Account.ILogon other);
    }
}
namespace QnSTradingCompany.Transfer.Modules.Account
{
    partial class JsonWebLogon : ModuleModel
    {
    }
}
namespace QnSTradingCompany.Transfer.Modules.Account
{
    using System;
    public partial class JsonWebLogon : QnSTradingCompany.Contracts.Modules.Account.IJsonWebLogon
    {
        static JsonWebLogon()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public JsonWebLogon()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public System.String Token
        {
            get;
            set;
        }
        public void CopyProperties(QnSTradingCompany.Contracts.Modules.Account.IJsonWebLogon other)
        {
            if (other == null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Token = other.Token;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QnSTradingCompany.Contracts.Modules.Account.IJsonWebLogon other, ref bool handled);
        partial void AfterCopyProperties(QnSTradingCompany.Contracts.Modules.Account.IJsonWebLogon other);
        public static Modules.Account.JsonWebLogon Create()
        {
            BeforeCreate();
            var result = new Modules.Account.JsonWebLogon();
            AfterCreate(result);
            return result;
        }
        public static Modules.Account.JsonWebLogon Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new Modules.Account.JsonWebLogon();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static Modules.Account.JsonWebLogon Create(QnSTradingCompany.Contracts.Modules.Account.IJsonWebLogon other)
        {
            BeforeCreate(other);
            var result = new Modules.Account.JsonWebLogon();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(Modules.Account.JsonWebLogon instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(Modules.Account.JsonWebLogon instance, object other);
        static partial void BeforeCreate(QnSTradingCompany.Contracts.Modules.Account.IJsonWebLogon other);
        static partial void AfterCreate(Modules.Account.JsonWebLogon instance, QnSTradingCompany.Contracts.Modules.Account.IJsonWebLogon other);
    }
}
namespace QnSTradingCompany.Transfer.Modules.Base
{
    partial class Translation : IdentityModel
    {
    }
}
namespace QnSTradingCompany.Transfer.Modules.Base
{
    using System;
    public partial class Translation : QnSTradingCompany.Contracts.Modules.Base.ITranslation
    {
        static Translation()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public Translation()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public System.String AppName
        {
            get;
            set;
        }
        = nameof(QnSTradingCompany);
        public QnSTradingCompany.Contracts.Modules.Common.LanguageCode KeyLanguage
        {
            get;
            set;
        }
        = Contracts.Modules.Common.LanguageCode.En;
        public System.String Key
        {
            get;
            set;
        }
        public QnSTradingCompany.Contracts.Modules.Common.LanguageCode ValueLanguage
        {
            get;
            set;
        }
        = Contracts.Modules.Common.LanguageCode.De;
        public System.String Value
        {
            get;
            set;
        }
        = string.Empty;
        public void CopyProperties(QnSTradingCompany.Contracts.Modules.Base.ITranslation other)
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
                AppName = other.AppName;
                KeyLanguage = other.KeyLanguage;
                Key = other.Key;
                ValueLanguage = other.ValueLanguage;
                Value = other.Value;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QnSTradingCompany.Contracts.Modules.Base.ITranslation other, ref bool handled);
        partial void AfterCopyProperties(QnSTradingCompany.Contracts.Modules.Base.ITranslation other);
        public static Modules.Base.Translation Create()
        {
            BeforeCreate();
            var result = new Modules.Base.Translation();
            AfterCreate(result);
            return result;
        }
        public static Modules.Base.Translation Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new Modules.Base.Translation();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static Modules.Base.Translation Create(QnSTradingCompany.Contracts.Modules.Base.ITranslation other)
        {
            BeforeCreate(other);
            var result = new Modules.Base.Translation();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(Modules.Base.Translation instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(Modules.Base.Translation instance, object other);
        static partial void BeforeCreate(QnSTradingCompany.Contracts.Modules.Base.ITranslation other);
        static partial void AfterCreate(Modules.Base.Translation instance, QnSTradingCompany.Contracts.Modules.Base.ITranslation other);
    }
}
namespace QnSTradingCompany.Transfer.Modules.Base
{
    partial class Setting : IdentityModel
    {
    }
}
namespace QnSTradingCompany.Transfer.Modules.Base
{
    using System;
    public partial class Setting : QnSTradingCompany.Contracts.Modules.Base.ISetting
    {
        static Setting()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public Setting()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public System.String AppName
        {
            get;
            set;
        }
        = nameof(QnSTradingCompany);
        public System.String Key
        {
            get;
            set;
        }
        public System.String Value
        {
            get;
            set;
        }
        = string.Empty;
        public void CopyProperties(QnSTradingCompany.Contracts.Modules.Base.ISetting other)
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
                AppName = other.AppName;
                Key = other.Key;
                Value = other.Value;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QnSTradingCompany.Contracts.Modules.Base.ISetting other, ref bool handled);
        partial void AfterCopyProperties(QnSTradingCompany.Contracts.Modules.Base.ISetting other);
        public static Modules.Base.Setting Create()
        {
            BeforeCreate();
            var result = new Modules.Base.Setting();
            AfterCreate(result);
            return result;
        }
        public static Modules.Base.Setting Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new Modules.Base.Setting();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static Modules.Base.Setting Create(QnSTradingCompany.Contracts.Modules.Base.ISetting other)
        {
            BeforeCreate(other);
            var result = new Modules.Base.Setting();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(Modules.Base.Setting instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(Modules.Base.Setting instance, object other);
        static partial void BeforeCreate(QnSTradingCompany.Contracts.Modules.Base.ISetting other);
        static partial void AfterCreate(Modules.Base.Setting instance, QnSTradingCompany.Contracts.Modules.Base.ISetting other);
    }
}
namespace QnSTradingCompany.Transfer.Modules.Language
{
    partial class Translation : IdentityModel
    {
    }
}
namespace QnSTradingCompany.Transfer.Modules.Language
{
    using System;
    public partial class Translation : QnSTradingCompany.Contracts.Modules.Language.ITranslation
    {
        static Translation()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public Translation()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public System.String AppName
        {
            get;
            set;
        }
        = nameof(QnSTradingCompany);
        public QnSTradingCompany.Contracts.Modules.Common.LanguageCode KeyLanguage
        {
            get;
            set;
        }
        = Contracts.Modules.Common.LanguageCode.En;
        public System.String Key
        {
            get;
            set;
        }
        public QnSTradingCompany.Contracts.Modules.Common.LanguageCode ValueLanguage
        {
            get;
            set;
        }
        = Contracts.Modules.Common.LanguageCode.De;
        public System.String Value
        {
            get;
            set;
        }
        = string.Empty;
        public void CopyProperties(QnSTradingCompany.Contracts.Modules.Language.ITranslation other)
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
                AppName = other.AppName;
                KeyLanguage = other.KeyLanguage;
                Key = other.Key;
                ValueLanguage = other.ValueLanguage;
                Value = other.Value;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QnSTradingCompany.Contracts.Modules.Language.ITranslation other, ref bool handled);
        partial void AfterCopyProperties(QnSTradingCompany.Contracts.Modules.Language.ITranslation other);
        public static Modules.Language.Translation Create()
        {
            BeforeCreate();
            var result = new Modules.Language.Translation();
            AfterCreate(result);
            return result;
        }
        public static Modules.Language.Translation Create(object other)
        {
            BeforeCreate(other);
            CommonBase.Extensions.ObjectExtensions.CheckArgument(other, nameof(other));
            var result = new Modules.Language.Translation();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        public static Modules.Language.Translation Create(QnSTradingCompany.Contracts.Modules.Language.ITranslation other)
        {
            BeforeCreate(other);
            var result = new Modules.Language.Translation();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(Modules.Language.Translation instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(Modules.Language.Translation instance, object other);
        static partial void BeforeCreate(QnSTradingCompany.Contracts.Modules.Language.ITranslation other);
        static partial void AfterCreate(Modules.Language.Translation instance, QnSTradingCompany.Contracts.Modules.Language.ITranslation other);
    }
}
