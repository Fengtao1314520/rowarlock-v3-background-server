using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_job", "id")]
[Serializable]
public class CuDJob
{
    [FieldAttrs("id", "TEXT", "job的id, 主键")]
    public string Id { get; set; }

    [FieldAttrs("type", "TEXT", "job的type")]
    public string Type { get; set; }

    [FieldAttrs("createuserid", "TEXT", "job创建人id")]
    public string CreateUserId { get; set; }

    [FieldAttrs("assigneeuserid", "TEXT", "job执行人id")]
    public string AssigneeUserId { get; set; }

    [FieldAttrs("clientid", "TEXT", "job执行客户端")]
    public string ClientId { get; set; }

    [FieldAttrs("status", "TEXT", "job的状态")]
    public string Status { get; set; }

    [FieldAttrs("expandtime", "INTEGER", "job预期耗时(秒)")]
    public int ExpandTime { get; set; }

    [FieldAttrs("elapsedtime", "INTEGER", "job已耗时(秒)")]
    public int ElapsedTime { get; set; }

    [FieldAttrs("starttime", "TEXT", "job开始时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string StartTime { get; set; }

    [FieldAttrs("endtime", "TEXT", "job结束时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string EndTime { get; set; }

    [FieldAttrs("jobcontent", "TEXT", "job内容")]
    public string JobContent { get; set; }

    [FieldAttrs("jobresult", "TEXT", "job结果")]
    public string JobResult { get; set; }

    [FieldAttrs("jobresultdesc", "TEXT", "job结果描述")]
    public string JobResultDesc { get; set; }

    [FieldAttrs("jobresultcode", "TEXT", "job结果代码")]
    public string JobResultCode { get; set; }

    [FieldAttrs("jobresulttime", "TEXT", "job结果时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string JobResultTime { get; set; }

    [FieldAttrs("finialresult", "TEXT", "最终结果")]
    public string FinialResult { get; set; }
}