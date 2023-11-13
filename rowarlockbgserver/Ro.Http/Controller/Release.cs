using Carter;
using Carter.Request;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Logs;
using Ro.CrossPlatform.TemplateFunc;
using Ro.CrossPlatform.Vaildator;

namespace Ro.Http.Controller;

public class Release : TCarterModule, ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getreleaselistbyyear", GetReleaseListByYear);
        app.MapPost("/updaterelease", UpdataRelease);
    }

    private IResult UpdataRelease(HttpContext ctx, CuDRelease cuDRelease)
    {
        ctx.Response.ContentType = "application/json";
        // 设置请求类型
        HOutObjType obj = new() {method = "post", api = "/api/updaterelease", para = cuDRelease};
        // 验证
        ResponseType result = RelatedFunc(obj, "updaterelease", cuDRelease, out LogStruct logStruct);
        // INFO 4: 日志输出
        ExtraLog.GenerateSystemFormatLog(result, ref logStruct); //结果输出
        OutLogStruct.Out(logStruct);
        return Results.Json(result);
    }

    /// <summary>
    /// 按年份获取发布列表
    /// 基于用户userid
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private IResult GetReleaseListByYear(HttpContext ctx)
    {
        ctx.Response.ContentType = "application/json";
        //检索首个userid
        IQueryCollection allQuery = ctx.Request.Query;
        string? userid = allQuery.AsMultiple<string>("userid").FirstOrDefault();
        // 验证
        dynamic paradata = new {userid = userid};
        // 设置请求类型
        HOutObjType obj = new() {method = "get", api = "/api/getreleaselistbyyear", para = paradata};

        ResponseType result = RelatedFunc(obj, "getreleaselistbyyear", paradata, out LogStruct logStruct);
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
            case "getreleaselistbyyear":
            {
                // INFO 3: 处理
                if (valid.IsValid)
                    result = ReleaseEvent.OnListReleaseBaseYearByUserInfo(hOutObjType, para, ref logStruct);
                break;
            }
            case "updaterelease":
            {
                // INFO 3: 处理
                if (valid.IsValid)
                    result = ReleaseEvent.OnUpdataRelease(hOutObjType, para, ref logStruct);
                break;
            }
        }

        return result;
    }
}