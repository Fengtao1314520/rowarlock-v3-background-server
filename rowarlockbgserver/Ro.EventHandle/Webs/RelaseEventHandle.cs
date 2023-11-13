using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Extension;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class RelaseEventHandle
{
    public ResponseType OnListReleaseBaseYearByUserInfo(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        string userid = (para as object).GetPropertyValue("userid").ToString();
        // 参数实例化
        CuDRelease cuDRelease = new()
        {
            AssigneeUserId = userid
        };

        // 执行
        using var dborm = new DBORM<CuDRelease>(ComArgs.SqliteConnection, cuDRelease);
        var queryreslut = dborm.Query("assigneeuserid", cuDRelease.AssigneeUserId);

        //按照年分组
        var group = queryreslut.GroupBy(release => Convert.ToDateTime(release.CreatedAt).Year);
        // 设置返回类型，失败的,设置返回类型，成功的
        var enumerable = group as IGrouping<int, CuDRelease>[] ?? group.ToArray();
        return ReqResFunc.GetResponseBody(enumerable.Any() ? UReqCode.Success : UReqCode.Fail, enumerable);
    }


    public ResponseType OnUpdataRelease(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        //重新更新一下modifydatetime
        CuDRelease cuDRelease = para as CuDRelease;
        cuDRelease.ModifiedAt = DateTime.Now.ToString("yyyyy-MM-dd HH-mm-ss.fff");

        // 执行
        using var dborm = new DBORM<CuDRelease>(ComArgs.SqliteConnection, cuDRelease);
        int queryreslut = dborm.Update();

        return ReqResFunc.GetResponseBody(!queryreslut.Equals(0) ? UReqCode.Success : UReqCode.Fail, queryreslut);
    }
}