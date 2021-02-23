//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using QnSTradingCompany.BlazorApp.Modules.Exception;
using QnSTradingCompany.BlazorApp.Modules.SessionStorage;
using QnSTradingCompany.BlazorApp.Services.Modules.Language;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class AccessComponent : ComponentBase
    {
        protected bool CanRender { get; private set; }
        [Inject]
        protected ITranslatorService Translator { get; private set; }
        [Inject]
        protected NavigationManager NavigationManager { get; private set; }
        [Inject]
        protected IProtectedBrowserStorage ProtectedBrowserStore { get; set; }

        protected AuthorizationSession AuthSession { get; private set; }
        protected string SessionToken => AuthSession?.Token;

        protected Func<string, string> Translate
        {
            get
            {
                return Translator.Translate;
            }
        }
        protected Func<string, string, string> TranslateWithDefault
        {
            get
            {
                return Translator.Translate;
            }
        }

        protected virtual void CheckServiceResult(ServiceResult serviceResult)
        {
            if (serviceResult.HasError)
            {
                if (serviceResult.Exception.Message.Equals(ErrorMessages.GetMessage(ErrorIdentity.AuthorizationTimeOut)))
                {
                    NavigationManager.NavigateTo($"/{StaticLiterals.LoginPage}");
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);
            if (firstRender)
            {
                CanRender = true;
                AuthSession = await ProtectedBrowserStore.GetAsync<AuthorizationSession>(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);
            }
        }

        protected static string[] ConvertToSubModelName(string contractTypeName)
        {
            contractTypeName.CheckArgument(nameof(contractTypeName));

            var startBuild = false;
            var items = contractTypeName.Split(".");
            var result = new List<string>();

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals("Contracts"))
                {
                    startBuild = true;
                    result.Add("Models");
                }
                else if (startBuild)
                {
                    if (i + 1 == items.Length)
                        result.Add(items[i][1..]);
                    else
                        result.Add(items[i]);
                }
            }
            return result.ToArray();
        }
    }
}
//MdEnd
