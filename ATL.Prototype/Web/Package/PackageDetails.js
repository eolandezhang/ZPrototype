var factoryId;
var packageNo;
var versionNo;
var productTypeId;
var produceProcTypeId;
var queryStr = ' ';
$(function () {
    packageNo = $.request.queryString["packageNo"];
    factoryId = $.request.queryString["factoryId"];
    versionNo = $.request.queryString["versionNo"];
    productTypeId = $.request.queryString["productTypeId"];
    produceProcTypeId = $.request.queryString["produceProcTypeId"];
    document.title = packageNo + '-' + versionNo + '基本信息';
    //Init_Item_Links();
    InitTabs("基本信息", packageNo, factoryId, versionNo, productTypeId, produceProcTypeId);
    InitPackageBaseInfo();
    Init_Table_PACKAGE_GROUPS();
    //Init_tt(factoryId, packageNo, versionNo);
    Dialog_PACKAGE_BASE_INFO();
    $('#update').click(function () {
        Init_Edit();
        $('#Dialog_PACKAGE_BASE_INFO').dialog('open');
    });
    $('#ORDER_TYPE').bind('change', function () {
        if ($(this).val() == 'SO#') {
            $('#SO_NO').attr('readonly', false);
        } else {
            $('#SO_NO').val('').attr('readonly', true);
        }
    });

    //var ViewModel = function () {
    //    this.PACKAGE_NO = ko.observable(packageNo);
    //    this.VERSION_NO = ko.observable(versionNo);
    //    this.FACTORY_ID = ko.observable(factoryId);
    //    this.PRODUCT_TYPE_ID = ko.observable(productTypeId);
    //    this.PRODUCT_PROC_TYPE_ID = ko.observable(produceProcTypeId);
    //};
    //ko.applyBindings(new ViewModel());
});

