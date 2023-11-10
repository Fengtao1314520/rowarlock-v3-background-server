using System.Reflection;
using System.Text;
using Microsoft.Data.Sqlite;
using Ro.Basic.Attributes.DataBase.UserDetails;
using Ro.Database.Dependent;

namespace Ro.Database.ORM;

public class DBORM<T> : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// 全属性值
    /// </summary>
    private readonly PropertyInfo[] _allproperties;

    /// <summary>
    ///  ORM 类型
    /// </summary>
    private readonly Type _ormType;

    /// <summary>
    /// T类型
    /// </summary>
    private readonly T _tobj;

    /// <summary>
    /// sql连接
    /// </summary>
    private readonly SqliteConnection _sqliteConnection;

    /// <summary>
    /// 表名
    /// </summary>
    private readonly string _tableName;

    /// <summary>
    /// 主键名
    /// </summary>
    private readonly string _keyName;

    /// <summary>
    /// 主键值
    /// 默认值null
    /// </summary>
    private readonly object? _keyValue = null;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sqliteConnection"></param>
    /// <param name="tobj"></param>
    public DBORM(SqliteConnection sqliteConnection, T tobj)
    {
        _tobj = tobj;
        // 类型
        _ormType = _tobj!.GetType();
        // 连接
        _sqliteConnection = sqliteConnection;
        // 获取所有属性
        _allproperties = _ormType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        // 判断是否包含attribute
        if (_ormType.GetCustomAttribute(typeof(TableAttrs)) is not TableAttrs tableAttrs) return;
        _tableName = tableAttrs.TableName;
        _keyName = tableAttrs.KeyName;

        //根据反射，获取主键值,是依赖attribute的
        PropertyInfo? keyProperty = _allproperties.FirstOrDefault(t =>
        {
            if (t.GetCustomAttribute(typeof(FieldAttrs), false) is not FieldAttrs fieldAttrs) return false;
            return fieldAttrs.FieldName == _keyName;
        });
        if (keyProperty != null) _keyValue = keyProperty.GetValue(_tobj);
    }

    #region 数据库操作

    /// <summary>
    ///  更新数据
    /// </summary>
    public int Update()
    {
        // 构造sql语句
        StringBuilder sb = new();
        // 获取每个属性的值
        _allproperties.ToList().ForEach(item =>
        {
            if (item.GetCustomAttribute(typeof(FieldAttrs), false) is not FieldAttrs fieldAttrs) return;
            // 获取属性名
            string fieldName = fieldAttrs.FieldName;
            // 获取属性值
            object? fieldValue = item.GetValue(_tobj);
            // 获取类型
            string fieldType = fieldAttrs.FieldType;
            // 根据类型,拼装
            switch (fieldType)
            {
                case "TEXT" when fieldValue is null:
                    sb.Append($"{fieldName} = NULL,");
                    break;
                case "TEXT":
                    sb.Append($"{fieldName} = '{fieldValue}',");
                    break;
                case "INTEGER":
                    sb.Append($"{fieldName} = {fieldValue},");
                    break;
                case "REAL":
                    sb.Append($"{fieldName} = {fieldValue},");
                    break;
                case "BLOB":
                    sb.Append($"{fieldName} = {fieldValue},");
                    break;
            }
        });
        //移除最后一处标点符号,并生成 
        string setdata = sb.ToString().Remove(sb.ToString().Length - 1, 1);

        // 创建condition
        string condition = $"{_keyName} = '{_keyValue}'";

        // 执行
        int result = Polymerization.UpdateUtil.UpdateDataWithCondition(_sqliteConnection, _tableName, setdata,
            condition);

        return result;
    }


    /// <summary>
    /// 插入数据
    /// </summary>
    public int Insert()
    {
        // 获取每个属性的值
        StringBuilder sb = new();
        StringBuilder sb2 = new();
        _allproperties.ToList().ForEach(item =>
        {
            if (item.GetCustomAttribute(typeof(FieldAttrs), false) is not FieldAttrs fieldAttrs) return;
            // 获取属性名
            string fieldName = fieldAttrs.FieldName;
            // 获取属性值
            object? fieldValue = item.GetValue(_tobj);
            // 获取类型
            string fieldType = fieldAttrs.FieldType;
            // 根据类型,拼装
            switch (fieldType)
            {
                case "TEXT" when fieldValue is null:
                    sb.Append($"{fieldName},");
                    sb2.Append($"NULL,");
                    break;
                case "TEXT":
                    sb.Append($"{fieldName},");
                    sb2.Append($"'{fieldValue}',");
                    break;

                case "INTEGER" when fieldValue is null:
                    sb.Append($"{fieldName},");
                    sb2.Append($"NULL,");
                    break;
                case "INTEGER":
                    sb.Append($"{fieldName},");
                    sb2.Append($"{fieldValue},");
                    break;
            }
        });

        //移除最后一处标点符号,并生成 
        string keys = sb.ToString().Remove(sb.ToString().Length - 1, 1);
        string values = sb2.ToString().Remove(sb2.ToString().Length - 1, 1);

        //执行插入
        int result = Polymerization.InsertUtil.InsertDataWithField(_sqliteConnection, _tableName, keys, values);

        return result;
    }


    /// <summary>
    /// 删除数据
    /// </summary>
    public void Delete()
    {
        // 根据T 拼接数据
        StringBuilder sb = new();
        // info: 根据t拼接删除的信息，只要信息不是null/""什么的，就添加为condition.
        _allproperties.ToList().ForEach(item =>
        {
            if (item.GetCustomAttribute(typeof(FieldAttrs), false) is not FieldAttrs fieldAttrs) return;
            // 获取属性名
            string fieldName = fieldAttrs.FieldName;
            // 获取属性值
            object? fieldValue = item.GetValue(_tobj);
            // 获取类型
            string fieldType = fieldAttrs.FieldType;

            // 根据类型,拼装
            switch (fieldType)
            {
                case "TEXT" when fieldValue is null:
                    sb.Append($"{fieldName} IS NULL AND ");
                    break;
                case "TEXT":
                    sb.Append($"{fieldName} = '{fieldValue}' AND ");
                    break;
                case "INTEGER" when fieldValue is null:
                    sb.Append($"{fieldName} IS NULL AND ");
                    break;
                case "INTEGER":
                    sb.Append($"{fieldName} = {fieldValue} AND ");
                    break;
            }
        });
        if (sb.Length <= 0) return;
        //移除最后一处标点符号,并生成deleteData
        string deleteData = sb.ToString().Remove(sb.ToString().Length - 4, 4);
        //执行删除
        Polymerization.DeleteUtil.DeleteData(_sqliteConnection, _tableName, deleteData);
    }


    /// <summary>
    /// 根据主键查询数据
    /// </summary>
    public List<Dictionary<string, object>> Query()
    {
        return Query(_keyName, _keyValue);
    }


    /// <summary>
    /// 根据给定key和value查询数据
    /// </summary>
    /// <param name="key">查询字段名</param>
    /// <param name="value">查询字段值</param>
    public List<Dictionary<string, object>> Query(string key, object value)
    {
        string condition = $"{key} = '{value}'";
        return Query(condition);
    }

    /// <summary>
    /// 根据给定条件查询数据
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public List<Dictionary<string, object>> Query(string condition)
    {
        var result = Polymerization.SelectUtil.SelectDataToDictList(_sqliteConnection, _tableName, "*", condition);
        return result;
    }


    public List<TK> Query<TK>()
    {
        return Query<TK>(_keyName, _keyValue);
    }


    /// <summary>
    /// 根据给定key和value查询数据
    /// </summary>
    /// <param name="key">查询字段名</param>
    /// <param name="value">查询字段值</param>
    public List<TK> Query<TK>(string key, object value)
    {
        string condition = $"{key} = '{value}'";
        return Query<TK>(condition);
    }


    public List<TK> Query<TK>(string condition)
    {
        var result = new List<TK>();
        var queryresult = Polymerization.SelectUtil.SelectDataToDictList(_sqliteConnection, _tableName, "*", condition);

        if (queryresult.Any()) result = TranslateToType<TK>(queryresult);

        return result;
    }

    #endregion

    public void Dispose()
    {
        //_sqliteConnection.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        //await _sqliteConnection.DisposeAsync();
    }


    private List<TK> TranslateToType<TK>(List<Dictionary<string, object>> queryresult)
    {
        var result = new List<TK>();
        // 将 List<Dictionary<string, object>> 转为 List<T>,item为单条数据
        foreach (var item in queryresult)
        {
            // 创建指定 type 的新实例
            TK t = Activator.CreateInstance<TK>();

            _allproperties.ToList().ForEach(property =>
            {
                if (property.GetCustomAttribute(typeof(FieldAttrs), false) is not FieldAttrs fieldAttrs) return;
                // 获取属性名
                string fieldName = fieldAttrs.FieldName;
                // 获取属性值
                object? fieldValue = item[fieldName];
                Type propertyType = property.PropertyType;
                // 将fieldValue改为propertyType类型
                fieldValue = Convert.ChangeType(fieldValue, propertyType);
                t.GetType().GetProperty(property.Name).SetValue(t, fieldValue);
            });
            // 添加
            result.Add(t);
        }

        //返回值
        return result;
    }
}