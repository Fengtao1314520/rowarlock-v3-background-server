using System.Text;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;
using Ro.Database.Util;

namespace Ro.Database.Controller;

public class TableController
{
    /// <summary>
    /// 数据库连接
    /// 赋值
    /// </summary>
    private readonly SqliteConnection _sqliteConnection;

    /// <summary>
    /// 工具类
    /// </summary>
    private readonly TableUtil _tableUtil;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sqliteConnection"></param>
    public TableController(SqliteConnection sqliteConnection)
    {
        //赋值
        _tableUtil = new TableUtil();
        _sqliteConnection = sqliteConnection;
    }


    /// <summary>
    /// 检查数据表是否存在
    /// </summary>
    /// <param name="tablename">表名</param>
    /// <returns></returns>
    public bool CheckTableExist(string tablename)
    {
        return _tableUtil.TableExist(_sqliteConnection, tablename);
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
        _tableUtil.CreateTable(_sqliteConnection, name.ToString(), command);
    }
}