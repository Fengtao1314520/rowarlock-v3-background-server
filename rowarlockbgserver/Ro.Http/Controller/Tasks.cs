using System.Net;
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
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.CrossPlatform.Vaildator;

namespace Ro.Http.Controller;

/// <summary>
/// 👇 Create a Carter module for the API
/// </summary>
public class Tasks : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiUrl.STATISTICS, GetStatistics);
        app.MapGet(ApiUrl.TASK, SplitGetParams);
    }

    /// <summary>
    /// 获取用户的任务简略信息
    /// 基于用户的总数
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private IResult GetStatistics(HttpContext ctx)
    {
        //检索首个userid
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        CuDStatistics cuDStatistics = new()
        {
            UserId = userid
        };

        // INFO 0: 日志初始化
        LogStruct logStruct = new(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";
        ctx.Response.Headers.Origin = "*";

        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.GET,
            Api = ApiUrl.STATISTICS,
            Para = cuDStatistics
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDStatistics, typeof(CuDStatistics), "UserId");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = valid switch
        {
            // INFO 3.2 执行不同的操作
            true => StatisticsEvent.OnDiffEvent(hOutObjType, cuDStatistics, ref logStruct, hOutObjType.Api,
                hOutObjType.Method), //数据处理并返回结果
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
        CuDTask cudTask = new()
        {
            AssigneeUserId = userid
        };

        // INFO 0: 日志初始化
        LogStruct logStruct = new(true);

        // INFO 1: 设置返回类型
        ctx.Response.ContentType = "application/json";

        // info 需要解码encodeURI
        string decodedString = WebUtility.UrlDecode(condition);
        // INFO 2: 拼装请求对象
        HOutObjType hOutObjType = new()
        {
            Method = ApiMethod.GET,
            Api = ApiUrl.STATISTICS,
            Para = decodedString
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cudTask, typeof(CuDTask), "AssigneeUserId");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = null;
        if (valid)
        {
            // INFO 3.2 执行不同的操作
            dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(decodedString);
            string? qtype = dycondition["qtype"].ToString();
            result = qtype switch
            {
                "list" => TasksEvent.OnGetTaskListByUserIdEvent(hOutObjType, cudTask, ref logStruct),
                "detail" => TasksEvent.OnGetTaskDetailByIdEvent(hOutObjType, cudTask, ref logStruct),
                _ => result
            };
        }
        else
        {
            result = QuoteVaildator.NoneValidResponse;
        }

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