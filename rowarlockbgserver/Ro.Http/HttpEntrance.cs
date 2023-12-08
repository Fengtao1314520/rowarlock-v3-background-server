using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ro.Basic.UType.ConfigInfoType;


namespace Ro.Http;

/// <summary>
/// http入口类
/// </summary>
public class HttpEntrance : IDisposable
{
    /// <summary>
    /// http服务信息类型
    /// </summary>
    private readonly HttpServerType _httpServerType;

    /// <summary>
    /// 取消标识
    /// </summary>
    private readonly CancellationTokenSource _cts;

    /// <summary>
    /// http服务
    /// </summary>
    private readonly WebApplication _webApplication;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HttpEntrance(HttpServerType httpServerType)
    {
        string myAllowSpecificOrigins = "allany";
        //初始化
        _httpServerType = httpServerType;

        // 取消标识
        _cts = new CancellationTokenSource();

        // 👇 Create the WebApplicationBuilder
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        // 👇 Add the required Carter services
        builder.Services.AddCarter();
        // 👇 Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(myAllowSpecificOrigins, policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });
        // 👇 change Json
        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        // 👇 Close console log
        builder.Logging.ClearProviders();
        // 👇 Create the WebApplication instance
        _webApplication = builder.Build();
        // 👇 Set the CORS policy
        _webApplication.UseCors(myAllowSpecificOrigins);
        // 👇 Set the path base to /api
        _webApplication.UsePathBase("/api");
        // 👇 find all the Carter modules and register all the APIs
        _webApplication.MapCarter();
    }


    #region 对外方法

    public HttpEntrance Start()
    {
        string url = "http://" + _httpServerType.Address + ":" + _httpServerType.Port;
        // 👇 Run the application
        _webApplication.Urls.Add(url);
        _webApplication.StartAsync().Wait();

        return this;
    }

    public HttpEntrance Stop()
    {
        _webApplication.StopAsync().Wait();
        return this;
    }

    public void Dispose()
    {
        // throw new NotImplementedException();
    }

    #endregion
}