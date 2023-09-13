using Newtonsoft.Json.Linq;
using Ro.Basic.UType;

namespace Ro.CrossPlatform.Extension;

public static class JObjectExt
{
    public static List<KeyValueType> ToKeyValueTypeList(this JObject jObject)
    {
        List<KeyValueType> keyValueTypes = new();
        foreach (var item in jObject)
            keyValueTypes.Add(new KeyValueType()
            {
                Key = item.Key,
                Value = item.Value!
            });

        return keyValueTypes;
    }
}