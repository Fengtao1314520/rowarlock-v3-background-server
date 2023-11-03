namespace Ro.Basic.Attributes.DataBase.UserDetails;

[Serializable]
public class TableAttrs : Attribute
{
    /// <summary>
    ///  表名称
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 主键名称
    /// </summary>
    public string KeyName { get; set; }

    public TableAttrs(string tableName, string keyName)
    {
        TableName = tableName;
        KeyName = keyName;
    }
}