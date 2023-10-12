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

    private IResult UpdateUserInfo(HttpContext ctx, UserInfo userInfo)
    {
#if DEBUG
        Console.WriteLine("进入UpdateUserInfo");
        Console.WriteLine("userid: " + userInfo.UserId);
#endif

        ctx.Response.ContentType = "application/json";

        ValidationResult? result = ctx.Request.Validate(userInfo);
        if (result.IsValid)
        {
            var notempty = FBConnectionFun.GetResponseBody(UReqCode.Success, new UserInfo()
            {
                UserId = userInfo.UserId,
                UserName = "测试账号",
                UserPassword = "123456"
            });

            return Results.Json(notempty);
        }

        var empty = FBConnectionFun.GetResponseBody(UReqCode.ParaEmpty, new UserInfo()
        {
            UserId = userInfo.UserId,
            UserName = "",
            UserPassword = string.Empty
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
        string? queryid = ctx.Request.Query.AsMultiple<string>("userid").FirstOrDefault();


        // 不为空
        if (!string.IsNullOrEmpty(queryid))
            return Results.Json(new
            {
                errorcode = 200,
                errormsg = "ok",
                data = new
                {
                    userid = queryid,
                    username = "test",
                    userage = 18
                }
            });

        // 为空ß
        return Results.Json(new
        {
            errorcode = 1001,
            errormsg = "data is null"
        });
    }
}