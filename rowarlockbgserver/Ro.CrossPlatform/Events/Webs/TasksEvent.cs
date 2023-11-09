using Ro.Basic.UType.Communicate;
using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events.Webs;

public static class TasksEvent
{
    public static event RoFunc<HOutObjType, dynamic, ResponseType> SimpleTasksByUserInfo;
    public static event RoFunc<HOutObjType, dynamic, ResponseType> ListTasksBaseDayByUserInfo;

    public static ResponseType OnSimpleTasksByUserInfo(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return SimpleTasksByUserInfo.Invoke(houtobj, para, ref logstruct);
    }

    public static ResponseType OnListTasksBaseDayByUserInfo(HOutObjType houtobj, dynamic para, ref LogStruct logstruct)
    {
        return ListTasksBaseDayByUserInfo.Invoke(houtobj, para, ref logstruct);
    }
}