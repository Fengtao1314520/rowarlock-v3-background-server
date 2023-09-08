using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;
using Ro.MidBridge;
using rowarlockgbserver;

OutputHeader.LoginOutput();
MainEntrance me = new();
me.Start();
if (me.Status)
{
    LogCore.Log("系统加载完毕", UOutLevel.SUCCESS);
// 当有输入时,且必须是exit，停止服务
    string? rl = "";
    while (rl != "exit") rl = Console.ReadLine();
}
else
{
    //输出日志
    LogCore.Log("系统加载失败", UOutLevel.ERROR);
}

me.Stop().Dispose();
LogCore.Log("结束执行...", UOutLevel.INFO);