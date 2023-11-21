using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_statistics", "id")]
[Serializable]
public class CuDStatistics
{
    [FieldAttrs("id", "TEXT", "统计id, 主键")]
    public string? Id { get; set; }

    [FieldAttrs("userid", "TEXT", "统计人id")]
    public string? UserId { get; set; }

    [FieldAttrs("userstatus", "TEXT", "统计人当前状态")]
    public string? UserStatus { get; set; }

    [FieldAttrs("totalcreatetask", "INTEGER", "用户创建的总任务数,job也是task的一类")]
    public int TotalCreateTask { get; set; }

    [FieldAttrs("totalassigneetask", "INTEGER", "用户被指向的总任务数,job也是task的一类")]
    public int TotalAssignedTask { get; set; }

    [FieldAttrs("lastcreatetasklasttime", "TEXT", "用户创建任务的最后时间,job也是task的一类，根据实际去不同表内捞取时间段的")]
    public string? LastCreateTaskLastTime { get; set; }

    [FieldAttrs("lastassigneetasklasttime", "TEXT", "用户被指向的任务的最后时间,job也是task的一类，根据实际去不同表内捞取时间段的")]
    public string? LastAssignedTaskLastTime { get; set; }

    [FieldAttrs("jobrate", "INTEGER", "job的比率")]
    public int JobRate { get; set; }
}