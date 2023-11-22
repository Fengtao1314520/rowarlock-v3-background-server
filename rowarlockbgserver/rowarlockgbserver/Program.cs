using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.CrossPlatform.Logs;
using Ro.MidBridge;
using rowarlockgbserver;


//INFO: MAIN
LogCore.Debug("当前程序路径:" + ComArgs.ExecutionPath);
// IMP!: 切换到当前目录
Directory.SetCurrentDirectory(ComArgs.ExecutionPath);

// 开始执行
OutputHeader.LoginOutputTop();
MainEntrance me = new();
me.Start();
if (me.Status)
{
    //输出日志  
    LogCore.Pass("系统加载完毕");
    OutputHeader.LoginOutputBottom();
// 当有输入时,且必须是exit，停止服务
    string? rl = "";
    while (rl != "exit") rl = Console.ReadLine();
}
else
{
    //输出日志
    LogCore.Fail("系统加载失败");
}

// 关闭并释放资源
me.Stop().Dispose();
LogCore.Info("结束执行...");