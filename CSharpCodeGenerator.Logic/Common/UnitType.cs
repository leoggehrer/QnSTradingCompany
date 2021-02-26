//@QnSCodeCopy
//MdStart

using System;

namespace CSharpCodeGenerator.Logic.Common
{
	[Flags]
    public enum UnitType : long
    {
        General = 1,

        Contracts = 2 * General,
        Logic = 2 * Contracts,
        Transfer = 2 * Logic,
        Adapters = 2 * Transfer,
        WebApi = 2 * Adapters,

        BaseUnits = Contracts + Logic + Transfer + Adapters + WebApi,

        AspMvc = 2 * WebApi,
        BlazorApp = 2 * AspMvc,
        AngularApp = 2 * BlazorApp,

        NoneApps = 0,
        AllApps = AspMvc + BlazorApp + AngularApp,
    }
}
//MdEnd
