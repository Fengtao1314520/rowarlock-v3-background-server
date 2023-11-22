using Ro.Basic;
using Ro.Basic.UEnum;
using Ro.Basic.UType.Communicate;
using Ro.Basic.UType.DataBase;
using Ro.CrossPlatform.Events.Webs;
using Ro.CrossPlatform.Func;
using Ro.CrossPlatform.Logs;
using Ro.Database.ORM;

namespace Ro.EventHandle.Webs;

public class InterfacePackageEventHandle
{
    public void LoadEvent()
    {
        InterfacePackageEvent.GetInterfacePackageSimpleByUserIdEvent += OnGetInterfacePackageSimpleByUserIdEventHandle;
        InterfacePackageEvent.GetInterfacePackageDetailByIdEvent += OnGetInterfacePackageDetailByIdEventHandle;
        InterfacePackageEvent.GetAllInterfacePackageListByUserIdEvent +=
            OnGetAllInterfacePackageListByUserIdEventHandle;
    }

    public void UnLoadEvent()
    {
        InterfacePackageEvent.GetInterfacePackageSimpleByUserIdEvent -= OnGetInterfacePackageSimpleByUserIdEventHandle;
        InterfacePackageEvent.GetInterfacePackageDetailByIdEvent -= OnGetInterfacePackageDetailByIdEventHandle;
        InterfacePackageEvent.GetAllInterfacePackageListByUserIdEvent -=
            OnGetAllInterfacePackageListByUserIdEventHandle;
    }

    public ResponseType OnGetInterfacePackageSimpleByUserIdEventHandle(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        // 参数实例化
        CuDInterfacePackage cuDInterfacePackage = para;
        // string condition = houtobj.Para;
        // dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);

        // 执行
        using var dborm = new DBORM<CuDInterfacePackage>(ComArgs.SqliteConnection, cuDInterfacePackage);
        var queryreslut = dborm.Query("assigneeuserid", cuDInterfacePackage.AssigneeUserId);
        // todo: 丢给前端计算吧，后端累了
        return ReqResFunc.GetResponseBody(queryreslut.Any() ? UReqCode.Success : UReqCode.Fail, queryreslut);
    }


    public ResponseType OnGetAllInterfacePackageListByUserIdEventHandle(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        // 参数实例化
        CuDInterfacePackage cuDInterfacePackage = para;
        string condition = houtobj.Para;
        dynamic dycondition = JsonFunc.DeserialzeJsonObject<dynamic>(condition);
        dynamic id = dycondition["id"];
        cuDInterfacePackage.Id = id.ToString();

        // INFO 返回值
        dynamic result = new { };
        // 执行
        using var dborm = new DBORM<CuDInterfacePackage>(ComArgs.SqliteConnection, cuDInterfacePackage);
        var queryresult =
            dborm.Query($"assigneeuserid='{cuDInterfacePackage.AssigneeUserId}' AND id='{cuDInterfacePackage.Id}'");
        if (queryresult.Any())
        {
            //检索ro_interface表
            CuDInterfacePackage first = queryresult.First();
            // INFO 包名
            string? packageName = first.InterfacePackageName;
            result.name = packageName;
            var interfaceids = first.InterfaceIds?.Split(',').ToList();
            // 循环查询
            interfaceids?.ForEach(idno =>
            {
                // 组装数据
                CuDInterface cuDInterface = new()
                {
                    Id = idno,
                    PackageId = first.Id
                };
                // ORM查询
                using var dborm2 = new DBORM<CuDInterface>(ComArgs.SqliteConnection, cuDInterface);
                var queryresult2 =
                    dborm2.Query(
                        $"packageid='{cuDInterface.PackageId}' AND id='{cuDInterface.Id}'");
                if (queryresult2.Any())
                {
                    string? pacakgeName = queryresult2.First().InterfaceName;
                    dynamic child = new
                        {name = pacakgeName, id = cuDInterface.Id, packageid = cuDInterface.PackageId};
                    result.children.Add(child);
                }
            });
            // 设置返回类型，失败的,设置返回类型，成功的   
            return ReqResFunc.GetResponseBody(result.children.Any() ? UReqCode.Success : UReqCode.Fail, result);
        }

        // 设置返回类型，失败的,设置返回类型，成功的 
        return ReqResFunc.GetResponseBody(UReqCode.Error, queryresult);
    }

    public ResponseType OnGetInterfacePackageDetailByIdEventHandle(HOutObjType houtobj, dynamic para,
        ref LogStruct logstruct)
    {
        // 参数实例化
        CuDInterfacePackage cuDInterfacePackage = para;

        // 执行
        using var dborm = new DBORM<CuDInterfacePackage>(ComArgs.SqliteConnection, cuDInterfacePackage);
        var queryresult = dborm.Query("id", cuDInterfacePackage.Id);


        // TODO: 等待添加
        // 设置返回类型，失败的,设置返回类型，成功的
        return ReqResFunc.GetResponseBody(queryresult.Any() ? UReqCode.Success : UReqCode.Fail, queryresult);
    }
}