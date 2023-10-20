using Newtonsoft.Json;

namespace Ro.CrossPlatform.Func;

public abstract class JsonFunc
{
    /// <summary>
    /// 从文件内返回JSON对象
    /// </summary>
    /// <typeparam name="T">泛型,用户带入对象</typeparam>
    /// <param name="filepath">文件路径</param>
    /// <returns>JSON对象</returns>
    public static T ReturnJsonObjectByFile<T>(string filepath)
    {
        // 读取文件
        string filestring = File.ReadAllText(filepath);
        // 反序列化
        T jsonobject = JsonConvert.DeserializeObject<T>(filestring)!;
        return jsonobject;
    }

    /// <summary>
    /// string转JSON对象
    /// </summary>
    /// <param name="jsonstring"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T DeserialzeJsonObject<T>(string jsonstring)
    {
        // 反序列化
        T jsonobject = JsonConvert.DeserializeObject<T>(jsonstring)!;
        return jsonobject;
    }

    /// <summary>
    /// JSON对象转string
    /// </summary>
    /// <param name="jsonobject"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string SerializeJsonObject<T>(T jsonobject)
    {
        // 序列化
        string jsonstring = JsonConvert.SerializeObject(jsonobject);
        return jsonstring;
    }
}