function Init_Table_PACKAGE_GROUPS() {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_GROUPS/GetData',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo,
            'queryStr': ''
        },
        dataType: 'json',
        success: function (data) {
            if (data.length == 0) {
                $('#tt').tabs('disableTab', '设计信息');
                $('#tt').tabs('disableTab', '工序信息');
                //$('#tt').tabs('disableTab', '工序明细');
            } else {
                $('#tt').tabs('enableTab', '设计信息');
                $('#tt').tabs('enableTab', '工序信息');
                //$('#tt').tabs('enableTab', '工序明细');
            }
        }
    });
}
//function Init_Item_Links() {
//    $('#btn_PACKAGE_DESIGN_INFO').click(function () {
//        window.open('/Package/PACKAGE_DESIGN_INFO.aspx?PACKAGE_NO=' + packageNo + '&FACTORY_ID=' + factoryId + '&VERSION_NO=' + versionNo, '_blank');
//    });
//}
function InitPackageBaseInfo() {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_BASE_INFO/GetDataById',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo
        },
        dataType: 'json',
        success: function (data) {
            ko.applyBindings(new ViewModel(data));
        }
    });
}
var ViewModel = function (d) {
    this.PACKAGE_TYPE_ID = ko.observable(d.PACKAGE_TYPE_ID);
    this.EFFECT_DATE = ko.observable(d.EFFECT_DATE == null ? ' ' : d.EFFECT_DATE);
    this.PACKAGE_NO = ko.observable(d.PACKAGE_NO);
    this.VERSION_NO = ko.observable(d.VERSION_NO);
    this.FACTORY_ID = ko.observable(d.FACTORY_ID);
    this.GROUPS = ko.observable(d.GROUPS == null ? ' ' : d.GROUPS);
    this.GROUP_NO_LIST = ko.observable(d.GROUP_NO_LIST == null ? ' ' : d.GROUP_NO_LIST);
    this.GROUP_QTY_LIST = ko.observable(d.GROUP_QTY_LIST == null ? ' ' : d.GROUP_QTY_LIST);
    this.GROUPS_PURPOSE = ko.observable(d.GROUPS_PURPOSE == null ? ' ' : d.GROUPS_PURPOSE);
    this.PRODUCT_TYPE_ID = ko.observable(d.PRODUCT_TYPE_ID == null ? ' ' : d.PRODUCT_TYPE_ID);
    this.PRODUCT_PROC_TYPE_ID = ko.observable(d.PRODUCT_PROC_TYPE_ID == null ? ' ' : d.PRODUCT_PROC_TYPE_ID);
    this.BATTERY_MODEL = ko.observable(d.BATTERY_MODEL == null ? ' ' : d.BATTERY_MODEL);
    this.BATTERY_TYPE = ko.observable(d.BATTERY_TYPE == null ? ' ' : d.BATTERY_TYPE);
    this.BATTERY_LAYERS = ko.observable(d.BATTERY_LAYERS == null ? ' ' : d.BATTERY_LAYERS);
    this.BATTERY_QTY = ko.observable(d.BATTERY_QTY == null ? ' ' : d.BATTERY_QTY);
    this.BATTERY_PARTNO = ko.observable(d.BATTERY_PARTNO == null ? ' ' : d.BATTERY_PARTNO);
    this.PROJECT_CODE = ko.observable(d.PROJECT_CODE == null ? ' ' : d.PROJECT_CODE);
    this.CUSTOMER_CODE = ko.observable(d.CUSTOMER_CODE == null ? ' ' : d.CUSTOMER_CODE);
    this.PURPOSE = ko.observable(d.PURPOSE == null ? ' ' : d.PURPOSE);
    this.ORDER_TYPE = ko.observable(d.ORDER_TYPE == null ? ' ' : d.ORDER_TYPE);
    this.SO_NO = ko.observable(d.SO_NO == null ? ' ' : d.SO_NO);
    this.IS_URGENT = ko.observable(d.IS_URGENT);
    this.OUTPUT_TARGET_DATE = ko.observable(d.OUTPUT_TARGET_DATE == null ? ' ' : d.OUTPUT_TARGET_DATE);
    this.REASON_FORURGENT = ko.observable(d.REASON_FORURGENT == null ? ' ' : d.REASON_FORURGENT);
    this.PREPARED_BY = ko.observable(d.PREPARED_BY == null ? ' ' : d.PREPARED_BY);
    this.PREPARED_DATE = ko.observable(d.PREPARED_DATE == null ? ' ' : d.PREPARED_DATE);
    this.APPROVE_FLOW_ID = ko.observable(d.APPROVE_FLOW_ID == null ? ' ' : d.APPROVE_FLOW_ID);
    this.UPDATE_USER = ko.observable(d.UPDATE_USER);
    this.UPDATE_DATE = ko.observable(d.UPDATE_DATE);
    this.VALID_FLAG = ko.observable(d.VALID_FLAG);
    this.DELETE_FLAG = ko.observable(d.DELETE_FLAG);
    this.PRODUCT_CHANGE_HL = ko.observable(d.PRODUCT_CHANGE_HL);
    this.PROCESS_CHANGE_HL = ko.observable(d.PROCESS_CHANGE_HL);
    this.MATERIAL_CHANGE_HL = ko.observable(d.MATERIAL_CHANGE_HL);
    this.OTHER_CHANGE_HL = ko.observable(d.OTHER_CHANGE_HL);

    //this.fullName = ko.computed(function () {
    //    // Knockout tracks dependencies automatically. It knows that fullName depends on firstName and lastName, because these get called when evaluating fullName.
    //    return this.firstName() + " " + this.lastName();
    //}, this);
};


//初始化工序明细标签
function Init_tt(factoryId, packageNo, versionNo) {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetDataByPackageId',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo
        },
        dataType: 'json',
        success: function (data) {
            if (data.length == 0) {
                $('#tt').tabs('disableTab', '工序明细');
            } else {
                $('#tt').tabs('enableTab', '工序明细');
            }
        }
    });
}

