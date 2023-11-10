using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Extension;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;
using Task = Ro.Basic.UType.DataBase.Task;

namespace Ro.EventHandle.Webs;

public class TasksEventHandle
{
    public ResponseType OnSimpleTasksByUserInfo(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        string userid = (para as object).GetPropertyValue("userid").ToString();
        // 参数实例化
        Statistics statistics = new()
        {
            UserId = userid
        };
        // 执行
        using var dborm = new DBORM<Statistics>(ComArgs.SqliteConnection, statistics);
        var queryresult = dborm.Query("userid", statistics.UserId);

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(queryresult.Any() ? UReqCode.Success : UReqCode.Fail, queryresult.First());
    }

    public ResponseType OnListTasksBaseDayByUserInfo(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        string userid = (para as object).GetPropertyValue("userid").ToString();
        string days = (para as object).GetPropertyValue("days").ToString();
        // 参数实例化
        Task task = new()
        {
            AssigneeUserId = userid
        };
        // 执行
        using var dborm = new DBORM<Task>(ComArgs.SqliteConnection, task);
        //获取当前时间
        DateTime now = DateTime.Now;
        // 从当天往前计算days天
        DateTime start = Convert.ToDateTime(now.ToString("yyyy-MM-dd 00:00:00.000")).AddDays(-Convert.ToInt32(days));
        // var queryresult = dborm.Query("assigneeuserid", task.AssigneeUserId);
        var queryresult = dborm.Query($"assigneeuserid='{task.AssigneeUserId}'");
        List<Task> tempList = (from item in queryresult
            let ts = item.EndTime
            let endTime = Convert.ToDateTime(ts)
            where endTime < start
            select item).ToList();

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(tempList.Any() ? UReqCode.Success : UReqCode.Fail, tempList);
    }
}