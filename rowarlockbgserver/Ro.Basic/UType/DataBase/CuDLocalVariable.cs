using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_localvariable", "id")]
[Serializable]
public class CuDLocalVariable
{
    [FieldAttrs("id", "TEXT", "localvariable的id, 主键")]
    public string? Id { get; set; }

    [FieldAttrs("createuserid", "TEXT", "interfacepackage创建人id")]
    public string? CreateUserId { get; set; }

    [FieldAttrs("localvariablekey", "TEXT", "localvariable键值名")]
    public string? LocalVariableKey { get; set; }

    [FieldAttrs("localvariablevalue", "TEXT", "localvariable键值值")]
    public string? LocalVariableValue { get; set; }

    [FieldAttrs("status", "TEXT", "interfacepackage的状态")]
    public string? Status { get; set; }

    [FieldAttrs("createdatetime", "TEXT", "interfacepackage创建时间(YYYY-MM-DD HH:MM:SS.fff)")]
    public string? CreateDateTime { get; set; }

    [FieldAttrs("modifydatetime", "TEXT", "interfacepackage修改时间(YYYY-MM-DD HH:MM:SS.fff)")]
    public string? ModifyDateTime { get; set; }
}