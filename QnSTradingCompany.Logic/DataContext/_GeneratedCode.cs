//@QnSGeneratedCode
namespace QnSTradingCompany.Logic.DataContext.Db
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    partial class QnSTradingCompanyDbContext : GenericDbContext
    {
        protected DbSet<Entities.Persistence.MasterData.Customer> CustomerSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.MasterData.Product> ProductSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Language.Translation> TranslationSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Data.BinaryData> BinaryDataSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Configuration.Setting> SettingSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.App.Condition> ConditionSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.App.Order> OrderSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Account.ActionLog> ActionLogSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Account.Identity> IdentitySet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Account.IdentityXRole> IdentityXRoleSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Account.LoginSession> LoginSessionSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Account.Role> RoleSet
        {
            get;
            set;
        }
        protected DbSet<Entities.Persistence.Account.User> UserSet
        {
            get;
            set;
        }
        public override DbSet<E> Set<I, E>()
        {
            DbSet<E> result = null;
            if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer))
            {
                result = CustomerSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.MasterData.IProduct))
            {
                result = ProductSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Language.ITranslation))
            {
                result = TranslationSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Data.IBinaryData))
            {
                result = BinaryDataSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Configuration.ISetting))
            {
                result = SettingSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.ICondition))
            {
                result = ConditionSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.App.IOrder))
            {
                result = OrderSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IActionLog))
            {
                result = ActionLogSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IIdentity))
            {
                result = IdentitySet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IIdentityXRole))
            {
                result = IdentityXRoleSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.ILoginSession))
            {
                result = LoginSessionSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IRole))
            {
                result = RoleSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(QnSTradingCompany.Contracts.Persistence.Account.IUser))
            {
                result = UserSet as DbSet<E>;
            }
            return result;
        }
        static partial void DoModelCreating(ModelBuilder modelBuilder)
        {
            var customerBuilder = modelBuilder.Entity<Entities.Persistence.MasterData.Customer>();
            customerBuilder.ToTable("Customer", "MasterData").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.MasterData.Customer>().Property(p => p.RowVersion).IsRowVersion();
            customerBuilder.HasIndex(c => c.Number).IsUnique();
            customerBuilder.Property(p => p.Number).IsRequired().HasMaxLength(8);
            customerBuilder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            ConfigureEntityType(customerBuilder);
            var productBuilder = modelBuilder.Entity<Entities.Persistence.MasterData.Product>();
            productBuilder.ToTable("Product", "MasterData").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.MasterData.Product>().Property(p => p.RowVersion).IsRowVersion();
            productBuilder.HasIndex(c => c.Number).IsUnique();
            productBuilder.Property(p => p.Number).IsRequired().HasMaxLength(8);
            productBuilder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            ConfigureEntityType(productBuilder);
            var translationBuilder = modelBuilder.Entity<Entities.Persistence.Language.Translation>();
            translationBuilder.ToTable("Translation", "Language").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Language.Translation>().Property(p => p.RowVersion).IsRowVersion();
            translationBuilder.Property(p => p.AppName).IsRequired().HasMaxLength(128);
            translationBuilder.Property(p => p.Key).IsRequired().HasMaxLength(512);
            translationBuilder.Property(p => p.Value).HasMaxLength(1024);
            translationBuilder.HasIndex(c => new
            {
                c.AppName, c.KeyLanguage, c.Key
            }
            ).IsUnique();
            ConfigureEntityType(translationBuilder);
            var binaryDataBuilder = modelBuilder.Entity<Entities.Persistence.Data.BinaryData>();
            binaryDataBuilder.ToTable("BinaryData", "Data").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Data.BinaryData>().Property(p => p.RowVersion).IsRowVersion();
            binaryDataBuilder.HasIndex(c => c.Guid).IsUnique();
            binaryDataBuilder.Property(p => p.Guid).IsRequired();
            binaryDataBuilder.Property(p => p.Data).IsRequired();
            ConfigureEntityType(binaryDataBuilder);
            var settingBuilder = modelBuilder.Entity<Entities.Persistence.Configuration.Setting>();
            settingBuilder.ToTable("Setting", "Configuration").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Configuration.Setting>().Property(p => p.RowVersion).IsRowVersion();
            settingBuilder.Property(p => p.AppName).IsRequired().HasMaxLength(128);
            settingBuilder.Property(p => p.Key).IsRequired().HasMaxLength(512);
            settingBuilder.Property(p => p.Value).HasMaxLength(4096);
            settingBuilder.HasIndex(c => new
            {
                c.AppName, c.Key
            }
            ).IsUnique();
            ConfigureEntityType(settingBuilder);
            var conditionBuilder = modelBuilder.Entity<Entities.Persistence.App.Condition>();
            conditionBuilder.ToTable("Condition", "App").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.App.Condition>().Property(p => p.RowVersion).IsRowVersion();
            conditionBuilder.Property(p => p.Value).IsRequired().HasMaxLength(64);
            conditionBuilder.Property(p => p.Note).HasMaxLength(1024);
            ConfigureEntityType(conditionBuilder);
            var orderBuilder = modelBuilder.Entity<Entities.Persistence.App.Order>();
            orderBuilder.ToTable("Order", "App").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.App.Order>().Property(p => p.RowVersion).IsRowVersion();
            ConfigureEntityType(orderBuilder);
            var actionLogBuilder = modelBuilder.Entity<Entities.Persistence.Account.ActionLog>();
            actionLogBuilder.ToTable("ActionLog", "Account").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Account.ActionLog>().Property(p => p.RowVersion).IsRowVersion();
            ConfigureEntityType(actionLogBuilder);
            var identityBuilder = modelBuilder.Entity<Entities.Persistence.Account.Identity>();
            identityBuilder.ToTable("Identity", "Account").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Account.Identity>().Property(p => p.RowVersion).IsRowVersion();
            identityBuilder.Property(p => p.Guid).IsRequired().HasMaxLength(36);
            identityBuilder.HasIndex(c => c.Name).IsUnique();
            identityBuilder.Property(p => p.Name).IsRequired().HasMaxLength(128);
            identityBuilder.HasIndex(c => c.Email).IsUnique();
            identityBuilder.Property(p => p.Email).IsRequired().HasMaxLength(128);
            identityBuilder.Ignore(c => c.Password);
            ConfigureEntityType(identityBuilder);
            var identityXRoleBuilder = modelBuilder.Entity<Entities.Persistence.Account.IdentityXRole>();
            identityXRoleBuilder.ToTable("IdentityXRole", "Account").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Account.IdentityXRole>().Property(p => p.RowVersion).IsRowVersion();
            ConfigureEntityType(identityXRoleBuilder);
            var loginSessionBuilder = modelBuilder.Entity<Entities.Persistence.Account.LoginSession>();
            loginSessionBuilder.ToTable("LoginSession", "Account").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Account.LoginSession>().Property(p => p.RowVersion).IsRowVersion();
            loginSessionBuilder.Ignore(c => c.IsRemoteAuth);
            loginSessionBuilder.Ignore(c => c.Origin);
            loginSessionBuilder.Ignore(c => c.Name);
            loginSessionBuilder.Ignore(c => c.Email);
            loginSessionBuilder.Ignore(c => c.TimeOutInMinutes);
            loginSessionBuilder.Ignore(c => c.JsonWebToken);
            loginSessionBuilder.Property(p => p.SessionToken).IsRequired().HasMaxLength(128);
            loginSessionBuilder.Property(p => p.OptionalInfo).HasMaxLength(4096);
            ConfigureEntityType(loginSessionBuilder);
            var roleBuilder = modelBuilder.Entity<Entities.Persistence.Account.Role>();
            roleBuilder.ToTable("Role", "Account").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Account.Role>().Property(p => p.RowVersion).IsRowVersion();
            roleBuilder.HasIndex(c => c.Designation).IsUnique();
            roleBuilder.Property(p => p.Designation).IsRequired().HasMaxLength(64);
            roleBuilder.Property(p => p.Description).HasMaxLength(256);
            ConfigureEntityType(roleBuilder);
            var userBuilder = modelBuilder.Entity<Entities.Persistence.Account.User>();
            userBuilder.ToTable("User", "Account").HasKey("Id");
            modelBuilder.Entity<Entities.Persistence.Account.User>().Property(p => p.RowVersion).IsRowVersion();
            userBuilder.HasIndex(c => c.IdentityId).IsUnique();
            userBuilder.Property(p => p.Firstname).HasMaxLength(64);
            userBuilder.Property(p => p.Lastname).HasMaxLength(64);
            ConfigureEntityType(userBuilder);
        }
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.MasterData.Customer> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.MasterData.Product> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Language.Translation> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Data.BinaryData> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Configuration.Setting> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.App.Condition> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.App.Order> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.ActionLog> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.Identity> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.IdentityXRole> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.LoginSession> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.Role> entityTypeBuilder);
        static partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.User> entityTypeBuilder);
    }
}
