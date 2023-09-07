using Ro.Database;
using Newtonsoft.Json.Linq;
using Ro.Basic.UType;
using Ro.MidBridge.Resolve;

namespace Ro.MidBridge;

public class MainEntrance : IDisposable
{
    #region const 值

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

    private DatabaseEntrance _databaseEntrance;

    private readonly ResolveConfig _resolveConfig;

    #endregion


    /// <summary>
    /// 构造函数
    /// </summary>
    public MainEntrance()
    {
        _databaseEntrance = null!;
        _resolveConfig = new ResolveConfig();
    }


    #region 对外方法

    public void InitServer()
    {
        JObject configJobject = _resolveConfig.ResolveConfigToJObject(_configpath);
        DatabaseHandle(configJobject);
    }

    /// <summary>
    /// 开始服务
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// 关闭服务
    /// </summary>
    public void Stop()
    {
    }

    /// <summary>
    /// 释放服务
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        throw new NotImplementedException();
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
        DataBaseInfoType dataBaseInfoType = new()
        {
            Path = dbconfig["dbpath"]!.ToString(),
            UpdatePath = dbconfig["db_update"]!.ToString(),
            TablePath = dbconfig["db_table"]!.ToString()
        };
        //解析数据库配置
        _databaseEntrance = new DatabaseEntrance(dataBaseInfoType);
        // 连接数据库
        _databaseEntrance.ConnectDb();
        // 初始化
        _databaseEntrance.InitDatabase();
    }
}