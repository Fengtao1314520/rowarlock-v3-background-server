using Newtonsoft.Json.Linq;
using Ro.Basic.UType;
using Ro.CrossPlatform.Func;

namespace Ro.MidBridge.Resolve;

/// <summary>
///  解析配置文件
/// </summary>
public class ResolveConfig
{
    /// <summary>
    /// 解析配置文件到JObject
    /// <param name="configpath">文件路径</param>
    /// </summary>
    public JObject ResolveConfigToJObject(string configpath)
    {
        JObject config = JsonFunc.ReturnJsonObjectByFile<JObject>(configpath);
        return config;
    }


    /// <summary>
    /// 解析数据库配置
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    public DataBaseInfoType ResolveDataBaseInfoType(JObject config)
    {
        // 解析数据库配置
        JObject dbconfig = config["database"]!.ToObject<JObject>()!;
        // 数据库信息类型 属性
        DataBaseInfoType dataBaseInfoType = new()
        {
            Path = dbconfig["dbpath"]!.ToString(),
            UpdatePath = dbconfig["db_update"]!.ToString(),
            TablePath = dbconfig["db_table"]!.ToString()
        };
        return dataBaseInfoType;
    }

    /// <summary>
    /// 解析config节点
    /// 获取HttpServerType
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    public HttpServerType ResolveHttpServerType(JObject config)
    {
        // 解析http配置
        JObject httpconfig = config["httpserver"]!.ToObject<JObject>()!;
        // http信息类型 属性
        HttpServerType httpServerType = new()
        {
            Address = httpconfig["web_server_address"]!.ToString(),
            Port = httpconfig["web_server_port"]!.ToObject<int>()!
        };
        return httpServerType;
    }
}