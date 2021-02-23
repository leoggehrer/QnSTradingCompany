//@QnSCodeCopy
//MdStart
using System;

namespace QnSTradingCompany.BlazorApp.Modules.App
{
    public class AppState
    {
        public event Action OnAlertsHaveChanged;

        public void AlertsHaveChanged()
        {
            NotifyAlertsChanged();
        }

        private void NotifyAlertsChanged() => OnAlertsHaveChanged?.Invoke();
    }
}
//MdEnd
