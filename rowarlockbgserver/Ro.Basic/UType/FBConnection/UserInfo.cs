using System.ComponentModel.DataAnnotations;

namespace Ro.Basic.UType.FBConnection;

/// <summary>
/// 用户信息
/// </summary>
public class UserInfo
{
    public string userid { get; set; }
    public string username { get; set; }
    public string? userpassword { get; set; }
    public string? datetime { get; set; }
}