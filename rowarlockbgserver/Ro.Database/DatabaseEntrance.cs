using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using Ro.Basic.UType;
using Ro.CrossPlatform.Logs;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Func;
using Ro.Database.Controller;

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
    /// 数据库链接
    /// </summary>
    private SqliteConnection _sqliteConnection;

    /// <summary>
    ///  数据库表控制器
    /// </summary>
    private Table _tableControls;

    /// <summary>
    /// 数据库状态
    /// </summary>
    public bool DatabaseStatus { get; }

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public DatabaseEntrance(DataBaseInfoType dataBaseInfoType)
    {
        // 初始化
        _sqliteConnection = null!;
        _tableControls = null!;
        _dataBaseInfoType = dataBaseInfoType;
        // 数据库前置操作
        bool prestatus = PreDatabase(_dataBaseInfoType);
        DatabaseStatus = prestatus;
    }

    #endregion


    #region 公有方法

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <returns></returns>
    public bool InitDatabase()
    {
        string tablepath = _dataBaseInfoType.TablePath;
        LogCore.Log("系统数据库初始化,请稍等...", UOutLevel.INFO);
        var tableJobjectArray = JsonFunc.ReturnJsonObjectByFile<List<JObject>>(tablepath);
        LogCore.Log($"正在检测数据表是否完整...", UOutLevel.INFO);
        bool checkstatus = CheckDbTableComplete(tableJobjectArray);
        // 返回结果
        return checkstatus;
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
            _sqliteConnection = new SqliteConnection(connectionString);
            _sqliteConnection.Open();
            LogCore.Log("数据库连接成功", UOutLevel.SUCCESS);

            // 赋值
            _tableControls = new Table(_sqliteConnection);
        }
        catch (Exception e)
        {
            LogCore.Log($"数据库连接失败, {e.Message}", UOutLevel.ERROR);
        }

        return this;
    }

    /// <summary>
    /// 断开数据库
    /// </summary>
    public DatabaseEntrance DisconnectDb()
    {
        _sqliteConnection.Close();
        return this;
    }


    /// <summary>
    ///  释放数据库
    /// </summary>
    public void Dispose()
    {
        _sqliteConnection.Dispose();
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="dataBaseInfoType"></param>
    private bool PreDatabase(DataBaseInfoType dataBaseInfoType)
    {
        //INFO: 创建数据库文件，(如果不存在）
        bool filestatus = CreateDatabaseFile(dataBaseInfoType.Path);
        if (!filestatus)
        {
            LogCore.Log("数据库文件创建失败", UOutLevel.ERROR);
            return false;
        }

        LogCore.Log("数据库文件创建成功", UOutLevel.SUCCESS);
        return true;
    }


    /// <summary>
    /// 创建数据库文件
    /// </summary>
    /// <param name="dbfilepath"></param>
    private bool CreateDatabaseFile(string dbfilepath)
    {
        //如果文件不存在,则创建一个新的数据库文件
        if (File.Exists(dbfilepath)) return true;
        try
        {
            LogCore.Log("系统未检测到数据库,将重新新建", UOutLevel.ERROR);
            string? dpath = Path.GetDirectoryName(dbfilepath);
            if (string.IsNullOrEmpty(dpath)) return false;
            Directory.CreateDirectory(dpath);
            // 依赖File类创建文件
            File.Create(dbfilepath);
            LogCore.Log("系统重建数据库成功", UOutLevel.SUCCESS);
            return true;
        }
        catch (Exception e)
        {
            LogCore.Log($"系统重建数据库失败, {e.Message}", UOutLevel.EXCEPT);
            return false;
        }
    }


    /// <summary>
    /// 检查数据库表是否完整
    /// </summary>
    /// <param name="tableJobjectArray"></param>
    private bool CheckDbTableComplete(List<JObject> tableJobjectArray)
    {
        try
        {
            //循环定义,做执行
            tableJobjectArray.ForEach(element =>
            {
                string tablename = element["name"]!.ToString(); //获取表名称
                bool isexist = _tableControls.CheckTableExist(tablename);
                if (!isexist.Equals(false)) return;
                LogCore.Log($"系统未检测到数据表:'{tablename}',将新建数据表", UOutLevel.WARN);
                //根据表名执行创建
                _tableControls.CreateTableByName(element);
                LogCore.Log($"创建数据表:'{tablename}' 成功", UOutLevel.SUCCESS);
            });
            return true;
        }
        catch (Exception e)
        {
            LogCore.Log($"创建数据表失败, {e.Message}", UOutLevel.EXCEPT);
            return false;
        }
    }

    #endregion
}