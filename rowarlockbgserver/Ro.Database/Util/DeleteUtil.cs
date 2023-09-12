using Microsoft.Data.Sqlite;

namespace Ro.Database.Util;

public class DeleteUtil
{
    /// <summary>
    /// 删除表内所有数据
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="tabname">表名</param>
    public void DeleteAll(SqliteConnection connection, string tabname)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"DELETE FROM {tabname}"
        };
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
    public bool DeleteData(SqliteConnection connection, string tabname, string condition)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"DELETE FROM {tabname} WHERE {condition}"
        };
        int count = cmd.ExecuteNonQuery();

        return count != 0;
    }
}