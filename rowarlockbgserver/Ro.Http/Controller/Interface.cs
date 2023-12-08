using System.Text.Json;
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

public class Interface : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiUrl.INTERFACE, GetInterface);
        app.MapPost(ApiUrl.INTERFACE, CreateInterface);
        app.MapPut(ApiUrl.INTERFACE, UpdateInterface);
    }

    private IResult UpdateInterface(HttpContext ctx, CuDInterface cuDInterface)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.PUT,
            Api = ApiUrl.INTERFACE,
            Para = cuDInterface
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDInterface, typeof(CuDInterface), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => InterfaceEvent.OnUpdateInterfaceEvent(hOutObjType, cuDInterface,
                ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        EmojiLog.GenerateFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result, new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        });
    }

    private IResult CreateInterface(HttpContext ctx, CuDInterface cuDInterface)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.POST,
            Api = ApiUrl.INTERFACE,
            Para = cuDInterface
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDInterface, typeof(CuDInterface), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => InterfaceEvent.OnCreateInterfaceEvent(hOutObjType, cuDInterface,
                ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        EmojiLog.GenerateFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result, new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        });
    }

    private IResult GetInterface(HttpContext ctx)
    {
        // 接受params
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        string condition = allQuery["condition"].ToString();
        CuDInterface cuDInterface = new()
        {
            CreateUserId = userid
        };

        // INFO 0: 日志初始化
        LogStruct logStruct = new(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";

        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.GET,
            Api = ApiUrl.INTERFACE,
            Para = condition
        };
        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDInterface, typeof(CuDInterface), "CreateUserId");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => InterfaceEvent.OnGetInterfaceEvent(hOutObjType, cuDInterface,
                ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        EmojiLog.GenerateFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result, new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        });
    }
}