//@QnSCodeCopy
using System;
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Protection
{
    public sealed class TimeKeyService
    {
        private const int ValidToMinutes = 20;
        private Dictionary<string, DateTime> TimeKeys { get; set; }

        public TimeKeyService()
        {
            TimeKeys = new Dictionary<string, DateTime>();
        }

        public void AddTimeKey(string key)
        {
            if (TimeKeys.ContainsKey(key) == false)
            {
                TimeKeys.Add(key, DateTime.Now.AddMinutes(ValidToMinutes));
            }
            else
            {
                TimeKeys[key] = DateTime.Now.AddMinutes(ValidToMinutes);
            }
        }

        public void RemoveTimeKey(string key)
        {
            if (TimeKeys.ContainsKey(key))
            {
                TimeKeys.Remove(key);
            }
        }

        public bool IsKeyValid(string key)
        {
            var result = false;

            if (TimeKeys.ContainsKey(key))
            {
                result = DateTime.Now <= TimeKeys[key];

                if (result == false)
                {
                    TimeKeys.Remove(key);
                }
            }
            return result;
        }
    }
}
