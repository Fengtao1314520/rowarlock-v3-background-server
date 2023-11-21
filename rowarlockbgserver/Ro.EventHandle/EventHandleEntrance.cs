using Ro.EventHandle.Webs;

namespace Ro.EventHandle;

/// <summary>
/// 事件处理入口
/// </summary>
public class EventHandleEntrance
{
    private readonly UserInfoEventHandle _userInfoEventHandle;
    private readonly TasksEventHandle _taskEventHandle;
    private readonly RelaseEventHandle _relaseEventHandle;
    private readonly InterfacePackageEventHandle _interfacePackageEventHandle;
    private readonly StatisticsEventHandle _statisticsEventHandle;

    /// <summary>
    /// 构造函数
    /// </summary>
    public EventHandleEntrance()
    {
        _userInfoEventHandle = new UserInfoEventHandle();
        _statisticsEventHandle = new StatisticsEventHandle();
        _taskEventHandle = new TasksEventHandle();
        _relaseEventHandle = new RelaseEventHandle();
        _interfacePackageEventHandle = new InterfacePackageEventHandle();
    }

    /// <summary>
    /// 加载事件
    /// </summary>
    public void LoadEvents()
    {
        _userInfoEventHandle.LoadEvent();
        _statisticsEventHandle.LoadEvent();
        _taskEventHandle.LoadEvent();
        _relaseEventHandle.LoadEvent();
        _interfacePackageEventHandle.LoadEvent();
    }


    /// <summary>
    /// 卸载事件
    /// </summary>
    public void UnLoadEvents()
    {
        _userInfoEventHandle.UnLoadEvent();
        _statisticsEventHandle.UnLoadEvent();
        _taskEventHandle.UnLoadEvent();
        _relaseEventHandle.UnLoadEvent();
        _interfacePackageEventHandle.UnLoadEvent();
    }
}