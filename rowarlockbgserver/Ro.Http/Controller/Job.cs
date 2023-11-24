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

public class Job : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiUrl.JOB, GetJob);
        app.MapPost(ApiUrl.JOB, CreateJob);
        app.MapPut(ApiUrl.JOB, UpdateJob);
    }

    private IResult UpdateJob(HttpContext ctx, CuDJob cuDJob)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.PUT,
            Api = ApiUrl.JOB,
            Para = cuDJob
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDJob, typeof(CuDJob), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => JobEvent.OnUpdateJobEvent(hOutObjType, cuDJob,
                ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        EmojiLog.GenerateFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }

    private IResult CreateJob(HttpContext ctx, CuDJob cuDJob)
    {
        // INFO 0: 日志初始化
        LogStruct logStruct = new(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.POST,
            Api = ApiUrl.JOB,
            Para = cuDJob
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDJob, typeof(CuDJob), "Id");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => JobEvent.OnCreateJobEvent(hOutObjType, cuDJob,
                ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        EmojiLog.GenerateFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }

    private IResult GetJob(HttpContext ctx)
    {
        // 接受params
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        string condition = allQuery["condition"].ToString();
        CuDJob cuDJob = new()
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
            Api = ApiUrl.JOB,
            Para = condition
        };
        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDJob, typeof(CuDJob), "CreateUserId");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => JobEvent.OnGetJobEvent(hOutObjType, cuDJob,
                ref logStruct), //数据处理并返回结果
            false => QuoteVaildator.NoneValidResponse
        };
        // INFO 4: 日志输出
        EmojiLog.GenerateFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        // INFO 5: 返回结果
        return Results.Json(result);
    }
}