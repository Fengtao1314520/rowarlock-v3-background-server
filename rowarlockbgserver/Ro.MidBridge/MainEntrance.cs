using Newtonsoft.Json.Linq;
using Ro.Basic.UType;
using Ro.CrossPlatform.Logs;
using Ro.Database;
using Ro.Http;
using Ro.MidBridge.Resolve;

namespace Ro.MidBridge;

public class MainEntrance : IDisposable
{
    #region const 值

    public bool Status { get; private set; }

    /// <summary>
    /// 基础配置文件json路径
    /// </summary>
    private readonly string _configpath = @"./ServerResource/config.json";

    /// <summary>
    /// SQL文件的文件夹路径
    /// </summary>
    private readonly string _sqlfolder = @"./ServerResource/sqls";

    #endregion

    #region 私有 各种类入口

    /// <summary>
    /// 数据库entrance
    /// </summary>
    private DatabaseEntrance _databaseEntrance;

    /// <summary>
    ///  网络entrance
    /// </summary>
    private HttpEntrance _httpEntrance;

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
        _httpEntrance = null!;
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
        // info 1. 打开数据库
        StartDatabase(configJObject);
        // info 2. 读取sql文件夹，并更新
        UpdateSql();
        // info 3.打开网络服务
        StartHttpServer(configJObject);
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

    #region 私有方法

    /// <summary>
    /// 数据库操作
    /// 1. 判断db文件是否存在
    /// 2. 判断各表是否存在且完整
    /// 3. 更新主板号
    /// </summary>
    /// <param name="configJObject"></param>
    private void StartDatabase(JObject configJObject)
    {
        // 数据库信息类型 属性
        DataBaseInfoType dataBaseInfoType = _resolveConfig.ResolveDataBaseInfoType(configJObject);
        // 解析数据库配置
        _databaseEntrance = new DatabaseEntrance(dataBaseInfoType);
        Status = _databaseEntrance.DatabaseStatus;
        if (Status)
        {
            JObject version = configJObject["version"]!.ToObject<JObject>()!;
            // 连接数据库，并初始化,并更新版本号
            _databaseEntrance.ConnectDb().InitDatabase(version);
            // 更新状态
            Status = _databaseEntrance.DatabaseStatus;
        }
        else
        {
            Status = false;
        }
    }


    /// <summary>
    /// 升级sql文件
    /// </summary>
    private void UpdateSql()
    {
        if (!Directory.Exists(_sqlfolder))
        {
            LogCore.Info("未找到SQL文件夹, 跳过更新");
            return;
        }

        DirectoryInfo directoryInfo = new(_sqlfolder);
        //判断文件夹内是否有文件
        if (directoryInfo.GetFiles().Length == 0)
        {
            LogCore.Info("SQL文件夹内没有文件,跳过更新");
            return;
        }

        //获取文件夹内的文件
        var sqlfiles = directoryInfo.GetFiles();
        //更新数据库
        _databaseEntrance.UpdateByFile(sqlfiles);
        Status = true;
    }


    /// <summary>
    /// 启动httpserver
    /// </summary>
    /// <param name="configJObject"></param>
    private void StartHttpServer(JObject configJObject)
    {
        // 数据库信息类型 属性
        HttpServerType httpServerType = _resolveConfig.ResolveHttpServerType(configJObject);
        // 初始化http服务
        _httpEntrance = new HttpEntrance(httpServerType);

        _httpEntrance.Start();
    }

    #endregion
}