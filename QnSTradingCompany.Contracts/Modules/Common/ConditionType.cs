namespace QnSTradingCompany.Contracts.Modules.Common
{
    public enum ConditionType : int
    {
        PieceDiscountRelative,    // Rabatt ab einer bestimmten Stückzahl
        PieceDiscountAbsolute,    // Abzug ab einer bestimmten Stückzahl
        ValueDiscountRelative,    // Rabatt ab einem bestimmten Bestellwert
        ValueDiscountAbsolute,  // Abzug ab einem bestimmten Bestellwert
    }
}
