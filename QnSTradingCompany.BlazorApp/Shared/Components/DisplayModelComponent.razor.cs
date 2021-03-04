//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TModelMember = QnSTradingCompany.BlazorApp.Models.Modules.Form.DisplayModelMember;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DisplayModelComponent
    {
        [Parameter]
        public ModelObject Model
        {
            get => model;
            set
            {
                model = value;
                LoadContainer();
            }
        }

        private ModelObject model;
        private IEnumerable<TModelMember> modelMembers;

        public override string ForPrefix => Model != null ? Model.GetType().Name : base.ForPrefix;
        public IEnumerable<TModelMember> ModelMembers
        {
            get => modelMembers ?? System.Array.Empty<TModelMember>();
            private set => modelMembers = value;
        }
        private void LoadContainer()
        {
            bool handled = false;

            BeforeLoadContainer(ref handled);
            if (handled == false)
            {
                if (Model != null)
                {
                    var items = new List<TModelMember>();

                    foreach (var item in Model.GetType().GetAllTypeProperties())
                    {
                        var isScaffoldItem = ParentComponent.IsScaffoldItem(item.Value.PropertyInfo);

                        if (isScaffoldItem)
                        {
                            var modelMember = CreateModelMember(ParentComponent, Model, item.Value.PropertyInfo);

                            if (modelMember != null)
                            {
                                items.Add(modelMember);
                            }
                        }
                    }
                    ModelMembers = items.OrderBy(e => e.Order);
                }
            }
            AfterLoadContainer();
        }
        private TModelMember CreateModelMember(DisplayComponent displayComponent, ModelObject modelObject, PropertyInfo propertyInfo)
        {
            var createHandled = false;
            var modelMember = default(TModelMember);

            displayComponent?.CreateDisplayModelMember(modelObject, propertyInfo, ref modelMember, ref createHandled);
            if (createHandled == false)
            {
                CreateModelMember(Model, propertyInfo, ref modelMember, ref createHandled);
            }
            if (createHandled == false && propertyInfo.CanRead)
            {
                var displayProperty = GetOrCreateDisplayProperty(modelObject.GetType(), propertyInfo);

                modelMember = new TModelMember(modelObject, propertyInfo, displayProperty);
            }
            if (modelMember != null)
            {
                displayComponent?.CreatedDisplayModelMember(modelMember);
            }
            return modelMember;
        }
        partial void CreateModelMember(object model, PropertyInfo propertyInfo, ref TModelMember modelMember, ref bool handled);
        partial void BeforeLoadContainer(ref bool handled);
        partial void AfterLoadContainer();
    }
}
//MdEnd
