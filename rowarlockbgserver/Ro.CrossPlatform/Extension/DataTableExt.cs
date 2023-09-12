using System.Data;


namespace Ro.CrossPlatform.Extension;

public static class DataTableExt
{
    /// <summary>
    /// 转换为字典列表
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static List<Dictionary<string, object>> ToListDictionary(this DataTable dt)
    {
        // 返回值
        var result = new List<Dictionary<string, object>>();
        DataColumnCollection columns = dt.Columns;
        dt.AsEnumerable().ToList().ForEach(element =>
        {
            //获取item列表
            object?[] itemarray = element.ItemArray;
            // 单个item
            var dic = new Dictionary<string, object>();
            //循环item各个值
            for (int i = 0; i < itemarray.Length; i++)
                // 添加到字典, key:列名, value:值
                dic.Add(!string.IsNullOrEmpty(columns[i].ToString()) ? columns[i].ToString() : $"temp_{i}",
                    itemarray[i]!);
            // 添加
            result.Add(dic);
        });
        return result;
    }
}