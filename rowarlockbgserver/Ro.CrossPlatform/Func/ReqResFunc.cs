using Ro.Basic.UEnum;
using Ro.Basic.UType;

namespace Ro.CrossPlatform.Func;

public static class ReqResFunc
{
    /// <summary>
    /// 获取返回体
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ResponseType GetResponseBody<T>(UReqCode code, T? data)
    {
        ResponseType rt = new()
        {
            rescode = (int) code,
            resdata = data
        };

        // 根据返回码，设置返回信息
        switch (code)
        {
            case UReqCode.Success:
                rt.resmessage = "后端检索、执行成功";
                return rt;
            case UReqCode.Similar:
                rt.resmessage = "后端检索、执行成功，但是有相似结果";
                return rt;
            case UReqCode.Fail:
                rt.resmessage = "后端检索、执行失败";
                return rt;
            case UReqCode.Error:
                rt.resmessage = "后端检索、执行错误";
                return rt;
            case UReqCode.NotFound:
                rt.resmessage = "后端检索,未找到结果";
                return rt;
            case UReqCode.TypeError:
                rt.resmessage = "数据类型错误";
                return rt;
            case UReqCode.QueryEmpty:
                rt.resmessage = "GET Method中, Query为空";
                return rt;
            case UReqCode.ParaEmpty:
                rt.resmessage = "POST Method中, 参数为空";
                return rt;
            default:
                rt.resmessage = "未知错误";
                return rt;
        }

        return rt;
    }


    /// <summary>
    /// 获取返回体
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static ResponseType GetErrorResponseBody(UReqCode code)
    {
        var tempobj = new {datetime = GatherFunc.NowDateTime()};
        return GetResponseBody(code, tempobj);
    }
}