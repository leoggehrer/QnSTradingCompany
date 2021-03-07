//@QnSCodeCopy
//MdStart
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QnSTradingCompany.BlazorApp.Models
{
    public partial class ModelObject
    {
        private List<ModelObject> subObjects = null;
        protected List<ModelObject> SubObjects
        {
            get
            {
                if (subObjects == null)
                {
                    subObjects = new List<ModelObject>();
                }
                return subObjects;
            }
        }
        public IEnumerable<ModelObject> GetSubObjects() => SubObjects;

        public virtual void BeforeDisplay()
        {
        }

        public virtual void BeforeEdit()
        {
        }
        public virtual void BeforeSave()
        {
        }
        public virtual void AfterSave()
        {
        }
        public virtual void CancelEdit()
        {
        }

        public virtual void BeforeDelete()
        {
        }
        public virtual void ConfirmedDelete()
        {
        }
        public virtual void AfterDelete()
        {
        }
        public virtual void CancelDelete()
        {
        }

        public virtual void EvaluateDisplayProperty(DisplayProperty displayProperty)
        {
        }
        protected static bool IsEqualsWith(object obj1, object obj2)
        {
            bool result = false;

            if (obj1 == null && obj2 == null)
            {
                result = true;
            }
            else if (obj1 != null && obj2 != null)
            {
                if (obj1 is IEnumerable objEnum1 && obj2 is IEnumerable objEnum2)
                {
                    var enumerable1 = objEnum1.Cast<object>().ToList();
                    var enumerable2 = objEnum2.Cast<object>().ToList();

                    result = enumerable1.SequenceEqual(enumerable2);
                }
                else
                {
                    result = obj1.Equals(obj2);
                }
            }
            return result;
        }
    }
}
//MdEnd
