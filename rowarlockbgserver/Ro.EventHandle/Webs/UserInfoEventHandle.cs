using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class UserInfoEventHandle
{
    public void LoadEvent()
    {
        UserInfoEvent.LoginEvent += OnLoginEventHandle;
        UserInfoEvent.LogoutEvent += OnLogoutEventHandle;
        UserInfoEvent.CreateUserInfoEvent += OnCreateUserInfoEvnetHanle;
        UserInfoEvent.UpdateUserInfoEvent += OnUpdateUserInfoEvnetHanle;
        UserInfoEvent.GetUserInfoEvent += OnGetUserInfoEventHandle;
    }

    public void UnLoadEvent()
    {
        UserInfoEvent.LoginEvent -= OnLoginEventHandle;
        UserInfoEvent.LogoutEvent -= OnLogoutEventHandle;
        UserInfoEvent.CreateUserInfoEvent -= OnCreateUserInfoEvnetHanle;
        UserInfoEvent.UpdateUserInfoEvent -= OnUpdateUserInfoEvnetHanle;
        UserInfoEvent.GetUserInfoEvent -= OnGetUserInfoEventHandle;
    }

    /// <summary>
    /// 登录事件处理
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <returns></returns>
    public ResponseType OnLoginEventHandle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdateHandle(houtobj, para, ref logstruct);
    }

    /// <summary>
    /// 登录事件处理
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <returns></returns>
    public ResponseType OnLogoutEventHandle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdateHandle(houtobj, para, ref logstruct);
    }

    /// <summary>
    /// 获取用户信息事件处理
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <returns></returns>
    public ResponseType OnGetUserInfoEventHandle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDUserDetails cuDUserDetails = para;

        // 执行
        using var dborm = new DBORM<CuDUserDetails>(ComArgs.SqliteConnection, cuDUserDetails);
        var result = dborm.Query();
        dborm.Dispose(); //GC释放
        return ReqResFunc.GetResponseBody(result.Count >= 1 ? UReqCode.Success : UReqCode.Fail, result);
    }

    /// <summary>
    /// 更新用户信息事件处理
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <returns></returns>
    public ResponseType OnUpdateUserInfoEvnetHanle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return UpdateHandle(houtobj, para, ref logstruct);
    }

    /// <summary>
    /// 创建用户信息事件处理
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <returns></returns>
    public ResponseType OnCreateUserInfoEvnetHanle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        // 参数实例化
        CuDUserDetails cuDUserDetails = (CuDUserDetails) para;
        // 执行
        using var dborm = new DBORM<CuDUserDetails>(ComArgs.SqliteConnection, cuDUserDetails);
        int result = dborm.Insert();
        dborm.Dispose(); //GC释放

        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(result == 1 ? UReqCode.Success : UReqCode.Fail, result);
    }

    /// <summary>
    /// 私有方法
    /// 更新用户信息
    /// </summary>
    /// <param name="houtobj"></param>
    /// <param name="para"></param>
    /// <param name="logstruct"></param>
    /// <returns></returns>
    private ResponseType UpdateHandle(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
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
}