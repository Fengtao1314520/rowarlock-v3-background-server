using Microsoft.Data.Sqlite;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;

namespace Ro.Database.Util;

public class InsertUtil
{
    /// <summary>
    /// 插入数据
    /// 不包含标头
    /// </summary>
    /// <param name="connection">sqlite连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="data">数据</param>
    internal void InsertData(SqliteConnection connection, string tablename, string data)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"INSERT INTO {tablename} VALUES ({data})"
        };
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// 插入数据
    /// 包含表头名
    /// </summary>
    /// <param name="connection">sqlite连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="fieldname">标头名</param>
    /// <param name="data">数据</param>
    internal int InsertDataWithField(SqliteConnection connection, string tablename, string fieldname,
        string data)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"INSERT INTO {tablename} ({fieldname}) VALUES ({data})"
        };
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        int count = cmd.ExecuteNonQuery();

        return count;
    }
}