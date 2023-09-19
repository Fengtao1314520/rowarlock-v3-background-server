using Microsoft.Data.Sqlite;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;

namespace Ro.Database.Util;

/// <summary>
/// 裸执行工具
/// </summary>
internal class NudeExecuteUtil
{
    /// <summary>
    /// 裸执行command
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="command"></param>
    internal void NudeExecute(SqliteConnection connection, string command)
    {
        SqliteCommand cmd = new()
        {
            Connection = connection,
            CommandText = command
        };
#if DEBUG
        LogCore.Log(cmd.CommandText, UOutLevel.DEBUG);
#endif
        cmd.ExecuteNonQuery();
    }
}