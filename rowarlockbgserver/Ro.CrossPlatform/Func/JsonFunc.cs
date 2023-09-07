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
}