using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events;

/// <summary>
/// 自定义的 Rowarlock 委托
/// </summary>
public delegate TResult RoFunc<in T1, in T2, out TResult>(T1 houtobj, T2 para, ref LogStruct logstruct);