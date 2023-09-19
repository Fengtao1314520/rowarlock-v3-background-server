using System.Text;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using Ro.Basic.UEnum;
using Ro.Basic.UType;
using Ro.CrossPlatform.Extension;
using Ro.CrossPlatform.Logs;
using Ro.Database.Dependent;


namespace Ro.Database.Controller;

public class TableController
{
    /// <summary>
    /// 数据库连接
    /// 赋值
    /// </summary>
    private readonly SqliteConnection _sqliteConnection;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sqliteConnection"></param>
    public TableController(SqliteConnection sqliteConnection)
    {
        //赋值
        _sqliteConnection = sqliteConnection;
    }


    /// <summary>
    /// 检查数据表是否存在
    /// </summary>
    /// <param name="tablename">表名</param>
    /// <returns></returns>
    public bool CheckTableExist(string tablename)
    {
        try
        {
            return Polymerization.TableUtil.ExistTable(_sqliteConnection, tablename);
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
    public void CreateTableByName(JObject jobject)
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
        Polymerization.TableUtil.CreateTable(_sqliteConnection, name.ToString(), command);
    }


    /// <summary>
    /// 更新表
    /// </summary>
    /// <param name="tabname"></param>
    /// <param name="jobject"></param>
    /// <param name="fieldkey"></param>
    public void ReplaceTableByName(string tabname, JObject jobject, string fieldkey)
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
        int count = Polymerization.SelectUtil.SelectDataCount(_sqliteConnection, tabname, findkv.Key,
            findkv.Value.ToString()!);

        //小于1 说明不存在
        if (count < 1)
            // 插入数据
            Polymerization.InsertUtil.InsertDataWithField(_sqliteConnection, tabname, keys, values);
        else
            // 更新数据
            Polymerization.UpdateUtil.UpdateDataWithCondition(_sqliteConnection, tabname, sqlstring,
                $"{fieldkey} = '{findkv.Value}'");
    }
}