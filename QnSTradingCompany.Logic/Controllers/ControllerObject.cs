//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using QnSTradingCompany.Logic.DataContext;
using QnSTradingCompany.Logic.Modules.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// This is the base class for all controller classes.
    /// </summary>
    internal abstract partial class ControllerObject : IDisposable
    {
        #region Class-Constructors
        static ControllerObject()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        #endregion Class-Contructors

        #region Context
        private readonly bool contextOwner;
        protected IContext Context { get; private set; }
        #endregion Context

        #region SessionToken
        protected event EventHandler ChangedSessionToken;

        private string sessionToken;

        /// <summary>
        /// Sets the session token.
        /// </summary>
        public string SessionToken
        {
            internal get => sessionToken;
            set
            {
                sessionToken = value;
                ChangedSessionToken?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion SessionToken

        #region Instance-Constructors
        protected ControllerObject(IContext context)
        {
            context.CheckArgument(nameof(context));

            Constructing();
            contextOwner = true;
            Context = context;
            InitManagedMembers();
            ChangedSessionToken += HandleChangeSessionToken;
            Constructed();
        }
        protected ControllerObject(ControllerObject controller)
        {
            controller.CheckArgument(nameof(controller));

            Constructing();
            contextOwner = false;
            Context = controller.Context;
            SessionToken = controller.SessionToken;
            InitManagedMembers();
            ChangedSessionToken += HandleChangeSessionToken;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        #endregion Instance-Constructors

        #region Handle managed members
        private IEnumerable<FieldInfo> ControllerManagedFields => GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                                                           .Where(p => p.GetCustomAttributes<Attributes.ControllerManagedFieldAttribute>()
                                                                           .Any());
        private IEnumerable<PropertyInfo> ControllerManagedProperties => GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                  .Where(p => p.GetCustomAttributes<Attributes.ControllerManagedPropertyAttribute>()
                                                                                  .Any());
        private static ConstructorInfo GetControllerConstructor(Type type) => type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                  .Single(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(ControllerObject));

        private void HandleChangeSessionToken(object source, EventArgs e)
        {
            var handled = false;

            BeforeHandleManagedMembers(ref handled);

            if (handled == false)
            {
                ControllerManagedProperties.ForeachAction(item =>
                {
                    if (item.GetValue(this) is ControllerObject controllerObject)
                    {
                        controllerObject.SessionToken = SessionToken;
                    }
                });
                ControllerManagedFields.ForeachAction(item =>
                {
                    if (item.GetValue(this) is ControllerObject controllerObject)
                    {
                        controllerObject.SessionToken = SessionToken;
                    }
                });
            }
            AfterHandleManagedMembers();
        }
        partial void BeforeHandleManagedMembers(ref bool handled);
        partial void AfterHandleManagedMembers();

        protected virtual void InitManagedMembers()
        {
            var handled = false;

            BeforeInitManagedMembers(ref handled);

            if (handled == false)
            {
                ControllerManagedProperties.ForeachAction(item =>
                {
                    var constructor = GetControllerConstructor(item.PropertyType);

                    if (constructor?.Invoke(new ControllerObject[] { this }) is ControllerObject controllerObject)
                    {
                        item.SetValue(this, controllerObject);
                    }
                });
                ControllerManagedFields.ForeachAction(item =>
                {
                    var constructor = GetControllerConstructor(item.FieldType);

                    if (constructor?.Invoke(new ControllerObject[] { this }) is ControllerObject controllerObject)
                    {
                        item.SetValue(this, controllerObject);
                    }
                });
            }
            AfterInitManagedMembers();
        }
        partial void BeforeInitManagedMembers(ref bool handled);
        partial void AfterInitManagedMembers();

        protected virtual void DisposeManagedMembers()
        {
            var handled = false;

            BeforeDisposeManagedMembers(ref handled);

            if (handled == false)
            {
                ControllerManagedProperties.ForeachAction(item =>
                {
                    if (item.GetValue(this) is ControllerObject controllerObject)
                    {
                        controllerObject.Dispose();
                    }
                    item.SetValue(this, null);
                });
                ControllerManagedFields.ForeachAction(item =>
                {
                    if (item.GetValue(this) is ControllerObject controllerObject)
                    {
                        controllerObject.Dispose();
                    }
                    item.SetValue(this, null);
                });
            }
            AfterDisposeManagedMembers();
        }
        partial void BeforeDisposeManagedMembers(ref bool handled);
        partial void AfterDisposeManagedMembers();
        #endregion Handle managed members

        #region Authorization
        protected virtual async Task CheckAuthorizationAsync(MethodBase methodeBase, AccessType accessType)
        {
            methodeBase.CheckArgument(nameof(methodeBase));

            bool handled = false;

            BeforeCheckAuthorization(methodeBase, accessType, ref handled);
            if (handled == false)
            {
                await Authorization.CheckAuthorizationAsync(SessionToken, methodeBase, accessType).ConfigureAwait(false);
            }
            AfterCheckAuthorization(methodeBase, accessType);
        }
        partial void BeforeCheckAuthorization(MethodBase methodeBase, AccessType accessType, ref bool handled);
        partial void AfterCheckAuthorization(MethodBase methodeBase, AccessType accessType);

        protected virtual async Task CheckAuthorizationAsync(Type instanceType, MethodBase methodeBase, AccessType accessType)
        {
            instanceType.CheckArgument(nameof(instanceType));
            methodeBase.CheckArgument(nameof(methodeBase));

            bool handled = false;

            BeforeCheckAuthorization(instanceType, methodeBase, accessType, ref handled);
            if (handled == false)
            {
                await Authorization.CheckAuthorizationAsync(SessionToken, instanceType, methodeBase, accessType).ConfigureAwait(false);
            }
            AfterCheckAuthorization(instanceType, methodeBase, accessType);
        }
        partial void BeforeCheckAuthorization(Type instanceType, MethodBase methodeBase, AccessType accessType, ref bool handled);
        partial void AfterCheckAuthorization(Type instanceType, MethodBase methodeBase, AccessType accessType);
        #endregion Authorization

        #region SaveChanges
        protected virtual async Task BeforeSaveChangesAsync()
        {
            foreach (var item in ControllerManagedProperties)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.BeforeSaveChangesAsync().ConfigureAwait(false);
                }
            }
            foreach (var item in ControllerManagedFields)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.BeforeSaveChangesAsync().ConfigureAwait(false);
                }
            }
        }
        public virtual async Task SaveChangesAsync()
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Save).ConfigureAwait(false);

            await ExecuteSaveChangesAsync().ConfigureAwait(false);
        }
        internal async Task ExecuteSaveChangesAsync()
        {
            await BeforeSaveChangesAsync().ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            await AfterSaveChangesAsync().ConfigureAwait(false);
        }
        protected virtual async Task AfterSaveChangesAsync()
        {
            foreach (var item in ControllerManagedProperties)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.AfterSaveChangesAsync().ConfigureAwait(false);
                }
            }
            foreach (var item in ControllerManagedFields)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.AfterSaveChangesAsync().ConfigureAwait(false);
                }
            }
        }
        internal virtual async Task SaveChangesDirectAsync()
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Save).ConfigureAwait(false);

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected virtual async Task BeforeRejectChangesAsync()
        {
            foreach (var item in ControllerManagedProperties)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.BeforeRejectChangesAsync().ConfigureAwait(false);
                }
            }
            foreach (var item in ControllerManagedFields)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.BeforeRejectChangesAsync().ConfigureAwait(false);
                }
            }
        }
        public virtual async Task RejectChangesAsync()
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Reject).ConfigureAwait(false);

            await ExecuteRejectChangesAsync().ConfigureAwait(false);
        }
        internal async Task ExecuteRejectChangesAsync()
        {
            await BeforeSaveChangesAsync().ConfigureAwait(false);
            await Context.RejectChangesAsync().ConfigureAwait(false);
            await AfterSaveChangesAsync().ConfigureAwait(false);
        }
        protected virtual async Task AfterRejectChangesAsync()
        {
            foreach (var item in ControllerManagedProperties)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.AfterRejectChangesAsync().ConfigureAwait(false);
                }
            }
            foreach (var item in ControllerManagedFields)
            {
                if (item.GetValue(this) is ControllerObject controllerObject)
                {
                    await controllerObject.AfterRejectChangesAsync().ConfigureAwait(false);
                }
            }
        }
        internal virtual async Task RejectChangesDirectAsync()
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Reject).ConfigureAwait(false);

            await Context.RejectChangesAsync().ConfigureAwait(false);
        }
        #endregion SaveChanges

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (contextOwner && Context != null)
                    {
                        Context.Dispose();
                    }
                    DisposeManagedMembers();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                Context = null;
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ControllerObject()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
//MdEnd
