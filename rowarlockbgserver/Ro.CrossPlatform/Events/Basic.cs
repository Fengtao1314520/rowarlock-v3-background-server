using Ro.CrossPlatform.Logs;

namespace Ro.CrossPlatform.Events;

/// <summary>
/// 自定义的 Rowarlock 委托
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <param name="houtobj"></param>
/// <param name="para"></param>
/// <param name="logstruct"></param>
/// <returns></returns>
public delegate TResult RoFunc<in T1, in T2, out TResult>(T1 houtobj, T2 para, ref LogStruct logstruct);