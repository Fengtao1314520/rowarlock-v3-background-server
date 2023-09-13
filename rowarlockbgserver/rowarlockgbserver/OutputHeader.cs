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
        LogCore.Log("ROWARLOCK...", UOutLevel.DEBUG);
        LogCore.Log("系统初始化...", UOutLevel.SUCCESS);
        LogCore.Log("硬加载启动中...", UOutLevel.INFO);
        LogCore.Log("软加载启动中...", UOutLevel.ERROR);
        LogCore.Log("输出测试...", UOutLevel.WARN);
        LogCore.Log("自检测试...", UOutLevel.EXCEPT);
        LogCore.Log("自检完成...", UOutLevel.JSON);
        LogCore.Info("========================================================");
    }


    public static void LoginOutputBottom()
    {
        // 系统加载完毕后输出
        LogCore.Info("========================================================");
        LogCore.Log("欢迎使用ROWARLOCK", UOutLevel.SUCCESS);
        LogCore.Info("========================================================");
    }
}