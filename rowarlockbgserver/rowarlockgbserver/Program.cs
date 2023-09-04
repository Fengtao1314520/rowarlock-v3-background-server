using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;

namespace rowarlockgbserver;

internal static class Program
{
    private static void Main(string[] args)
    { 
        // 输出
        LoginOutput();
        //MainEntrance me = new MainEntrance();
        //me.Start();
        LogCore.Log("系统加载完毕", UOutLevel.SUCCESS);
        Console.ReadKey(true);
        //me.Stop();
        LogCore.Log("结束执行...", UOutLevel.INFO);
    }

    /// <summary>
    ///  登录输出
    /// </summary>
    private static void LoginOutput()
    {
        LogCore.Out("========================================================");
        LogCore.Log("ROWARLOCK...", UOutLevel.DEBUG);
        LogCore.Log("系统初始化...", UOutLevel.SUCCESS);
        LogCore.Log("硬加载启动中...", UOutLevel.INFO);
        LogCore.Log("软加载启动中...", UOutLevel.ERROR);
        LogCore.Log("输出测试...", UOutLevel.WARN);
        LogCore.Log("自检测试...", UOutLevel.EXCEPT);
        LogCore.Log("自检完成...", UOutLevel.JSON);
        LogCore.Out("========================================================");
    }
}