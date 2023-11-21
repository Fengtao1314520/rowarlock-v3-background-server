using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_interface", "id")]
[Serializable]
public class CuDInterface
{
    [FieldAttrs("id", "TEXT", "interface的id, 主键")]
    public string? Id { get; set; }

    [FieldAttrs("createuserid", "TEXT", "interface创建人id")]
    public string? CreateUserId { get; set; }

    [FieldAttrs("assigneeuserid", "TEXT", "interface执行人id")]
    public string? AssigneeuserId { get; set; }

    [FieldAttrs("packageid", "TEXT", "归属package的id")]
    public string? PackageId { get; set; }

    [FieldAttrs("interfacename", "TEXT", "interface的名称")]
    public string? InterfaceName { get; set; }

    [FieldAttrs("interfacedescription", "TEXT", "interface的描述")]
    public string? InterfaceDescription { get; set; }

    [FieldAttrs("status", "TEXT", "interfacepackage的状态")]
    public string? Status { get; set; }

    [FieldAttrs("createdatetime", "TEXT", "interface创建时间(YYYY-MM-DD HH:MM:SS.fff)")]
    public string? CreateDateTime { get; set; }

    [FieldAttrs("modifydatetime", "TEXT", "interface修改时间(YYYY-MM-DD HH:MM:SS.fff)")]
    public string? ModifyDateTime { get; set; }

    [FieldAttrs("interfacecontent", "TEXT", "interface内容,JSON格式内容")]
    public string? InterfaceContent { get; set; }
}