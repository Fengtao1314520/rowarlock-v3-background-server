using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_release", "id")]
[Serializable]
public class CuDRelease
{
    [FieldAttrs("id", "TEXT", "release的id, 主键")]
    public string? Id { get; set; }

    [FieldAttrs("createuserid", "TEXT", "release创建人id")]
    public string? CreateUserId { get; set; }

    [FieldAttrs("assigneeuserid", "TEXT", "release执行人id")]
    public string? AssigneeUserId { get; set; }

    [FieldAttrs("releasename", "TEXT", "release的名称")]
    public string? ReleaseName { get; set; }

    [FieldAttrs("releasedescription", "TEXT", "release的描述")]
    public string? ReleaseDescription { get; set; }

    [FieldAttrs("status", "TEXT", "release的状态")]
    public string? Status { get; set; }

    [FieldAttrs("createdatetime", "TEXT", "release创建时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string? CreateDateTime { get; set; }

    [FieldAttrs("modifydatetime", "TEXT", "release修改时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string? ModifyDateTime { get; set; }

    [FieldAttrs("releasecontent", "TEXT", "release的内容,JSON格式，除去已被定义的内容外，全部写入content")]
    public string? ReleaseContent { get; set; }
}