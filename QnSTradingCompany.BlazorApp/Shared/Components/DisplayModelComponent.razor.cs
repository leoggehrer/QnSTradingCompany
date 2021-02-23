//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TModelMember = QnSTradingCompany.BlazorApp.Models.Modules.Form.DisplayModelMember;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DisplayModelComponent
    {
        private ModelObject model;
        private IEnumerable<TModelMember> modelMembers;

        [Parameter]
        public ModelPage ModelPage { get; set; }
        [Parameter]
        public DisplayComponent ParentComponent { get; set; }
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
                        var isScaffoldItem = ModelPage.IsScaffoldItem(item.Value.PropertyInfo) 
                                             && ParentComponent.IsScaffoldItem(item.Value.PropertyInfo);

                        if (isScaffoldItem)
                        {
                            var modelMember = CreateModelMember(ModelPage, ParentComponent, Model, item.Value.PropertyInfo);

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
        private TModelMember CreateModelMember(ModelPage modelPage, DisplayComponent displayComponent, ModelObject modelObject, PropertyInfo propertyInfo)
        {
            var createHandled = false;
            var modelMember = default(TModelMember);

            displayComponent?.CreateDisplayModelMember(modelObject, propertyInfo, ref modelMember, ref createHandled);
            if (createHandled == false)
            {
                modelPage?.CreateDisplayModelMember(modelObject, propertyInfo, ref modelMember, ref createHandled);
            }
            if (createHandled == false)
            {
                CreateModelMember(Model, propertyInfo, ref modelMember, ref createHandled);
            }
            if (createHandled == false && propertyInfo.CanRead)
            {
                modelMember = new TModelMember(modelPage, modelObject, propertyInfo);
            }
            if (modelMember != null)
            {
                displayComponent?.CreatedDisplayModelMember(modelMember);
                modelPage?.CreatedDisplayModelMember(modelMember);
            }
            return modelMember;
        }
        partial void CreateModelMember(object model, PropertyInfo propertyInfo, ref TModelMember modelMember, ref bool handled);
        partial void BeforeLoadContainer(ref bool handled);
        partial void AfterLoadContainer();
    }
}
//MdEnd
