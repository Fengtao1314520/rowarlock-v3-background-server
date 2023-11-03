using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using Ro.Basic;
using Ro.CrossPlatform.Logs;
using Ro.Basic.UEnum;
using Ro.Basic.UType.ConfigInfoType;
using Ro.CrossPlatform.Func;
using Ro.Database.EntranceHandler;

namespace Ro.Database;

/// <summary>
/// database入口类
/// </summary>
public class DatabaseEntrance : IDisposable
{
    /// <summary>
    /// 数据库信息
    /// </summary>
    private readonly DataBaseInfoType _dataBaseInfoType;


    /// <summary>
    /// 数据库状态
    /// null代表发生错误
    /// </summary>
    public bool? Status { get; private set; }

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public DatabaseEntrance(DataBaseInfoType dataBaseInfoType)
    {
        // 初始化, 强制赋值
        _dataBaseInfoType = dataBaseInfoType;
        Status = null;
    }

    #endregion


    #region 公有方法 数据库

    /// <summary>
    /// 连接数据库
    /// </summary>
    public DatabaseEntrance ConnectDb()
    {
        // 输出日志
        LogCore.Log("数据库连接中...", UOutLevel.INFO);
        string connectionString = $"Data Source = {_dataBaseInfoType.Path}";
        try
        {
            ComArgs.SqliteConnection = new SqliteConnection(connectionString);
            ComArgs.SqliteConnection.Open();
            LogCore.Log("数据库连接成功", UOutLevel.SUCCESS);
            Status = true;
        }
        catch (Exception e)
        {
            LogCore.Log($"数据库连接失败, {e.Message}", UOutLevel.ERROR);
            Status = false;
        }

        return this;
    }

    /// <summary>
    /// 断开数据库
    /// </summary>
    public DatabaseEntrance DisconnectDb()
    {
        ComArgs.SqliteConnection.Close();
        return this;
    }


    /// <summary>
    ///  释放数据库
    /// </summary>
    public void Dispose()
    {
        ComArgs.SqliteConnection.Dispose();
    }

    #endregion

    #region 公有方法

    /// <summary>
    /// 检查数据库文件
    /// </summary>
    public void CheckDatabaseFile()
    {
        string dbfilepath = _dataBaseInfoType.Path;
        // 如果文件不存在,则创建一个新的数据库文件
        if (File.Exists(dbfilepath))
        {
            LogCore.Success("系统已检测到数据库文件");
            Status = true;
        }
        else
        {
            // INFO: 创建数据库文件，(如果不存在）
            try
            {
                LogCore.Warn("系统未检测到数据库,将重新新建数据库文件");
                string dpath = Path.GetDirectoryName(dbfilepath)!;
                // 创建文件夹
                Directory.CreateDirectory(dpath);
                // 创建文件
                FileStream filestream = File.Create(dbfilepath);
                // UPDATE: 释放资源
                filestream.Close();
                filestream.Dispose();
                LogCore.Success("系统创建数据库成功");
                Status = null;
            }
            catch (Exception e)
            {
                LogCore.Exception($"系统重建数据库失败, {e.Message}");
                Status = false;
            }
        }
    }


    /// <summary>
    /// 初始化数据库
    /// info: 1. 检查数据库文件是否存在
    /// info: 2. 更新版本号
    /// </summary>
    /// <param name="version">版本号</param>
    /// <returns></returns>
    public void InitDatabase(JObject version)
    {
        try
        {
            string tablepath = _dataBaseInfoType.TablePath;
            LogCore.Info("系统数据库初始化,请稍等...");
            // 读取数据表JSON
            var tableJobjectArray = JsonFunc.ReturnJsonObjectByFile<List<JObject>>(tablepath);
            // 初始化数据库表
            InitDatabaseTable(tableJobjectArray);
            // 输出日志
            LogCore.Info($"数据表检测完毕,结果:{Status}");
            // 更新版本号
            UpdateDatabaseVersion(version);
            // 输出日志
            LogCore.Info($"数据库初始化完毕,结果:{Status}");
            Status = true;
        }
        catch (Exception e)
        {
            LogCore.Exception($"系统数据库初始化失败, {e.Message}");
            Status = false;
        }
    }


    /// <summary>
    /// 更新数据库
    /// 基于sql文件
    /// </summary>
    /// <param name="sqlfiles"></param>
    public void UpdateByFile(FileInfo[] sqlfiles)
    {
        LogCore.Info("系统数据库更新,请稍等...");
        try
        {
            // INFO 排序
            Array.Sort(sqlfiles, new FileNameComparerFunc());
            // INFO 执行
            sqlfiles.ToList().ForEach(file =>
            {
                string name = file.Name;
                LogCore.Log($"系统正在读取{name}的内容,即将执行...", UOutLevel.INFO);
                TableHandler.ExecuteFileCommands(file);
            });
            LogCore.Success("系统数据库更新成功");
            Status = true;
        }
        catch (Exception e)
        {
            LogCore.Exception($"系统数据库更新失败, {e.Message}");
            Status = false;
        }
    }

    #endregion


    #region 私有方法

    /// <summary>
    /// 初始化数据库表
    /// </summary>
    /// <param name="tableJobjectArray"></param>
    private void InitDatabaseTable(List<JObject> tableJobjectArray)
    {
        LogCore.Info($"正在检测数据表是否完整...");
        try
        {
            //循环定义,做执行
            tableJobjectArray.ForEach(element =>
            {
                string tablename = element["name"]!.ToString(); //获取表名称
                bool isexist = TableHandler.CheckTableExist(tablename);
                if (isexist) return;
                LogCore.Success($"系统未检测到数据表, 创建数据表:'{tablename}' 成功");
                //根据表名执行创建
                TableHandler.CreateTableByName(element);
            });
        }
        catch (Exception e)
        {
            LogCore.Exception($"创建数据表失败, {e.Message}");
        }
    }


    /// <summary>
    /// 更新数据库版本号
    /// </summary>
    private void UpdateDatabaseVersion(JObject version)
    {
        try
        {
            //根据表名执行创建
            TableHandler.ReplaceTableByName("ro_version", version, "name");
            LogCore.Success("更新version表成功");
        }
        catch (Exception e)
        {
            LogCore.Exception($"更新version表失败, {e.Message}");
        }
    }

    #endregion
}