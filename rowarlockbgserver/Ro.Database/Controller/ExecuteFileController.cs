using Microsoft.Data.Sqlite;
using Ro.Database.Dependent;

namespace Ro.Database.Controller;

/// <summary>
///  执行文件控制器
/// </summary>
public class ExecuteFileController
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
    public ExecuteFileController(SqliteConnection sqliteConnection)
    {
        //赋值
        _sqliteConnection = sqliteConnection;
    }


    /// <summary>
    /// 执行文件的命令列
    /// </summary>
    /// <param name="fileInfo"></param>
    public void ExecuteFileCommands(FileInfo fileInfo)
    {
        string name = fileInfo.Name;

        StreamReader read = fileInfo.OpenText();
        // 从文件读取并显示行，直到文件的末尾
        while (read.ReadLine() is { } line)
            if (!string.IsNullOrEmpty(line))
                Polymerization.NudeExecuteUtil.NudeExecute(_sqliteConnection, line);
        read.Close();
        read.Dispose();
    }
}