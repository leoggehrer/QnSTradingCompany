//@QnSCodeCopy
//MdStart

using System;

namespace CSharpCodeGenerator.Logic.Common
{
    [Flags]
    public enum ItemType : long
    {
        BusinessEntity = 1,
        ModuleEntity = 2,
        PersistenceEntity = 4,
        ShadowEntity = 8,
        Entiy = BusinessEntity + ModuleEntity + PersistenceEntity + ShadowEntity,

        DbContext = 16,
        Factory = 32,

        BusinessController = 64,
        PersistenceController = 128,
        ShadowController = 256,
        WebApiController = 512,
        Controller = BusinessController  + PersistenceController + ShadowController + WebApiController,

        BusinessModel = 1024,
        ModuleModel = 2048,
        PersistenceModel = 4096,
        ShadowModel = 8192,
        Model = BusinessModel + ModuleModel + PersistenceModel + ShadowModel,

        IndexRazorPage = 8192 * 2,
        IndexRazorPageCode = 8192 * 4,
        DataGridHandlerCode = 8192 * 8,
        DataGridComponentRazor = 8192 * 16,
        DataGridComponentCode = 8192 * 32,
        DataGridComponentCommonCode = 8192 * 64,
        DataGridColumnsComponentRazor = 8192 * 128,
        DataGridColumnsComponentCode = 8192 * 256,
        DataGridDetailComponentRazor = 8192 * 512,
        DataGridDetailComponentCode = 8192 * 1024,

        FieldSetHandlerCode = 8192 * 2048,
        FieldSetComponentRazor = 8192 * 4096,
        FieldSetComponentCode = 8192 * 8192,
        FieldSetDetailComponentRazor = 8192 * 8192 * 2,
        FieldSetDetailComponentCode = 8192 * 8192 * 4,

        EditFormComponentRazor = 8192 * 8192 * 8,
        EditFormComponentCode = 8192 * 8192 * 16,

        TypeScriptEnum = (long)8192 * 8192 * 32,
        TypeScriptContract = (long)8192 * 8192 * 64,

        Translations = (long)8192 * 8192 * 128,
        Properties = (long)8192 * 8192 * 256,

        IndexRazorPageAll = IndexRazorPage + IndexRazorPageCode,
        DataGridAll = DataGridHandlerCode 
                    + DataGridComponentRazor 
                    + DataGridComponentCode
                    + DataGridComponentCommonCode
                    + DataGridDetailComponentRazor 
                    + DataGridDetailComponentCode
                    + DataGridColumnsComponentRazor 
                    + DataGridColumnsComponentCode,
        FieldSetAll = FieldSetHandlerCode + FieldSetComponentRazor + FieldSetComponentCode
                    + FieldSetDetailComponentRazor + FieldSetDetailComponentCode,
        EditFormAll = EditFormComponentRazor + EditFormComponentCode,
    }
}
//MdEnd
