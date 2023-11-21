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

public class InterfacePackage : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiUrl.INTERFACEPACKAGE, SplitGetParams);
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
        CuDInterfacePackage cuDInterfacePackage = new()
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
            Api = ApiUrl.INTERFACEPACKAGE,
            Para = condition
        };

        // INFO 3: 验证
        bool valid = QuoteVaildator.IsQuote(cuDInterfacePackage, typeof(CuDTask), "AssigneeUserId");

        // INFO 3.1: 验证结果并执行
        ResponseType? result = null;
        if (valid)
        {
            // INFO 3.2 执行不同的操作
            dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
            string? qtype = dycondition["qtype"].ToString();
            result = qtype switch
            {
                "simple" => InterfacePackageEvent.OnGetInterfacePackageSimpleByUserIdEvent(hOutObjType,
                    cuDInterfacePackage,
                    ref logStruct),
                "list" => InterfacePackageEvent.OnGetAllInterfacePackageListByUserIdEvent(hOutObjType,
                    cuDInterfacePackage,
                    ref logStruct),
                "detail" => InterfacePackageEvent.OnGetInterfacePackageDetailByIdEvent(hOutObjType,
                    cuDInterfacePackage,
                    ref logStruct),
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