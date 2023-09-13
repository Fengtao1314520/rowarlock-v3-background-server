using Ro.Basic.UEnum;
using Ro.Common;
using Ro.CrossPlatform.Logs;
using Ro.MidBridge;
using rowarlockgbserver;


//INFO: MAIN
LogCore.Log("当前程序路径:" + ComArgs.ExecutionPath, UOutLevel.DEBUG);
// IMP!: 切换到当前目录
Directory.SetCurrentDirectory(ComArgs.ExecutionPath);

// 开始执行
OutputHeader.LoginOutputTop();
MainEntrance me = new();
me.Start();
if (me.Status)
{
    //输出日志  
    LogCore.Log("系统加载完毕", UOutLevel.SUCCESS);
    OutputHeader.LoginOutputBottom();
// 当有输入时,且必须是exit，停止服务
    string? rl = "";
    while (rl != "exit") rl = Console.ReadLine();
}
else
{
    //输出日志
    LogCore.Log("系统加载失败", UOutLevel.ERROR);
}

// 关闭并释放资源
me.Stop().Dispose();
LogCore.Log("结束执行...", UOutLevel.INFO);