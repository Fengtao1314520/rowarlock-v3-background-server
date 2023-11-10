using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_task", "id")]
[Serializable]
public class CuDTask
{
    [FieldAttrs("id", "TEXT", "任务id, 主键")]
    public string Id { get; set; }

    [FieldAttrs("type", "TEXT", "task的type")]
    public string Type { get; set; }

    [FieldAttrs("createuserid", "TEXT", "task创建人id")]
    public string CreateUserId { get; set; }

    [FieldAttrs("assigneeuserid", "TEXT", "task执行人id")]
    public string AssigneeUserId { get; set; }

    [FieldAttrs("taskname", "TEXT", "task的名称")]
    public string TaskName { get; set; }

    [FieldAttrs("taskcontent", "TEXT", "task的内容")]
    public string TaskContent { get; set; }

    [FieldAttrs("status", "TEXT", "task的状态")]
    public string Status { get; set; }

    [FieldAttrs("expandtime", "INTEGER", "task预期耗时(秒)")]
    public int ExpandTime { get; set; }

    [FieldAttrs("elapsedtime", "INTEGER", "task已耗时(秒)")]
    public int ElapsedTime { get; set; }

    [FieldAttrs("starttime", "TEXT", "task开始时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string StartTime { get; set; }

    [FieldAttrs("endtime", "TEXT", "task结束时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string EndTime { get; set; }
}