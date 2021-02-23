//@QnSCodeCopy
//MdStart
using System;

namespace QnSTradingCompany.BlazorApp.Modules.Common
{
    [Flags]
    public enum ControlType

    {
        TextBox = 1,
        TextArea = 2,
        Numeric = 4,
        NumericNull = 8,
        FloatingPoint = 16,
        FloatingPointNull = 32,
        Select = 64,
        SelectNull = 128,
        DatePicker = 256,
        DatePickerNull = 512,
        TimePicker = 1024,
        TimePickerNull = 2048,
        CheckBox = 2 * 2048,
        CheckBoxNull = 4 * 2048,

        ValidateType = TextBox + TextArea + Numeric + FloatingPoint + DatePicker + TimePicker,
    }
}
//MdEnd
