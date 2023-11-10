using Ro.Basic.Attributes.DataBase.UserDetails;

namespace Ro.Basic.UType.DataBase;

[TableAttrs("ro_userdetails", "id")]
[Serializable]
public class CuDUserDetails
{
    [FieldAttrs("id", "TEXT", "用户id, 主键")]
    public string Id { get; set; }

    [FieldAttrs("uname", "TEXT", "用户名")]
    public string Uname { get; set; }

    [FieldAttrs("unickname", "TEXT", "用户昵称")]
    public string Unickname { get; set; }

    [FieldAttrs("upsd", "TEXT", "用户密码")]
    public string Upsd { get; set; }

    [FieldAttrs("uemail", "TEXT", "用户注册用的邮箱")]
    public string Uemail { get; set; }

    [FieldAttrs("ulogintime", "TEXT", "用户登陆的时间(YYYY-MM-DD HH:MM:SS.SSS)")]
    public string Ulogintime { get; set; }
}