//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DisplayComponent : CommonComponent
    {
        [Parameter]
        public DisplayComponent ParentComponent { get; set; }
        protected DisplayPropertyContainer DisplayProperties { get; } = new DisplayPropertyContainer();

        #region EventHandler
        public event EventHandler<DisplayPropertyContainer> InitDisplayPropertiesHandler;

        public event EventHandler<DisplayModelMemberInfo> CreateDisplayModelMemberHandler;
        public event EventHandler<DisplayModelMember> CreatedDisplayModelMemberHandler;

        public event EventHandler<EditModelMemberInfo> CreateEditModelMemberHandler;
        public event EventHandler<EditModelMember> CreatedEditModelMemberHandler;

        public event EventHandler<DisplayProperty> EvaluateDisplayPropertyHandler;
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

        public virtual IEnumerable<DisplayProperty> GetAllDisplayProperties()
        {
            var handled = false;
            var result = new List<DisplayProperty>();

            BeforeGetAllDisplayProperties(result, ref handled);
            if (handled == false)
            {
                foreach (var item in DisplayProperties)
                {
                    result.Add(item.Value);
                }
                if (ParentComponent != null)
                {
                    result.AddRange(ParentComponent.GetAllDisplayProperties());
                }
            }
            AfterGetAllDisplayProperties(result);
            return result;
        }
        partial void BeforeGetAllDisplayProperties(List<DisplayProperty> result, ref bool handled);
        partial void AfterGetAllDisplayProperties(List<DisplayProperty> result);

        public virtual DisplayProperty GetDisplayProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var handled = false;
            var result = default(DisplayProperty);
            var originName = propertyInfo.Name;

            BeforeGetDisplayProperty(originName, ref result, ref handled);
            if (handled == false)
            {
                result = ParentComponent?.GetDisplayProperty(propertyInfo);
                if (result == default)
                {
                    DisplayProperties.TryGetValue(originName, out result);
                }
            }
            AfterGetDisplayProperty(originName, result);
            return result;
        }
        partial void BeforeGetDisplayProperty(string originName, ref DisplayProperty result, ref bool handled);
        partial void AfterGetDisplayProperty(string originName, DisplayProperty result);

        private record DisplayItem(string FormatValue, bool ScaffoldItem, bool Readonly, bool Visible, bool DisplayVisible, bool EditVisible, bool ListSortable, bool ListFilterable, string ListWidth, int Order);
        public virtual DisplayProperty GetOrCreateDisplayProperty(Type modelType, PropertyInfo propertyInfo)
        {
            modelType.CheckArgument(nameof(modelType));

            var result = GetDisplayProperty(propertyInfo);

            if (result == null)
            {
                var jsonValue = Settings.GetValue($"{modelType.Name}.{propertyInfo.Name}", string.Empty);

                if (jsonValue.HasContent())
                {
                    result = JsonSerializer.Deserialize<DisplayProperty>(jsonValue);
                    result.OriginName = propertyInfo.Name;
                }
                else
                {
                    result = new DisplayProperty(propertyInfo.Name);
                }
            }
            return result;
        }

        public virtual void EvaluateDisplayProperty(DisplayProperty displayProperty)
        {
            displayProperty.CheckArgument(nameof(displayProperty));

            var handled = false;

            BeforeEvaluateDisplayProperty(displayProperty, ref handled);
            if (handled == false)
            {
                ParentComponent?.EvaluateDisplayProperty(displayProperty);
                EvaluateDisplayPropertyHandler?.Invoke(this, displayProperty);
            }
            AfterEvaluteDisplayProperty(displayProperty);
        }
        partial void BeforeEvaluateDisplayProperty(DisplayProperty displayProperty, ref bool handled);
        partial void AfterEvaluteDisplayProperty(DisplayProperty displayProperty);

        public virtual bool IsScaffoldItem(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = ParentComponent == null || ParentComponent.IsScaffoldItem(propertyInfo);

            if (result && DisplayProperties.TryGetValue(propertyInfo.Name, out DisplayProperty dp))
            {
                result = dp.ScaffoldItem;
            }
            return result;
        }
        public virtual void CreateDisplayModelMember(ModelObject model, PropertyInfo propertyInfo, ref DisplayModelMember modelMember, ref bool handled)
        {
            ParentComponent?.CreateDisplayModelMember(model, propertyInfo, ref modelMember, ref handled);
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

            ParentComponent?.CreatedDisplayModelMember(modelMember);
            if (DisplayProperties.TryGetValue(modelMember.Name, out DisplayProperty dp))
            {
                modelMember.Visible = dp.DisplayVisible;
                modelMember.Order = dp.Order;
            }
            CreatedDisplayModelMemberHandler?.Invoke(this, modelMember);
        }
        public virtual void CreateEditModelMember(ModelObject model, PropertyInfo propertyInfo, ref EditModelMember modelMember, ref bool handled)
        {
            ParentComponent?.CreateEditModelMember(model, propertyInfo, ref modelMember, ref handled);
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

            ParentComponent?.CreatedEditModelMember(modelMember);
            if (DisplayProperties.TryGetValue(modelMember.Name, out DisplayProperty dp))
            {
                modelMember.Visible = dp.EditVisible;
                modelMember.Readonly = dp.Readonly;
                modelMember.Order = dp.Order;
            }
            CreatedEditModelMemberHandler?.Invoke(this, modelMember);
        }
    }
}
//MdEnd
