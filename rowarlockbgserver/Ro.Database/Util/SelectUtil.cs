using System.Data;
using Microsoft.Data.Sqlite;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Extension;
using Ro.CrossPlatform.Logs;

namespace Ro.Database.Util;

internal class SelectUtil
{
    /// <summary>
    /// 带条件查询数据
    /// 返回数据
    /// INFO: 基础
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="columndata">返回的表字段名</param>
    /// <param name="condition">查询条件</param>
    /// <returns>返回数据</returns>
    internal DataTable SelectDataWithCondition(SqliteConnection connection, string tablename, string columndata,
        string condition)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"SELECT {columndata} FROM {tablename} WHERE {condition}"
        };

#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        // 执行查询
        SqliteDataReader reader = cmd.ExecuteReader();
        DataTable dataTable = new();
        dataTable.Load(reader);
        reader.Close();
        reader.Dispose();
        // 返回
        return dataTable;
    }


    /// <summary>
    /// 查询数据
    /// 返回数据是List
    /// </summary>
    /// <param name="connection">连接</param>
    /// <param name="tablename">表名</param>
    /// <returns>字典列表,一行为一个字典</returns>
    internal List<Dictionary<string, object>> SelectDataAllToListDict(SqliteConnection connection, string tablename)
    {
        var result = SelectDataToDictList(connection, tablename, "*");
        return result;
    }

    /// <summary>
    /// 查询数据
    /// 返回数据
    /// 返回LIST形式的字典集合
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="columndata">返回的表字段名</param>
    /// <returns>返回数据</returns>
    internal List<Dictionary<string, object>> SelectDataToDictList(SqliteConnection connection, string tablename,
        string columndata)
    {
        SqliteCommand cmd = new() {Connection = connection, CommandText = $"SELECT {columndata} FROM {tablename}"};

#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        SqliteDataReader reader = cmd.ExecuteReader();
        DataTable dataTable = new();
        dataTable.Load(reader);
        var result = dataTable.ToListDictionary();

        reader.Close();
        reader.Dispose();
        return result;
    }


    /// <summary>
    /// 查询数据
    /// 返回数据
    /// 返回LIST形式的字典集合
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="columndata">返回的表字段名</param>
    /// <param name="condition">查询条件</param>
    /// <returns>返回数据</returns>
    internal List<Dictionary<string, object>> SelectDataToDictList(SqliteConnection connection, string tablename,
        string columndata, string condition)
    {
        DataTable dataTable = SelectDataWithCondition(connection, tablename, columndata, condition);
        var result = dataTable.ToListDictionary();
        // 返回值
        return result;
    }


    /// <summary>
    /// 查询数据
    /// 返回数量
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="field">查询字段名</param>
    /// <param name="data">查询值</param>
    /// <returns></returns>
    internal int SelectDataCount(SqliteConnection connection, string tablename, string field, string data)
    {
        return SelectDataCount(connection, tablename, $"{field} = '{data}'");
    }

    /// <summary>
    /// 查询数据
    /// 返回数量
    /// 具有多重查询条件时可以使用
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="tablename">表名</param>
    /// <param name="conditiondata">查询条件集合</param>
    /// <returns></returns>
    internal int SelectDataCount(SqliteConnection connection, string tablename, string conditiondata)
    {
        int result = 0;
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = $"SELECT COUNT(*) AS 'totalcount' FROM {tablename} WHERE {conditiondata}"
        };

#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif

        SqliteDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            object totacount = reader["totalcount"];
            result = int.Parse(totacount.ToString() ?? string.Empty);
        }

        // 释放资源
        reader.Close();
        reader.Dispose();
        return result; //如果没有值或查询不到，直接就是0
    }
}