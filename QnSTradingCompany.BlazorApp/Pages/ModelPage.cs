//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Validator;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Services;
using Radzen;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Pages
{
    public partial class ModelPage : CommonPage
    {
        [Inject]
        public IServiceAdapter ServiceAdapter { get; private set; }

        protected DisplayPropertyContainer DisplayProperties { get; init; } = new DisplayPropertyContainer();

        #region EventHandler
        public event EventHandler<DisplayPropertyContainer> InitDisplayPropertiesHandler;

        public event EventHandler<DisplayModelMemberInfo> CreateDisplayModelMemberHandler;
        public event EventHandler<DisplayModelMember> CreatedDisplayModelMemberHandler;

        public event EventHandler<EditModelMemberInfo> CreateEditModelMemberHandler;
        public event EventHandler<EditModelMember> CreatedEditModelMemberHandler;

        //public event EventHandler<DisplayProperty> EvaluateDisplayPropertyHandler;
        #endregion EventHandler

        protected virtual void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            BeforeInitialized();
            InitDisplayProperties(DisplayProperties);
            InitDisplayPropertiesHandler?.Invoke(this, DisplayProperties);
            AfterInitialized();
        }
        protected virtual void BeforeInitialized()
        {
        }
        protected virtual void AfterInitialized()
        {
        }

        public virtual Contracts.Client.IAdapterAccess<T> CreateService<T>() 
            where T : Contracts.IIdentifiable
        {
            return ServiceAdapter.Create<T>(AuthorizationSession.Token);
        }
        public virtual Contracts.Client.IAdapterAccess<T> CreateService<T>(string sessionToken)
            where T : Contracts.IIdentifiable
        {
            return ServiceAdapter.Create<T>(sessionToken);
        }

        public virtual DisplayProperty GetDisplayProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var handled = false;
            var result = default(DisplayProperty);
            var originName = propertyInfo.Name;

            BeforeGetDisplayProperty(originName, ref result, ref handled);
            if (handled == false)
            {
                DisplayProperties.TryGetValue(originName, out result);
            }
            AfterGetDisplayProperty(originName, result);
            return result;
        }
        partial void BeforeGetDisplayProperty(string originName, ref DisplayProperty result, ref bool handled);
        partial void AfterGetDisplayProperty(string originName, DisplayProperty result);

        public virtual bool IsScaffoldItem(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = true;

            if (DisplayProperties.TryGetValue(propertyInfo.Name, out DisplayProperty dp))
            {
                result = dp.ScaffoldItem;
            }
            return result;
        }
        public virtual void CreateDisplayModelMember(ModelObject model, PropertyInfo propertyInfo, ref DisplayModelMember modelMember, ref bool handled)
        {
            if (handled == false)
            {
                var memberInfo = new DisplayModelMemberInfo()
                {
                    Model = model,
                    Property = propertyInfo,
                    Created = false,
                };
                CreateDisplayModelMemberHandler?.Invoke(this, memberInfo);
                handled = memberInfo.Created;
                if (handled)
                {
                    modelMember = memberInfo.ModelMember;
                }
            }
        }
        public virtual void CreatedDisplayModelMember(DisplayModelMember modelMember)
        {
            modelMember.CheckArgument(nameof(modelMember));

            if (DisplayProperties.TryGetValue(modelMember.Name, out DisplayProperty dp))
            {
                modelMember.IsVisible = dp.DisplayVisible;
                modelMember.Order = dp.Order;
            }
            CreatedDisplayModelMemberHandler?.Invoke(this, modelMember);
        }
        public virtual void CreateEditModelMember(ModelObject model, PropertyInfo propertyInfo, ref EditModelMember modelMember, ref bool handled)
        {
            if (handled == false)
            {
                var memberInfo = new EditModelMemberInfo()
                {
                    Model = model,
                    Property = propertyInfo,
                    Created = false,
                };
                CreateEditModelMemberHandler?.Invoke(this, memberInfo);
                handled = memberInfo.Created;
                if (handled)
                {
                    modelMember = memberInfo.ModelMember;
                }
            }
        }
        public virtual void CreatedEditModelMember(EditModelMember modelMember)
        {
            modelMember.CheckArgument(nameof(modelMember));

            if (DisplayProperties.TryGetValue(modelMember.Name, out DisplayProperty dp))
            {
                modelMember.IsVisible = dp.EditVisible;
                modelMember.ReadOnly = dp.Readonly;
                modelMember.Order = dp.Order;
            }
            CreatedEditModelMemberHandler?.Invoke(this, modelMember);
        }

        protected virtual void ValidateItem(object obj)
        {
            ModelValidator.Validate(obj);
        }
        public Task InvokePageAsync(Action action)
        {
            action.CheckArgument(nameof(action));

            return InvokeAsync(() => action());
        }

        public virtual void OnMenuItemClick(MenuItemEventArgs args)
        {
        }
    }
}
//MdEnd
