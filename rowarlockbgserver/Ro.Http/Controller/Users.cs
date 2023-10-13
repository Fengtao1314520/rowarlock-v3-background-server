using Carter;
using Carter.ModelBinding;
using Carter.Request;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ro.Basic.UEnum;
using Ro.Basic.UType.FBConnection;
using Ro.CrossPlatform.Func;

namespace Ro.Http.Controller;

/// <summary>
/// 👇 Create a Carter module for the API
/// </summary>
public class Users : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/userinfo", GetUserInfo);
        app.MapPost("/upuserinfo", UpdateUserInfo);
    }


    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private IResult UpdateUserInfo(HttpContext ctx, UserInfo userInfo)
    {
#if DEBUG
        Console.WriteLine("进入UpdateUserInfo");
        Console.WriteLine("userid: " + userInfo.userid);
#endif

        ctx.Response.ContentType = "application/json";

        // 验证
        ValidationResult? result = ctx.Request.Validate(userInfo);

        if (result.IsValid)
        {
            var notempty = FBConnectionFun.GetResponseBody(UReqCode.Success, new UserInfo()
            {
                userid = userInfo.userid,
                username = "测试账号",
                datetime = GatherFunc.NowDateTime()
            });

            return Results.Json(notempty);
        }

        var empty = FBConnectionFun.GetResponseBody(UReqCode.ParaEmpty, new UserInfo()
        {
            userid = userInfo.userid,
            datetime = GatherFunc.NowDateTime()
        });
        return Results.Json(empty);
    }


    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private IResult GetUserInfo(HttpContext ctx)
    {
#if DEBUG
        Console.WriteLine("进入GetUserInfo");
#endif
        ctx.Response.ContentType = "application/json";

        //检索首个userid
        string? userid = ctx.Request.Query.AsMultiple<string>("userid").FirstOrDefault();

        // 不为空
        if (!string.IsNullOrEmpty(userid))
        {
            var notempty = FBConnectionFun.GetResponseBody(UReqCode.Success, new UserInfo()
            {
                userid = userid,
                username = "测试账号",
                datetime = GatherFunc.NowDateTime()
            });

            return Results.Json(notempty);
        }

        // 为空
        var empty = FBConnectionFun.GetResponseBody(UReqCode.QueryEmpty, new UserInfo()
        {
            userid = userid,
            datetime = GatherFunc.NowDateTime()
        });
        return Results.Json(empty);
    }
}