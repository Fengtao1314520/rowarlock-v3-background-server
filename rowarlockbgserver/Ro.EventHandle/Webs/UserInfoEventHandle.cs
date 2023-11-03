using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType;
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
    public object OnBasic(HOutObjType houtobj, object para, ref LogStruct logstruct)
    {
        // 参数实例化
        UserDetails userDetails = (UserDetails) para;

        using var dborm = new DBORM<UserDetails>(ComArgs.SqliteConnection, userDetails);
        int result = dborm.Update();

        // 设置返回类型，失败的
        return ReqResFunc.GetResponseBody(result > 0
            ?
            // 设置返回类型，成功的
            UReqCode.Success
            : UReqCode.Fail, result);
    }
}