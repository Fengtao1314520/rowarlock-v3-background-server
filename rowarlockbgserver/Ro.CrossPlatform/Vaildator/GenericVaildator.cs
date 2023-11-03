namespace Ro.CrossPlatform.Vaildator;

public static class GenericVaildator
{
    public static bool IsValid { get; private set; }

    public static void Vailidation(dynamic tobj, Type compareType)
    {
        // 对比 tobj的类型和ttype
        Type origintype = tobj.GetType();

        IsValid = origintype == compareType;
    }
}