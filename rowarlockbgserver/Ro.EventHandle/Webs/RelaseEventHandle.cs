using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class RelaseEventHandle
{
    public void LoadEvent()
    {
        ReleaseEvent.GetReleaseYearListByUserIdEvent += OnGetReleaseYearListByUserIdEventHandle;
        ReleaseEvent.GetReleaseDetailByIdEvent += OnGetReleaseDetailByIdEventHandle;
        ReleaseEvent.GetReleaseListByUserIdEvent += OnGetReleaseListByUserIdEventHandle;
        ReleaseEvent.UpdateReleaseEvent += OnUpdateReleaseEventHandle;
        ReleaseEvent.CreateReleaseEvent += OnCreateReleaseEventHandle;
    }


    public void UnLoadEvent()
    {
        ReleaseEvent.GetReleaseYearListByUserIdEvent -= OnGetReleaseYearListByUserIdEventHandle;
        ReleaseEvent.GetReleaseDetailByIdEvent -= OnGetReleaseDetailByIdEventHandle;
        ReleaseEvent.GetReleaseListByUserIdEvent -= OnGetReleaseListByUserIdEventHandle;
        ReleaseEvent.UpdateReleaseEvent -= OnUpdateReleaseEventHandle;
        ReleaseEvent.CreateReleaseEvent -= OnCreateReleaseEventHandle;
    }

    public ResponseType OnGetReleaseYearListByUserIdEventHandle(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        // 参数实例化
        CuDRelease cuDRelease = para;
        // string condition = houtobj.Para;
        // dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);

        // 执行
        using var dborm = new DBORM<CuDRelease>(ComArgs.SqliteConnection, cuDRelease);
        var queryresult = dborm.Query($"assigneeuserid='{cuDRelease.AssigneeUserId}'");
        if (queryresult.Any())
        {
            // 按年分组,年为分组的主键
            var group = queryresult.GroupBy(release => Convert.ToDateTime(release.CreateDateTime).Year);
            var enumerable = group.ToDictionary(obj => obj.Key, obj => obj.ToList());
            return ReqResFunc.GetResponseBody(enumerable.Any() ? UReqCode.Success : UReqCode.Fail, enumerable);
        }

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(UReqCode.NotFound, queryresult);
    }

    public ResponseType OnGetReleaseDetailByIdEventHandle(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        // 参数实例化
        CuDRelease cuDRelease = para;
        string condition = houtobj.Para;
        dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        dynamic id = dycondition["id"];
        cuDRelease.Id = id.ToString();

        // 执行
        using var dborm = new DBORM<CuDRelease>(ComArgs.SqliteConnection, cuDRelease);
        var queryresult = dborm.Query($"assigneeuserid='{cuDRelease.AssigneeUserId}' AND id='{cuDRelease.Id}'");
        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(queryresult.Any() ? UReqCode.Success : UReqCode.Fail, queryresult);
    }

    public ResponseType OnGetReleaseListByUserIdEventHandle(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        // 参数实例化
        CuDRelease cuDRelease = para;
        string condition = houtobj.Para;
        dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        dynamic year = dycondition["year"];

        // 执行
        using var dborm = new DBORM<CuDRelease>(ComArgs.SqliteConnection, cuDRelease);
        var queryresult = dborm.Query($"assigneeuserid='{cuDRelease.AssigneeUserId}'");
        if (queryresult.Any())
        {
            // 按年分组后, 对比year给列表
            var group = queryresult.GroupBy(release => Convert.ToDateTime(release.CreateDateTime).Year);

            var enumerable = group.ToList();
            if (enumerable.Any())
            {
                var yearlist = enumerable.Where(release => release.Key.ToString() == year.ToString()).ToList();
                return ReqResFunc.GetResponseBody(yearlist.Any() ? UReqCode.Success : UReqCode.Fail, yearlist);
            }
        }

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(UReqCode.NotFound, queryresult);
    }

    public ResponseType OnUpdateReleaseEventHandle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        //重新更新一下modifydatetime
        CuDRelease cuDRelease = para;
        cuDRelease.ModifyDateTime = DateTime.Now.ToString("yyyyy-MM-dd HH-mm-ss.fff");

        // 执行
        using var dborm = new DBORM<CuDRelease>(ComArgs.SqliteConnection, cuDRelease);
        int queryreslut = dborm.Update();

        return ReqResFunc.GetResponseBody(!queryreslut.Equals(0) ? UReqCode.Success : UReqCode.Fail, queryreslut);
    }

    public ResponseType OnCreateReleaseEventHandle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        //重新更新一下modifydatetime
        CuDRelease cuDRelease = para;
        cuDRelease.ModifyDateTime = DateTime.Now.ToString("yyyyy-MM-dd HH-mm-ss.fff");

        // 执行
        using var dborm = new DBORM<CuDRelease>(ComArgs.SqliteConnection, cuDRelease);
        int queryreslut = dborm.Insert();

        return ReqResFunc.GetResponseBody(!queryreslut.Equals(0) ? UReqCode.Success : UReqCode.Fail, queryreslut);
    }
}