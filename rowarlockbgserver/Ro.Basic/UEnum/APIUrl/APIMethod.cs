namespace Ro.Basic.UEnum.APIUrl;

public abstract class ApiMethod
{
    /// <summary>
    /// 创建数据
    /// </summary>
    public const string POST = "POST";

    /// <summary>
    /// 获取数据
    /// </summary>
    public const string GET = "GET";

    /// <summary>
    /// 差量修改数据
    /// </summary>
    public const string PATCH = "PATCH";

    /// <summary>
    /// 全量修改数据
    /// 默认全量修改
    /// </summary>
    public const string PUT = "PUT";

    /// <summary>
    /// 删除数据
    /// </summary>
    public const string DELETE = "DELETE";
}