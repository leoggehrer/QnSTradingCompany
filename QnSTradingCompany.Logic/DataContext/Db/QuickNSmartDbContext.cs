//@QnSCodeCopy
//MdStart
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using QnSTradingCompany.Logic.Entities.Persistence.Account;
using System.Linq;

namespace QnSTradingCompany.Logic.DataContext.Db
{
    internal partial class QnSTradingCompanyDbContext
    {
        static QnSTradingCompanyDbContext()
        {
            ClassConstructing();
            ConnectionString = Modules.Configuration.Settings.Get(CommonBase.StaticLiterals.ConnectionString);
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

#if DEBUG
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            //builder
            //    .AddFilter((category, level) =>
            //        category == DbLoggerCategory.Database.Command.Name
            //        && level == LogLevel.Information)
            //    .AddDebug();
        });
#endif
        private static string ConnectionString { get; set; }

        public QnSTradingCompanyDbContext()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        #region Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            BeforeConfiguring(optionsBuilder);
            optionsBuilder
#if DEBUG        
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(loggerFactory)
#endif
                .UseSqlServer(ConnectionString);
            AfterConfiguring(optionsBuilder);
        }
        partial void BeforeConfiguring(DbContextOptionsBuilder optionsBuilder);
        partial void AfterConfiguring(DbContextOptionsBuilder optionsBuilder);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BeforeModelCreating(modelBuilder);
            DoModelCreating(modelBuilder);
            AfterModelCreating(modelBuilder);
#if DEBUG
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
#endif
            base.OnModelCreating(modelBuilder);
        }
        static partial void BeforeModelCreating(ModelBuilder modelBuilder);
        static partial void DoModelCreating(ModelBuilder modelBuilder);
        static partial void AfterModelCreating(ModelBuilder modelBuilder);

        static partial void ConfigureEntityType(EntityTypeBuilder<Identity> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(p => p.PasswordHash)
                .HasMaxLength(512)
                .IsRequired();
            entityTypeBuilder
                .Property(p => p.PasswordSalt)
                .HasMaxLength(512)
                .IsRequired();
        }
        #endregion Configuration
    }
}
//MdEnd
