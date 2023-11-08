using Ro.CrossPlatform.Events.Webs;
using Ro.EventHandle.Webs;

namespace Ro.EventHandle;

/// <summary>
/// 事件处理入口
/// </summary>
public class EventHandleEntrance
{
    private readonly UserInfoEventHandle _userInfoEventHandle;

    private readonly TasksEventHandle _taskEventHandle;

    /// <summary>
    /// 构造函数
    /// </summary>
    public EventHandleEntrance()
    {
        _userInfoEventHandle = new UserInfoEventHandle();
        _taskEventHandle = new TasksEventHandle();
    }

    public void LoadEvents()
    {
        UserInfoEvent.BasicEvent += _userInfoEventHandle.OnBasic;
        UserInfoEvent.GetInfoEvent += _userInfoEventHandle.OnGetInfo;
        TasksEvent.SimpleTasksByUserInfo += _taskEventHandle.OnSimpleTasksByUserInfo;
    }

    public void UnLoadEvents()
    {
        UserInfoEvent.BasicEvent -= _userInfoEventHandle.OnBasic;
        UserInfoEvent.GetInfoEvent -= _userInfoEventHandle.OnGetInfo;
        TasksEvent.SimpleTasksByUserInfo -= _taskEventHandle.OnSimpleTasksByUserInfo;
    }
}