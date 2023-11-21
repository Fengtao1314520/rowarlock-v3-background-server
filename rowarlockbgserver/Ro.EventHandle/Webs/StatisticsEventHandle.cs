using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class StatisticsEventHandle
{
    public void LoadEvent()
    {
        StatisticsEvent.GetStatisticsEvent += OnGetStatisticsEventHandle;
    }

    public void UnLoadEvent()
    {
        StatisticsEvent.GetStatisticsEvent -= OnGetStatisticsEventHandle;
    }


    public ResponseType OnGetStatisticsEventHandle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDStatistics cuDStatistics = para;
        // 执行
        using var dborm = new DBORM<CuDStatistics>(ComArgs.SqliteConnection, cuDStatistics);
        var queryresult = dborm.Query("userid", cuDStatistics.UserId);

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(queryresult.Any() ? UReqCode.Success : UReqCode.Fail, queryresult.First());
    }
}