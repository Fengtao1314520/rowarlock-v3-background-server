using Carter;
using Carter.Request;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.CrossPlatform.TemplateFunc;
using Ro.CrossPlatform.Vaildator;

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
    /// <param name="cuDUserInfo"></param>
    /// <returns></returns>
    private IResult UpdateUserInfo(HttpContext ctx, CuDUserDetails cuDUserInfo)
    {
        // 设置返回类型
        ctx.Response.ContentType = "application/json";
        // 设置请求类型
        HOutObjType obj = new() {method = "post", api = "/api/upuserinfo", para = cuDUserInfo};
        // 验证
        ResponseType result = RelatedFunc(obj, "upuserinfo", cuDUserInfo, out LogStruct logStruct);
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        return Results.Json(result);
    }


    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="cuDUserInfo"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private IResult Login(HttpContext ctx, CuDUserDetails cuDUserInfo)
    {
        ctx.Response.ContentType = "application/json";
        // 设置请求类型
        HOutObjType obj = new() {method = "post", api = "/api/userlogin", para = cuDUserInfo};
        // 验证
        ResponseType result = RelatedFunc(obj, "userlogin", cuDUserInfo, out LogStruct logStruct);
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
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
        ctx.Response.ContentType = "application/json";
        // 设置请求类型
        HOutObjType obj = new() {method = "post", api = "/api/userlogout", para = cuDUserInfo};
        // 验证
        ResponseType result = RelatedFunc(obj, "userlogout", cuDUserInfo, out LogStruct logStruct);
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
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
        ResponseType result = RelatedFunc(obj, "getuserinfo", userid, out LogStruct logStruct);
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        return Results.Json(result);
    }

    /// <summary>
    /// 相关函数
    /// </summary>
    /// <param name="hOutObjType">HTTP内容对象类型</param>
    /// <param name="apitype">HTTP类型</param>
    /// <param name="para">附件参数</param>
    /// <param name="logStruct">LOG结构体</param>
    protected override ResponseType RelatedFunc(HOutObjType hOutObjType, string apitype, dynamic para,
        out LogStruct logStruct)
    {
        // 返回类型结果
        ResponseType result = new();

        // INFO 1: 日志初始化
        logStruct = new LogStruct();
        logStruct.Init(true);

        // INFO 2: 验证类型
        if (apitype == "getuserinfo")
        {
            // todo:
            string? userid = para as string;
            if (!string.IsNullOrEmpty(userid))
                //不为空
                result = UserInfoEvent.OnGetInfoEvent(hOutObjType, userid, ref logStruct);
        }
        else
        {
            // info: 验证
            StrongTypeVaildator strongTypeVaildator = new(typeof(CuDUserDetails), "Id");
            ValidationResult valid = strongTypeVaildator.Validate(para);
            // INFO 3: 验证结果
            result = valid.IsValid switch
            {
                // INFO 3.1 根据请求类型，执行不同的操作
                true => UserInfoEvent.OnBasicEvent(hOutObjType, para, ref logStruct), //数据处理并返回结果
                false => ReqResFunc.GetErrorResponseBody(UReqCode.ParaEmpty)
            };
        }

        // INFO 5: 返回结果
        return result;
    }
}