var factoryId;
var packageNo;
var versionNo;
var productTypeId;
var produceProcTypeId;
$(function () {
    $('#wf').click(function () {
        packageNo = $.request.queryString["packageNo"];
        factoryId = $.request.queryString["factoryId"];
        versionNo = $.request.queryString["versionNo"];
        productTypeId = $.request.queryString["productTypeId"];
        produceProcTypeId = $.request.queryString["produceProcTypeId"];
        var j = {
            'PACKAGE_NO': packageNo,
            'VERSION_NO': versionNo,
            'FACTORY_ID': factoryId
        };
        $.ajax({
            type: 'get',
            url: '/api/Preview/Init_PACKAGE_WF_STEP',
            data: j,
            dataType: 'json',
            success: function (data) {
                if (data == 0) {
                    $.messager.show({
                        title: '消息',
                        msg: '未开启审批流程',
                        showType: 'show'
                    });
                } else if (data == -1) {
                    $.messager.show({
                        title: '消息',
                        msg: '无权限',
                        showType: 'show'
                    });
                } else {
                    window.location.href = '/Package/Preview.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
                }
            }
        });
    });
})
//标签
function InitTabs(currentTab, packageNo, factoryId, versionNo, productTypeId, produceProcTypeId) {
    $('#tt').tabs({
        onSelect: function (title) {
            switch (title) {
                case "基本信息":
                    if (currentTab != "基本信息")
                        window.location.href = '/Package/PackageDetails.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
                    break;
                case "分组信息":
                    if (currentTab != "分组信息")
                        window.location.href = '/Package/PACKAGE_GROUPS.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
                    break;
                case "设计信息":
                    if (currentTab != "设计信息")
                        window.location.href = '/Package/PACKAGE_DESIGN_INFO.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
                    break;
                case "工序信息":
                    if (currentTab != "工序信息")
                        window.location.href = '/Package/PACKAGE_FLOW_INFO.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
                    break;
                case "工序明细":
                    if (currentTab != "工序明细")
                        window.location.href = '/Package/PackageProcessDetails.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
                    break;
            }
        }
    });
    if (currentTab != "基本信息") {
        var ViewModel = function () {
            this.PACKAGE_NO = ko.observable(packageNo);
            this.VERSION_NO = ko.observable(versionNo);
            this.FACTORY_ID = ko.observable(factoryId);
            this.PRODUCT_TYPE_ID = ko.observable(productTypeId);
            this.PRODUCT_PROC_TYPE_ID = ko.observable(produceProcTypeId);
        };
        ko.applyBindings(new ViewModel());
    }
}