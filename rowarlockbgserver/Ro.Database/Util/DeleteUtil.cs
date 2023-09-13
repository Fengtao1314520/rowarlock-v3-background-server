using Microsoft.Data.Sqlite;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;

namespace Ro.Database.Util;

internal class DeleteUtil
{
    /// <summary>
    /// 删除表内所有数据
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="tabname">表名</param>
    internal void DeleteAll(SqliteConnection connection, string tabname)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"DELETE FROM {tabname}"
        };
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// 删除数据
    /// 符合的都是删除
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="tabname"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    internal bool DeleteData(SqliteConnection connection, string tabname, string condition)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"DELETE FROM {tabname} WHERE {condition}"
        };
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        int count = cmd.ExecuteNonQuery();

        return count != 0;
    }
}