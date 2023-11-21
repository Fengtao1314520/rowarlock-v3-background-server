using System.Text;
using Ro.Basic.UType.Adapter;

namespace Ro.CrossPlatform.Extension;

public static class KeyValueTypeListExt
{
    /// <summary>
    /// 获取键
    /// 转为字符串，符合sql语法
    /// </summary>
    /// <param name="keyValueTypeList"></param>
    /// <returns></returns>
    public static string GetKeys(this List<KeyValueType> keyValueTypeList)
    {
        StringBuilder sb = new();
        foreach (KeyValueType item in keyValueTypeList)
            sb.Append($"{item.Key},");

        //移除最后一处标点符号
        return sb.ToString().Remove(sb.ToString().Length - 1, 1);
    }

    /// <summary>
    /// 获取值
    /// 转为字符串，符合sql语法
    /// </summary>
    /// <param name="keyValueTypeList"></param>
    /// <returns></returns>
    public static string GetValues(this List<KeyValueType> keyValueTypeList)
    {
        StringBuilder sb = new();
        foreach (KeyValueType item in keyValueTypeList)
            sb.Append($"'{item.Value}',");

        //移除最后一处标点符号
        return sb.ToString().Remove(sb.ToString().Length - 1, 1);
    }

    /// <summary>
    /// 获取键值对
    /// 转为sql的键值对字符串
    /// </summary>
    /// <param name="keyValueTypeList"></param>
    /// <returns></returns>
    public static string GetKeyAndValueAsSqlString(this List<KeyValueType> keyValueTypeList)
    {
        StringBuilder sb = new();
        foreach (KeyValueType item in keyValueTypeList)
            sb.Append($"{item.Key}='{item.Value}',");

        //移除最后一处标点符号
        return sb.ToString().Remove(sb.ToString().Length - 1, 1);
    }
}