using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class UserInfoEventHandle
{
    /// <summary>
    /// 单路执行
    /// UserInfo的事件处理
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <returns></returns>
    public ResponseType OnBasic(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDUserDetails cuDUserDetails = (CuDUserDetails) para;
        // 执行
        using var dborm = new DBORM<CuDUserDetails>(ComArgs.SqliteConnection, cuDUserDetails);
        int result = dborm.Update();
        dborm.Dispose(); //GC释放

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(result > 0 ? UReqCode.Success : UReqCode.Fail, result);
    }

    public ResponseType OnGetInfo(HOutObjType houtobj, string para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDUserDetails cuDUserDetails = new()
        {
            Id = para
        };
        // 执行
        using var dborm = new DBORM<CuDUserDetails>(ComArgs.SqliteConnection, cuDUserDetails);
        var result = dborm.Query();
        dborm.Dispose(); //GC释放
        return ReqResFunc.GetResponseBody(result.Count > 0 ? UReqCode.Success : UReqCode.Fail, result);
    }
}