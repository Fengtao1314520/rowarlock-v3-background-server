using Carter;
using Carter.Request;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ro.Basic.UEnum.APIUrl;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Logs;
using Ro.CrossPlatform.Vaildator;

namespace Ro.Http.Controller;

/// <summary>
/// 👇 Create a Carter module for the API
/// </summary>
public class Users : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiUrl.USERINFO, GetUserInfo);
        app.MapPost(ApiUrl.USERINFO, CreateUserInfo);
        app.MapPut(ApiUrl.USERINFO, UpdateUserInfo);
        app.MapPost(ApiUrl.LOGIN, Login);
        app.MapPost(ApiUrl.LOGOUT, Logout);
    }


    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="cuDUserInfo"></param>
    /// <returns></returns>
    private IResult Login(HttpContext ctx, CuDUserDetails cuDUserInfo)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new();
        logStruct.Init(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.POST,
            Api = ApiUrl.LOGIN,
            Para = cuDUserInfo
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDUserInfo, typeof(CuDUserDetails), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => UserInfoEvent.OnDiffEvent(hOutObjType, cuDUserInfo, ref logStruct, hOutObjType.Api,
                hOutObjType.Method), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }


    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="cuDUserInfo"></param>
    /// <returns></returns>
    private IResult Logout(HttpContext ctx, CuDUserDetails cuDUserInfo)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new();
        logStruct.Init(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.POST,
            Api = ApiUrl.LOGOUT,
            Para = cuDUserInfo
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDUserInfo, typeof(CuDUserDetails), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => UserInfoEvent.OnDiffEvent(hOutObjType, cuDUserInfo, ref logStruct, hOutObjType.Api,
                hOutObjType.Method), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }


    /// <summary>
    /// 创建用户信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="cuDUserInfo"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private IResult CreateUserInfo(HttpContext ctx, CuDUserDetails cuDUserInfo)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new();
        logStruct.Init(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.POST,
            Api = ApiUrl.USERINFO,
            Para = cuDUserInfo
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDUserInfo, typeof(CuDUserDetails), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => UserInfoEvent.OnDiffEvent(hOutObjType, cuDUserInfo, ref logStruct, hOutObjType.Api,
                hOutObjType.Method), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }


    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="cuDUserInfo"></param>
    /// <returns></returns>
    private IResult UpdateUserInfo(HttpContext ctx, CuDUserDetails cuDUserInfo)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new();
        logStruct.Init(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.PUT,
            Api = ApiUrl.USERINFO,
            Para = cuDUserInfo
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDUserInfo, typeof(CuDUserDetails), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => UserInfoEvent.OnDiffEvent(hOutObjType, cuDUserInfo, ref logStruct, hOutObjType.Api,
                hOutObjType.Method), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }


    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private IResult GetUserInfo(HttpContext ctx)
    {
        //检索首个userid
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        // 拼装
        CuDUserDetails cuDUserInfo = new()
        {
            Id = userid
        };

        // INFO 0: 日志初始化
        LogStruct logStruct = new();
        logStruct.Init(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.GET,
            Api = ApiUrl.USERINFO,
            Para = cuDUserInfo
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDUserInfo, typeof(CuDUserDetails), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => UserInfoEvent.OnDiffEvent(hOutObjType, cuDUserInfo, ref logStruct, hOutObjType.Api,
                hOutObjType.Method), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }
}