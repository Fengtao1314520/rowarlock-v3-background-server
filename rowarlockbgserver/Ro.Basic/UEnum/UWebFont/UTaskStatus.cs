namespace Ro.Basic.UEnum.UWebFont;

/// <summary>
/// Task 状态
/// </summary>
public enum UTaskStatus
{
    Active = 0,
    Completed = 1,
    Canceled = 2,
    Failed = 3,
    Paused = 4,
    Running = 5,
    Scheduled = 6,
    Waiting = 7,
    WaitingForActivation = 8,
    WaitingForChildren = 9,
    WaitingToRun = 10,
    Blocked = 11,
    BlockedByParent = 12,
    Suspended = 13
}