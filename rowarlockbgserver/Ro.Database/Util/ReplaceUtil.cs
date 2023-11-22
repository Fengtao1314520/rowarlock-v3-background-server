using Microsoft.Data.Sqlite;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;

namespace Ro.Database.Util;

internal class ReplaceUtil
{
    /// <summary>
    /// 替换数据
    /// </summary>
    /// <param name="connection">连接</param>
    /// <param name="tablename">表明</param>
    /// <param name="fieldname">字段</param>
    /// <param name="data">值</param>
    internal void ReplaceData(SqliteConnection connection, string tablename, string fieldname, string data)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"INSERT OR REPLACE INTO {tablename} ({fieldname}) VALUES ({data})"
        };

#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.Debug);
#endif
        cmd.ExecuteNonQuery();
    }
}