function Init_Edit() {
    var j = {
        'factoryId': factoryId,
        'packageNo': packageNo,
        'versionNo': versionNo
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_BASE_INFO/GetDataById',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#PACKAGE_TYPE_ID').val(data.PACKAGE_TYPE_ID).attr('readonly', true).css('border', 'none');
            $('#FACTORY_ID').val(data.FACTORY_ID).attr('readonly', true).css('border', 'none');
            $('#PACKAGE_NO').val(data.PACKAGE_NO).attr('readonly', true).css('border', 'none');
            $('#VERSION_NO').val(data.VERSION_NO).attr('readonly', true).css('border', 'none');
            $('#PRODUCT_TYPE_ID').val(data.PRODUCT_TYPE_ID).attr('readonly', true).css('border', 'none');
            $('#PRODUCT_PROC_TYPE_ID').val(data.PRODUCT_PROC_TYPE_ID).attr('readonly', true).css('border', 'none');
            Init_BATTERY_MODEL();
            Init_BATTERY_PARTNO();
            Init_PROJECT_CODE();
            Init_CUSTOMER_CODE();
            Init_APPROVE_FLOW_ID();
            $('#BATTERY_MODEL').combobox('setValue', data.BATTERY_MODEL);
            $('#BATTERY_PARTNO').combobox('setValue', data.BATTERY_PARTNO);
            $('#PROJECT_CODE').combobox('setValue', data.PROJECT_CODE);
            $('#CUSTOMER_CODE').combobox('setValue', data.CUSTOMER_CODE);
            $('#APPROVE_FLOW_ID').combobox('setValue', data.APPROVE_FLOW_ID);
            $('#BATTERY_TYPE').val(data.BATTERY_TYPE);
            $('#BATTERY_LAYERS').val(data.BATTERY_LAYERS);
            $('#BATTERY_QTY').val(data.BATTERY_QTY).attr('readonly', true).css('border', 'none');;
            $('#ORDER_TYPE').val(data.ORDER_TYPE);
            $('#SO_NO').val(data.SO_NO);
            $('#PURPOSE').val(data.PURPOSE);
            $('#IS_URGENT').val(data.IS_URGENT);
            $('#REASON_FORURGENT').val(data.REASON_FORURGENT);
            $('#OUTPUT_TARGET_DATE').datebox('setValue', data.OUTPUT_TARGET_DATE);
            $('#EFFECT_DATE').val(data.EFFECT_DATE).attr('readonly', true).css('border', 'none');;
            $('#PREPARED_BY').val(data.PREPARED_BY).css('border', 'none').attr('readonly', true);
            $('#PREPARED_DATE').val(data.PREPARED_DATE).css('border', 'none').attr('readonly', true);
            $('#GROUPS_PURPOSE').val(data.GROUPS_PURPOSE);
            $('#VALID_FLAG_PACKAGE_BASE_INFO').val(data.VALID_FLAG);
            $('#DELETE_FLAG').val(data.DELETE_FLAG);
            $('#STATUS').val(data.STATUS);
            $('#UPDATE_USER_PACKAGE_BASE_INFO').val(data.UPDATE_USER).attr('readonly', true).css('border', 'none');
            $('#UPDATE_DATE_PACKAGE_BASE_INFO').val(data.UPDATE_DATE).attr('readonly', true).css('border', 'none');
            $('#PRODUCT_CHANGE_HL').val(data.PRODUCT_CHANGE_HL);
            $('#PROCESS_CHANGE_HL').val(data.PROCESS_CHANGE_HL);
            $('#MATERIAL_CHANGE_HL').val(data.MATERIAL_CHANGE_HL);
            $('#OTHER_CHANGE_HL').val(data.OTHER_CHANGE_HL);
            InitWf();
        }
    });
}
function Dialog_PACKAGE_BASE_INFO() {
    $('#Dialog_PACKAGE_BASE_INFO').dialog({
        modal: false,
        width: 700,
        height: 460,
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                Edit_PACKAGE_BASE_INFO();
            }
        }]
    });
}
function Validate_PACKAGE_BASE_INFO() {
    var valid = false;
    //必填项        
    if ($('#PACKAGE_NO').val() == '' ||
        $('#FACTORY_ID').val() == '' ||
        $('#VERSION_NO').val() == '' ||
        $('#PRODUCT_TYPE_ID').val() == '' ||
        $('#PRODUCT_PROC_TYPE_ID').val() == '' ||
        $('#BATTERY_MODEL').combobox('getValue') == '' ||
        $('#BATTERY_LAYERS').val() == '' ||
        //$('#BATTERY_QTY').val() == '' ||
        $('#ORDER_TYPE').val() == '' ||
        $('#PROJECT_CODE').combobox('getValue') == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写必填项',
            showType: 'show'
        });
        valid = false;
        return valid;
    }

    //验证输入合法性   
    //var v_BATTERY_TYPE = $('#BATTERY_TYPE').validatebox('isValid');
    var v_BATTERY_LAYERS = $('#BATTERY_LAYERS').validatebox('isValid');
    //var v_BATTERY_QTY = $('#BATTERY_QTY').validatebox('isValid');
    //var v_ORDER_TYPE = $('#ORDER_TYPE').validatebox('isValid');
    var v_SO_NO = $('#SO_NO').validatebox('isValid');
    var v_PURPOSE = $('#PURPOSE').validatebox('isValid');
    var v_REASON_FORURGENT = $('#REASON_FORURGENT').validatebox('isValid');
    var v_GROUPS_PURPOSE = $('#GROUPS_PURPOSE').validatebox('isValid');
    var v_PRODUCT_CHANGE_HL = $('#PRODUCT_CHANGE_HL').validatebox('isValid');
    var v_PROCESS_CHANGE_HL = $('#PROCESS_CHANGE_HL').validatebox('isValid');
    var v_MATERIAL_CHANGE_HL = $('#MATERIAL_CHANGE_HL').validatebox('isValid');
    var v_OTHER_CHANGE_HL = $('#OTHER_CHANGE_HL').validatebox('isValid');
    if (!(
        //v_BATTERY_TYPE &&
        //v_BATTERY_LAYERS &&
        //v_BATTERY_QTY &&
        //v_ORDER_TYPE &&
        v_SO_NO &&
        v_PURPOSE &&
        v_REASON_FORURGENT &&
        v_GROUPS_PURPOSE &&
        v_PRODUCT_CHANGE_HL &&
        v_PROCESS_CHANGE_HL &&
        v_MATERIAL_CHANGE_HL &&
        v_OTHER_CHANGE_HL
        )) {
        $.messager.show({
            title: '消息',
            msg: '请按照提示填写',
            showType: 'show'
        });
        valid = false;
        return valid;
    }


    var msg = "";
    var flag = true;
    //验证存在
    if ($('#BATTERY_PARTNO').combobox('getValue') != '') {
        if (!Valid_BATTERY_PARTNO()) {
            msg += '电池料号不存在<br/>';
            flag = flag && false;
        }
    }
    if ($('#BATTERY_MODEL').combobox('getValue') != '') {
        if (!Valid_BATTERY_MODEL()) {
            msg += '品种不存在<br/>';
            flag = flag && false;
        }
    }
    if ($('#PROJECT_CODE').combobox('getValue') != '') {
        if (!Valid_PROJECT_CODE()) {
            $.messager.show({
                title: '消息',
                msg: '输入不正确',
                showType: 'show'
            });
            msg += '项目代码不存在<br/>';
            flag = flag && false;
        }
    }
    if ($('#CUSTOMER_CODE').combobox('getValue') != '') {
        if (!Valid_CUSTOMER_CODE()) {
            $.messager.show({
                title: '消息',
                msg: '输入不正确',
                showType: 'show'
            });
            msg += '客户代码不存在<br/>';
            flag = flag && false;
        }
    }
    if (flag) {
        valid = true;
        return valid;
    } else {
        $.messager.show({
            title: '消息',
            msg: msg,
            showType: 'show'
        });
        return false;
    }

}
function Edit_PACKAGE_BASE_INFO() {
    if (!Validate_PACKAGE_BASE_INFO()) {
        return;
    }
    var j = {
        'PACKAGE_NO': $('#PACKAGE_NO').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'VERSION_NO': $('#VERSION_NO').val(),
        'GROUPS_PURPOSE': $('#GROUPS_PURPOSE').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').val(),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').val(),
        'PACKAGE_TYPE_ID': $('#PACKAGE_TYPE_ID').val(),
        'EFFECT_DATE': $('#EFFECT_DATE').val(),
        'BATTERY_MODEL': $('#BATTERY_MODEL').combobox('getValue'),
        'BATTERY_TYPE': $('#BATTERY_TYPE').val(),
        'BATTERY_LAYERS': $('#BATTERY_LAYERS').val(),
        'BATTERY_QTY': $('#BATTERY_QTY').val(),
        'BATTERY_PARTNO': $('#BATTERY_PARTNO').combobox('getValue').toUpperCase(),
        'PROJECT_CODE': $('#PROJECT_CODE').combobox('getValue').toUpperCase(),
        'CUSTOMER_CODE': $('#CUSTOMER_CODE').combobox('getValue'),
        'PURPOSE': $('#PURPOSE').val(),
        'ORDER_TYPE': $('#ORDER_TYPE').val(),
        'SO_NO': $('#SO_NO').val(),
        'IS_URGENT': $('#IS_URGENT').val(),
        'OUTPUT_TARGET_DATE': $('#OUTPUT_TARGET_DATE').datebox('getValue'),
        'REASON_FORURGENT': $('#REASON_FORURGENT').val(),
        'PREPARED_BY': $('#PREPARED_BY').val(),
        'PREPARED_DATE': $('#PREPARED_DATE').val(),
        'APPROVE_FLOW_ID': $('#APPROVE_FLOW_ID').combobox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_BASE_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_BASE_INFO').val(),
        'VALID_FLAG': $('#VALID_FLAG_PACKAGE_BASE_INFO').val(),
        'DELETE_FLAG': $('#DELETE_FLAG').val(),
        'STATUS': $('#STATUS').val(),
        'PRODUCT_CHANGE_HL': $('#PRODUCT_CHANGE_HL').val(),
        'PROCESS_CHANGE_HL': $('#PROCESS_CHANGE_HL').val(),
        'MATERIAL_CHANGE_HL': $('#MATERIAL_CHANGE_HL').val(),
        'OTHER_CHANGE_HL': $('#OTHER_CHANGE_HL').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BASE_INFO/PostEdit',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                history.go(0);
                $('#Dialog_PACKAGE_BASE_INFO').dialog('close');
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}

