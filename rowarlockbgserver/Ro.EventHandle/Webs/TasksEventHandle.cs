using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class TasksEventHandle
{
    public void LoadEvent()
    {
        TasksEvent.GetTaskDetailByIdEvent += OnGetTaskDetailByIdEventHanlde;
        TasksEvent.GetTaskListByUserIdEvent += OnGetTaskListByUserIdEventHanlde;
    }

    public void UnLoadEvent()
    {
        TasksEvent.GetTaskDetailByIdEvent -= OnGetTaskDetailByIdEventHanlde;
        TasksEvent.GetTaskListByUserIdEvent -= OnGetTaskListByUserIdEventHanlde;
    }


    public ResponseType OnGetTaskListByUserIdEventHanlde(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDTask cuDTask = para;
        string condition = houtobj.Para;
        dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        dynamic days = dycondition["days"];
        // 执行
        using var dborm = new DBORM<CuDTask>(ComArgs.SqliteConnection, cuDTask);
        //获取当前时间
        DateTime now = DateTime.Now;
        // 从当天往前计算days天
        DateTime start = Convert.ToDateTime(now.ToString("yyyy-MM-dd 00:00:00.000")).AddDays(-Convert.ToInt32(days));
        // var queryresult = dborm.Query("assigneeuserid", task.AssigneeUserId);
        var queryresult = dborm.Query($"assigneeuserid='{cuDTask.AssigneeUserId}'");
        List<CuDTask> tempList = (from item in queryresult
            let ts = item.EndTime
            let endTime = Convert.ToDateTime(ts)
            where endTime < start
            select item).ToList();

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(tempList.Any() ? UReqCode.Success : UReqCode.Fail, tempList);
    }


    public ResponseType OnGetTaskDetailByIdEventHanlde(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDTask cuDTask = para;
        string condition = houtobj.Para;
        dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        dynamic id = dycondition["id"];
        cuDTask.Id = id.ToString();
        // 执行
        using var dborm = new DBORM<CuDTask>(ComArgs.SqliteConnection, cuDTask);
        var queryresult = dborm.Query($"assigneeuserid='{cuDTask.AssigneeUserId}' AND id='{cuDTask.Id}'");

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(queryresult.Any() ? UReqCode.Success : UReqCode.Fail, queryresult);
    }
}