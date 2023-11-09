using Carter;
using Carter.Request;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Logs;
using Ro.CrossPlatform.TemplateFunc;
using Ro.CrossPlatform.Vaildator;

namespace Ro.Http.Controller;

/// <summary>
/// 👇 Create a Carter module for the API
/// </summary>
public class Tasks : TCarterModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getusertasksimple", GetUserTaskSimple);
        app.MapGet("/getusertasklistbydays", GetUserTaskListByDays);
    }

    /// <summary>
    /// 按照日期获取用户的task列表
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private IResult GetUserTaskListByDays(HttpContext ctx)
    {
        ctx.Response.ContentType = "application/json";
        //检索首个userid
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        string? days = allQuery.AsMultiple<string>("days").FirstOrDefault();
        // 验证
        dynamic paradata = new {userid = userid, days = days};
        // 设置请求类型
        HOutObjType obj = new() {method = "get", api = "/api/getusertasklistbydays", para = paradata};

        ResponseType result = RelatedFunc(obj, "getusertasklistbydays", paradata, out LogStruct logStruct);
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        return Results.Json(result);
    }

    /// <summary>
    /// 获取用户的任务简略信息
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private IResult GetUserTaskSimple(HttpContext ctx)
    {
        ctx.Response.ContentType = "application/json";
        //检索首个userid
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        string? days = allQuery.AsMultiple<string>("days").FirstOrDefault();
        // 验证
        dynamic paradata = new {userid = userid, days = days};
        // 设置请求类型
        HOutObjType obj = new() {method = "get", api = "/api/getusertasksimple", para = paradata};
        // 执行操作
        ResponseType result = RelatedFunc(obj, "getusertasksimple", paradata, out LogStruct logStruct);
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        return Results.Json(result);
    }


    protected override ResponseType RelatedFunc(HOutObjType hOutObjType, string apitype, dynamic para,
        out LogStruct logStruct)
    {
        // INFO 0: 返回类型结果
        ResponseType result = new();

        // INFO 1: 日志初始化
        logStruct = new LogStruct();
        logStruct.Init(true);
        // INFO 2: 验证
        DynamicTypeVaildator dynamicTypeVaildator = new();
        ValidationResult valid = dynamicTypeVaildator.Validate(para);
        switch (apitype)
        {
            case "getusertasksimple":
                // INFO 3: 处理
                if (valid.IsValid)
                    result = TasksEvent.OnSimpleTasksByUserInfo(hOutObjType, para, ref logStruct);
                break;
            case "getusertasklistbydays":
                // INFO 3: 处理
                if (valid.IsValid)
                    result = TasksEvent.OnListTasksBaseDayByUserInfo(hOutObjType, para, ref logStruct);
                break;
        }

        // INFO 5: 返回结果
        return result;
    }
}