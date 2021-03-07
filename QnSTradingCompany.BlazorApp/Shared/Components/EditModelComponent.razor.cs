//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TModelMember = QnSTradingCompany.BlazorApp.Models.Modules.Form.EditModelMember;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class EditModelComponent
    {
        [Parameter]
        public IdentityModel Model
        {
            get => model;
            set
            {
                model = value;
                LoadContainer();
            }
        }

        private IdentityModel model;
        private IEnumerable<TModelMember> modelMembers;

        [Parameter]
        public bool Cloneable { get; set; } = true;
        public override string ForPrefix => Model != null ? Model.GetType().Name : base.ForPrefix;
        public IEnumerable<TModelMember> ModelMembers
        {
            get => modelMembers ?? System.Array.Empty<TModelMember>();
            private set => modelMembers = value;
        }

        public EditModelComponent()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        protected virtual void LoadContainer()
        {
            bool handled = false;
            IEnumerable<TModelMember> CreateModelMembers(ModelObject model)
            {
                var result = new List<TModelMember>();

                foreach (var item in model.GetType().GetAllTypeProperties())
                {
                    var isScaffoldItem = ParentComponent.IsScaffoldItem(item.Value.PropertyInfo);

                    if (isScaffoldItem)
                    {
                        var modelMember = CreateModelMember(ParentComponent, model, item.Value.PropertyInfo);

                        if (modelMember != null)
                        {
                            result.Add(modelMember);
                        }
                    }
                }
                return result;
            }

            BeforeLoadContainer(ref handled);
            if (handled == false)
            {
                if (Model != null)
                {
                    var items = new List<TModelMember>(CreateModelMembers(Model));

                    foreach (var subObject in Model.GetSubObjects())
                    {
                        items.AddRange(CreateModelMembers(subObject));
                    }
                    ModelMembers = items.OrderBy(e => e.Order);
                }
            }
            AfterLoadContainer();
        }
        partial void BeforeLoadContainer(ref bool handled);
        partial void AfterLoadContainer();

        protected virtual TModelMember CreateModelMember(DisplayComponent displayComponent, ModelObject modelObject, PropertyInfo propertyInfo)
        {
            var createHandled = false;
            var modelMember = default(TModelMember);

            displayComponent?.CreateEditModelMember(modelObject, propertyInfo, ref modelMember, ref createHandled);
            if (createHandled == false)
            {
                CreateModelMember(Model, propertyInfo, ref modelMember, ref createHandled);
            }
            if (createHandled == false && propertyInfo.CanRead)
            {
                var displayProperty = GetOrCreateDisplayProperty(modelObject.GetType(), propertyInfo);

                modelMember = new TModelMember(displayComponent, modelObject, propertyInfo, displayProperty);
            }
            if (modelMember != null)
            {
                displayComponent?.CreatedEditModelMember(modelMember);
                if (modelMember.Property.PropertyType.IsEnum)
                {
                    modelMember.SelectItems.ToList().ForEach(i => i.Text = Translate(i.Text, i.Text));
                }
            }
            return modelMember;
        }
        partial void CreateModelMember(object model, PropertyInfo propertyInfo, ref TModelMember modelMember, ref bool handled);
    }
}
//MdEnd
