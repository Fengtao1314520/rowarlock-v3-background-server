namespace Ro.Basic.Attributes.DataBase.UserDetails;

[Serializable]
public class FieldAttrs : Attribute
{
    public string FieldName { get; set; }
    public string FieldType { get; set; }
    public string FieldDescription { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <param name="fieldType">字段类型</param>
    /// <param name="description">描述</param>
    public FieldAttrs(string fieldName, string fieldType, string description)
    {
        FieldName = fieldName;
        FieldType = fieldType;
        FieldDescription = description;
    }
}