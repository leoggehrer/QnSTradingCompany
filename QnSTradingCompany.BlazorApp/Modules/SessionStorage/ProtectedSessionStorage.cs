//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;

namespace QnSTradingCompany.BlazorApp.Modules.SessionStorage
{
    /// <summary>
    /// Provides mechanisms for storing and retrieving data in the browser's
    /// 'sessionStorage' collection.
    ///
    /// This data will be scoped to the current browser tab. The data will be
    /// discarded if the user closes the browser tab or closes the browser itself.
    /// </summary>
    public class ProtectedSessionStorage : ProtectedBrowserStorage
    {
        /// <summary>
        /// Constructs an instance of <see cref="ProtectedSessionStorage"/>.
        /// </summary>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        public ProtectedSessionStorage(IJSRuntime jsRuntime, IDataProtectionProvider dataProtectionProvider)
            : base("sessionStorage", jsRuntime, dataProtectionProvider)
        {
        }
    }
}
//MdEnd
