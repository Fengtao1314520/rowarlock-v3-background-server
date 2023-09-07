using Newtonsoft.Json.Linq;
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
    public JObject ResolveDatabaseJObject(JObject config)
    {
        JObject dbconfig = (config["database"] as JObject)!;
        return dbconfig;
    }
}