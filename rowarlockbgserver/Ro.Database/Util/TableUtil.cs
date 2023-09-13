using Microsoft.Data.Sqlite;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;

namespace Ro.Database.Util;

/// <summary>
/// TABLE
/// 工具
/// 底层执行
/// </summary>
internal class TableUtil
{
    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="connection">sqlite连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="data">数据</param>
    internal void CreateTable(SqliteConnection connection, string tablename, string data)
    {
        SqliteCommand cmd = new() {Connection = connection, CommandText = $"CREATE TABLE {tablename} ({data})"};
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        cmd.ExecuteNonQuery();
    }


    /// <summary>
    /// 检查表是否存在
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="tablename"></param>
    /// <returns></returns>
    internal bool ExistTable(SqliteConnection connection, string tablename)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"SELECT COUNT(*) FROM sqlite_master where type='table' and name='{tablename}';"
        };
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        return 0 != Convert.ToInt32(cmd.ExecuteScalar());
    }

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="tablename"></param>
    internal void DeleteTable(SqliteConnection connection, string tablename)
    {
        SqliteCommand cmd = new() {Connection = connection, CommandText = $"DROP TABLE {tablename}"};
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        cmd.ExecuteNonQuery();
    }
}