using Carter;
using Carter.Request;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ro.Basic.UEnum.APIUrl;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.CrossPlatform.Vaildator;

namespace Ro.Http.Controller;

public class Release : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiUrl.RELEASE, SplitGetParams);
        app.MapPut(ApiUrl.RELEASE, UpdateRelease);
        app.MapPost(ApiUrl.RELEASE, CreateRelease);
    }

    private IResult CreateRelease(HttpContext ctx, CuDRelease cuDRelease)
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
            Api = ApiUrl.RELEASE,
            Para = cuDRelease
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDRelease, typeof(CuDRelease), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => ReleaseEvent.OnCreateReleaseEvent(hOutObjType, cuDRelease, ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }


    private IResult UpdateRelease(HttpContext ctx, CuDRelease cuDRelease)
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
            Api = ApiUrl.RELEASE,
            Para = cuDRelease
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDRelease, typeof(CuDRelease), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => ReleaseEvent.OnUpdateReleaseEvent(hOutObjType, cuDRelease, ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }


    /// <summary>
    /// 基于RESTFUL API的模式,当一个GET存在时，需要基于Params进行分拆
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private IResult SplitGetParams(HttpContext ctx)
    {
        // 接受params
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        string condition = allQuery["condition"].ToString();
        CuDRelease cuDRelease = new()
        {
            AssigneeUserId = userid
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
            Api = ApiUrl.STATISTICS,
            Para = condition
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDRelease, typeof(CuDRelease), "AssigneeUserId");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = null;
        if (valid)
        {
            // INFO 3.2 执行不同的操作
            dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
            string? qtype = dycondition["qtype"].ToString();
            result = qtype switch
            {
                "year" => ReleaseEvent.OnGetReleaseYearListByUserIdEvent(hOutObjType, cuDRelease, ref logStruct),
                "list" => ReleaseEvent.OnGetReleaseListByUserIdEvent(hOutObjType, cuDRelease, ref logStruct),
                "detail" => ReleaseEvent.OnGetReleaseDetailByIdEvent(hOutObjType, cuDRelease, ref logStruct),
                _ => result
            };
        }
        else
        {
            result = QuoteVaildator.NoneValidResponse;
        }

        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }
}