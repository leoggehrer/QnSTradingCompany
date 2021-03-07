//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Modules.Helpers;
using QnSTradingCompany.BlazorApp.Pages;
using QnSTradingCompany.BlazorApp.Shared.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QnSTradingCompany.BlazorApp.Modules.DataGrid
{
    public class DataGridAssociationItem<TModel, TItem> : IDisposable
        where TModel : ModelObject, Contracts.IIdentifiable, new()
        where TItem : Contracts.IIdentifiable
    {
        public ModelPage ModelPage => DataGridHandler.ModelPage;
        public DisplayComponent DisplayComponent { get; private set; }
        public IDataGridHandler<TModel> DataGridHandler { get; private set; }
        public string ItemRefIdName { get; init; }
        public Func<int, TModel, bool> Compare { get; init; }
        public Func<TItem, string> ItemToText { get; init; }
        public Action<TModel, TItem> ModelAssignment { get; init; }
        private ItemContainer<TItem> items = null;

        public IEnumerable<TItem> Items
        {
            get
            {
                if (items == null)
                {
                    items = new ItemContainer<TItem>(ModelPage);
                }
                return items.Items;
            }
        }
        public DataGridAssociationItem(DisplayComponent displayComponent, IDataGridHandler<TModel> dataGridHandler, string itemRefIdName, Func<TItem, string> itemToText, Action<TModel, TItem> modelAssignment)
        {
            displayComponent.CheckArgument(nameof(displayComponent));
            dataGridHandler.CheckArgument(nameof(dataGridHandler));
            itemToText.CheckArgument(nameof(itemToText));
            modelAssignment.CheckArgument(nameof(modelAssignment));

            DisplayComponent = displayComponent;
            DataGridHandler = dataGridHandler;
            ItemRefIdName = itemRefIdName;
            ItemToText = itemToText;
            ModelAssignment = modelAssignment;

            DisplayComponent.InitDisplayPropertiesHandler += InitDisplayPropertiesHandler;
            DisplayComponent.CreatedDisplayModelMemberHandler += CreatedDisplayModelMemberHandler;
            DisplayComponent.CreateEditModelMemberHandler += CreateEditModelMemberHandler;
            DataGridHandler.LoadModelDataHandler += LoadModelDataHandler;
        }

        private void InitDisplayPropertiesHandler(object sender, DisplayPropertyContainer e)
        {
            if (e.ContainsKey($"{typeof(TModel).Name}{ItemRefIdName}") == false)
            {
                e.Add(new DisplayProperty(typeof(TModel).Name, ItemRefIdName) 
                {
                    Order = 1,
                    ListVisible = false,
                    DisplayVisible = false,
                    EditVisible = true,
                });
            }
        }

        protected void LoadModelDataHandler(object sender, TModel[] models)
        {
            models.CheckArgument(nameof(models));

            var property = models.Length > 0 ? models[0].GetType().GetProperty(ItemRefIdName) : null;

            if (property != null)
            {
                foreach (var model in models)
                {
                    var refId = (int?)property.GetValue(model);
                    var refItem = Items.FirstOrDefault(i => i.Id == refId.GetValueOrDefault());

                    if (refItem != null)
                    {
                        ModelAssignment(model, refItem);
                    }
                }
            }
        }
        protected void CreatedDisplayModelMemberHandler(object sender, DisplayModelMember modelMember)
        {
            if (modelMember.Name.Equals(ItemRefIdName))
            {
                modelMember.ToDisplayValue = v =>
                {
                    var result = string.Empty;
                    var refId = (int?)v;
                    var refItem = Items.FirstOrDefault(i => i.Id == refId.GetValueOrDefault());

                    if (refItem != null)
                    {
                        result = ItemToText(refItem);
                    }
                    return result;
                };
            }
        }
        protected void CreateEditModelMemberHandler(object sender, EditModelMemberInfo memberInfo)
        {
            if (memberInfo.Property.Name.Equals(ItemRefIdName))
            {
                var refId = (int?)memberInfo.Property.GetValue(memberInfo.Model);
                var displayProperty = DisplayComponent.GetOrCreateDisplayProperty(memberInfo.Model.GetType(), memberInfo.Property);

                memberInfo.Created = true;
                memberInfo.ModelMember = new SelectEditMember<TItem>(ModelPage, memberInfo.Model, memberInfo.Property, displayProperty, Items, a => ItemToText(a), a => a.Id == refId.GetValueOrDefault());
            }
        }

        #region Dispose-pattern
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    DisplayComponent.InitDisplayPropertiesHandler -= InitDisplayPropertiesHandler;
                    DisplayComponent.CreatedDisplayModelMemberHandler -= CreatedDisplayModelMemberHandler;
                    DisplayComponent.CreateEditModelMemberHandler -= CreateEditModelMemberHandler;
                    DisplayComponent = null;

                    DataGridHandler.LoadModelDataHandler -= LoadModelDataHandler;
                    DataGridHandler = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AssociationItem()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose-pattern
    }
}
//MdEnd
