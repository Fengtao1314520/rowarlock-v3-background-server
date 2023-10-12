using Carter;
using Microsoft.AspNetCore.Builder;
using Ro.Basic.UType;


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
        //初始化
        _httpServerType = httpServerType;

        // 取消标识
        _cts = new CancellationTokenSource();

        // 👇 Create the WebApplicationBuilder
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        // 👇 Add the required Carter services
        builder.Services.AddCarter();
        // 👇 Create the WebApplication instance
        _webApplication = builder.Build();
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
        throw new NotImplementedException();
    }

    #endregion


    #region 内部方法

    #endregion
}