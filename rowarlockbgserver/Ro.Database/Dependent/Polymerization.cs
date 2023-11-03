using Ro.Database.Util;

namespace Ro.Database.Dependent;

/// <summary>
///* 方法聚合类，聚合Util的各类方法，毕竟都是针对table进行操作
/// </summary>
public static class Polymerization
{
    internal static TableUtil TableUtil { get; private set; }
    internal static DeleteUtil DeleteUtil { get; private set; }
    internal static InsertUtil InsertUtil { get; private set; }
    internal static SelectUtil SelectUtil { get; private set; }
    internal static UpdateUtil UpdateUtil { get; private set; }
    internal static ReplaceUtil ReplaceUtil { get; private set; }
    internal static NudeExecuteUtil NudeExecuteUtil { get; private set; }

    static Polymerization()
    {
        TableUtil = new TableUtil();
        DeleteUtil = new DeleteUtil();
        InsertUtil = new InsertUtil();
        SelectUtil = new SelectUtil();
        UpdateUtil = new UpdateUtil();
        ReplaceUtil = new ReplaceUtil();
        NudeExecuteUtil = new NudeExecuteUtil();
    }
};