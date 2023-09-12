using Ro.Database;
using Newtonsoft.Json.Linq;
using Ro.Basic.UEnum;
using Ro.Basic.UType;
using Ro.CrossPlatform.Logs;
using Ro.MidBridge.Resolve;

namespace Ro.MidBridge;

public class MainEntrance : IDisposable
{
    #region const 值

    public bool Status { get; set; }

    /// <summary>
    /// 基础配置文件json路径
    /// </summary>
    private readonly string _configpath = @"./ServerResource/config.json";

    /// <summary>
    /// SQL文件的文件夹路径
    /// </summary>
    private readonly string _sqlfolder = @"./ResFiles/UPDATESQL";

    #endregion

    #region 私有 类

    /// <summary>
    /// 数据库entrance
    /// </summary>
    private DatabaseEntrance _databaseEntrance;

    /// <summary>
    /// 解析配置
    /// </summary>
    private readonly ResolveConfig _resolveConfig;

    #endregion


    /// <summary>
    /// 构造函数
    /// </summary>
    public MainEntrance()
    {
        // INFO: 赋值
        _databaseEntrance = null!;
        _resolveConfig = new ResolveConfig();
    }


    #region 对外方法

    /// <summary>
    /// 开始服务
    /// </summary>
    public MainEntrance Start()
    {
        // config的JObject
        JObject configJObject = _resolveConfig.ResolveConfigToJObject(_configpath);
        // 数据库处理
        DatabaseHandle(configJObject);
        return this;
    }

    /// <summary>
    /// 关闭服务
    /// </summary>
    public MainEntrance Stop()
    {
        _databaseEntrance.DisconnectDb();
        return this;
    }

    /// <summary>
    /// 释放服务
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        _databaseEntrance.Dispose();
    }

    #endregion


    /// <summary>
    /// 数据库操作
    /// </summary>
    /// <param name="configJobject"></param>
    private void DatabaseHandle(JObject configJobject)
    {
        // 解析数据库配置
        JObject dbconfig = configJobject["database"]!.ToObject<JObject>()!;
        // 数据库信息类型 属性
        DataBaseInfoType dataBaseInfoType = new()
        {
            Path = dbconfig["dbpath"]!.ToString(),
            UpdatePath = dbconfig["db_update"]!.ToString(),
            TablePath = dbconfig["db_table"]!.ToString()
        };
        //解析数据库配置
        _databaseEntrance = new DatabaseEntrance(dataBaseInfoType);
        if (_databaseEntrance.DatabaseStatus)
        {
            // 连接数据库，并初始化
            _databaseEntrance.ConnectDb().InitDatabase();
            Status = _databaseEntrance.DatabaseStatus;

            // 更新数据库版本号
            if (!Status) return;
            //update:2023-09-08 更新数据库版本号
            JObject version = configJobject["version"]!.ToObject<JObject>()!;
            _databaseEntrance.UpdateDatabaseVersion(version);
        }
        else
        {
            Status = false;
        }
    }
}