//品种（下拉框）
function Init_BATTERY_MODEL() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $.ajax({
        type: 'get',
        url: '/api/PRODUCT_MODEL_LIST/GetDataByType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId,
        async: false,
        success: function (data) {
            $('#BATTERY_MODEL').combobox({
                panelHeight: 100,
                valueField: 'PRODUCT_MODEL_ID',
                textField: 'PRODUCT_MODEL_ID',
                data: data,
                editable: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var x = data;
                        var flag = false;
                        $.each(x, function (i) {
                            if (x[i].PRODUCT_MODEL_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#BATTERY_MODEL').combobox('setValue', x[i].PRODUCT_MODEL_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_BATTERY_MODEL').hide();
                        } else {
                            $('#alert_BATTERY_MODEL').show();
                        }
                    }
                }
            });
        }
    });

}
function Valid_BATTERY_MODEL() {
    var productModelId = $('#BATTERY_MODEL').combobox('getValue');
    var factoryId = $('#FACTORY_ID').val();
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (productModelId == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'PRODUCT_MODEL_ID': productModelId.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/PRODUCT_MODEL_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                r = true;
            } else {
                r = false;
            }
        }
    });
    return r;
}
//电池料号
function Init_BATTERY_PARTNO() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PN_LIST/GetDataByCategoryId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId + '&MATERIAL_CATEGORY_ID=GC-S',
        async: false,
        success: function (data) {
            $('#BATTERY_PARTNO').combobox({
                panelHeight: 100,
                valueField: 'MATERIAL_PN_ID',
                textField: 'MATERIAL_PN_ID',
                data: data,
                editable: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var flag = false;
                        var x = data;
                        $.each(x, function (i) {
                            if (x[i].MATERIAL_PN_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#BATTERY_PARTNO').combobox('setValue', x[i].MATERIAL_PN_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_BATTERY_PARTNO').hide();
                        } else {
                            $('#alert_BATTERY_PARTNO').show();
                        }
                    }
                }
            });
        }
    });
}
function Valid_BATTERY_PARTNO() {
    var batteryPartNo = $('#BATTERY_PARTNO').combobox('getValue');
    var factoryId = $('#FACTORY_ID').val();
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (batteryPartNo == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'MATERIAL_PN_ID': batteryPartNo.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId,
        'MATERIAL_CATEGORY_ID': 'GC-S'
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PN_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {

            if (data) {
                r = true;
            } else {
                r = false;
            }

        }
    });
    return r;
}
//项目代码（下拉框）
function Init_PROJECT_CODE() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $.ajax({
        type: 'get',
        url: '/api/PROJ_CODE_LIST/GetDataByType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId,
        async: false,
        success: function (data) {
            $('#PROJECT_CODE').combobox({
                panelHeight: 100,
                valueField: 'PROJ_CODE_ID',
                textField: 'PROJ_CODE_ID',
                data: data,
                editable: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var flag = false;
                        var x = data;
                        $.each(x, function (i) {
                            if (x[i].PROJ_CODE_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#PROJECT_CODE').combobox('setValue', x[i].PROJ_CODE_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_PROJECT_CODE').hide();
                        } else {
                            $('#alert_PROJECT_CODE').show();
                        }
                    }
                }
            });
        }
    });
}
function Valid_PROJECT_CODE() {
    var projCodeId = $('#PROJECT_CODE').combobox('getValue');
    var factoryId = $('#FACTORY_ID').val();
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (projCodeId == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'PROJ_CODE_ID': projCodeId.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/PROJ_CODE_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                r = true;
            } else {
                r = false;
            }

        }
    });
    return r;
}
//客户代码（下拉框）
function Init_CUSTOMER_CODE() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $.ajax({
        type: 'get',
        url: '/api/CUSTOMER_CODE_LIST/GetDataByType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId,
        async: false,
        success: function (data) {
            $('#CUSTOMER_CODE').combobox({
                panelHeight: 100,
                valueField: 'CUSTOMER_CODE_ID',
                textField: 'CUSTOMER_CODE_ID',
                data: data,
                editable: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var flag = false;
                        var x = data;
                        $.each(x, function (i) {
                            if (x[i].CUSTOMER_CODE_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#CUSTOMER_CODE').combobox('setValue', x[i].CUSTOMER_CODE_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_CUSTOMER_CODE').hide();
                        } else {
                            $('#alert_CUSTOMER_CODE').show();
                        }
                    }
                }
            });
        }
    });
}
function Valid_CUSTOMER_CODE() {
    var customerCodeId = $('#CUSTOMER_CODE').combobox('getValue');
    var factoryId = $('#FACTORY_ID').val();
    var productTypeId = $('#PRODUCT_TYPE_ID').val();
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').val();
    if (customerCodeId == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'CUSTOMER_CODE_ID': customerCodeId.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/CUSTOMER_CODE_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                r = true;
            } else {
                r = false;
            }

        }
    });
    return r;
}
//审批流程
function Init_APPROVE_FLOW_ID() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    if (factoryId == '') {
        return;
    }
    $('#APPROVE_FLOW_ID').combobox({
        panelHeight: 100,
        valueField: 'WF_SET_NUM',
        textField: 'WF_SET_NAME',
        method: 'get',
        url: '/api/WF_SET/GetDataByFactoryId?FACTORY_ID=' + factoryId + '&queryStr=',
        editable: false
    });
}

function InitWf() {
    var j = {
        'PACKAGE_NO': $('#PACKAGE_NO').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'VERSION_NO': $('#VERSION_NO').val()
    };
    $.ajax({
        type: 'get',
        url: '/api/Preview/HasBeginWf',
        data: j,
        dataType: 'json',
        success: function (data) {
            if (data) { $('#APPROVE_FLOW_ID').combobox('readonly', true); }
            else { $('#APPROVE_FLOW_ID').combobox('readonly', false); }
        }
    });
}