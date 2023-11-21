using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_interfacepackage", "id")]
[Serializable]
public class CuDInterfacePackage
{
    [FieldAttrs("id", "TEXT", "interfacepackage的id, 主键")]
    public string? Id { get; set; }

    [FieldAttrs("createuserid", "TEXT", "interfacepackage创建人id")]
    public string? CreateUserId { get; set; }

    [FieldAttrs("assigneeuserid", "TEXT", "interfacepackage执行人id")]
    public string? AssigneeUserId { get; set; }

    [FieldAttrs("interfacepackagename", "TEXT", "interfacepackage的名称")]
    public string? InterfacePackageName { get; set; }

    [FieldAttrs("interfacepackagedescription", "TEXT", "interfacepackage的描述")]
    public string? InterfacePackageDescription { get; set; }

    [FieldAttrs("status", "TEXT", "interfacepackage的状态")]
    public string? Status { get; set; }

    [FieldAttrs("createdatetime", "TEXT", "interfacepackage创建时间(YYYY-MM-DD HH:MM:SS.fff)")]
    public string? CreateDateTime { get; set; }

    [FieldAttrs("modifydatetime", "TEXT", "interfacepackage修改时间(YYYY-MM-DD HH:MM:SS.fff)")]
    public string? ModifyDateTime { get; set; }

    [FieldAttrs("interfaceids", "TEXT", "包含的interface的id列表")]
    public string? InterfaceIds { get; set; }

    [FieldAttrs("totalinterfaces", "INTEGER", "interface的总数量")]
    public string? TotalInterfaces { get; set; }

    [FieldAttrs("completedinterfaces", "INTEGER", "interface状态completed的数量")]
    public string? CompletedInterfaces { get; set; }

    [FieldAttrs("localvariableids", "TEXT", "此包包含的本地参数化设置的id列表")]
    public string? LocalVariableIds { get; set; }

    [FieldAttrs("preinterfaceids", "TEXT", "此包全量运行时,前置interface的id列表")]
    public string? PreInterfaceIds { get; set; }
}