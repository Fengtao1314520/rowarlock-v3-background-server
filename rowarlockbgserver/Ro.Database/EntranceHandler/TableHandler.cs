using System.Text;
using Newtonsoft.Json.Linq;
using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Adapter;
using Ro.CrossPlatform.Extension;
using Ro.CrossPlatform.Logs;
using Ro.Database.Dependent;

namespace Ro.Database.EntranceHandler;

public abstract class TableHandler
{
    /// <summary>
    /// 检查数据表是否存在
    /// </summary>
    /// <param name="tablename">表名</param>
    /// <returns></returns>
    public static bool CheckTableExist(string tablename)
    {
        try
        {
            return Polymerization.TableUtil.ExistTable(ComArgs.SqliteConnection, tablename);
        }
        catch (Exception e)
        {
            return false;
        }
    }


    /// <summary>
    /// 根据表名，创建表和字段
    /// </summary>
    /// <param name="jobject">单个json对象</param>
    public static void CreateTableByName(JObject jobject)
    {
        JToken name = jobject["name"]!;
        JArray jArray = JArray.Parse(jobject["field"]!.ToString());
        StringBuilder sb = new();
        foreach (JToken field in jArray)
        {
            string fname = field["name"]!.ToString();
            string fvalue = field["type"]!.ToString();
            //追加
            sb.Append($"{fname} {fvalue},");
        }

        //移除最后一处标点符号
        string command = sb.ToString().Remove(sb.ToString().Length - 1, 1);
#if DEBUG
        // 输出
        LogCore.Log(command, UOutLevel.DEBUG);
#endif
        //执行创建table
        Polymerization.TableUtil.CreateTable(ComArgs.SqliteConnection, name.ToString(), command);
    }


    /// <summary>
    /// 更新表
    /// </summary>
    /// <param name="tabname"></param>
    /// <param name="jobject"></param>
    /// <param name="fieldkey"></param>
    public static void ReplaceTableByName(string tabname, JObject jobject, string fieldkey)
    {
        var keyValueTypeList = jobject.ToKeyValueTypeList();
        // keys
        string keys = keyValueTypeList.GetKeys();
        // values
        string values = keyValueTypeList.GetValues();
        // 转为sql语句
        string sqlstring = keyValueTypeList.GetKeyAndValueAsSqlString();

        KeyValueType? findkv = keyValueTypeList.Find(item => item.Key == fieldkey);

        if (findkv is null) return;

        // 查询字段对应数据是否存在
        int count = Polymerization.SelectUtil.SelectDataCount(ComArgs.SqliteConnection, tabname, findkv.Key,
            findkv.Value.ToString()!);

        //小于1 说明不存在
        if (count < 1)
            // 插入数据
            Polymerization.InsertUtil.InsertDataWithField(ComArgs.SqliteConnection, tabname, keys, values);
        else
            // 更新数据
            Polymerization.UpdateUtil.UpdateDataWithCondition(ComArgs.SqliteConnection, tabname, sqlstring,
                $"{fieldkey} = '{findkv.Value}'");
    }


    /// <summary>
    /// 执行文件的命令列
    /// </summary>
    /// <param name="fileInfo"></param>
    public static void ExecuteFileCommands(FileInfo fileInfo)
    {
        string filePath = fileInfo.FullName;
        using FileStream stream = new(filePath, FileMode.Open);
        using StreamReader reader = new(stream);
        string sql = reader.ReadToEnd();
        Polymerization.NudeExecuteUtil.NudeExecute(ComArgs.SqliteConnection, sql);
    }


    /// <summary>
    /// 获取数据库版本
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Dictionary<string, object>> GetDatabaseVersion()
    {
        return Polymerization.SelectUtil.SelectDataToDictList(ComArgs.SqliteConnection, "ro_version", "*",
            "name='database'");
    }

    /// <summary>
    /// 升级数据库版本
    /// </summary>
    /// <param name="version"></param>
    public static void UpdateDatabaseVersion(string version)
    {
        // 查询字段对应数据是否存在
        int count = Polymerization.SelectUtil.SelectDataCount(ComArgs.SqliteConnection, "ro_version",
            "name='database'");

        //小于1 说明不存在
        if (count < 1)
            // 插入数据
            Polymerization.InsertUtil.InsertDataWithField(ComArgs.SqliteConnection, "ro_version",
                "name,version,author,lastupdate",
                $"'database','{version}','system','{DateTime.Now:yyyy-MM-dd HH-mm-ss.fff}'");
        else
            // 更新数据
            Polymerization.UpdateUtil.UpdateDataWithCondition(ComArgs.SqliteConnection, "ro_version",
                "name='database',version='" + version + "'", "name='database'");
    }
}