using Microsoft.Data.Sqlite;

namespace Ro.Database.Util;

public class UpdateUtil
{
    /// <summary>
    /// 按照条件进行更新数据
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="setdata">更新数据</param>
    /// <param name="conditiondata">筛选条件</param>
    /// <returns></returns>
    internal int UpdateDataWithCondition(SqliteConnection connection, string tablename, string setdata,
        string conditiondata)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"UPDATE {tablename} SET {setdata} WHERE {conditiondata}"
        };
        int result = cmd.ExecuteNonQuery();

        return result;
    }


    /// <summary>
    /// 全表更新，更新数据
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="setdata">更新数据</param>
    /// <returns></returns>
    internal int UpdateData(SqliteConnection connection, string tablename, string setdata)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"UPDATE {tablename} SET {setdata}"
        };
        int result = cmd.ExecuteNonQuery();

        return result;
    }
}