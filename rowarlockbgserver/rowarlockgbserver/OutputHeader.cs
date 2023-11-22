using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;

namespace rowarlockgbserver;

/// <summary>
/// 输出头
/// </summary>
public abstract class OutputHeader
{
    /// <summary>
    ///  登录输出
    /// </summary>
    public static void LoginOutputTop()
    {
        LogCore.Info("========================================================");
        LogCore.Log("ROWARLOCK...", UOutLevel.Debug);
        LogCore.Log("系统初始化...", UOutLevel.Pass);
        LogCore.Log("硬加载启动中...", UOutLevel.Info);
        LogCore.Log("软加载启动中...", UOutLevel.Fail);
        LogCore.Log("输出测试...", UOutLevel.Warning);
        LogCore.Log("自检测试...", UOutLevel.Exception);
        LogCore.Log("自检完成...", UOutLevel.DataType);
        LogCore.Info("========================================================");
    }


    public static void LoginOutputBottom()
    {
        // 系统加载完毕后输出
        LogCore.Info("========================================================");
        LogCore.Pass("欢迎使用ROWARLOCK");
        LogCore.Info("========================================================");
    }
}