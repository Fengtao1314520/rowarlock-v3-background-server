using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class InterfaceEventHandle
{
    public void LoadEvent()
    {
        InterfaceEvent.GetInterfaceEvent += OnGetInterfaceEvent;
        InterfaceEvent.CreateInterfaceEvent += OnCreateInterfaceEvent;
        InterfaceEvent.UpdateInterfaceEvent += OnUpdateInterfaceEvent;
    }

    public void UnLoadEvent()
    {
        InterfaceEvent.GetInterfaceEvent -= OnGetInterfaceEvent;
        InterfaceEvent.CreateInterfaceEvent -= OnCreateInterfaceEvent;
        InterfaceEvent.UpdateInterfaceEvent -= OnUpdateInterfaceEvent;
    }

    public ResponseType OnGetInterfaceEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDInterface cuDInterface = para;
        string condition = houtobj.Para;
        dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        int max = dycondition["max"];
        // 执行
        using var dborm = new DBORM<CuDInterface>(ComArgs.SqliteConnection, cuDInterface);

        var queryresult = dborm.Query("CreateUserId", cuDInterface.CreateUserId);

        // 按max数量进行分组，一组最多max个
        var groups = queryresult.Select((item, index) => new {Item = item, Index = index})
            .GroupBy(x => x.Index / max)
            .Select(g => g.Select(x => x.Item).ToList());

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(groups.Any() ? UReqCode.Success : UReqCode.Fail, groups);
    }

    public ResponseType OnCreateInterfaceEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDInterface cuDInterface = para;
        // string condition = houtobj.Para;
        // dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        // int max = dycondition["max"];
        // 执行
        using var dborm = new DBORM<CuDInterface>(ComArgs.SqliteConnection, cuDInterface);
        int insertresult = dborm.Insert();
        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(insertresult >= 1 ? UReqCode.Success : UReqCode.Fail, insertresult);
    }

    public ResponseType OnUpdateInterfaceEvent(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDInterface cuDInterface = para;
        // string condition = houtobj.Para;
        // dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        // int max = dycondition["max"];
        // 执行
        using var dborm = new DBORM<CuDInterface>(ComArgs.SqliteConnection, cuDInterface);
        int updateresult = dborm.Update();
        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(updateresult >= 1 ? UReqCode.Success : UReqCode.Fail, updateresult);
    }
}