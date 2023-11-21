namespace Ro.Basic.UType.Communicate;

/// <summary>
/// HTTP命令来了以后，需要输出
/// 输出对象
/// </summary>
public class HOutObjType
{
    private string? _url;
    public required string Method { get; set; }
    public required string Api { get; set; }
    public required dynamic Para { get; set; }

    public string? Url
    {
        get => _url;
        set
        {
            _url = value;
            if (!string.IsNullOrEmpty(Api)) _url = $"{Pathbase}/{Api}";
        }
    }

    public string Pathbase { get; } = "/api";
}