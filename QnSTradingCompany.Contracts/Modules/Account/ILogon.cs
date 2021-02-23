//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.Contracts.Modules.Account
{
    public partial interface ILogon
    {
        string Email { get; set; }
        string Password { get; set; }
        string OptionalInfo { get; set; }
    }
}
//MdEnd
