using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public static class TasksEvent
{
    public static event RoFunc<HOutObjType, object, ResponseType> SimpleTasksByUserInfo;

    public static ResponseType OnSimpleTasksByUserInfo(HOutObjType houtobj, object para, ref LogStruct logstruct)
    {
        return SimpleTasksByUserInfo.Invoke(houtobj, para, ref logstruct);
    }
}