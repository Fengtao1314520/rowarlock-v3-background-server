using Carter;
using Carter.Request;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ro.Basic.UEnum;
using Ro.Basic.UType;
using Ro.Basic.UType.FBConnection;
using Ro.CrossPlatform.Extension;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.CrossPlatform.TemplateFunc;

namespace Ro.Http.Controller;

/// <summary>
/// 👇 Create a Carter module for the API
/// </summary>
public class Users : TCarterModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/userinfo", GetUserInfo);
        app.MapPost("/upuserinfo", UpdateUserInfo);
        app.MapPost("/userlogin", Login);
        app.MapPost("/userlogout", Logout);
    }


    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private IResult UpdateUserInfo(HttpContext ctx, UserInfo userInfo)
    {
        // 设置返回类型
        ctx.Response.ContentType = "application/json";
        // 设置请求类型
        HOutObjType obj = new() {method = "post", api = "/api/upuserinfo", para = userInfo};
        // 验证
        ResponseType result = RelatedFunc(obj, "userinfo", userInfo.userid, out LogStruct logStruct);
        return Results.Json(result);
    }


    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private IResult Login(HttpContext ctx, UserInfo userInfo)
    {
        ctx.Response.ContentType = "application/json";
        // 设置请求类型
        HOutObjType obj = new() {method = "post", api = "/api/userlogin", para = userInfo};
        // 验证
        ResponseType result = RelatedFunc(obj, "userlogin", userInfo.userid, out LogStruct logStruct);
        return Results.Json(result);
    }


    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private IResult Logout(HttpContext ctx, UserInfo userInfo)
    {
        ctx.Response.ContentType = "application/json";
        // 设置请求类型
        HOutObjType obj = new() {method = "post", api = "/api/userlogout", para = userInfo};
        // 验证
        ResponseType result = RelatedFunc(obj, "userlogout", userInfo.userid, out LogStruct logStruct);
        return Results.Json(result);
    }


    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private IResult GetUserInfo(HttpContext ctx)
    {
        ctx.Response.ContentType = "application/json";
        //检索首个userid
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        // 设置请求类型
        HOutObjType obj = new() {method = "get", api = "/api/userinfo", para = allQuery};
        // 验证
        ResponseType result = RelatedFunc(obj, "userlogout", userid, out LogStruct logStruct);
        return Results.Json(result);
    }

    /// <summary>
    /// 相关函数
    /// </summary>
    /// <param name="hOutObjType">HTTP内容对象类型</param>
    /// <param name="apitype">HTTP类型</param>
    /// <param name="para">附件参数</param>
    /// <param name="logStruct">LOG结构体</param>
    protected override ResponseType RelatedFunc(HOutObjType hOutObjType, string apitype, object? para,
        out LogStruct logStruct)
    {
        // 返回类型结果
        ResponseType result;

        // INFO 1: 日志初始化
        logStruct = new LogStruct();
        logStruct.Init(true);

        // INFO 2: 验证
        ValidationResult validationResult = (para as string).Vaildator();

        // INFO 3: 验证结果
        if (validationResult.IsValid)
        {
            // INFO 3.1 验证通过

            //TODO: 3.1.1 根据请求类型，执行不同的操作
            object data = new { };
#if DEBUG
            data = new UserInfo()
            {
                userid = (string) para,
                username = "测试账号",
                datetime = GatherFunc.NowDateTime()
            };
#endif
            result = ReqResFunc.GetResponseBody(UReqCode.Success, data);
        }
        else
        {
            // INFO 3.2 验证未通过
            // 未通过验证
            UserInfo empty = new() {datetime = GatherFunc.NowDateTime()};
            // 设置返回类型，错误的，直接给个空的
            result = ReqResFunc.GetErrorResponseBody(UReqCode.ParaEmpty);
        }

        // INFO 3.3 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);

        return result;
    }
}