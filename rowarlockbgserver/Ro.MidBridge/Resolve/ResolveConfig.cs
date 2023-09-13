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
}