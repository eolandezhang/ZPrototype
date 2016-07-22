var pageSize = 20;
var factoryId;
var packageNo;
var versionNo;
var productTypeId;
var produceProcTypeId;
var queryStr = ' ';

var editIndex_PACKAGE_GROUPS = undefined;
var addOrEdit_PACKAGE_GROUPS = null;

var editIndex_PACKAGE_DESIGN_INFO = undefined;
var addOrEdit_PACKAGE_DESIGN_INFO = null;

var editIndex_PACKAGE_FLOW_INFO = undefined;
var addOrEdit_PACKAGE_FLOW_INFO = null;

var editIndex_PACKAGE_PARAM_SPEC_INFO = undefined;
var addOrEdit_PACKAGE_PARAM_SPEC_INFO = null;

var editIndex_PACKAGE_PARAM_SETTING = undefined;
var addOrEdit_PACKAGE_PARAM_SETTING = null;

var editIndex_PACKAGE_PROC_MATERIAL_INFO = undefined;
var addOrEdit_PACKAGE_PROC_MATERIAL_INFO = null;

var editIndex_PACKAGE_PROC_PN_INFO = undefined;
var addOrEdit_PACKAGE_PROC_PN_INFO = null;

var editIndex_PACKAGE_PROC_EQUIP_CLASS_INFO = undefined;
var addOrEdit_PACKAGE_PROC_EQUIP_CLASS_INFO = null;

var editIndex_PACKAGE_PROC_EQUIP_INFO = undefined;
var addOrEdit_PACKAGE_PROC_EQUIP_INFO = null;

var editIndex_PACKAGE_ILLUSTRATION_INFO = undefined;
var addOrEdit_PACKAGE_ILLUSTRATION_INFO = null;

var editIndex_PACKAGE_BOM_SPEC_INFO = undefined;
var addOrEdit_PACKAGE_BOM_SPEC_INFO = null;

var editIndex_PACKAGE_PROC_GRP = undefined;
var addOrEdit_PACKAGE_PROC_GRP = null;

var editIndex_PACKAGE_PROC_GRP_LIST = undefined;
var addOrEdit_PACKAGE_PROC_GRP_LIST = null;

var PROCESS_ID;
var ILLUSTRATION_ID;
var ILLUSTRATION_DESC;
var VALID_FLAG;
var GROUPS;
var GROUP_NO;

var QueryStr_ParamType;

var ParamType = {
    "PRODUCT_PROCESS": "PRODUCT_PROCESS",
    "EQUIP_CLASS": "EQUIP_CLASS",
    "EQUIP_INFO": "EQUIP_INFO",
    "IMG": "IMG",
    "MATERIAL_TYPE": "MATERIAL_TYPE",
    "MATERIAL_PN": "MATERIAL_PN"
};
var CurrentParamType;

$(function () {
    packageNo = $.request.queryString["packageNo"];
    factoryId = $.request.queryString["factoryId"];
    versionNo = $.request.queryString["versionNo"];
    productTypeId = $.request.queryString["productTypeId"];
    produceProcTypeId = $.request.queryString["produceProcTypeId"];
    document.title = packageNo + '-' + versionNo + '工序明细';
    InitTabs();
    //InitTabs("工序明细", packageNo, factoryId, versionNo, productTypeId, produceProcTypeId);
    var ViewModel = function () {
        this.PACKAGE_NO = ko.observable(packageNo);
        this.VERSION_NO = ko.observable(versionNo);
        this.FACTORY_ID = ko.observable(factoryId);
        this.PRODUCT_TYPE_ID = ko.observable(productTypeId);
        this.PRODUCT_PROC_TYPE_ID = ko.observable(produceProcTypeId);
    };
    ko.applyBindings(new ViewModel());
    InitBtnWf();
});
function InitTabs() {
    $('#tt').tabs({
        onSelect: function (title) {
            $('.easyui-dialog').dialog('close');
            switch (title) {
                case "基本信息":
                    InitPackageBaseInfo();
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
                    break;
                case "分组信息":
                    Init_Table_PACKAGE_GROUPS();
                    Dialog_PACKAGE_GROUPS();
                    $('#GROUP_NO').bind('keyup', function () {
                        var v = $(this).val().toUpperCase();
                        $(this).val(v);
                    });
                    break;
                case "设计信息":
                    Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
                    Dialog_PACKAGE_DESIGN_INFO();

                    Init_btn_ANODE_STUFF_ID('ANODE_STUFF_ID', 'A-001', '阳极材料');
                    Init_btn_ANODE_STUFF_ID('CATHODE_STUFF_ID', 'C-001', '阴极材料');
                    Init_btn_ANODE_STUFF_ID('ANODE_FOIL_ID', 'A-002', '阳极集流体材料');
                    Init_btn_ANODE_STUFF_ID('CATHODE_FOIL_ID', 'C-002', '阴极集流体材料');
                    Init_btn_ANODE_STUFF_ID('SEPARATOR_ID', 'S-001', '隔离膜材料');
                    Init_btn_ANODE_STUFF_ID('ELECTROLYTE_ID', 'E-001', '电解液配方');
                    Init_btn_ANODE_FORMULA_ID('ANODE_FORMULA_ID', 'AMIX', '阳极配方');
                    Init_btn_ANODE_FORMULA_ID('CATHODE_FORMULA_ID', 'CMIX', '阴极配方');
                    $('.checkInfo').attr('checked', false);
                    Init_AllCheckbox();
                    break;
                case "工序明细":
                    Table_PROCESS_LIST();
                    Init_Table_PROCESS_LIST();
                    Dialog_PACKAGE_FLOW_INFO();
                    Table_PACKAGE_FLOW_INFO([]);
                    Init_PROCESS_ID();
                    Dialog_PACKAGE_FLOW_INFO_BatchAdd();

                    //Table_PROCESS_LIST_PACKAGE();
                    //Init_Table_PROCESS_LIST_PACKAGE();

                    //Table_GROUP_NO_PACKAGE_PROCESS();

                    Table_PACKAGE_PARAM_SETTING();
                    Dialog_PACKAGE_PARAM_SETTING();
                    Dialog_PACKAGE_PARAM_SETTING_BatchAdd();

                    Table_PACKAGE_PARAM_SPEC_INFO();
                    Dialog_PACKAGE_PARAM_SPEC_INFO();

                    Dialog_PACKAGE_PROC_MATERIAL_INFO();
                    Dialog_PACKAGE_PROC_PN_INFO();
                    Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO();
                    Dialog_PACKAGE_PROC_EQUIP_INFO();
                    Dialog_PACKAGE_ILLUSTRATION_INFO();

                    Dialog_PACKAGE_BOM_SPEC_INFO();

                    $('#Param').hide();
                    $('#panel_showimg').panel('close');

                    Init_tt1();
                    Dialog_PACKAGE_PROC_GRP();
                    Dialog_PACKAGE_PROC_GRP_LIST();
                    break;
            }
        }
    });
}
function Init_tt1() {
    $('#tt1').tabs('disableTab', '产品及工艺参数信息');
    $('#tt1').tabs('disableTab', '物料信息');
    $('#tt1').tabs('disableTab', '设备信息');
    $('#tt1').tabs('disableTab', '附图信息');
    $('#tt1').tabs('disableTab', 'BOM信息');
    $('#tt1').tabs('disableTab', '大分组');
    $('#tt1').tabs({
        onSelect: function (title) {
            switch (title) {
                case "大分组":
                    $('#Param').hide();
                    $('#panel_showimg').panel('close');
                    Table_PACKAGE_PROC_GRP_();
                    Table_PACKAGE_PROC_GRP_LIST_();
                    break;
                case "产品及工艺参数信息":
                    CurrentParamType = ParamType.PRODUCT_PROCESS;
                    Init_Table_PACKAGE_PARAM_SETTING();
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#Param').show();
                    $('#panel_showimg').panel('close');
                    break;
                case "物料信息":
                    Table_PACKAGE_PROC_MATERIAL_INFO();
                    Init_Table_PACKAGE_PROC_MATERIAL_INFO();
                    Table_PACKAGE_PROC_PN_INFO();
                    Init_Table_PACKAGE_PROC_PN_INFO();
                    MATERIAL_CATEGORY_ID();
                    $('#Param').show();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#panel_showimg').panel('close');

                    break;
                case "设备信息":
                    CurrentParamType = null;
                    Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                    Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                    Table_PACKAGE_PROC_EQUIP_INFO();
                    Init_Table_PACKAGE_PROC_EQUIP_INFO();
                    EQUIPMENT_TYPE_ID();
                    $('#Param').show();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#panel_showimg').panel('close');
                    break;
                case "附图信息":
                    CurrentParamType = null;
                    Table_PACKAGE_ILLUSTRATION_INFO();
                    Init_Table_PACKAGE_ILLUSTRATION_INFO();
                    $('#Param').show();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#panel_showimg').panel('open');
                    break;
                case "BOM信息":
                    Table_PACKAGE_BOM_SPEC_INFO();
                    Init_Table_PACKAGE_BOM_SPEC_INFO();
                    $('#Param').hide();
                    $('#panel_showimg').panel('close');
                    break;

            }
        }
    });
}
/***********************************基本信息***********************************/
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
            $('#txt_FACTORY_ID').html(data.FACTORY_ID);
            $('#txt_PACKAGE_TYPE_ID').html(data.PACKAGE_TYPE_ID);
            $('#txt_EFFECT_DATE').html(data.EFFECT_DATE);
            $('#txt_PACKAGE_NO').html(data.PACKAGE_NO);
            $('#txt_VERSION_NO').html(data.VERSION_NO);
            $('#txt_ORDER_TYPE').html(data.ORDER_TYPE);
            $('#txt_SO_NO').html(data.SO_NO);
            $('#txt_GROUPS').html(data.GROUPS);
            $('#txt_GROUP_NO_LIST').html(data.GROUP_NO_LIST);
            $('#txt_GROUP_QTY_LIST').html(data.GROUP_QTY_LIST);
            $('#txt_PURPOSE').html(data.PURPOSE);
            $('#txt_GROUPS_PURPOSE').html(data.GROUPS_PURPOSE);
            $('#txt_PRODUCT_TYPE_ID').html(data.PRODUCT_TYPE_ID);
            $('#txt_PRODUCT_PROC_TYPE_ID').html(data.PRODUCT_PROC_TYPE_ID);
            $('#txt_BATTERY_TYPE').html(data.BATTERY_TYPE);
            $('#txt_BATTERY_MODEL').html(data.BATTERY_MODEL);
            $('#txt_BATTERY_LAYERS').html(data.BATTERY_LAYERS);
            $('#txt_BATTERY_QTY').html(data.BATTERY_QTY);
            $('#txt_PROJECT_CODE').html(data.PROJECT_CODE);
            $('#txt_BATTERY_PARTNO').html(data.BATTERY_PARTNO);
            $('#txt_CUSTOMER_CODE').html(data.CUSTOMER_CODE);
            $('#txt_OUTPUT_TARGET_DATE').html(data.OUTPUT_TARGET_DATE);
            $('#txt_IS_URGENT').html(data.IS_URGENT);
            $('#txt_REASON_FORURGENT').html(data.REASON_FORURGENT);
            $('#txt_PREPARED_BY').html(data.PREPARED_BY);
            $('#txt_PREPARED_DATE').html(data.PREPARED_DATE);
            $('#txt_PRODUCT_CHANGE_HL').html(data.PRODUCT_CHANGE_HL);
            $('#txt_PROCESS_CHANGE_HL').html(data.PROCESS_CHANGE_HL);
            $('#txt_MATERIAL_CHANGE_HL').html(data.MATERIAL_CHANGE_HL);
            $('#txt_OTHER_CHANGE_HL').html(data.OTHER_CHANGE_HL);
            $('#txt_UPDATE_USER').html(data.UPDATE_USER);
            $('#txt_UPDATE_DATE').html(data.UPDATE_DATE);
            $('#txt_VALID_FLAG').html(data.VALID_FLAG);
            $('#txt_DELETE_FLAG').html(data.DELETE_FLAG);
            $('#txt_APPROVE_FLOW_ID').html(data.APPROVE_FLOW_ID);
        }
    });
}

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
            InitWf();
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
            $('#VALID_FLAG_PACKAGE_BASE_INFO').val(data.VALID_FLAG).attr('disabled', true);
            $('#DELETE_FLAG').val(data.DELETE_FLAG);
            $('#STATUS').val(data.STATUS).attr('disabled', true);
            $('#UPDATE_USER_PACKAGE_BASE_INFO').val(data.UPDATE_USER).attr('readonly', true).css('border', 'none');
            $('#UPDATE_DATE_PACKAGE_BASE_INFO').val(data.UPDATE_DATE).attr('readonly', true).css('border', 'none');
            $('#PRODUCT_CHANGE_HL').val(data.PRODUCT_CHANGE_HL);
            $('#PROCESS_CHANGE_HL').val(data.PROCESS_CHANGE_HL);
            $('#MATERIAL_CHANGE_HL').val(data.MATERIAL_CHANGE_HL);
            $('#OTHER_CHANGE_HL').val(data.OTHER_CHANGE_HL);

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
        return false;
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
        return false;
    }
    return true;
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
                InitPackageBaseInfo();
                $('#Dialog_PACKAGE_BASE_INFO').dialog('close');
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == -2) {
                $.messager.show({
                    title: '消息',
                    msg: '输入不正确',
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
/***********************************分组信息***********************************/
function Table_PACKAGE_GROUPS(data) {
    $('#Table_PACKAGE_GROUPS').datagrid({
        title: '',
        singleSelect: true,
        width: '966',
        height: '500',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_GROUPS = 'add';
                $('#GROUP_NO').val('').attr('readonly', false);
                $('#GROUP_QTY').val('').attr('readonly', false);
                $('#Dialog_PACKAGE_GROUPS').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_GROUPS = 'edit';
                var x = $('#Table_PACKAGE_GROUPS').datagrid('getSelected');
                if (x == null) return;
                $('#GROUP_NO').val(x.GROUP_NO).attr('readonly', true);
                $('#GROUP_QTY').val(x.GROUP_QTY);
                $('#Dialog_PACKAGE_GROUPS').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_GROUPS();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_GROUPS').datagrid('endEdit', editIndex_PACKAGE_GROUPS);
                var changedRow = $('#Table_PACKAGE_GROUPS').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_GROUPS(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_GROUPS = undefined;
                //$('#Table_PACKAGE_GROUPS').datagrid('clearSelections');
                $('#Table_PACKAGE_GROUPS').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_GROUPS').datagrid('rejectChanges');
            }
        }],
        rownumbers: true,
        idField: 'GROUP_NO',
        frozenColumns: [[
            {
                field: 'GROUP_NO', title: '组别', align: 'left', styler: function (value, row, index) { return 'color:blue'; }
            }
        ]],
        columns: [[
            {
                field: 'GROUP_QTY', title: '数量', align: 'left', width: 300, editor: 'numberbox'
            }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_GROUPS(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_GROUPS = index;
            row.editing = true;
            $('#Table_PACKAGE_GROUPS').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_GROUPS').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_GROUPS').datagrid('refreshRow', index);
        }
    });
    $('#Table_PACKAGE_GROUPS').datagrid("loadData", data).datagrid('acceptChanges');
}
function editrow_PACKAGE_GROUPS(index) {
    if (editIndex_PACKAGE_GROUPS != undefined)
        $('#Table_PACKAGE_GROUPS').datagrid('endEdit', editIndex_PACKAGE_GROUPS);
    $('#Table_PACKAGE_GROUPS').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_GROUPS() {
    var row = $('#Table_PACKAGE_GROUPS').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '将会删除与"' + row.GROUP_NO + '"组相关的所有信息:<br/>设计信息,工序信息,参数信息,参数设定信息,物料信息,设备及其参数,附图信息,BOM信息.<br/>确定要删除吗?', function (r) {
        if (r) {
            Delete_PACKAGE_GROUPS(row);
        }
    });
}
function Init_Table_PACKAGE_GROUPS() {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_GROUPS/GetData',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo,
            'queryStr': queryStr
        },
        dataType: 'json',
        success: function (data) {
            if (data.length == 0) {
                $('#tt').tabs('disableTab', '设计信息');
                //$('#tt').tabs('disableTab', '工序信息');
                //$('#tt').tabs('disableTab', '工序明细');
            } else {
                $('#tt').tabs('enableTab', '设计信息');
                //$('#tt').tabs('enableTab', '工序信息');
                //$('#tt').tabs('enableTab', '工序明细');
            }
            Table_PACKAGE_GROUPS(data);
        }
    });
}
function Dialog_PACKAGE_GROUPS() {
    $('#Dialog_PACKAGE_GROUPS').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_GROUPS == 'add') {
                    Add_PACKAGE_GROUPS();
                }
                else if (addOrEdit_PACKAGE_GROUPS == 'edit') {
                    Edit_PACKAGE_GROUPS();
                }
            }
        }]
    });
}
function Validate_PACKAGE_GROUPS() {
    if (!(
        $('#GROUP_NO').validatebox('isValid') &&
        $('#GROUP_QTY').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '填写不正确',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_GROUPS() {
    if (!Validate_PACKAGE_GROUPS()) return;
    var j = {
        'PACKAGE_NO': packageNo,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'GROUP_NO': $('#GROUP_NO').val().toUpperCase(),
        'GROUP_QTY': $('#GROUP_QTY').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_GROUPS/PostAdd',
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
                Init_Table_PACKAGE_GROUPS();
                //$('#Dialog_PACKAGE_GROUPS').dialog('close');
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
function Edit_PACKAGE_GROUPS() {
    if (!Validate_PACKAGE_GROUPS()) return;
    var j = {
        'PACKAGE_NO': packageNo,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'GROUP_NO': $('#GROUP_NO').val().toUpperCase(),
        'GROUP_QTY': $('#GROUP_QTY').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_GROUPS/PostEdit',
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
                Init_Table_PACKAGE_GROUPS();
                $('#Dialog_PACKAGE_GROUPS').dialog('close');
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
function Edit_Cell_PACKAGE_GROUPS(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'GROUP_NO': row.GROUP_NO,
        'GROUP_QTY': row.GROUP_QTY
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_GROUPS/PostEdit',
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
                Init_Table_PACKAGE_GROUPS()
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
                $('#Table_PACKAGE_GROUPS').datagrid('rejectChanges');
            } else {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
                $('#Table_PACKAGE_GROUPS').datagrid('rejectChanges');
            }
        }
    });
}
function Delete_PACKAGE_GROUPS(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'GROUP_NO': row.GROUP_NO
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_GROUPS/PostDelete',
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
                Init_Table_PACKAGE_GROUPS();
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
/***********************************设计信息***********************************/
function Table_PACKAGE_DESIGN_INFO(data) {
    $('#Table_PACKAGE_DESIGN_INFO').datagrid({
        title: '',
        singleSelect: true, //只能选择单行
        width: '966',
        height: '460',
        fitColumns: false,
        autoRowHeight: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_DESIGN_INFO = 'add';
                $('.checkInfo').attr('disabled', false);
                $('.checkInfo').attr('checked', false);
                GROUP_NO_PACKAGE_DESIGN_INFO();
                $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('readonly', false);
                Init_MATERIAL_PN_ID('ANODE_STUFF_ID', 'A-001');
                Init_MATERIAL_PN_ID('CATHODE_STUFF_ID', 'C-001');
                Init_MATERIAL_PN_ID('ANODE_FOIL_ID', 'A-002');
                Init_MATERIAL_PN_ID('CATHODE_FOIL_ID', 'C-002');
                Init_MATERIAL_PN_ID('SEPARATOR_ID', 'S-001');
                Init_MATERIAL_PN_ID('ELECTROLYTE_ID', 'E-001');
                Init_RECIPE_ID('ANODE_FORMULA_ID', 'AMIX');
                Init_RECIPE_ID('CATHODE_FORMULA_ID', 'CMIX');
                $('#CELL_CAP').val('').attr('readonly', false);
                $('#BEG_VOL').val('').attr('readonly', false);
                $('#END_VOL').val('').attr('readonly', false);
                $('#ANODE_COATING_WEIGHT').val('').attr('readonly', false);
                $('#ANODE_DENSITY').val('').attr('readonly', false);
                $('#CATHODE_STUFF_ID').val('').attr('readonly', true);
                $('#CATHODE_COATING_WEIGHT').val('').attr('readonly', false);
                $('#CATHODE_DENSITY').val('').attr('readonly', false);
                $('#INJECTION_QTY').val('').attr('readonly', false);
                $('#LIQUID_PER').val('').attr('readonly', false);
                $('#MODEL_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(1);
                $('#DESIGN_DATE').datebox('setValue', dateFormat(new Date(), "mm/dd/yyyy"));
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val('').attr('readonly', false);
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val('').attr('readonly', false);
                $('#ANODE_THICKNESS').val('').attr('readonly', false);
                $('#CATHODE_THICKNESS').val('').attr('readonly', false);
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('open');
                PACKAGE_NO_forInit();
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_DESIGN_INFO = 'edit';
                $('.checkInfo').attr('disabled', false);
                $('.checkInfo').attr('checked', false);
                var x = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getSelected');
                if (x == null) return;
                GROUP_NO_PACKAGE_DESIGN_INFO();
                Init_MATERIAL_PN_ID('ANODE_STUFF_ID', 'A-001');
                Init_MATERIAL_PN_ID('CATHODE_STUFF_ID', 'C-001');
                Init_MATERIAL_PN_ID('ANODE_FOIL_ID', 'A-002');
                Init_MATERIAL_PN_ID('CATHODE_FOIL_ID', 'C-002');
                Init_MATERIAL_PN_ID('SEPARATOR_ID', 'S-001');
                Init_MATERIAL_PN_ID('ELECTROLYTE_ID', 'E-001');
                Init_RECIPE_ID('ANODE_FORMULA_ID', 'AMIX');
                Init_RECIPE_ID('CATHODE_FORMULA_ID', 'CMIX');
                $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('setValue', x.GROUP_NO);
                $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('readonly', true);
                $('#CELL_CAP').val(x.CELL_CAP);
                $('#BEG_VOL').val(x.BEG_VOL);
                $('#END_VOL').val(x.END_VOL);
                $('#ANODE_STUFF_ID').combobox('setValue', x.ANODE_STUFF_ID);
                $('#ANODE_FORMULA_ID').combobox('setValue', x.ANODE_FORMULA_ID);
                $('#ANODE_COATING_WEIGHT').val(x.ANODE_COATING_WEIGHT);
                $('#ANODE_DENSITY').val(x.ANODE_DENSITY);
                $('#ANODE_FOIL_ID').combobox('setValue', x.ANODE_FOIL_ID);
                $('#CATHODE_STUFF_ID').combobox('setValue', x.CATHODE_STUFF_ID);
                $('#CATHODE_FORMULA_ID').combobox('setValue', x.CATHODE_FORMULA_ID);
                $('#CATHODE_COATING_WEIGHT').val(x.CATHODE_COATING_WEIGHT);
                $('#CATHODE_DENSITY').val(x.CATHODE_DENSITY);
                $('#CATHODE_FOIL_ID').combobox('setValue', x.CATHODE_FOIL_ID);
                $('#SEPARATOR_ID').combobox('setValue', x.SEPARATOR_ID);
                $('#ELECTROLYTE_ID').combobox('setValue', x.ELECTROLYTE_ID);
                $('#INJECTION_QTY').val(x.INJECTION_QTY);
                $('#LIQUID_PER').val(x.LIQUID_PER);
                $('#MODEL_DESC').val(x.MODEL_DESC);
                $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(x.VALID_FLAG);
                $('#DESIGN_DATE').datebox('setValue', dateFormat(x.DESIGN_DATE, "mm/dd/yyyy"));
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(x.UPDATE_USER);
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(x.UPDATE_DATE);
                $('#ANODE_THICKNESS').val(x.ANODE_THICKNESS);
                $('#CATHODE_THICKNESS').val(x.CATHODE_THICKNESS);
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('open');
                PACKAGE_NO_forInit();
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_DESIGN_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_DESIGN_INFO').datagrid('endEdit', editIndex_PACKAGE_DESIGN_INFO);
                var changedRow = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_DESIGN_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_DESIGN_INFO = undefined;
                $('#Table_PACKAGE_DESIGN_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_DESIGN_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            {
                field: 'GROUP_NO', title: '组别', align: 'left', width: 30,
                styler: function (value, row, index) { return 'color:blue'; }
            }
        ]],
        columns: [[
            {
                field: 'CELL_CAP', title: '电池容量', align: 'left', width: 55,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'BEG_VOL', title: '起始电压', align: 'left', width: 55,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'END_VOL', title: '截至电压', align: 'left', width: 55,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'ANODE_STUFF_ID', title: '阳极材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=A-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ANODE_FORMULA_ID', title: '阳极配方', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'RECIPE_ID',
                        textField: 'RECIPE_ID',
                        url: '/api/RECIPE_LIST/GetDataQuery?RECIPE_TYPE_ID=AMIX&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&RECIPE_ID=&RECIPE_NAME=&RECIPE_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ANODE_COATING_WEIGHT', title: '阳极涂布重量', align: 'left', width: 80,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'ANODE_DENSITY', title: '阳极压实密度', align: 'left', width: 80,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'ANODE_FOIL_ID', title: '阳极集流体材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=A-002&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ANODE_THICKNESS', title: '阳极集流体厚度', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'CATHODE_STUFF_ID', title: '阴极材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=C-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            }, {
                field: 'CATHODE_FORMULA_ID', title: '阴极配方', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'RECIPE_ID',
                        textField: 'RECIPE_ID',
                        url: '/api/RECIPE_LIST/GetDataQuery?RECIPE_TYPE_ID=CMIX&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&RECIPE_ID=&RECIPE_NAME=&RECIPE_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'CATHODE_COATING_WEIGHT', title: '阴极涂布重量', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'CATHODE_DENSITY', title: '阴极压实密度', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'CATHODE_FOIL_ID', title: '阴极集流体材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=C-002&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'CATHODE_THICKNESS', title: '阴极集流体厚度', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'SEPARATOR_ID', title: '隔离膜材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=S-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ELECTROLYTE_ID', title: '电解液配方', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=E-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'INJECTION_QTY', title: '注液量', align: 'left', width: 70,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'LIQUID_PER', title: '保液系数', align: 'left', width: 70,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'MODEL_DESC', title: '补充说明', align: 'left', width: 70,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[25]']
                    }
                }
            },
            { field: 'DESIGN_DATE', title: '设计日期', align: 'left', width: 100, editor: 'datebox' },
            { field: 'UPDATE_USER', title: '最后修改人', align: 'left', width: 70 },
            { field: 'UPDATE_DATE', title: '最后修改日期', align: 'left', width: 120 },
            { field: 'VALID_FLAG', title: '启用', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_DESIGN_INFO(rowIndex);

            var editors = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getEditors', rowIndex);
            $(editors[0].target)[0].focus(); $(editors[0].target).select();

            //电池容量0
            editors[0].target
                .bind('click', function () { $(editors[0].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //起始电压1
            editors[1].target
                .bind('click', function () { $(editors[1].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //截至电压2
            editors[2].target
                .bind('click', function () { $(editors[2].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阳极涂布重量5
            editors[5].target
                .bind('click', function () { $(editors[5].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阳极压实密度6
            editors[6].target
                .bind('click', function () { $(editors[6].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阳极集流体厚度8
            editors[8].target
                .bind('click', function () { $(editors[8].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阴极涂布重量11
            editors[11].target
                .bind('click', function () { $(editors[11].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阴极压实密度12
            editors[12].target
                .bind('click', function () { $(editors[12].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阴极集流体厚度14
            editors[14].target
                .bind('click', function () { $(editors[14].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //注液量17
            editors[17].target
                .bind('click', function () { $(editors[17].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //保液系数18
            editors[18].target
                .bind('click', function () { $(editors[18].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //补充说明 19
            editors[19].target
                .bind('click', function () { $(editors[19].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            ////阳极材料
            //editors[3].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 4, rowIndex, 'A-001', '阳极材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////阴极材料
            //editors[9].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 10, rowIndex, 'C-001', '阴极材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阳极集流体材料
            //editors[7].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 8, rowIndex, 'A-002', '阳极集流体材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阴极集流体材料
            //editors[13].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 14, rowIndex, 'C-002', '阴极集流体材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////隔离膜材料
            //editors[15].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 16, rowIndex, 'S-001', '隔离膜材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////电解液配方
            //editors[16].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 17, rowIndex, 'E-001', '电解液配方'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阳极配方
            //editors[4].target
            //    .bind('click', function () { Row_Init_btn_ANODE_FORMULA_ID($(this), 5, rowIndex, 'AMIX', '阳极配方'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阴极配方
            //editors[10].target
            //    .bind('click', function () { Row_Init_btn_ANODE_FORMULA_ID($(this), 11, rowIndex, 'CMIX', '阴极配方'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_DESIGN_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_DESIGN_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_DESIGN_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_DESIGN_INFO').datagrid('refreshRow', index);
        }
    });

    $('#Table_PACKAGE_DESIGN_INFO').datagrid("loadData", data).datagrid('acceptChanges');


}
function editrow_PACKAGE_DESIGN_INFO(index) {
    if (editIndex_PACKAGE_DESIGN_INFO != undefined)
        $('#Table_PACKAGE_DESIGN_INFO').datagrid('endEdit', editIndex_PACKAGE_DESIGN_INFO);
    $('#Table_PACKAGE_DESIGN_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_DESIGN_INFO() {
    var row = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_DESIGN_INFO(row);
        }
    });
}
function Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr) {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_DESIGN_INFO/GetData',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo,
            'queryStr': queryStr
        },
        dataType: 'json',
        success: function (data) {
            Table_PACKAGE_DESIGN_INFO(data);
        }
    });
}
function Dialog_PACKAGE_DESIGN_INFO() {
    $('#Dialog_PACKAGE_DESIGN_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_DESIGN_INFO == 'add') {
                    Add_PACKAGE_DESIGN_INFO();
                }
                else if (addOrEdit_PACKAGE_DESIGN_INFO == 'edit') {
                    Edit_PACKAGE_DESIGN_INFO();
                }
            }
        }, {
            text: '开始查询',
            iconCls: 'icon-search',
            handler: function () {
                Init_Table_PACKAGE_DESIGN_INFO_Search();
            }
        }]
    });
}
function Validate_PACKAGE_DESIGN_INFO() {
    if (!
        ($('#CELL_CAP').validatebox('isValid') &&
        $('#BEG_VOL').validatebox('isValid') &&
        $('#END_VOL').validatebox('isValid') &&
        $('#ANODE_COATING_WEIGHT').validatebox('isValid') &&
        $('#CATHODE_COATING_WEIGHT').validatebox('isValid') &&
        $('#ANODE_DENSITY').validatebox('isValid') &&
        $('#CATHODE_DENSITY').validatebox('isValid') &&
        $('#ANODE_THICKNESS').validatebox('isValid') &&
        $('#CATHODE_THICKNESS').validatebox('isValid') &&
        $('#INJECTION_QTY').validatebox('isValid') &&
        $('#LIQUID_PER').validatebox('isValid')

        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    if (
        $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getText') == '' ||
        $('#ANODE_STUFF_ID').combobox('getText') == '' ||
        $('#CATHODE_STUFF_ID').combobox('getText') == '' ||
        $('#ANODE_FORMULA_ID').combobox('getText') == '' ||
        $('#CATHODE_FORMULA_ID').combobox('getText') == '' ||
        $('#ANODE_FOIL_ID').combobox('getText') == '' ||
        $('#CATHODE_FOIL_ID').combobox('getText') == '' ||
        $('#SEPARATOR_ID').combobox('getText') == '' ||
        $('#ELECTROLYTE_ID').combobox('getText') == ''
        ) {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return false;
    }
    var groupNo = $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValues') + '';
    if (groupNo.length == 0) {
        $.messager.show({
            title: '消息',
            msg: '请选择分组',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_DESIGN_INFO() {
    var groupNo = $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValues') + '';
    if (!Validate_PACKAGE_DESIGN_INFO()) return;
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'GROUP_NO': groupNo,
        'CELL_CAP': $('#CELL_CAP').val(),
        'BEG_VOL': $('#BEG_VOL').val(),
        'END_VOL': $('#END_VOL').val(),
        'ANODE_STUFF_ID': $('#ANODE_STUFF_ID').combobox('getValue'),
        'ANODE_FORMULA_ID': $('#ANODE_FORMULA_ID').combobox('getValue'),
        'ANODE_COATING_WEIGHT': $('#ANODE_COATING_WEIGHT').val(),
        'ANODE_DENSITY': $('#ANODE_DENSITY').val(),
        'ANODE_FOIL_ID': $('#ANODE_FOIL_ID').combobox('getValue'),
        'CATHODE_STUFF_ID': $('#CATHODE_STUFF_ID').combobox('getValue'),
        'CATHODE_FORMULA_ID': $('#CATHODE_FORMULA_ID').combobox('getValue'),
        'CATHODE_COATING_WEIGHT': $('#CATHODE_COATING_WEIGHT').val(),
        'CATHODE_DENSITY': $('#CATHODE_DENSITY').val(),
        'CATHODE_FOIL_ID': $('#CATHODE_FOIL_ID').combobox('getValue'),
        'SEPARATOR_ID': $('#SEPARATOR_ID').combobox('getValue'),
        'ELECTROLYTE_ID': $('#ELECTROLYTE_ID').combobox('getValue'),
        'INJECTION_QTY': $('#INJECTION_QTY').val(),
        'LIQUID_PER': $('#LIQUID_PER').val(),
        'MODEL_DESC': $('#MODEL_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(),
        'DESIGN_DATE': $('#DESIGN_DATE').val(),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(),
        'ANODE_THICKNESS': $('#ANODE_THICKNESS').val(),
        'CATHODE_THICKNESS': $('#CATHODE_THICKNESS').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostBatchAdd',
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
                Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('close');
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
function Edit_PACKAGE_DESIGN_INFO() {
    var groupNo = $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValue');
    if (groupNo == '') {
        return;
    }
    if (!Validate_PACKAGE_DESIGN_INFO()) return;
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'GROUP_NO': $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValue'),
        'CELL_CAP': $('#CELL_CAP').val(),
        'BEG_VOL': $('#BEG_VOL').val(),
        'END_VOL': $('#END_VOL').val(),
        'ANODE_STUFF_ID': $('#ANODE_STUFF_ID').combobox('getValue'),
        'ANODE_FORMULA_ID': $('#ANODE_FORMULA_ID').combobox('getValue'),
        'ANODE_COATING_WEIGHT': $('#ANODE_COATING_WEIGHT').val(),
        'ANODE_DENSITY': $('#ANODE_DENSITY').val(),
        'ANODE_FOIL_ID': $('#ANODE_FOIL_ID').combobox('getValue'),
        'CATHODE_STUFF_ID': $('#CATHODE_STUFF_ID').combobox('getValue'),
        'CATHODE_FORMULA_ID': $('#CATHODE_FORMULA_ID').combobox('getValue'),
        'CATHODE_COATING_WEIGHT': $('#CATHODE_COATING_WEIGHT').val(),
        'CATHODE_DENSITY': $('#CATHODE_DENSITY').val(),
        'CATHODE_FOIL_ID': $('#CATHODE_FOIL_ID').combobox('getValue'),
        'SEPARATOR_ID': $('#SEPARATOR_ID').combobox('getValue'),
        'ELECTROLYTE_ID': $('#ELECTROLYTE_ID').combobox('getValue'),
        'INJECTION_QTY': $('#INJECTION_QTY').val(),
        'LIQUID_PER': $('#LIQUID_PER').val(),
        'MODEL_DESC': $('#MODEL_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(),
        'DESIGN_DATE': $('#DESIGN_DATE').datebox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(),
        'ANODE_THICKNESS': $('#ANODE_THICKNESS').val(),
        'CATHODE_THICKNESS': $('#CATHODE_THICKNESS').val(),
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };

    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostEdit',
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
                Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('close');
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == -2) {
                $.messager.show({
                    title: '消息',
                    msg: '输入不正确',
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
function Edit_Cell_PACKAGE_DESIGN_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'CELL_CAP': row.CELL_CAP,
        'BEG_VOL': row.BEG_VOL,
        'END_VOL': row.END_VOL,
        'ANODE_STUFF_ID': row.ANODE_STUFF_ID,
        'ANODE_FORMULA_ID': row.ANODE_FORMULA_ID,
        'ANODE_COATING_WEIGHT': row.ANODE_COATING_WEIGHT,
        'ANODE_DENSITY': row.ANODE_DENSITY,
        'ANODE_FOIL_ID': row.ANODE_FOIL_ID,
        'CATHODE_STUFF_ID': row.CATHODE_STUFF_ID,
        'CATHODE_FORMULA_ID': row.CATHODE_FORMULA_ID,
        'CATHODE_COATING_WEIGHT': row.CATHODE_COATING_WEIGHT,
        'CATHODE_DENSITY': row.CATHODE_DENSITY,
        'CATHODE_FOIL_ID': row.CATHODE_FOIL_ID,
        'SEPARATOR_ID': row.SEPARATOR_ID,
        'ELECTROLYTE_ID': row.ELECTROLYTE_ID,
        'INJECTION_QTY': row.INJECTION_QTY,
        'LIQUID_PER': row.LIQUID_PER,
        'MODEL_DESC': row.MODEL_DESC,
        'VALID_FLAG': row.VALID_FLAG,
        'DESIGN_DATE': dateFormat(row.DESIGN_DATE, "mm/dd/yyyy"),
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'ANODE_THICKNESS': row.ANODE_THICKNESS,
        'CATHODE_THICKNESS': row.CATHODE_THICKNESS,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostEdit',
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
function Delete_PACKAGE_DESIGN_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostDelete',
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
                Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
//物料编号下拉框
function Init_MATERIAL_PN_ID(controlID, grpNum) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': grpNum,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'MATERIAL_TYPE_ID': "",
        'MATERIAL_PN_ID': "",
        'MATERIAL_PN_NAME': "",
        'MATERIAL_PN_DESC': "",
        'queryStr': ''
    };

    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PN_LIST/GetDataQuery',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#' + controlID).combobox({
                panelHeight: 100,
                valueField: 'MATERIAL_PN_ID',
                textField: 'MATERIAL_PN_ID',
                data: data,
                editable: true,
                required: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var x = $('#' + controlID).combobox('getData');
                        if (x == null) {
                            return;
                        }
                        var flag = false;
                        $.each(x, function (i) {
                            if (x[i].MATERIAL_PN_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#' + controlID).combobox('setValue', x[i].MATERIAL_PN_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_' + controlID).hide();
                        } else {
                            $('#alert_' + controlID).show();
                        }
                    }
                }
            });

        }
    });
}
//物料编号查询框
function MATERIAL_TYPE_ID_() {
    $('#MATERIAL_TYPE_ID_').combogrid({
        idField: 'MATERIAL_TYPE_ID',
        textField: 'MATERIAL_TYPE_DESC',
        editable: false,
        required: false,
        multiple: false,
        panelWidth: 400,
        panelHeight: 250,
        columns: [[
            { field: 'MATERIAL_TYPE_ID', title: '类型', width: 80 },
            { field: 'MATERIAL_TYPE_NAME', title: '英文名', width: 100 },
            { field: 'MATERIAL_TYPE_DESC', title: '中文名', width: 200 }
        ]]
    });
}
function Table_MATERIAL_PN_ID_(controlID) {
    $('#Table_MATERIAL_PN_ID_').datagrid({
        title: '',
        singleSelect: true,
        width: '300',
        height: '236',
        columns: [[
            { field: 'MATERIAL_PN_ID', title: '物料PN', width: 260, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            $('#' + controlID).combobox('setValue', rowData.MATERIAL_PN_ID);
            $('#Dialog_MATERIAL_PN_ID_').dialog('close');
        }
    });
}
function Init_btn_ANODE_STUFF_ID(controlID, grpNum, title) {
    $('#btn_' + controlID).click(function () {
        MATERIAL_TYPE_ID_();
        Init_MATERIAL_TYPE_ID_(grpNum);
        Table_MATERIAL_PN_ID_(controlID);
        $('#Table_MATERIAL_PN_ID_').datagrid('loadData', []);
        $('#MATERIAL_TYPE_ID_').combogrid('clear');
        $('#Search_MATERIAL_PN_ID_').val('');
        $('#Search_MATERIAL_PN_NAME_').val('');
        $('#Search_MATERIAL_PN_DESC_').val('');
        $('#btn_Search').click(function () {
            var j = {
                'MATERIAL_TYPE_GRP_NUM': grpNum,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'FACTORY_ID': factoryId,
                'MATERIAL_TYPE_ID': $('#MATERIAL_TYPE_ID_').combogrid('getValue'),
                'MATERIAL_PN_ID': $('#Search_MATERIAL_PN_ID_').val(),
                'MATERIAL_PN_NAME': $('#Search_MATERIAL_PN_NAME_').val(),
                'MATERIAL_PN_DESC': $('#Search_MATERIAL_PN_DESC_').val(),
                'queryStr': ''
            };

            $.ajax({
                type: 'get',
                url: '/api/MATERIAL_PN_LIST/GetDataQuery',
                data: j,
                dataType: 'json',
                cache: false,
                success: function (data) {
                    Table_MATERIAL_PN_ID_(controlID);
                    $('#Table_MATERIAL_PN_ID_').datagrid('loadData', data);
                }
            });
        });
        $('#Dialog_MATERIAL_PN_ID_').dialog({ 'title': title }).dialog('open');
    });
}
function Init_MATERIAL_TYPE_ID_(grpNum) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': grpNum,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_TYPE_GRP_LIST/GetDataByGrpId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#MATERIAL_TYPE_ID_').combogrid('grid').datagrid('loadData', data);
        }
    });
}
//配方编号下拉框
function Init_RECIPE_ID(controlID, typeID) {
    var j = {
        'RECIPE_TYPE_ID': typeID,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'RECIPE_ID': "",
        'RECIPE_NAME': "",
        'RECIPE_DESC': "",
        'queryStr': ''
    };

    $.ajax({
        type: 'get',
        url: '/api/RECIPE_LIST/GetDataQuery',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#' + controlID).combobox({
                panelHeight: 100,
                valueField: 'RECIPE_ID',
                textField: 'RECIPE_ID',
                data: data,
                editable: true,
                required: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var x = $('#' + controlID).combobox('getData');
                        if (x == null) {
                            return;
                        }
                        var flag = false;
                        $.each(x, function (i) {
                            if (x[i].RECIPE_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#' + controlID).combobox('setValue', x[i].RECIPE_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_' + controlID).hide();
                        } else {
                            $('#alert_' + controlID).show();
                        }
                    }
                }
            });
        }
    });
}
//配方编号查询框
function Table_RECIPE_ID(controlID) {
    $('#Table_RECIPE_ID').datagrid({
        title: '',
        singleSelect: true,
        width: '300',
        height: '236',
        columns: [[
            { field: 'RECIPE_ID', title: '编号', width: 260, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            $('#' + controlID).combobox('setValue', rowData.RECIPE_ID);
            $('#Dialog_RECIPE_ID').dialog('close');
        }
    });
}
function Init_btn_ANODE_FORMULA_ID(controlID, typeID, title) {
    $('#btn_' + controlID).click(function () {
        Table_RECIPE_ID(controlID);
        $('#Table_RECIPE_ID').datagrid('loadData', []);
        $('#Search_RECIPE_ID').val('');
        $('#Search_RECIPE_NAME').val('');
        $('#Search_RECIPE_DESC').val('');
        $('#btn_Search_RECIPE_ID').click(function () {
            var j = {
                'RECIPE_TYPE_ID': typeID,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'FACTORY_ID': factoryId,
                'RECIPE_ID': $('#Search_RECIPE_ID').val(),
                'RECIPE_NAME': $('#Search_RECIPE_NAME').val(),
                'RECIPE_DESC': $('#Search_RECIPE_DESC').val(),
                'queryStr': ''
            };

            $.ajax({
                type: 'get',
                url: '/api/RECIPE_LIST/GetDataQuery',
                data: j,
                dataType: 'json',
                cache: false,
                success: function (data) {
                    Table_RECIPE_ID(controlID);
                    $('#Table_RECIPE_ID').datagrid('loadData', data);
                }
            });
        });
        $('#Dialog_RECIPE_ID').dialog({ 'title': title }).dialog('open');
    });
}
//查询
function InitQueryStr() {
    Search_GROUP_NO();
    Init_btnSearch();
}
function Search_GROUP_NO() {
    $('#Search_GROUP_NO').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: true
    });
}
function Init_btnSearch() {
    $('#btnSearch').click(function () {
        var qs = $('#Search_GROUP_NO').combobox('getValues');
        queryStr = '';
        if (qs.length != 0) {
            var strqs = [];
            for (var i = 0; i < qs.length; i++) {
                strqs[i] = "'" + qs[i] + "'";
            }
            queryStr = "AND GROUP_NO IN (" + strqs + ")";
        }
        Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
    });
}
//初始化下拉框
function GROUP_NO_PACKAGE_DESIGN_INFO() {
    $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetGroupsNotInDesignInfo?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo,
        editable: false
    });
}
//选择Package来初始化设计信息
function PACKAGE_NO_forInit() {
    $('#PACKAGE_NO_forInit').combobox({
        panelHeight: 100,
        valueField: 'PACKAGE_NO',
        textField: 'PACKAGE_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_BASE_INFO/GetDataByFactoryId?factoryId=' + factoryId,
        editable: true,
        onSelect: function (record) {
            VERSION_NO_forInit(record.PACKAGE_NO);
        },
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != '') {
                var x = $('#PACKAGE_NO_forInit').combobox('getData');
                $.each(x, function (i) {
                    if (x[i].PACKAGE_NO.toUpperCase() == newValue.toUpperCase()) {
                        $('#PACKAGE_NO_forInit').combobox('setValue', x[i].PACKAGE_NO);
                        VERSION_NO_forInit(x[i].PACKAGE_NO);
                        return;
                    }
                })
            }
        }
    });
}
//选择版本
function VERSION_NO_forInit(packageNoForInit) {
    $('#VERSION_NO_forInit').combobox({
        panelHeight: 100,
        valueField: 'VERSION_NO',
        textField: 'VERSION_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_BASE_INFO/GetDataByPackageNo?factoryId=' + factoryId + '&PackageNo=' + packageNoForInit,
        editable: true,
        onSelect: function (record) {
            GROUP_NO_forInit(packageNoForInit, record.VERSION_NO);
        },
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != '') {
                var x = $('#VERSION_NO_forInit').combobox('getData');
                $.each(x, function (i) {
                    if (x[i].VERSION_NO.toUpperCase() == newValue.toUpperCase()) {
                        $('#VERSION_NO_forInit').combobox('setValue', x[i].VERSION_NO);
                        GROUP_NO_forInit(packageNoForInit, x[i].VERSION_NO);
                        return;
                    }
                })
            }
        }
    });
}
//选择分组来初始化设计信息
function GROUP_NO_forInit(packageNoForInit, versionNoForInit) {
    $('#GROUP_NO_forInit').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNoForInit + '&versionNo=' + versionNoForInit + '&queryStr=',
        editable: true,
        onSelect: function (record) {
            InitDesignInfo(packageNoForInit, versionNoForInit, record.GROUP_NO);
        },
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != '') {
                var x = $('#GROUP_NO_forInit').combobox('getData');
                $.each(x, function (i) {
                    if (x[i].GROUP_NO.toUpperCase() == newValue.toUpperCase()) {
                        $('#GROUP_NO_forInit').combobox('setValue', x[i].GROUP_NO);
                        InitDesignInfo(packageNoForInit, versionNoForInit, x[i].GROUP_NO);
                        return;
                    }
                })
            }
        }
    });

}
function InitDesignInfo(packageNoForInit, versionNoForInit, groupNoForInit) {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_DESIGN_INFO/GetDataById',
        data: {
            'groupNo': groupNoForInit,
            'factoryId': factoryId,
            'packageNo': packageNoForInit,
            'versionNo': versionNoForInit
        },
        dataType: 'json',
        success: function (data) {
            if (data.length == 0) {
                $('#CELL_CAP').val('');
                $('#BEG_VOL').val('');
                $('#END_VOL').val('');
                $('#ANODE_STUFF_ID').combobox('clear');
                $('#CATHODE_STUFF_ID').combobox('clear');
                $('#ANODE_FORMULA_ID').combobox('clear');
                $('#CATHODE_FORMULA_ID').combobox('clear');
                $('#ANODE_COATING_WEIGHT').val('');
                $('#CATHODE_COATING_WEIGHT').val('');
                $('#ANODE_DENSITY').val('');
                $('#CATHODE_DENSITY').val('');
                $('#ANODE_FOIL_ID').combobox('clear');
                $('#CATHODE_FOIL_ID').combobox('clear');
                $('#ANODE_THICKNESS').val('');
                $('#CATHODE_THICKNESS').val('');
                $('#SEPARATOR_ID').combobox('clear');
                $('#ELECTROLYTE_ID').combobox('clear');
                $('#INJECTION_QTY').val('');
                $('#LIQUID_PER').val('');
                $('#MODEL_DESC').val('');
                $('#DESIGN_DATE').datebox('clear');
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val('');
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val('');
                $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(1);
                return;
            }
            var d = data[0];
            //$('#GROUP_NO').combobox('setValue', d.GROUP_NO);
            $('#CELL_CAP').val(d.CELL_CAP);
            $('#BEG_VOL').val(d.BEG_VOL);
            $('#END_VOL').val(d.END_VOL);
            $('#ANODE_STUFF_ID').combobox('setValue', d.ANODE_STUFF_ID);
            $('#CATHODE_STUFF_ID').combobox('setValue', d.CATHODE_STUFF_ID);
            $('#ANODE_FORMULA_ID').combobox('setValue', d.ANODE_FORMULA_ID);
            $('#CATHODE_FORMULA_ID').combobox('setValue', d.CATHODE_FORMULA_ID);
            $('#ANODE_COATING_WEIGHT').val(d.ANODE_COATING_WEIGHT);
            $('#CATHODE_COATING_WEIGHT').val(d.CATHODE_COATING_WEIGHT);
            $('#ANODE_DENSITY').val(d.ANODE_DENSITY);
            $('#CATHODE_DENSITY').val(d.CATHODE_DENSITY);
            $('#ANODE_FOIL_ID').combobox('setValue', d.ANODE_FOIL_ID);
            $('#CATHODE_FOIL_ID').combobox('setValue', d.CATHODE_FOIL_ID);
            $('#ANODE_THICKNESS').val(d.ANODE_THICKNESS);
            $('#CATHODE_THICKNESS').val(d.CATHODE_THICKNESS);
            $('#SEPARATOR_ID').combobox('setValue', d.SEPARATOR_ID);
            $('#ELECTROLYTE_ID').combobox('setValue', d.ELECTROLYTE_ID);
            $('#INJECTION_QTY').val(d.INJECTION_QTY);
            $('#LIQUID_PER').val(d.LIQUID_PER);
            $('#MODEL_DESC').val(d.MODEL_DESC);
            $('#DESIGN_DATE').datebox('setValue', dateFormat(d.DESIGN_DATE, "mm/dd/yyyy"));
        }
    });
}
//查询项目
function Init_AllCheckbox() {
    Init_CheckBox('CELL_CAP');
    Init_CheckBox('BEG_VOL');
    Init_CheckBox('END_VOL');
    Init_CheckBox('ANODE_COATING_WEIGHT');
    Init_CheckBox('CATHODE_COATING_WEIGHT');
    Init_CheckBox('ANODE_DENSITY');
    Init_CheckBox('CATHODE_DENSITY');
    Init_CheckBox('ANODE_THICKNESS');
    Init_CheckBox('CATHODE_THICKNESS');
    Init_CheckBox('INJECTION_QTY');
    Init_CheckBox('LIQUID_PER');
}
function Init_CheckBox(id) {
    $('#check_' + id).attr('checked', false);
    $('#region_' + id).hide();
    $('#check_' + id).change(function () {
        var x = $('#check_' + id).is(':checked');
        if (x) { $('#region_' + id).show(); $('#' + id + '_tolerance').val(''); }
        else $('#region_' + id).hide();
    })
}
//查询结果
function Table_PACKAGE_DESIGN_INFO_Search(data) {
    $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid({
        title: '',
        singleSelect: true, //只能选择单行
        width: '730',
        height: '200',
        fitColumns: false,
        autoRowHeight: false,
        frozenColumns: [[
            {
                field: 'ACTION', width: 35,
                formatter: function (value, row, index) {
                    var openstr = '<a style=\"cursor:pointer;color:blue;\"  onclick="opendetail_PACKAGE_BASE_INFO(' + index + ')">打开</a>';
                    return openstr;
                }
            },
            { field: 'PACKAGE_NO', title: '文件编号', align: 'left', width: 120 },
            { field: 'VERSION_NO', title: '版本号', align: 'left', width: 40 },
            { field: 'GROUP_NO', title: '组别', align: 'left', width: 30 }
        ]],
        columns: [[
            { field: 'CELL_CAP', title: '电池容量', align: 'left', width: 55 },
            { field: 'BEG_VOL', title: '起始电压', align: 'left', width: 55 },
            { field: 'END_VOL', title: '截至电压', align: 'left', width: 55 },
            { field: 'ANODE_STUFF_ID', title: '阳极材料', align: 'left', width: 130 },
            { field: 'ANODE_FORMULA_ID', title: '阳极配方', align: 'left', width: 130 },
            { field: 'ANODE_COATING_WEIGHT', title: '阳极涂布重量', align: 'left', width: 80 },
            { field: 'ANODE_DENSITY', title: '阳极压实密度', align: 'left', width: 80 },
            { field: 'ANODE_FOIL_ID', title: '阳极集流体材料', align: 'left', width: 130 },
            { field: 'ANODE_THICKNESS', title: '阳极集流体厚度', align: 'left', width: 90 },
            { field: 'CATHODE_STUFF_ID', title: '阴极材料', align: 'left', width: 130 },
            { field: 'CATHODE_FORMULA_ID', title: '阴极配方', align: 'left', width: 130 },
            { field: 'CATHODE_COATING_WEIGHT', title: '阴极涂布重量', align: 'left', width: 90 },
            { field: 'CATHODE_DENSITY', title: '阴极压实密度', align: 'left', width: 90 },
            { field: 'CATHODE_FOIL_ID', title: '阴极集流体材料', align: 'left', width: 130 },
            { field: 'CATHODE_THICKNESS', title: '阴极集流体厚度', align: 'left', width: 90 },
            { field: 'SEPARATOR_ID', title: '隔离膜材料', align: 'left', width: 130 },
            { field: 'ELECTROLYTE_ID', title: '电解液配方', align: 'left', width: 130 },
            { field: 'INJECTION_QTY', title: '注液量', align: 'left', width: 70 },
            { field: 'LIQUID_PER', title: '保液系数', align: 'left', width: 70 },
            { field: 'MODEL_DESC', title: '补充说明', align: 'left', width: 70 },
            { field: 'DESIGN_DATE', title: '设计日期', align: 'left', width: 100, editor: 'datebox' },
            { field: 'UPDATE_USER', title: '最后修改人', align: 'left', width: 70 },
            { field: 'UPDATE_DATE', title: '最后修改日期', align: 'left', width: 120 },
            { field: 'VALID_FLAG', title: '启用', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]]
    })
}
function Init_Table_PACKAGE_DESIGN_INFO_Search() {
    var x = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getSelected');
    if (x == null) return;
    if (!
        ($('#CELL_CAP_tolerance').validatebox('isValid') &&
        $('#BEG_VOL_tolerance').validatebox('isValid') &&
        $('#END_VOL_tolerance').validatebox('isValid') &&
        $('#ANODE_COATING_WEIGHT_tolerance').validatebox('isValid') &&
        $('#CATHODE_COATING_WEIGHT_tolerance').validatebox('isValid') &&
        $('#ANODE_DENSITY_tolerance').validatebox('isValid') &&
        $('#CATHODE_DENSITY_tolerance').validatebox('isValid') &&
        $('#ANODE_THICKNESS_tolerance').validatebox('isValid') &&
        $('#CATHODE_THICKNESS_tolerance').validatebox('isValid') &&
        $('#INJECTION_QTY_tolerance').validatebox('isValid') &&
        $('#LIQUID_PER_tolerance').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return;
    }
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValue'),
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'CELL_CAP': $('#CELL_CAP').val(),
        'BEG_VOL': $('#BEG_VOL').val(),
        'END_VOL': $('#END_VOL').val(),
        'ANODE_STUFF_ID': $('#ANODE_STUFF_ID').combobox('getValue'),
        'ANODE_FORMULA_ID': $('#ANODE_FORMULA_ID').combobox('getValue'),
        'ANODE_COATING_WEIGHT': $('#ANODE_COATING_WEIGHT').val(),
        'ANODE_DENSITY': $('#ANODE_DENSITY').val(),
        'ANODE_FOIL_ID': $('#ANODE_FOIL_ID').combobox('getValue'),
        'CATHODE_STUFF_ID': $('#CATHODE_STUFF_ID').combobox('getValue'),
        'CATHODE_FORMULA_ID': $('#CATHODE_FORMULA_ID').combobox('getValue'),
        'CATHODE_COATING_WEIGHT': $('#CATHODE_COATING_WEIGHT').val(),
        'CATHODE_DENSITY': $('#CATHODE_DENSITY').val(),
        'CATHODE_FOIL_ID': $('#CATHODE_FOIL_ID').combobox('getValue'),
        'SEPARATOR_ID': $('#SEPARATOR_ID').combobox('getValue'),
        'ELECTROLYTE_ID': $('#ELECTROLYTE_ID').combobox('getValue'),
        'INJECTION_QTY': $('#INJECTION_QTY').val(),
        'LIQUID_PER': $('#LIQUID_PER').val(),
        'MODEL_DESC': $('#MODEL_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(),
        'DESIGN_DATE': $('#DESIGN_DATE').datebox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(),
        'ANODE_THICKNESS': $('#ANODE_THICKNESS').val(),
        'CATHODE_THICKNESS': $('#CATHODE_THICKNESS').val(),
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'CELL_CAP_tolerance': $('#CELL_CAP_tolerance').val(),
        'BEG_VOL_tolerance': $('#BEG_VOL_tolerance').val(),
        'END_VOL_tolerance': $('#END_VOL_tolerance').val(),
        'ANODE_COATING_WEIGHT_tolerance': $('#ANODE_COATING_WEIGHT_tolerance').val(),
        'ANODE_DENSITY_tolerance': $('#ANODE_DENSITY_tolerance').val(),
        'CATHODE_COATING_WEIGHT_tolerance': $('#CATHODE_COATING_WEIGHT_tolerance').val(),
        'CATHODE_DENSITY_tolerance': $('#CATHODE_DENSITY_tolerance').val(),
        'INJECTION_QTY_tolerance': $('#INJECTION_QTY_tolerance').val(),
        'LIQUID_PER_tolerance': $('#LIQUID_PER_tolerance').val(),
        'ANODE_THICKNESS_tolerance': $('#ANODE_THICKNESS_tolerance').val(),
        'CATHODE_THICKNESS_tolerance': $('#CATHODE_THICKNESS_tolerance').val(),
        'check_CELL_CAP': $('#check_CELL_CAP').is(':checked'),
        'check_BEG_VOL': $('#check_BEG_VOL').is(':checked'),
        'check_END_VOL': $('#check_END_VOL').is(':checked'),
        'check_ANODE_STUFF_ID': $('#check_ANODE_STUFF_ID').is(':checked'),
        'check_ANODE_FORMULA_ID': $('#check_ANODE_FORMULA_ID').is(':checked'),
        'check_ANODE_COATING_WEIGHT': $('#check_ANODE_COATING_WEIGHT').is(':checked'),
        'check_ANODE_DENSITY': $('#check_ANODE_DENSITY').is(':checked'),
        'check_ANODE_FOIL_ID': $('#check_ANODE_FOIL_ID').is(':checked'),
        'check_CATHODE_STUFF_ID': $('#check_CATHODE_STUFF_ID').is(':checked'),
        'check_CATHODE_FORMULA_ID': $('#check_CATHODE_FORMULA_ID').is(':checked'),
        'check_CATHODE_COATING_WEIGHT': $('#check_CATHODE_COATING_WEIGHT').is(':checked'),
        'check_CATHODE_DENSITY': $('#check_CATHODE_DENSITY').is(':checked'),
        'check_CATHODE_FOIL_ID': $('#check_CATHODE_FOIL_ID').is(':checked'),
        'check_SEPARATOR_ID': $('#check_SEPARATOR_ID').is(':checked'),
        'check_ELECTROLYTE_ID': $('#check_ELECTROLYTE_ID').is(':checked'),
        'check_INJECTION_QTY': $('#check_INJECTION_QTY').is(':checked'),
        'check_LIQUID_PER': $('#check_LIQUID_PER').is(':checked'),
        'check_ANODE_THICKNESS': $('#check_ANODE_THICKNESS').is(':checked'),
        'check_CATHODE_THICKNESS': $('#check_CATHODE_THICKNESS').is(':checked')
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostDataQuery',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            Table_PACKAGE_DESIGN_INFO_Search(data);
            $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid('loadData', data);
        }
    });
}
function opendetail_PACKAGE_BASE_INFO(index) {
    $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid('selectRow', index);
    var row = $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid('getSelected');
    window.open('/Package/PackageDetails.aspx?packageNo=' + row.PACKAGE_NO + '&factoryId=' + row.FACTORY_ID + '&versionNo=' + row.VERSION_NO + '&productTypeId=' + row.PRODUCT_TYPE_ID + '&produceProcTypeId=' + row.PRODUCT_PROC_TYPE_ID, '_blank');
}
/***********************************所有工序***********************************/
function Table_PROCESS_LIST() {
    $('#Table_PROCESS_LIST').datagrid({
        title: '工序',
        singleSelect: true,
        width: '130',
        height: '500',
        fitColumns: true,
        nowrap: false,
        //frozenColumns: [[
        //    { field: 'PROCESS_ID', title: '编号', align: 'left', width: 60, styler: function (value, row, index) { return 'color:blue'; } }
        //]],
        columns: [[
            { field: 'PROCESS_DESC', title: '名称', align: 'left', width: 100, editor: 'text' }
        ]],
        onClickRow: function (index, value) {
            Init_Table_PACKAGE_FLOW_INFO_();
            $('#tt1').tabs('enableTab', '产品及工艺参数信息');
            $('#tt1').tabs('enableTab', '物料信息');
            $('#tt1').tabs('enableTab', '设备信息');
            $('#tt1').tabs('enableTab', '附图信息');
            $('#tt1').tabs('enableTab', 'BOM信息');
            $('#tt1').tabs('enableTab', '大分组');

            var tab = $('#tt1').tabs('getSelected');
            var index = $('#tt1').tabs('getTabIndex', tab);
            switch (index) {
                case 0:
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 1:
                    $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PROC_PN_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 2:
                    $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 3:
                    $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#showimg').html('');
                    break;
                case 4:
                    $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 5:
                    Init_Table_PACKAGE_PROC_GRP_();
                    $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('loadData', []);
                    break;
            }
        }
    });
}
function Init_Table_PROCESS_LIST() {
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId',
        data: {
            'factoryId': factoryId,
            'productTypeId': productTypeId,
            'produceProcTypeId': produceProcTypeId
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_PROCESS_LIST').datagrid('loadData', data);
        }
    });
}
/*******************************Package所包含的所有工序*******************************/
function Table_PROCESS_LIST_PACKAGE() {
    $('#Table_PROCESS_LIST_PACKAGE').datagrid({
        title: '<b style="color:red;">第一步：请选择工序.</b>',
        singleSelect: true, //只能选择单行
        width: '200',
        height: '506',
        fitColumns: true,
        striped: true,
        nowrap: false,
        columns: [[
            //{ field: 'SEQUENCE_NO', title: '', align: 'left', width: 50 },
            { field: 'PROCESS_ID', title: '编号', align: 'left', width: 50 },
            { field: 'PROCESS_DESC', title: '工序', align: 'left', width: 130 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_GROUP_NO_PACKAGE_PROCESS(rowData.PROCESS_ID);

            //$('#tt1').tabs('select', '参数信息');
            $('#tt1').tabs('enableTab', '产品及工艺参数信息');
            $('#tt1').tabs('enableTab', '物料信息');
            $('#tt1').tabs('enableTab', '设备信息');
            $('#tt1').tabs('enableTab', '附图信息');
            $('#tt1').tabs('enableTab', 'BOM信息');
            $('#tt1').tabs('enableTab', '大分组');

            var tab = $('#tt1').tabs('getSelected');
            var index = $('#tt1').tabs('getTabIndex', tab);
            switch (index) {
                case 0:
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 1:
                    $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PROC_PN_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 2:
                    $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 3:
                    $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#showimg').html('');
                    break;
                case 4:
                    $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 5:
                    Init_Table_PACKAGE_PROC_GRP_();
                    $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('loadData', []);
                    break;
            }
        }
    });
    //$('#Table_PROCESS_LIST_PACKAGE').datagrid("loadData", data);
}
function Init_Table_PROCESS_LIST_PACKAGE() {
    var j = {
        'packageNo': packageNo,
        'versionNo': versionNo,
        'factoryId': factoryId
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetDataByPackageId',
        data: j,
        dataType: 'json',
        success: function (data) {
            //Table_PROCESS_LIST_PACKAGE(data);
            $('#Table_PROCESS_LIST_PACKAGE').datagrid("loadData", data);
        }
    });
}

/***********************************分组工序***********************************/
function Table_PACKAGE_FLOW_INFO(data) {
    $('#Table_PACKAGE_FLOW_INFO').datagrid({
        title: '分组工序信息',
        singleSelect: true,
        width: '120',
        height: '500',
        nowrap: false,
        toolbar: [
            //{
            //    text: '批量新增',
            //    iconCls: 'icon-add',
            //    handler: function () {
            //        GROUP_NO_BATCHADD();
            //        PROCESS_ID_BATCHADD();
            //        $('#Dialog_PACKAGE_FLOW_INFO_BatchAdd').dialog('open');
            //    }
            //},
        {
            text: '新增',
            //iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_FLOW_INFO = 'add';
                $('#grp').show();
                GROUP_NO_BATCHEDIT_ADD();
                SUB_GROUP_NO();
                $('#PROCESS_ID').combogrid('readonly', true);
                //var x = $('#Table_PACKAGE_GROUPS').datagrid('getSelected');
                //if (x != null) {
                //    $('#GROUP_NO_BATCHEDIT').combobox('setValue', x.GROUP_NO);
                //}
                var p = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (p != null) {
                    $('#PROCESS_ID').combogrid('setValue', p.PROCESS_ID);
                    $('#PROC_SEQUENCE_NO').val(p.SEQUENCE_NO);
                    $('#PREVIOUS_PROCESS_ID').combogrid('grid').datagrid('clearSelections');
                    $('#NEXT_PROCESS_ID').combogrid('grid').datagrid('clearSelections');
                    $('#PREVIOUS_PROCESS_ID').combogrid('setValue', p.PREVIOUS_PROCESS_ID);
                    $('#NEXT_PROCESS_ID').combogrid('setValue', p.NEXT_PROCESS_ID);
                }
                //$('#PROCESS_ID').combogrid('clear').combogrid('readonly', false);
                //$('#PROC_SEQUENCE_NO').val('');
                //$('#PREVIOUS_PROCESS_ID').combogrid('clear');
                //$('#NEXT_PROCESS_ID').combogrid('clear');
                $('#UPDATE_USER_PACKAGE_FLOW_INFO').val('');
                $('#UPDATE_DATE_PACKAGE_FLOW_INFO').val('');
                $('#PKG_PROC_DESC').val('');
                $('#UPDATE_USER_PACKAGE_FLOW_INFO').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_FLOW_INFO').attr('readonly', true);
                $('#Dialog_PACKAGE_FLOW_INFO').dialog('open');
            }
        }, {
            text: '修改',
            //iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_FLOW_INFO = 'edit';
                SUB_GROUP_NO();
                $('#grp').show();
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x == null) {
                    $.messager.show({
                        title: '消息',
                        msg: '请先选择分组,然后选择工序',
                        showType: 'show'
                    });
                    return;
                }
                $('#PROCESS_ID').combogrid('setValue', x.PROCESS_ID).combogrid('readonly', true);
                $('#PROC_SEQUENCE_NO').val(x.PROC_SEQUENCE_NO);
                $('#PREVIOUS_PROCESS_ID').combogrid('setValue', x.PREVIOUS_PROCESS_ID);
                $('#NEXT_PROCESS_ID').combogrid('setValue', x.NEXT_PROCESS_ID);
                $('#UPDATE_USER_PACKAGE_FLOW_INFO').val(x.UPDATE_USER);
                $('#UPDATE_DATE_PACKAGE_FLOW_INFO').val(x.UPDATE_DATE);
                $('#PKG_PROC_DESC').val(x.PKG_PROC_DESC);
                $('#SUB_GROUP_NO').combobox('setValues', x.SUB_GROUP_NO == null ? '' : x.SUB_GROUP_NO.split(','));
                $('#UPDATE_USER_PACKAGE_FLOW_INFO').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_FLOW_INFO').attr('readonly', true);
                $('#Dialog_PACKAGE_FLOW_INFO').dialog('open');
                GROUP_NO_BATCHEDIT(x.GROUP_NO);
            }
        }, {
            text: '删除',
            //iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_FLOW_INFO();
            }
        },
        //{
        //    text: '批量删除',
        //    iconCls: 'icon-cancel',
        //    handler: function () {
        //        var x = $('#Table_PACKAGE_GROUPS').datagrid('getSelected');
        //        if (x == null) {
        //            $.messager.show({
        //                title: '消息',
        //                msg: '请选择分组',
        //                showType: 'show'
        //            });
        //            return;
        //        }
        //        var groupNo = x.GROUP_NO;

        //        $('#GROUP_NO_BatchDel').combobox({
        //            panelHeight: 200,
        //            valueField: 'GROUP_NO',
        //            textField: 'GROUP_NO',
        //            multiple: true,
        //            method: 'get',
        //            url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + "&queryStr= AND GROUP_NO!='" + groupNo + "'",
        //            editable: false
        //        });

        //        $('#Dialog_PACKAGE_FLOW_INFO_BatchDel').dialog({
        //            toolbar: [{
        //                text: '保存',
        //                iconCls: 'icon-save',
        //                handler: function () {
        //                    var p = $('#Table_PACKAGE_FLOW_INFO_BatchDel').datagrid('getSelections');
        //                    var processArr = [];
        //                    var processStr = '';
        //                    $.each(p, function (index, value) {
        //                        processArr.push(value.PROCESS_ID);
        //                    });
        //                    processStr = $.unique(processArr).sort().join(',');
        //                    var groups = $('#GROUP_NO_BatchDel').combobox('getValues') + '';
        //                    $('#loading-mask').show();
        //                    var j = {
        //                        'PACKAGE_NO': packageNo,
        //                        'GROUP_NO': groupNo,
        //                        'GROUP_NOS': groups,
        //                        'FACTORY_ID': factoryId,
        //                        'VERSION_NO': versionNo,
        //                        'PROCESS_IDS': processStr
        //                    };
        //                    $.ajax({
        //                        type: 'post',
        //                        url: '/api/PACKAGE_FLOW_INFO/PostDelete_Batch',
        //                        data: JSON.stringify(j),
        //                        dataType: 'json',
        //                        contentType: 'application/json',
        //                        success: function (data) {
        //                            $('#loading-mask').hide();
        //                            if (data > 0) {
        //                                $.messager.show({
        //                                    title: '消息',
        //                                    msg: '成功',
        //                                    showType: 'show'
        //                                });
        //                                Init_Table_PACKAGE_FLOW_INFO_();
        //                                $('#Dialog_PACKAGE_FLOW_INFO_BatchDel').dialog('close');
        //                                Init_tt();
        //                            } else if (data == 0) {
        //                                $.messager.show({
        //                                    title: '消息',
        //                                    msg: '失败',
        //                                    showType: 'show'
        //                                });
        //                            }
        //                        }
        //                    });
        //                }
        //            }]
        //        });

        //        $('#Table_PACKAGE_FLOW_INFO_BatchDel').datagrid({
        //            title: '',
        //            singleSelect: true,
        //            width: '280',
        //            height: '294',
        //            nowrap: false,
        //            singleSelect: false,
        //            url: '/api/PACKAGE_FLOW_INFO/GetDataNoPage?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=&groupNo=' + groupNo,
        //            method: 'get',
        //            columns: [[{ checkbox: true },
        //                { field: 'PROCESS_DESC', title: '工序', align: 'left', width: 230 }
        //            ]]
        //        });
        //        $('#Dialog_PACKAGE_FLOW_INFO_BatchDel').dialog('open');
        //    }
        //},
        //'-', {
        //    text: '保存',
        //    //iconCls: 'icon-save',
        //    handler: function () {
        //        $('#Table_PACKAGE_FLOW_INFO').datagrid('endEdit', editIndex_PACKAGE_FLOW_INFO);
        //        var changedRow = $('#Table_PACKAGE_FLOW_INFO').datagrid('getChanges');
        //        if (changedRow.length > 0) {
        //            for (var i = 0; i < changedRow.length; i++) {
        //                Edit_Cell_PACKAGE_FLOW_INFO(changedRow[i]);
        //            }
        //        }
        //        editIndex_PACKAGE_FLOW_INFO = undefined;
        //        $('#Table_PACKAGE_FLOW_INFO').datagrid('acceptChanges');
        //    }
        //}, {
        //    text: '取消',
        //    //iconCls: 'icon-undo',
        //    handler: function () {
        //        $('#Table_PACKAGE_FLOW_INFO').datagrid('rejectChanges');
        //    }
        //}
        ],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' }
            //,
            //{
            //    field: 'PROCESS_DESC', title: '工序', align: 'left', width: 150,
            //    styler: function (value, row, index) { return 'color:blue'; }
            //}
        ]],
        columns: [[
            {
                field: 'PROC_SEQUENCE_NO', title: '序号', align: 'left', width: 60,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            }
            //,
            //{
            //    field: 'PREVIOUS_PROCESS_ID', title: '前一工序', align: 'left', width: 150,
            //    editor: {
            //        type: 'combobox',
            //        options: {
            //            valueField: 'PROCESS_ID',
            //            textField: 'PROCESS_DESC',
            //            url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
            //            method: 'get',
            //            required: false
            //        }
            //    },
            //    formatter: function (value, row, index) {
            //        return row.PROCESS_DESC_P;
            //    }
            //},
            //{
            //    field: 'NEXT_PROCESS_ID', title: '后一工序', align: 'left', width: 150,
            //    editor: {
            //        type: 'combobox',
            //        options: {
            //            valueField: 'PROCESS_ID',
            //            textField: 'PROCESS_DESC',
            //            url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
            //            method: 'get',
            //            required: false
            //        }
            //    },
            //    formatter: function (value, row, index) {
            //        return row.PROCESS_DESC_N;
            //    }
            //},
            //{
            //    field: 'SUB_GROUP_NO', title: '分组归类', align: 'left', width: 55,
            //    editor: {
            //        type: 'combobox',
            //        options: {
            //            valueField: 'GROUP_NO',
            //            textField: 'GROUP_NO',
            //            url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
            //            method: 'get',
            //            required: false,
            //            multiple: true
            //        }
            //    }
            //},
            //{
            //    field: 'PKG_PROC_DESC', title: '说明', align: 'left', width: 200,
            //    editor: {
            //        type: 'validatebox',
            //        options: {
            //            required: false,
            //            validType: ['maxLength[100]']
            //        }
            //    }
            //},
            //{ field: 'UPDATE_USER', title: '最后修改者', align: 'left', width: 70 },
            //{ field: 'UPDATE_DATE', title: '最后修改日期', align: 'left', width: 130 },
            //{
            //    field: 'PROCESS_ID', title: '工序编号', align: 'left', width: 55,
            //    styler: function (value, row, index) { return 'color:blue'; }
            //}
        ]],
        onClickRow: function (rowIndex, rowData) {
            //editrow_PACKAGE_FLOW_INFO(rowIndex);
            var tab = $('#tt1').tabs('getSelected');
            var index = $('#tt1').tabs('getTabIndex', tab);
            switch (index) {
                case 0:
                    Init_Table_PACKAGE_PARAM_SETTING();
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid("loadData", []);
                    break;
                case 1:
                    Init_Table_PACKAGE_PROC_MATERIAL_INFO();
                    Init_Table_PACKAGE_PROC_PN_INFO();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 2:
                    Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                    Init_Table_PACKAGE_PROC_EQUIP_INFO();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 3:
                    Init_Table_PACKAGE_ILLUSTRATION_INFO();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid("loadData", [])
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#showimg').html('');
                    break;
                case 4:
                    Init_Table_PACKAGE_BOM_SPEC_INFO();
                    break;
                case 5:
                    break;
            }


            $('#tt1').tabs('enableTab', '产品及工艺参数信息');
            $('#tt1').tabs('enableTab', '物料信息');
            $('#tt1').tabs('enableTab', '设备信息');
            $('#tt1').tabs('enableTab', '附图信息');
            $('#tt1').tabs('enableTab', 'BOM信息');
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_FLOW_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_FLOW_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_FLOW_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_FLOW_INFO').datagrid('refreshRow', index);
        }
    });
    $('#Table_PACKAGE_FLOW_INFO').datagrid("loadData", data).datagrid('acceptChanges');
}
function Init_Table_PACKAGE_FLOW_INFO_() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetDataByProcessId',
        data: {
            'FACTORY_ID': factoryId,
            'PACKAGE_NO': packageNo,
            'VERSION_NO': versionNo,
            'PROCESS_ID': PROCESS_ID
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_FLOW_INFO').datagrid('loadData', data);
        }
    });
}
function editrow_PACKAGE_FLOW_INFO(index) {
    if (editIndex_PACKAGE_FLOW_INFO != undefined)
        $('#Table_PACKAGE_FLOW_INFO').datagrid('endEdit', editIndex_PACKAGE_FLOW_INFO);
    $('#Table_PACKAGE_FLOW_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_FLOW_INFO() {
    var row = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '将会删除相关工序明细:<br />参数信息,参数设定信息,物料信息,设备信息,附图信息,BOM信息.确认删除?', function (r) {
        if (r) {
            Delete_PACKAGE_FLOW_INFO(row);
        }
    });
}
function Dialog_PACKAGE_FLOW_INFO() {
    $('#Dialog_PACKAGE_FLOW_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_FLOW_INFO == 'edit') {
                    Edit_PACKAGE_FLOW_INFO();
                } else if (addOrEdit_PACKAGE_FLOW_INFO == 'add') {
                    BatchAddOneProcess();
                }

            }
        }]
    });
}
function Validate_PACKAGE_FLOW_INFO_() {
    if (!(
        $('#PROC_SEQUENCE_NO').validatebox('isValid') &&
        $('#PKG_PROC_DESC').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '填写不正确',
            showType: 'show'
        });
        return false;
    }

    return true;
}
function Edit_PACKAGE_FLOW_INFO() {
    var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (!Validate_PACKAGE_FLOW_INFO_()) return;
    var g = $('#GROUP_NO_BATCHEDIT').combobox('getValues') + '';
    var groupNos = '';
    if (g.length != 0) {
        groupNos = g;
    }
    var j = {
        'PACKAGE_NO': x.PACKAGE_NO,
        'GROUP_NO': x.GROUP_NO,
        'FACTORY_ID': x.FACTORY_ID,
        'VERSION_NO': x.VERSION_NO,
        'PROCESS_ID': x.PROCESS_ID,
        'PROC_SEQUENCE_NO': $('#PROC_SEQUENCE_NO').val(),
        'PREVIOUS_PROCESS_ID': $('#PREVIOUS_PROCESS_ID').combogrid('getValue'),
        'NEXT_PROCESS_ID': $('#NEXT_PROCESS_ID').combogrid('getValue'),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_FLOW_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_FLOW_INFO').val(),
        'PKG_PROC_DESC': $('#PKG_PROC_DESC').val(),
        'SUB_GROUP_NO': $('#SUB_GROUP_NO').combobox('getValues') + '',
        'GROUP_NOS': groupNos
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostEdit',
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
                Init_Table_PACKAGE_FLOW_INFO_();
                $('#Dialog_PACKAGE_FLOW_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_FLOW_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'PROC_SEQUENCE_NO': row.PROC_SEQUENCE_NO,
        'PREVIOUS_PROCESS_ID': row.PREVIOUS_PROCESS_ID,
        'NEXT_PROCESS_ID': row.NEXT_PROCESS_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PKG_PROC_DESC': row.PKG_PROC_DESC,
        'SUB_GROUP_NO': row.SUB_GROUP_NO
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostEdit',
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
                Init_Table_PACKAGE_FLOW_INFO_();
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
function Delete_PACKAGE_FLOW_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostDelete',
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
                Init_Table_PACKAGE_FLOW_INFO_();
                //Init_tt();
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
//分组归类
function SUB_GROUP_NO() {
    $('#SUB_GROUP_NO').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: false
    });
}
//分组
//function Table_PACKAGE_GROUPS(data) {
//    $('#Table_PACKAGE_GROUPS').datagrid({
//        title: '分组',
//        singleSelect: true,
//        width: '100',
//        height: '500',
//        fitColumns: true,
//        frozenColumns: [[
//            { field: 'GROUP_NO', title: '组别', align: 'left', width: 40, styler: function (value, row, index) { return 'color:blue'; } }
//        ]],
//        columns: [[
//            { field: 'GROUP_QTY', title: '数量', align: 'left', width: 40, editor: 'text' }
//        ]],
//        onClickRow: function (index, value) {
//            Init_Table_PACKAGE_FLOW_INFO_();
//        }
//    });
//    $('#Table_PACKAGE_GROUPS').datagrid("loadData", data).datagrid('acceptChanges');
//}
//function Init_Table_PACKAGE_GROUPS() {
//    $.ajax({
//        type: 'get',
//        url: '/api/PACKAGE_GROUPS/GetData',
//        data: {
//            'factoryId': factoryId,
//            'packageNo': packageNo,
//            'versionNo': versionNo,
//            'queryStr': queryStr
//        },
//        dataType: 'json',
//        success: function (data) {
//            Table_PACKAGE_GROUPS(data);
//        }
//    });
//}
//工序
function Init_PROCESS_ID() {
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
        success: function (data) {
            $('#PROCESS_ID').combogrid({
                idField: 'PROCESS_ID',
                textField: 'PROCESS_DESC',
                multiple: false,
                method: 'get',
                panelWidth: 350,
                panelHeight: 250,
                data: data,
                columns: [[{ checkbox: true },
                { field: 'PROCESS_ID', title: '编号', width: 130 },
                { field: 'PROCESS_DESC', title: '名称', width: 150 }
                ]],
                onClickRow: function (rowIndex, rowData) {
                    $('#PROC_SEQUENCE_NO').val(rowData.SEQUENCE_NO);
                    $('#PREVIOUS_PROCESS_ID').combogrid('grid').datagrid('clearSelections');
                    $('#NEXT_PROCESS_ID').combogrid('grid').datagrid('clearSelections');
                    $('#PREVIOUS_PROCESS_ID').combogrid('setValue', rowData.PREVIOUS_PROCESS_ID);
                    $('#NEXT_PROCESS_ID').combogrid('setValue', rowData.NEXT_PROCESS_ID);
                }
            });
            $('#PREVIOUS_PROCESS_ID').combogrid({
                idField: 'PROCESS_ID',
                textField: 'PROCESS_DESC',
                multiple: false,
                method: 'get',
                panelWidth: 350,
                panelHeight: 250,
                data: data,
                columns: [[{ checkbox: true },
                { field: 'PROCESS_ID', title: '编号', width: 130 },
                { field: 'PROCESS_DESC', title: '名称', width: 150 }
                ]]
            });

            //$('#PREVIOUS_PROCESS_ID').combobox({
            //    panelHeight: 200,
            //    valueField: 'PROCESS_ID',
            //    textField: 'PROCESS_DESC',
            //    multiple: false,
            //    editable: true,
            //    data: data
            //});

            $('#NEXT_PROCESS_ID').combogrid({
                idField: 'PROCESS_ID',
                textField: 'PROCESS_DESC',
                multiple: false,
                method: 'get',
                panelWidth: 350,
                panelHeight: 250,
                data: data,
                columns: [[{ checkbox: true },
                { field: 'PROCESS_ID', title: '编号', width: 130 },
                { field: 'PROCESS_DESC', title: '名称', width: 150 }
                ]]
            });
            //$('#NEXT_PROCESS_ID').combobox({
            //    panelHeight: 200,
            //    valueField: 'PROCESS_ID',
            //    textField: 'PROCESS_DESC',
            //    multiple: false,
            //    editable: true,
            //    data: data
            //});
        }
    });

}
//批量添加多个工序到多个分组中
function Dialog_PACKAGE_FLOW_INFO_BatchAdd() {
    $('#Dialog_PACKAGE_FLOW_INFO_BatchAdd').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                BatchAdd();
            }
        }]
    });
}
//分组下拉框
function GROUP_NO_BATCHADD() {
    $('#GROUP_NO_BATCHADD').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: false
    });
}
//工序下拉框
function PROCESS_ID_BATCHADD() {
    $('#PROCESS_ID_BATCHADD').combogrid({
        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
        idField: 'PROCESS_ID',
        textField: 'PROCESS_DESC',
        multiple: true,
        method: 'get',
        panelWidth: 350,
        panelHeight: 250,
        columns: [[{ checkbox: true },
        { field: 'PROCESS_ID', title: '编号', width: 130 },
        { field: 'PROCESS_DESC', title: '名称', width: 150 }
        ]]
    });
}
//分组——批量新增修改
function GROUP_NO_BATCHEDIT(groupNo) {
    $('#GROUP_NO_BATCHEDIT').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr= AND GROUP_NO!=\'' + groupNo + '\'',
        editable: false
    });
}
//为多个分组,增加一个工序
function BatchAdd() {
    if (!Validate_PACKAGE_FLOW_INFO_()) return;
    var x = $('#GROUP_NO_BATCHADD').combobox('getValues');
    var y = $('#PROCESS_ID_BATCHADD').combogrid('getValues');
    if (x == '' || y == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $('#loading-mask').show();
    var j = {
        'PACKAGE_NO': packageNo,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'GROUP_NOS': x + '',
        'PROCESS_IDS': y + '',
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    }
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostAddBatch',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $('#loading-mask').hide();
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                $('#Dialog_PACKAGE_FLOW_INFO_BatchAdd').dialog('close');
                Init_Table_PACKAGE_FLOW_INFO_();
                //Init_tt();
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
function GROUP_NO_BATCHEDIT_ADD() {
    $('#GROUP_NO_BATCHEDIT').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: false
    });
}
function BatchAddOneProcess() {
    var x = $('#GROUP_NO_BATCHEDIT').combobox('getValues') + '';
    var z = $('#PROCESS_ID').combogrid('getValue');
    if (x == '' || z == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择分组和工序',
            showType: 'show'
        });
        return;
    }
    //$('#loading-mask').show();
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NOS': x + '',
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PROCESS_ID': z,
        'PROC_SEQUENCE_NO': $('#PROC_SEQUENCE_NO').val(),
        'PREVIOUS_PROCESS_ID': $('#PREVIOUS_PROCESS_ID').combogrid('getValue'),
        'NEXT_PROCESS_ID': $('#NEXT_PROCESS_ID').combogrid('getValue'),
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PKG_PROC_DESC': $('#PKG_PROC_DESC').val(),
        'SUB_GROUP_NO': $('#SUB_GROUP_NO').combobox('getValues') + '',
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    }
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostAddBatchOneProcess',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            //$('#loading-mask').hide();
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                $('#Dialog_PACKAGE_FLOW_INFO').dialog('close');
                Init_Table_PACKAGE_FLOW_INFO_();
                //Init_tt();
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

/**************************Package所包含的所有工序,所在分组****************************/
function Table_GROUP_NO_PACKAGE_PROCESS() {
    $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid({
        title: '<b style="color:red;">分组</b>',
        singleSelect: true, //只能选择单行
        width: '50',
        height: '506',
        fitColumns: false,
        loadMsg: "",
        striped: true,
        nowrap: false,
        columns: [[
            { field: 'GROUP_NO', title: '分组', align: 'left', width: 46 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            var tab = $('#tt1').tabs('getSelected');
            var index = $('#tt1').tabs('getTabIndex', tab);
            switch (index) {
                case 0:
                    Init_Table_PACKAGE_PARAM_SETTING();
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid("loadData", []);
                    break;
                case 1:
                    Init_Table_PACKAGE_PROC_MATERIAL_INFO();
                    Init_Table_PACKAGE_PROC_PN_INFO();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 2:
                    Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                    Init_Table_PACKAGE_PROC_EQUIP_INFO();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    break;
                case 3:
                    Init_Table_PACKAGE_ILLUSTRATION_INFO();
                    $('#Table_PACKAGE_PARAM_SETTING').datagrid("loadData", [])
                    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                    $('#showimg').html('');
                    break;
                case 4:
                    Init_Table_PACKAGE_BOM_SPEC_INFO();
                    break;
                case 5:
                    break;
            }


            $('#tt1').tabs('enableTab', '产品及工艺参数信息');
            $('#tt1').tabs('enableTab', '物料信息');
            $('#tt1').tabs('enableTab', '设备信息');
            $('#tt1').tabs('enableTab', '附图信息');
            $('#tt1').tabs('enableTab', 'BOM信息');
        }
    });
}
function Init_Table_GROUP_NO_PACKAGE_PROCESS(processId) {
    var j = {
        'factoryId': factoryId,
        'packageNo': packageNo,
        'versionNo': versionNo,
        'processId': processId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid("loadData", data);
        }
    });
}

/***********************************参数设定信息*************************************/
function Table_PACKAGE_PARAM_SETTING() {
    $('#Table_PACKAGE_PARAM_SETTING').datagrid({
        title: '',
        singleSelect: true,
        width: '712',
        height: '323',
        fitColumns: false,
        toolbar: [{
            text: '新增多个参数',
            id: 'btn_addMulti_PACKAGE_PARAM_SETTING',
            iconCls: 'icon-add',
            handler: function () {
                //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (x == null) {
                    $.messager.show({
                        title: '消息',
                        msg: '请选择工序',
                        showType: 'show'
                    });
                    return;
                }
                if (CurrentParamType == null) {
                    return;
                }
                PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(CurrentParamType);
                GROUP_NO_Dialog_PACKAGE_PARAM_SETTING_BatchAdd();
                //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (y != null) {
                    $('#GROUP_NO_Dialog_PACKAGE_PARAM_SETTING_BatchAdd').combobox('setValue', y.GROUP_NO);
                }
                $('#Dialog_PACKAGE_PARAM_SETTING_BatchAdd').dialog('open');


            }
        }, {
            text: '新增一个参数',
            iconCls: 'icon-add',
            id: 'btn_addOne_PACKAGE_PARAM_SETTING',
            handler: function () {
                //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (x == null) {
                    $.messager.show({
                        title: '消息',
                        msg: '请选择工序',
                        showType: 'show'
                    });
                    return;
                }

                addOrEdit_PACKAGE_PARAM_SETTING = 'addOne';
                GROUP_NO_BATCH();
                //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (y != null) {
                    $('#GROUP_NO_BATCH').combobox('setValue', y.GROUP_NO);
                }
                if (CurrentParamType == null) {
                    return;
                }
                PARAMETER_ID_SETTING(CurrentParamType);
                $('#PARAMETER_ID_SETTING').combogrid('readonly', false);
                $('#PARAM_TYPE_ID_SETTING').val('');
                $('#DISP_ORDER_IN_SC_SETTING').val('').attr('readonly', false);
                $('#PARAM_IO_SETTING').val('1');
                $('#IS_GROUP_PARAM_SETTING').val('0');
                $('#IS_FIRST_CHECK_PARAM_SETTING').val('0');
                $('#IS_PROC_MON_PARAM_SETTING').val('0');
                $('#IS_OUTPUT_PARAM_SETTING').val('0');
                $('#PARAM_UNIT_SETTING').val('');
                $('#PARAM_DATATYPE_SETTING').val('');
                $('#TARGET_SETTING').val('');
                $('#USL_SETTING').val('');
                $('#LSL_SETTING').val('');

                $('#UPDATE_USER_SETTING').val('').css('border', 'none').attr('readonly', true);
                $('#UPDATE_DATE_SETTING').val('').css('border', 'none').attr('readonly', true);
                $('#SAMPLING_FREQUENCY_SETTING').val('').attr('readonly', false);
                $('#CONTROL_METHOD_SETTING').val('').attr('readonly', false);
                $('#IS_SC_PARAM_SETTING').val(0);
                $('#Dialog_PACKAGE_PARAM_SETTING').dialog('open');
            }
        },
        {
            text: '批量修改',
            iconCls: 'icon-edit',
            id: 'btn_editOne_PACKAGE_PARAM_SETTING',
            handler: function () {
                //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                var x = $('#Table_PROCESS_LIST').datagrid('getSelected')
                var z = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
                if (x == null || z == null) {
                    $.messager.show({
                        title: '消息',
                        msg: '请选择工序,分组,参数',
                        showType: 'show'
                    });
                    return;
                }
                addOrEdit_PACKAGE_PARAM_SETTING = 'edit';
                GROUP_NO_BATCH_Edit();
                if (CurrentParamType == null) {
                    return;
                }
                PARAMETER_ID_SETTING(CurrentParamType);

                $('#PARAMETER_ID_SETTING').combogrid('setValue', z.PARAMETER_ID).combogrid('readonly', false);

                $('#PARAM_TYPE_ID_SETTING').val(z.PARAM_TYPE_ID);
                $('#DISP_ORDER_IN_SC_SETTING').val(z.DISP_ORDER_IN_SC);
                $('#PARAM_IO_SETTING').val(z.PARAM_IO);
                $('#IS_GROUP_PARAM_SETTING').val(z.IS_GROUP_PARAM);
                $('#IS_FIRST_CHECK_PARAM_SETTING').val(z.IS_FIRST_CHECK_PARAM);
                $('#IS_PROC_MON_PARAM_SETTING').val(z.IS_PROC_MON_PARAM);
                $('#IS_OUTPUT_PARAM_SETTING').val(z.IS_OUTPUT_PARAM);
                $('#PARAM_UNIT_SETTING').val(z.PARAM_UNIT);
                $('#PARAM_DATATYPE_SETTING').val(z.PARAM_DATATYPE);
                $('#TARGET_SETTING').val(z.TARGET);
                $('#USL_SETTING').val(z.USL);
                $('#LSL_SETTING').val(z.LSL);
                $('#UPDATE_USER_SETTING').val(z.UPDATE_USER).css('border', 'none').attr('readonly', true);
                $('#UPDATE_DATE_SETTING').val(z.UPDATE_DATE).css('border', 'none').attr('readonly', true);
                $('#SAMPLING_FREQUENCY_SETTING').val(z.SAMPLING_FREQUENCY);
                $('#CONTROL_METHOD_SETTING').val(z.CONTROL_METHOD);
                $('#IS_SC_PARAM_SETTING').val(z.IS_SC_PARAM);
                $('#Dialog_PACKAGE_PARAM_SETTING').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_PARAM_SETTING();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('endEdit', editIndex_PACKAGE_PARAM_SETTING);
                var changedRow = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_PARAM_SETTING(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_PARAM_SETTING = undefined;
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('rejectChanges');
            }
        }],
        idField: 'PARAMETER_ID',
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            {
                field: 'PARAM_TYPE_ID', title: '类型', width: 60, formatter: function (value, row, index) {
                    switch (value) {
                        case 'PRODUCT':
                            return "产品参数";
                        case 'PROCESS':
                            return "工艺参数";
                        case 'MC':
                            return "设备参数";
                        case 'MATERIAL':
                            return "物料参数";
                        case 'TESTER':
                            return "测试仪器参数";
                        case 'FIXTURE':
                            return "夹具参数";
                    }
                }
            },
            { field: 'PARAM_DESC', title: '参数中文名', align: 'left', width: 150, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[

            {
                field: 'TARGET', title: '目标值', align: 'left', width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[25]']
                    }
                }
            },
            {
                field: 'USL', title: '上限值', align: 'left', width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[25]']
                    }
                }
            },
            {
                field: 'LSL', title: '下限值', align: 'left', width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[25]']
                    }
                }
            }, {
                field: 'PARAM_UNIT', title: '单位', align: 'left', width: 40,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[8]']
                    }
                }
            }, {
                field: 'IS_FIRST_CHECK_PARAM', title: '首件', align: 'left', width: 40,
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "是";
                            break;
                        case '0':
                            return "";
                            break;
                    }
                }
            },
            {
                field: 'IS_PROC_MON_PARAM', title: '过程', align: 'left', width: 40,
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "是";
                            break;
                        case '0':
                            return "";
                            break;
                    }
                }
            }, {
                field: 'IS_OUTPUT_PARAM', title: '出货', align: 'left', width: 40,
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "是";
                            break;
                        case '0':
                            return "";
                            break;
                    }
                }
            },
             {
                 field: 'IS_SC_PARAM', title: '在规格牌中', align: 'left', width: 80,
                 editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                 formatter: function (value, row, index) {
                     switch (value) {
                         case '1':
                             return "是";
                             break;
                         case '0':
                             return "";
                             break;
                     }
                 }
             },
            { field: 'DISP_ORDER_IN_SC', title: '规格牌顺序', align: 'left', width: 80, editor: 'text' },
            {
                field: 'PARAM_IO', title: '输入/输出', align: 'left', width: 60,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'value',
                        textField: 'label',
                        required: false,
                        data: [{
                            label: '输入',
                            value: '1'
                        }, {
                            label: '输出',
                            value: '2'
                        }]
                    }
                },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "输入";
                            break;
                        case '2':
                            return "输出";
                            break;
                    }
                }
            }, {
                field: 'IS_GROUP_PARAM', title: '是分组参数', align: 'left', width: 65,
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "是";
                            break;
                        case '0':
                            return "";
                            break;
                    }
                }
            },

            {
                field: 'PARAM_DATATYPE', title: '数据类型', align: 'left', width: 60,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'value',
                        textField: 'label',
                        required: false,
                        data: [{
                            label: '字符串',
                            value: 'STRING'
                        }, {
                            label: '数值',
                            value: 'NUMBER'
                        }]
                    }
                },
                formatter: function (value, row, index) {
                    switch (value) {
                        case 'STRING':
                            return "字符串";
                            break;
                        case 'NUMBER':
                            return "数值";
                            break;
                    }
                }
            },
            {
                field: 'SAMPLING_FREQUENCY', title: '抽样频率', align: 'left', width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[50]']
                    }
                }
            },
            {
                field: 'CONTROL_METHOD', title: '控制方法', align: 'left', width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[50]']
                    }
                }
            },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 80 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 130 },
            { field: 'PARAMETER_ID', title: '参数编号', align: 'left', width: 140, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PARAM_SETTING(rowIndex);
            Init_Table_PACKAGE_PARAM_SPEC_INFO();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PARAM_SETTING = index;
            row.editing = true;
            $('#Table_PACKAGE_PARAM_SETTING').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PARAM_SETTING').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PARAM_SETTING').datagrid('refreshRow', index);
        }
    });
    //$('#btn_addMulti_PACKAGE_PARAM_SETTING').tooltip({
    //    position: 'bottom',
    //    content: '<span style="color:#fff">为多个分组，添加多个参数<br />请先选择工序</span>',
    //    onShow: function () {
    //        $(this).tooltip('tip').css({
    //            backgroundColor: '#666',
    //            borderColor: '#666'
    //        });
    //    }
    //});

    //$('#btn_addOne_PACKAGE_PARAM_SETTING').tooltip({
    //    position: 'bottom',
    //    content: '<span style="color:#fff">为多个分组，添加一个参数<br />请先选择工序</span>',
    //    onShow: function () {
    //        $(this).tooltip('tip').css({
    //            backgroundColor: '#666',
    //            borderColor: '#666'
    //        });
    //    }
    //});

    //$('#btn_editOne_PACKAGE_PARAM_SETTING').tooltip({
    //    position: 'bottom',
    //    content: '<span style="color:#fff">修改多个分组中的一个参数<br />请先选择参数</span>',
    //    onShow: function () {
    //        $(this).tooltip('tip').css({
    //            backgroundColor: '#666',
    //            borderColor: '#666'
    //        });
    //    }
    //});
}
function Init_Table_PACKAGE_PARAM_SETTING() {

    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;

    var url = '';
    var j = null;

    if (CurrentParamType == null) {
        return;
    }
    switch (CurrentParamType) {
        case ParamType.PRODUCT_PROCESS:
            url = '/api/PACKAGE_PARAM_SETTING/GetDataByProcessIdAndGroupNo';
            j = {
                'PACKAGE_NO': packageNo,
                'GROUP_NO': GROUP_NO,
                'VERSION_NO': versionNo,
                'FACTORY_ID': factoryId,
                'PROCESS_ID': PROCESS_ID,
                'queryStr': ""
            };
            break;
        case ParamType.EQUIP_CLASS:
            var e1 = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
            if (e1 != null) {
                url = '/api/PACKAGE_PARAM_SETTING/GetDataByEquipmentClass';
                j = {
                    'PACKAGE_NO': packageNo,
                    'GROUP_NO': GROUP_NO,
                    'VERSION_NO': versionNo,
                    'FACTORY_ID': factoryId,
                    'EQUIPMENT_CLASS_ID': e1.EQUIPMENT_CLASS_ID,
                    'queryStr': ''
                };
            }
            break;
        case ParamType.EQUIP_INFO:
            var e2 = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
            if (e2 != null) {
                url = '/api/PACKAGE_PARAM_SETTING/GetDataByEquipmentInfo';
                j = {
                    'PACKAGE_NO': packageNo,
                    'GROUP_NO': GROUP_NO,
                    'VERSION_NO': versionNo,
                    'FACTORY_ID': factoryId,
                    'EQUIPMENT_ID': e2.EQUIPMENT_ID,
                    'queryStr': ''
                };
            }
            break;
        case ParamType.IMG:
            var i = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
            if (i != null) {
                url = '/api/PACKAGE_PARAM_SETTING/GetDataByIllustrationId';
                j = {
                    'PACKAGE_NO': packageNo,
                    'GROUP_NO': GROUP_NO,
                    'VERSION_NO': versionNo,
                    'FACTORY_ID': factoryId,
                    'ILLUSTRATION_ID': i.ILLUSTRATION_ID,
                    'queryStr': ''
                };
            };
            break;
        case ParamType.MATERIAL_TYPE:
            var i = $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('getSelected');
            if (i != null) {
                url = '/api/PACKAGE_PARAM_SETTING/GetDataByMaterialTypeId';
                j = {
                    'PACKAGE_NO': packageNo,
                    'GROUP_NO': GROUP_NO,
                    'VERSION_NO': versionNo,
                    'FACTORY_ID': factoryId,
                    'MATERIAL_TYPE_ID': i.MATERIAL_TYPE_ID,
                    'queryStr': ''
                };
            };
            break;
        case ParamType.MATERIAL_PN:
            var i = $('#Table_PACKAGE_PROC_PN_INFO').datagrid('getSelected');
            if (i != null) {
                url = '/api/PACKAGE_PARAM_SETTING/GetDataByMaterialPN';
                j = {
                    'PACKAGE_NO': packageNo,
                    'GROUP_NO': GROUP_NO,
                    'VERSION_NO': versionNo,
                    'FACTORY_ID': factoryId,
                    'MATERIAL_PN_ID': i.MATERIAL_PN_ID,
                    'queryStr': ''
                };
            };
            break;
    }
    if (url == '' || j == null) {
        return;
    }

    $.ajax({
        type: 'get',
        url: url,
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_PARAM_SETTING').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}

function editrow_PACKAGE_PARAM_SETTING(index) {
    if (editIndex_PACKAGE_PARAM_SETTING != undefined)
        $('#Table_PACKAGE_PARAM_SETTING').datagrid('endEdit', editIndex_PACKAGE_PARAM_SETTING);
    $('#Table_PACKAGE_PARAM_SETTING').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PARAM_SETTING() {
    var row = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PARAM_SETTING(row);
        }
    });
}
function Dialog_PACKAGE_PARAM_SETTING() {
    $('#Dialog_PACKAGE_PARAM_SETTING').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PARAM_SETTING == 'add') {
                    Add_PACKAGE_PARAM_SETTING();
                }
                else if (addOrEdit_PACKAGE_PARAM_SETTING == 'edit') {
                    Edit_PACKAGE_PARAM_SETTING();
                } else if (addOrEdit_PACKAGE_PARAM_SETTING == 'addOne') {
                    Add_PACKAGE_PARAM_SETTING_BATCH();
                }
            }
        }]
    });
}
//一个分组,一个参数
function Add_PACKAGE_PARAM_SETTING() {
    //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (x == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择分组',
            showType: 'show'
        });
        return;
    }
    var groupNo = x.GROUP_NO;
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (y == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择工序',
            showType: 'show'
        });
        return;
    }
    var processId = x.PROCESS_ID;
    var z = $('#PARAMETER_ID_SETTING').combogrid('getValue');
    if (z == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择参数',
            showType: 'show'
        });
        return;
    }
    var parameterId = z;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': groupNo,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PARAMETER_ID': parameterId,
        'PARAM_TYPE_ID': $('#PARAM_TYPE_ID_SETTING').val(),
        'PROCESS_ID': processId,
        'PROC_TASK_ID': '',
        'DISP_ORDER_IN_SC': $('#DISP_ORDER_IN_SC_SETTING').val(),
        'PARAM_IO': $('#PARAM_IO_SETTING').val(),
        'IS_GROUP_PARAM': $('#IS_GROUP_PARAM_SETTING').val(),
        'IS_FIRST_CHECK_PARAM': $('#IS_FIRST_CHECK_PARAM_SETTING').val(),
        'IS_PROC_MON_PARAM': $('#IS_PROC_MON_PARAM_SETTING').val(),
        'IS_OUTPUT_PARAM': $('#IS_OUTPUT_PARAM_SETTING').val(),
        'PARAM_UNIT': $('#PARAM_UNIT_SETTING').val(),
        'PARAM_DATATYPE': $('#PARAM_DATATYPE_SETTING').val(),
        'TARGET': $('#TARGET_SETTING').val(),
        'USL': $('#USL_SETTING').val(),
        'LSL': $('#LSL_SETTING').val(),
        'ILLUSTRATION_ID': '',
        'UPDATE_USER': $('#UPDATE_USER_SETTING').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_SETTING').val(),
        'SAMPLING_FREQUENCY': $('#SAMPLING_FREQUENCY_SETTING').val(),
        'CONTROL_METHOD': $('#CONTROL_METHOD_SETTING').val(),
        'IS_SC_PARAM': $('#IS_SC_PARAM_SETTING').val(),
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SETTING/PostAdd',
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
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Dialog_PACKAGE_PARAM_SETTING').dialog('close');
                $('#Table_PACKAGE_PARAM_SETTING').datagrid({
                    'onLoadSuccess': function (data) {
                        $('#Table_PACKAGE_PARAM_SETTING').datagrid({ idField: 'PARAMETER_ID' });
                        $('#Table_PACKAGE_PARAM_SETTING').datagrid('selectRecord', parameterId);
                        Init_Table_PACKAGE_PARAM_SPEC_INFO();
                    }
                });

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
//多个分组,编辑一个参数
function Edit_PACKAGE_PARAM_SETTING() {
    //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (x == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择分组',
            showType: 'show'
        });
        return;
    }
    var groupNo = x.GROUP_NO;
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (y == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择工序',
            showType: 'show'
        });
        return;
    }
    var processId = x.PROCESS_ID;
    var z = $('#PARAMETER_ID_SETTING').combogrid('getValue');
    if (z == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择参数',
            showType: 'show'
        });
        return;
    }
    var parameterId = z;
    var g = $('#GROUP_NO_BATCH').combobox('getValues') + '';
    var groups = '';
    if (g != '') {
        groups = g;
    }
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': groupNo,
        'GROUPS': groups,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PARAMETER_ID': parameterId,
        'PARAM_TYPE_ID': $('#PARAM_TYPE_ID_SETTING').val(),
        'PROCESS_ID': processId,
        'PROC_TASK_ID': '',
        'DISP_ORDER_IN_SC': $('#DISP_ORDER_IN_SC_SETTING').val(),
        'PARAM_IO': $('#PARAM_IO_SETTING').val(),
        'IS_GROUP_PARAM': $('#IS_GROUP_PARAM_SETTING').val(),
        'IS_FIRST_CHECK_PARAM': $('#IS_FIRST_CHECK_PARAM_SETTING').val(),
        'IS_PROC_MON_PARAM': $('#IS_PROC_MON_PARAM_SETTING').val(),
        'IS_OUTPUT_PARAM': $('#IS_OUTPUT_PARAM_SETTING').val(),
        'PARAM_UNIT': $('#PARAM_UNIT_SETTING').val(),
        'PARAM_DATATYPE': $('#PARAM_DATATYPE_SETTING').val(),
        'TARGET': $('#TARGET_SETTING').val(),
        'USL': $('#USL_SETTING').val(),
        'LSL': $('#LSL_SETTING').val(),
        'ILLUSTRATION_ID': '',
        'UPDATE_USER': $('#UPDATE_USER_SETTING').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_SETTING').val(),
        'SAMPLING_FREQUENCY': $('#SAMPLING_FREQUENCY_SETTING').val(),
        'CONTROL_METHOD': $('#CONTROL_METHOD_SETTING').val(),
        'IS_SC_PARAM': $('#IS_SC_PARAM_SETTING').val(),
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SETTING/PostEdit',
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
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Dialog_PACKAGE_PARAM_SETTING').dialog('close');
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
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
function Edit_Cell_PACKAGE_PARAM_SETTING(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'PARAMETER_ID': row.PARAMETER_ID,
        'PARAM_TYPE_ID': row.PARAM_TYPE_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'PROC_TASK_ID': row.PROC_TASK_ID,
        'DISP_ORDER_IN_SC': row.DISP_ORDER_IN_SC,
        'PARAM_IO': row.PARAM_IO,
        'IS_GROUP_PARAM': row.IS_GROUP_PARAM,
        'IS_FIRST_CHECK_PARAM': row.IS_FIRST_CHECK_PARAM,
        'IS_PROC_MON_PARAM': row.IS_PROC_MON_PARAM,
        'IS_OUTPUT_PARAM': row.IS_OUTPUT_PARAM,
        'PARAM_UNIT': row.PARAM_UNIT,
        'PARAM_DATATYPE': row.PARAM_DATATYPE,
        'TARGET': row.TARGET,
        'USL': row.USL,
        'LSL': row.LSL,
        'ILLUSTRATION_ID': row.ILLUSTRATION_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'SAMPLING_FREQUENCY': row.SAMPLING_FREQUENCY,
        'CONTROL_METHOD': row.CONTROL_METHOD,
        'IS_SC_PARAM': row.IS_SC_PARAM,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SETTING/PostEdit',
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
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid("loadData", []);
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
function Delete_PACKAGE_PARAM_SETTING(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'PARAMETER_ID': row.PARAMETER_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SETTING/PostDelete',
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
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid("loadData", []);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}

//选择参数
function PARAMETER_ID_SETTING(type) {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;

    switch (type) {
        case ParamType.PRODUCT_PROCESS:
            var url = '/api/PROCESS_PARAM_INFO/GetDataByProcessIdQuery';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'PROCESS_ID': processId,
                'queryStr': "AND  IS_ILLUSTRATION_PARAM='0'" + "AND (PARAM.PARAM_TYPE_ID='PRODUCT' OR PARAM.PARAM_TYPE_ID='PROCESS') AND PARAM.VALID_FLAG='1'"
            };

            Init_PARAMETER_ID_SETTING(url, j);
            break;
        case ParamType.EQUIP_CLASS:
            var x = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
            if (x == null) {
                return;
            }
            var url = '/api/EQUIPMENT_CLASS_PARAM_INFO/GetDataByClassId';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'EQUIPMENT_CLASS_ID': x.EQUIPMENT_CLASS_ID,
                'queryStr': " AND PARAM.VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_SETTING(url, j);
            break;
        case ParamType.EQUIP_INFO:
            var x = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
            if (x == null) {
                return;
            }
            var url = '/api/EQUIPMENT_PARAM_INFO/GetDataByEquipmentId';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'EQUIPMENT_ID': x.EQUIPMENT_ID,
                'queryStr': " AND VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_SETTING(url, j);
            break;
        case ParamType.IMG:
            var x = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
            if (x == null) return;
            var url = '/api/ILLUSTRATION_PARAM_INFO/GetDataByImgId';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'ILLUSTRATION_ID': x.ILLUSTRATION_ID,
                'queryStr': " AND VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_SETTING(url, j);
            break;
        case ParamType.MATERIAL_TYPE:
            var x = $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('getSelected');
            if (x == null) return;
            //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
            var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
            if (y == null) return;
            var url = '/api/MATERIAL_PARA_INFO/GetDataByProcessIdAndTypeId';
            j = {
                'MATERIAL_TYPE_ID': x.MATERIAL_TYPE_ID,
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'PROCESS_ID': y.PROCESS_ID,
                'queryStr': " AND PARAM.VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_SETTING(url, j);
            break;
        case ParamType.MATERIAL_PN:
            var x = $('#Table_PACKAGE_PROC_PN_INFO').datagrid('getSelected');
            if (x == null) return;
            var url = '/api/MATERIAL_PN_PARA_INFO/GetDataByPN';
            j = {
                'MATERIAL_PN_ID': x.MATERIAL_PN_ID,
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'queryStr': " AND PARAM.VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_SETTING(url, j);
            break;
    }
}
function Init_PARAMETER_ID_SETTING(url, j) {
    $.ajax({
        type: 'get',
        url: url,
        data: j,
        dataType: 'json',
        async: false,
        success: function (d) {
            $('#PARAMETER_ID_SETTING').combogrid({
                idField: 'PARAMETER_ID',
                textField: 'PARAM_DESC',
                multiple: false,
                panelWidth: 400,
                panelHeight: 250,
                columns: [[
                    {
                        field: 'PARAM_TYPE_ID', title: '类型', width: 60
                        , formatter: function (value, row, index) {
                            switch (value) {
                                case 'PRODUCT':
                                    return "产品参数";
                                case 'PROCESS':
                                    return "工艺参数";
                                case 'MC':
                                    return "设备参数";
                                case 'MATERIAL':
                                    return "物料参数";
                                case 'TESTER':
                                    return "测试仪器参数";
                                case 'FIXTURE':
                                    return "夹具参数";

                            }
                        }
                    },
                    { field: 'PARAMETER_ID', title: '编号', width: 130 },
                    { field: 'PARAM_DESC', title: '名称', width: 150 }
                ]],
                onClickRow: function (rowIndex, rowData) {
                    $('#PARAM_IO_SETTING').val(rowData.PARAM_IO);
                    $('#PARAM_TYPE_ID_SETTING').val(rowData.PARAM_TYPE_ID);
                    $('#PARAM_UNIT_SETTING').val(rowData.PARAM_UNIT);
                    $('#TARGET_SETTING').val(rowData.TARGET);
                    $('#USL_SETTING').val(rowData.USL);
                    $('#LSL_SETTING').val(rowData.LSL);
                    $('#SAMPLING_FREQUENCY_SETTING').val(rowData.SAMPLING_FREQUENCY);
                    $('#CONTROL_METHOD_SETTING').val(rowData.CONTROL_METHOD);
                    $('#IS_FIRST_CHECK_PARAM_SETTING').val(rowData.IS_FIRST_CHECK_PARAM);
                    $('#IS_PROC_MON_PARAM_SETTING').val(rowData.IS_PROC_MON_PARAM);
                    $('#IS_OUTPUT_PARAM_SETTING').val(rowData.IS_OUTPUT_PARAM);
                    $('#IS_GROUP_PARAM_SETTING').val(rowData.IS_GROUP_PARAM);
                    $('#DISP_ORDER_IN_SC_SETTING').val(rowData.DISP_ORDER_IN_SC == null ? rowData.PARAM_ORDER_NO : rowData.DISP_ORDER_IN_SC);
                    $('#PARAM_DATATYPE_SETTING').val(rowData.PARAM_DATATYPE);
                    $('#IS_SC_PARAM_SETTING').val(rowData.IS_SC_PARAM == null ? '0' : rowData.IS_SC_PARAM);
                }
            });
            $('#PARAMETER_ID_SETTING').combogrid('grid').datagrid('loadData', d);



        }
    });

}
function Dialog_PACKAGE_PARAM_SETTING_BatchAdd() {
    $('#Dialog_PACKAGE_PARAM_SETTING_BatchAdd').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                Add_PACKAGE_PARAM_SETTING_BatchAdd();
            }
        }]
    });
}
function GROUP_NO_Dialog_PACKAGE_PARAM_SETTING_BatchAdd() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_Dialog_PACKAGE_PARAM_SETTING_BatchAdd').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
function PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(type) {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    switch (type) {
        case ParamType.PRODUCT_PROCESS:
            var url = '/api/PROCESS_PARAM_INFO/GetDataByProcessIdQuery';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'PROCESS_ID': processId,
                'queryStr': "AND  IS_ILLUSTRATION_PARAM='0'" + "AND (PARAM.PARAM_TYPE_ID='PRODUCT' OR PARAM.PARAM_TYPE_ID='PROCESS') AND PARAM.VALID_FLAG='1'"
            };

            Init_PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(url, j);
            break;
        case ParamType.EQUIP_CLASS:
            var x = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
            if (x == null) {
                return;
            }
            var url = '/api/EQUIPMENT_CLASS_PARAM_INFO/GetDataByClassId';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'EQUIPMENT_CLASS_ID': x.EQUIPMENT_CLASS_ID,
                'queryStr': " AND PARAM.VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(url, j);
            break;
        case ParamType.EQUIP_INFO:
            var x = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
            if (x == null) {
                return;
            }
            var url = '/api/EQUIPMENT_PARAM_INFO/GetDataByEquipmentId';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'EQUIPMENT_ID': x.EQUIPMENT_ID,
                'queryStr': " AND VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(url, j);
            break;
        case ParamType.IMG:
            var x = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
            if (x == null) return;
            var url = '/api/ILLUSTRATION_PARAM_INFO/GetDataByImgId';
            j = {
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'ILLUSTRATION_ID': x.ILLUSTRATION_ID,
                'queryStr': " AND VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(url, j);
            break;
        case ParamType.MATERIAL_TYPE:
            var x = $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('getSelected');
            if (x == null) return;
            //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
            var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
            if (y == null) return;
            var url = '/api/MATERIAL_PARA_INFO/GetDataByProcessIdAndTypeId';
            j = {
                'MATERIAL_TYPE_ID': x.MATERIAL_TYPE_ID,
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'PROCESS_ID': y.PROCESS_ID,
                'queryStr': " AND PARAM.VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(url, j);
            break;
        case ParamType.MATERIAL_PN:
            var x = $('#Table_PACKAGE_PROC_PN_INFO').datagrid('getSelected');
            if (x == null) return;
            var url = '/api/MATERIAL_PN_PARA_INFO/GetDataByPN';
            j = {
                'MATERIAL_PN_ID': x.MATERIAL_PN_ID,
                'FACTORY_ID': factoryId,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'queryStr': " AND PARAM.VALID_FLAG='1' "
            };
            Init_PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(url, j);
            break;
    }
}
function Init_PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd(url, j) {
    $.ajax({
        type: 'get',
        url: url,
        data: j,
        dataType: 'json',
        async: false,
        success: function (d) {
            $('#PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd').combogrid({
                idField: 'PARAMETER_ID',
                textField: 'PARAM_DESC',
                multiple: true,
                method: 'get',
                panelWidth: 400,
                panelHeight: 250,
                columns: [[
                    { checkbox: true },
                    {
                        field: 'PARAM_TYPE_ID', title: '类型', width: 60, formatter: function (value, row, index) {
                            switch (value) {
                                case 'PRODUCT':
                                    return "产品参数";
                                case 'PROCESS':
                                    return "工艺参数";
                                case 'MC':
                                    return "设备参数";
                                case 'MATERIAL':
                                    return "物料参数";
                                case 'TESTER':
                                    return "测试仪器参数";
                                case 'FIXTURE':
                                    return "夹具参数";

                            }
                        }
                    },
                { field: 'PARAMETER_ID', title: '编号', width: 130 },
                { field: 'PARAM_DESC', title: '名称', width: 150 }
                ]]
            });
            $('#PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd').combogrid('grid').datagrid('loadData', d);
        }
    });
}


//多个分组,新增多个参数
function Add_PACKAGE_PARAM_SETTING_BatchAdd() {
    var g = $('#GROUP_NO_Dialog_PACKAGE_PARAM_SETTING_BatchAdd').combobox('getValues');
    var p = $('#PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd').combogrid('getValues');
    var groups = g + '';
    var params = p + '';
    if (groups == '' || params == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择分组,工序',
            showType: 'show'
        });
        return;
    }
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择工序',
            showType: 'show'
        });
        return;
    }
    var processId = x.PROCESS_ID;

    var EQUIPMENT_CLASS_ID = "";
    var EQUIPMENT_ID = "";
    var ILLUSTRATION_ID = "";

    if (CurrentParamType == ParamType.EQUIP_CLASS) {
        var y = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
        if (y != null) EQUIPMENT_CLASS_ID = y.EQUIPMENT_CLASS_ID;
    }
    if (CurrentParamType == ParamType.EQUIP_INFO) {
        var z = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
        if (z != null) EQUIPMENT_ID = z.EQUIPMENT_ID;
    }
    if (CurrentParamType == ParamType.IMG) {
        var i = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
        if (i != null) ILLUSTRATION_ID = i.ILLUSTRATION_ID;
    }


    var j = {
        'PACKAGE_NO': packageNo,
        'GROUPS': groups,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PARAMS': params,
        'PROCESS_ID': processId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'TYPE': CurrentParamType,
        'EQUIPMENT_CLASS_ID': EQUIPMENT_CLASS_ID,
        'EQUIPMENT_ID': EQUIPMENT_ID,
        'ILLUSTRATION_ID': ILLUSTRATION_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SETTING/PostAddBatch',
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
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Dialog_PACKAGE_PARAM_SETTING_BatchAdd').dialog('close');

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
//多个分组,新增一个参数,分组对话框
function GROUP_NO_BATCH() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_BATCH').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
//多个分组,编辑一个参数,分组对话框,不包含所选分组
function GROUP_NO_BATCH_Edit() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var y = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
    if (y == null) {
        return;
    }
    $('#GROUP_NO_BATCH').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + "&queryStr=AND A.GROUP_NO!='" + y.GROUP_NO + "'",
        editable: false
    });
}
//多个分组,添加一个参数
function Add_PACKAGE_PARAM_SETTING_BATCH() {
    var groups = $('#GROUP_NO_BATCH').combobox('getValues') + '';
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    var y = $('#PARAMETER_ID_SETTING').combobox('getValue');
    if (groups == '' || x == null || y == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择工序,分组,参数',
            showType: 'show'
        });
        return;
    }
    var processId = x.PROCESS_ID;

    var j = {
        'PACKAGE_NO': packageNo,
        'GROUPS': groups,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PARAMETER_ID': y,
        'PARAM_TYPE_ID': $('#PARAM_TYPE_ID_SETTING').val(),
        'PROCESS_ID': processId,
        'PROC_TASK_ID': '',
        'DISP_ORDER_IN_SC': $('#DISP_ORDER_IN_SC_SETTING').val(),
        'PARAM_IO': $('#PARAM_IO_SETTING').val(),
        'IS_GROUP_PARAM': $('#IS_GROUP_PARAM_SETTING').val(),
        'IS_FIRST_CHECK_PARAM': $('#IS_FIRST_CHECK_PARAM_SETTING').val(),
        'IS_PROC_MON_PARAM': $('#IS_PROC_MON_PARAM_SETTING').val(),
        'IS_OUTPUT_PARAM': $('#IS_OUTPUT_PARAM_SETTING').val(),
        'PARAM_UNIT': $('#PARAM_UNIT_SETTING').val(),
        'PARAM_DATATYPE': $('#PARAM_DATATYPE_SETTING').val(),
        'TARGET': $('#TARGET_SETTING').val(),
        'USL': $('#USL_SETTING').val(),
        'LSL': $('#LSL_SETTING').val(),
        'ILLUSTRATION_ID': '',
        'UPDATE_USER': $('#UPDATE_USER_SETTING').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_SETTING').val(),
        'SAMPLING_FREQUENCY': $('#SAMPLING_FREQUENCY_SETTING').val(),
        'CONTROL_METHOD': $('#CONTROL_METHOD_SETTING').val(),
        'IS_SC_PARAM': $('#IS_SC_PARAM_SETTING').val(),
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SETTING/PostAddBatchAddOne',
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
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Dialog_PACKAGE_PARAM_SETTING_BatchAdd').dialog('close');

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
/*************************************产品及工艺参数信息**************************************/
function Table_PACKAGE_PARAM_SPEC_INFO() {
    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid({
        title: '',
        singleSelect: true, //只能选择单行
        width: '712',
        height: '150',
        fitColumns: false,
        toolbar: [
        {
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_PARAM_SPEC_INFO = 'add';
                //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x == null) return;
                var y = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
                if (y == null) return;
                $('#show_GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH').hide();
                $('#SPEC_TYPE').val('').attr('disabled', false);
                $('#PARAM_UNIT').val('').attr('readonly', false);
                $('#TARGET').val('').attr('readonly', false);
                $('#USL').val('').attr('readonly', false);
                $('#LSL').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PACKAGE_PARAM_SPEC_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_PARAM_SPEC_INFO = 'edit';
                var x = $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('getSelected');
                if (x == null) return;
                $('#show_GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH').show();
                GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH();
                $('#SPEC_TYPE').val(x.SPEC_TYPE).attr('disabled', true);
                $('#PARAM_UNIT').val(x.PARAM_UNIT);
                $('#TARGET').val(x.TARGET);
                $('#USL').val(x.USL);
                $('#LSL').val(x.LSL);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PACKAGE_PARAM_SPEC_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_PARAM_SPEC_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('endEdit', editIndex_PACKAGE_PARAM_SPEC_INFO);
                var changedRow = $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_PARAM_SPEC_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_PARAM_SPEC_INFO = undefined;
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            {
                field: 'PARAM_DESC', title: '参数', align: 'left', width: 150,
                styler: function (value, row, index) { return 'color:blue'; }
            },
            {
                field: 'SPEC_TYPE', title: '类型', align: 'left', width: 30,
                styler: function (value, row, index) { return 'color:blue'; },
                formatter: function (value, row, index) {
                    switch (value) {
                        case 'FAI':
                            return '首件';
                        case 'PMI':
                            return '过程';
                        case 'OI':
                            return '出货';
                    }
                }
            }
        ]],
        columns: [[
            { field: 'TARGET', title: '目标值', align: 'left', editor: 'text', width: 100 },
            { field: 'USL', title: '上限值', align: 'left', editor: 'text', width: 100 },
            { field: 'LSL', title: '下限值', align: 'left', editor: 'text', width: 100 },
            { field: 'PARAM_UNIT', title: '单位', align: 'left', editor: 'text', width: 60 },
            { field: 'UPDATE_USER', title: '最后更新人', align: 'left', width: 70 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 130 },
            { field: 'PARAM_NAME', title: '参数英文名', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PARAMETER_ID', title: '参数编号', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PARAM_SPEC_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PARAM_SPEC_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PACKAGE_PARAM_SPEC_INFO() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;
    var z = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
    if (z == null) return;
    var PARAMETER_ID = z.PARAMETER_ID;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': GROUP_NO,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PROCESS_ID': PROCESS_ID,
        'PARAMETER_ID': PARAMETER_ID
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_PARAM_SPEC_INFO/GetDataByParamId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_PARAM_SPEC_INFO(index) {
    if (editIndex_PACKAGE_PARAM_SPEC_INFO != undefined)
        $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('endEdit', editIndex_PACKAGE_PARAM_SPEC_INFO);
    $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PARAM_SPEC_INFO() {
    var row = $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PARAM_SPEC_INFO(row);
        }
    });
}
function Dialog_PACKAGE_PARAM_SPEC_INFO() {
    $('#Dialog_PACKAGE_PARAM_SPEC_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PARAM_SPEC_INFO == 'add') {
                    Add_PACKAGE_PARAM_SPEC_INFO();
                }
                else if (addOrEdit_PACKAGE_PARAM_SPEC_INFO == 'edit') {
                    Edit_PACKAGE_PARAM_SPEC_INFO();
                }
            }
        }]
    });
}
function Add_PACKAGE_PARAM_SPEC_INFO() {
    if ($('#SPEC_TYPE').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var x = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
    if (x == null) return;
    var parameterId = x.PARAMETER_ID;
    var groupNo = x.GROUP_NO;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': groupNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PARAMETER_ID': parameterId,
        'SPEC_TYPE': $('#SPEC_TYPE').val(),
        'PARAM_UNIT': $('#PARAM_UNIT').val(),
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };

    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SPEC_INFO/PostAdd',
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
                $('#Dialog_PACKAGE_PARAM_SPEC_INFO').dialog('close');
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Table_PACKAGE_PARAM_SETTING').datagrid({
                    'onLoadSuccess': function (data) {
                        $('#Table_PACKAGE_PARAM_SETTING').datagrid('selectRecord', parameterId);
                        var row = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
                        if (row != null) {
                            Init_Table_PACKAGE_PARAM_SPEC_INFO();
                            row.editing = false;
                        }

                    }
                });
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
function Edit_PACKAGE_PARAM_SPEC_INFO() {
    if ($('#SPEC_TYPE').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var x = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
    if (x == null) return;
    var parameterId = x.PARAMETER_ID;
    var groupNo = x.GROUP_NO;
    var g = $('#GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH').combobox('getValues') + '';

    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': groupNo,
        'GROUPS': g,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PARAMETER_ID': parameterId,
        'SPEC_TYPE': $('#SPEC_TYPE').val(),
        'PARAM_UNIT': $('#PARAM_UNIT').val(),
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SPEC_INFO/PostEdit',
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
                $('#Dialog_PACKAGE_PARAM_SPEC_INFO').dialog('close');
                Init_Table_PACKAGE_PARAM_SPEC_INFO();
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
function Edit_Cell_PACKAGE_PARAM_SPEC_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'SPEC_TYPE': row.SPEC_TYPE,
        'PARAM_UNIT': row.PARAM_UNIT,
        'TARGET': row.TARGET,
        'USL': row.USL,
        'LSL': row.LSL,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SPEC_INFO/PostEdit',
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
function Delete_PACKAGE_PARAM_SPEC_INFO(row) {
    var parameterId = row.PARAMETER_ID;
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'PARAMETER_ID': parameterId,
        'FACTORY_ID': row.FACTORY_ID,
        'SPEC_TYPE': row.SPEC_TYPE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PARAM_SPEC_INFO/PostDelete',
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
                Init_Table_PACKAGE_PARAM_SETTING();
                $('#Table_PACKAGE_PARAM_SETTING').datagrid({
                    'onLoadSuccess': function (data) {
                        $('#Table_PACKAGE_PARAM_SETTING').datagrid('selectRecord', parameterId);
                        var row = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
                        if (row != null) {
                            Init_Table_PACKAGE_PARAM_SPEC_INFO();
                            row.editing = false;
                        }
                    }
                });

            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH() {
    var x = $('#Table_PACKAGE_PARAM_SETTING').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + "&queryStr=AND A.GROUP_NO!='" + x.GROUP_NO + "'",
        editable: false
    });
}


/***********************************物料分类*************************************/
function MATERIAL_CATEGORY_ID() {
    $.getJSON('/api/MATERIAL_CATEGORY_LIST/GetDataByFactoryIdAndTypeId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + "&queryStr= AND VALID_FLAG='1' ",
        function (data) {
            data.push({ 'MATERIAL_CATEGORY_ID': "", 'MATERIAL_CATEGORY_DESC': "全部" })
            $('#MATERIAL_CATEGORY_ID').combogrid({
                idField: 'MATERIAL_CATEGORY_ID',
                textField: 'MATERIAL_CATEGORY_DESC',
                editable: false,
                required: false,
                multiple: false,
                method: 'get',
                panelWidth: 400,
                panelHeight: 250,
                columns: [[
                    { field: 'MATERIAL_CATEGORY_ID', title: '类型', width: 60 },
                    { field: 'MATERIAL_CATEGORY_DESC', title: '编号', width: 320 }
                ]],
                onClickRow: function (rowIndex, rowData) {
                    Init_Table_PACKAGE_PROC_MATERIAL_INFO();
                    Init_Table_PACKAGE_PROC_PN_INFO();
                }
            });
            $('#MATERIAL_CATEGORY_ID').combogrid('grid').datagrid('loadData', data);
        });
}
/***********************************物料信息(物料类型)*************************************/
function Table_PACKAGE_PROC_MATERIAL_INFO() {
    $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid({
        title: '物料类型',
        singleSelect: true,
        width: '355',
        height: '236',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (y == null) {
                    return;
                }
                addOrEdit_PACKAGE_PROC_MATERIAL_INFO = 'add';
                GROUP_NO_MATERIAL_INFO_BATCH();

                //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x != null) {
                    $('#GROUP_NO_MATERIAL_INFO_BATCH').combobox('setValue', x.GROUP_NO);
                }

                $('#MATERIAL_TYPE_ID').attr('readonly', true);
                $('#UPDATE_USER_PACKAGE_PROC_MATERIAL_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_MATERIAL_INFO').val('').attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_MATERIAL_INFO').dialog('open');
                $('#btn_MATERIAL_TYPE_ID').click(function () {
                    MATERIAL_CATEGORY_ID_PACKAGE_PROC_MATERIAL_INFO();
                    var c = $('#MATERIAL_CATEGORY_ID').combogrid('getValue');
                    if (c != '') {
                        $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_MATERIAL_INFO').combogrid('setValue', c);
                    }
                    Table_MATERIAL_TYPE_ID();
                    $('#Table_MATERIAL_TYPE_ID').datagrid('loadData', []);
                    $('#btn_Search_MATERIAL_TYPE_ID').click(function () {
                        var j = {
                            'PROCESS_ID': y.PROCESS_ID,
                            'PRODUCT_TYPE_ID': productTypeId,
                            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                            'FACTORY_ID': factoryId,
                            'MATERIAL_CATEGORY_ID': $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_MATERIAL_INFO').combogrid('getValue'),
                            'MATERIAL_TYPE_ID': $('#Search_MATERIAL_TYPE_ID').val() == '' ? '' : $('#Search_MATERIAL_TYPE_ID').val(),
                            'MATERIAL_TYPE_NAME': $('#Search_MATERIAL_TYPE_NAME').val() == '' ? '' : $('#Search_MATERIAL_TYPE_NAME').val(),
                            'MATERIAL_TYPE_DESC': $('#Search_MATERIAL_TYPE_DESC').val() == '' ? '' : $('#Search_MATERIAL_TYPE_DESC').val(),
                            'queryStr': " AND A.VALID_FLAG='1'"
                        };
                        $.ajax({
                            type: 'get',
                            url: '/api/PROCESS_MATERIAL_INFO/GetDataQuery',
                            data: j,
                            dataType: 'json',
                            success: function (data) {
                                Table_MATERIAL_TYPE_ID();
                                $('#Table_MATERIAL_TYPE_ID').datagrid('loadData', data);
                            }
                        });
                    });

                    $('#Dialog_MATERIAL_TYPE_ID').dialog('open');
                });
            }
        },
        //{
        //    text: '修改',
        //    iconCls: 'icon-edit',
        //    handler: function () {
        //        var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
        //        var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
        //        var z = $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('getSelected');
        //        if (x == null || y == null || z == null) {
        //            return;
        //        }
        //        addOrEdit_PACKAGE_PROC_MATERIAL_INFO = 'edit';
        //        MATERIAL_TYPE_ID();
        //        $('#alert_MATERIAL_TYPE_ID').hide();
        //        $('#MATERIAL_TYPE_ID').combobox('setValue', z.MATERIAL_TYPE_ID).combobox('readonly', true);
        //        $('#UPDATE_USER_PACKAGE_PROC_MATERIAL_INFO').val(z.UPDATE_USER);
        //        $('#UPDATE_DATE_PACKAGE_PROC_MATERIAL_INFO').val(z.UPDATE_DATE);
        //        $('#UPDATE_USER_PACKAGE_PROC_MATERIAL_INFO').attr('readonly', true);
        //        $('#UPDATE_DATE_PACKAGE_PROC_MATERIAL_INFO').attr('readonly', true);
        //        $('#Dialog_PACKAGE_PROC_MATERIAL_INFO').dialog('open');
        //    }
        //},
        {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_PROC_MATERIAL_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_MATERIAL_INFO);
                var changedRow = $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_PROC_MATERIAL_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_PROC_MATERIAL_INFO = undefined;
                $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            { field: 'MATERIAL_TYPE_ID', title: '物料类型', width: 200, align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'MATERIAL_TYPE_NAME', title: '名称', width: 150, align: 'left' },
            { field: 'MATERIAL_TYPE_DESC', title: '描述', width: 150, align: 'left' },
            { field: 'UPDATE_USER', title: '最后更新者', width: 80, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新时间', width: 130, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PROC_MATERIAL_INFO(rowIndex);
            $('#Table_PACKAGE_PROC_PN_INFO').datagrid('unselectAll');
            CurrentParamType = ParamType.MATERIAL_TYPE;
            Init_Table_PACKAGE_PARAM_SETTING();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PROC_MATERIAL_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PACKAGE_PROC_MATERIAL_INFO() {
    var url = '';

    var t = $('#MATERIAL_CATEGORY_ID').combobox('getValue');
    if (t == '') {
        url = '/api/PACKAGE_PROC_MATERIAL_INFO/GetDataByProcessIdAndGroupNo';
    } else {
        url = '/api/PACKAGE_PROC_MATERIAL_INFO/GetDataByCategoryId'
    };

    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': GROUP_NO,
        'VERSION_NO': versionNo,
        'PROCESS_ID': PROCESS_ID,
        'FACTORY_ID': factoryId,
        'MATERIAL_CATEGORY_ID': t,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: url,
        data: j,
        dataType: 'json',
        success: function (data) {
            //Table_PACKAGE_PROC_MATERIAL_INFO(data);            
            $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_PROC_MATERIAL_INFO(index) {
    if (editIndex_PACKAGE_PROC_MATERIAL_INFO != undefined)
        $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_MATERIAL_INFO);
    $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PROC_MATERIAL_INFO() {
    var row = $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PROC_MATERIAL_INFO(row);
        }
    });
}
function Dialog_PACKAGE_PROC_MATERIAL_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PACKAGE_PROC_MATERIAL_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PROC_MATERIAL_INFO == 'add') {
                    Add_PACKAGE_PROC_MATERIAL_INFO();
                }
                else if (addOrEdit_PACKAGE_PROC_MATERIAL_INFO == 'edit') {
                    Edit_PACKAGE_PROC_MATERIAL_INFO();
                }
            }
        }]
    });
}
function Add_PACKAGE_PROC_MATERIAL_INFO() {

    var flag = $('#alert_MATERIAL_TYPE_ID').css('display');
    if (flag == 'inline') {
        return;
    }
    //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    var z = $('#GROUP_NO_MATERIAL_INFO_BATCH').combobox('getValues') + '';
    var materialTypeId = $('#MATERIAL_TYPE_ID').val();
    if (materialTypeId == '') return;
    if (y == null || z == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择工序,分组',
            showType: 'show'
        });
        return;
    }
    //var groupNo = x.GROUP_NO;
    var processId = y.PROCESS_ID;

    var j = {
        'PACKAGE_NO': packageNo,
        //'GROUP_NO': groupNo,
        'GROUPS': z,
        'VERSION_NO': versionNo,
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'MATERIAL_TYPE_ID': materialTypeId,
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_PROC_MATERIAL_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_PROC_MATERIAL_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_MATERIAL_INFO/PostAdd',
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
                Init_Table_PACKAGE_PROC_MATERIAL_INFO();
                $('#Dialog_PACKAGE_PROC_MATERIAL_INFO').dialog('close');
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
function Edit_PACKAGE_PROC_MATERIAL_INFO() {
    //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    var z = $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('getSelected');
    if (x == null || y == null || z == null) {
        return;
    }
    var groupNo = x.GROUP_NO;
    var processId = y.PROCESS_ID;
    var materialTypeId = z.MATERIAL_TYPE_ID;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': groupNo,
        'VERSION_NO': versionNo,
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'MATERIAL_TYPE_ID': materialTypeId,
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_PROC_MATERIAL_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_PROC_MATERIAL_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_MATERIAL_INFO/PostEdit',
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
                Init_Table_PACKAGE_PROC_MATERIAL_INFO();
                $('#Dialog_PACKAGE_PROC_MATERIAL_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_PROC_MATERIAL_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_MATERIAL_INFO/PostEdit',
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
function Delete_PACKAGE_PROC_MATERIAL_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_MATERIAL_INFO/PostDelete',
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
                Init_Table_PACKAGE_PROC_MATERIAL_INFO();
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function MATERIAL_TYPE_ID() {
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (y == null) {
        return;
    }
    var processId = y.PROCESS_ID;
    $('#MATERIAL_TYPE_ID').combobox({
        panelHeight: 200,
        valueField: 'MATERIAL_TYPE_ID',
        textField: 'MATERIAL_TYPE_ID',
        method: 'get',
        url: '/api/PROCESS_MATERIAL_INFO/GetDataByProcessId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&PROCESS_ID=' + processId + "&queryStr= AND A.VALID_FLAG='1' ",
        editable: true,
        required: true,
        onSelect: function (record) {

        },
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != null) {
                var x = $(this).combobox('getData');
                var flag = false;
                $.each(x, function (i) {
                    if (x[i].MATERIAL_TYPE_ID.toUpperCase() == newValue.toUpperCase()) {
                        $('#MATERIAL_TYPE_ID').combobox('setValue', x[i].MATERIAL_TYPE_ID);
                        flag = true;
                    }
                });
                if (flag) {
                    $('#alert_MATERIAL_TYPE_ID').hide();
                } else {
                    $('#alert_MATERIAL_TYPE_ID').show();
                }
            }
        }
    });
}
function MATERIAL_CATEGORY_ID_PACKAGE_PROC_MATERIAL_INFO() {
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_CATEGORY_LIST/GetDataByFactoryIdAndTypeId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + "&queryStr= AND VALID_FLAG='1' ",
        async: false,
        success: function (data) {
            $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_MATERIAL_INFO').combogrid({
                idField: 'MATERIAL_CATEGORY_ID',
                textField: 'MATERIAL_CATEGORY_DESC',
                editable: false,
                required: false,
                multiple: false,
                panelWidth: 400,
                panelHeight: 250,
                columns: [[
                    { field: 'MATERIAL_CATEGORY_ID', title: '类型', width: 60 },
                    { field: 'MATERIAL_CATEGORY_DESC', title: '编号', width: 320 }
                ]],
                onClickRow: function (rowIndex, rowData) {

                }
            });
            $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_MATERIAL_INFO').combogrid('grid').datagrid('loadData', data);
        }
    });

}
function GROUP_NO_MATERIAL_INFO_BATCH() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_MATERIAL_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
function Table_MATERIAL_TYPE_ID() {
    $('#Table_MATERIAL_TYPE_ID').datagrid({
        title: '',
        singleSelect: true,
        width: '300',
        height: '230',
        columns: [[
            { field: 'MATERIAL_TYPE_ID', title: '编号', width: 80, align: 'left' },
            { field: 'MATERIAL_TYPE_NAME', title: '英文名称', width: 100, align: 'left' },
            { field: 'MATERIAL_TYPE_DESC', title: '中文名称', width: 100, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            $('#MATERIAL_TYPE_ID').val(rowData.MATERIAL_TYPE_ID);
            $('#Dialog_MATERIAL_TYPE_ID').dialog('close');
        }
    });
}


/***********************************物料信息(物料PN)*************************************/
function Table_PACKAGE_PROC_PN_INFO() {
    $('#Table_PACKAGE_PROC_PN_INFO').datagrid({
        title: '物料PN',
        singleSelect: true,
        width: '355',
        height: '236',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (y == null) {
                    return;
                }
                addOrEdit_PACKAGE_PROC_PN_INFO = 'add';
                GROUP_NO_PN_INFO_BATCH();

                //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x != null) {
                    $('#GROUP_NO_PN_INFO_BATCH').combobox('setValue', x.GROUP_NO);
                }
                $('#MATERIAL_PN_ID').attr('readonly', true);
                $('#UPDATE_USER_PACKAGE_PROC_PN_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_PN_INFO').val('').attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_PN_INFO').dialog('open');
                $('#btn_MATERIAL_PN_ID').click(function () {
                    MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO();
                    var c = $('#MATERIAL_CATEGORY_ID').combogrid('getValue');
                    if (c != '') {
                        $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO').combogrid('setValue', c);
                        MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO();
                    }
                    Table_MATERIAL_PN_ID();

                    $('#Table_MATERIAL_PN_ID').datagrid('loadData', []);
                    $('#MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO').combogrid({
                        idField: 'MATERIAL_TYPE_ID',
                        textField: 'MATERIAL_TYPE_DESC',
                        editable: false,
                        required: false,
                        multiple: false,
                        panelWidth: 400,
                        panelHeight: 250,
                        columns: [[
                            { field: 'MATERIAL_TYPE_ID', title: '类型', width: 80 },
                            { field: 'MATERIAL_TYPE_NAME', title: '英文名', width: 100 },
                            { field: 'MATERIAL_TYPE_DESC', title: '中文名', width: 200 }
                        ]],
                        onClickRow: function (rowIndex, rowData) {

                        }
                    });
                    $('#MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO').combogrid('clear');
                    $('#btn_Search_MATERIAL_PN_ID').click(function () {
                        var j = {
                            'PROCESS_ID': y.PROCESS_ID,
                            'PRODUCT_TYPE_ID': productTypeId,
                            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                            'FACTORY_ID': factoryId,
                            'MATERIAL_CATEGORY_ID': $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO').combogrid('getValue'),
                            'MATERIAL_TYPE_ID': $('#MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO').combogrid('getValue'),
                            'MATERIAL_PN_ID': $('#Search_MATERIAL_PN_ID').val(),
                            'MATERIAL_PN_NAME': $('#Search_MATERIAL_PN_NAME').val(),
                            'MATERIAL_PN_DESC': $('#Search_MATERIAL_PN_DESC').val(),
                            'queryStr': ''
                        };

                        $.ajax({
                            type: 'get',
                            url: '/api/PROCESS_MATERIAL_PN_INFO/GetDataQuery',
                            data: j,
                            dataType: 'json',
                            success: function (data) {
                                Table_MATERIAL_PN_ID();
                                $('#Table_MATERIAL_PN_ID').datagrid('loadData', data);
                            }
                        });
                    });

                    $('#Dialog_MATERIAL_PN_ID').dialog('open');
                });
            }
        },
        //{
        //    text: '修改',
        //    iconCls: 'icon-edit',
        //    handler: function () {
        //        var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
        //        var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
        //        var z = $('#Table_PACKAGE_PROC_PN_INFO').datagrid('getSelected');
        //        if (x == null || y == null || z == null) {
        //            return;
        //        }
        //        addOrEdit_PACKAGE_PROC_PN_INFO = 'edit';
        //        MATERIAL_PN_ID();
        //        $('#MATERIAL_PN_ID').combobox('setValue', z.MATERIAL_PN_ID).combobox('readonly', true);
        //        $('#UPDATE_USER_PACKAGE_PROC_PN_INFO').val(z.UPDATE_USER);
        //        $('#UPDATE_DATE_PACKAGE_PROC_PN_INFO').val(z.UPDATE_DATE);
        //        $('#UPDATE_USER_PACKAGE_PROC_PN_INFO').attr('readonly', true);
        //        $('#UPDATE_DATE_PACKAGE_PROC_PN_INFO').attr('readonly', true);
        //        $('#Dialog_PACKAGE_PROC_PN_INFO').dialog('open');
        //    }
        //},
        {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_PROC_PN_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_PROC_PN_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_PN_INFO);
                var changedRow = $('#Table_PACKAGE_PROC_PN_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_PROC_PN_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_PROC_PN_INFO = undefined;
                $('#Table_PACKAGE_PROC_PN_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_PROC_PN_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            { field: 'MATERIAL_PN_ID', title: '物料PN', width: 200, align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: '最后更新者', width: 80, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 130, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PROC_PN_INFO(rowIndex);
            $('#Table_PACKAGE_PROC_MATERIAL_INFO').datagrid('unselectAll');
            CurrentParamType = ParamType.MATERIAL_PN;
            Init_Table_PACKAGE_PARAM_SETTING();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PROC_PN_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_PROC_PN_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_PN_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_PN_INFO').datagrid('refreshRow', index);
        }
    });


}
function Init_Table_PACKAGE_PROC_PN_INFO() {
    var url = '';

    var t = $('#MATERIAL_CATEGORY_ID').combobox('getValue');
    if (t == '') {
        url = '/api/PACKAGE_PROC_PN_INFO/GetDataByProcessIdAndGroupNo';
    } else {
        url = '/api/PACKAGE_PROC_PN_INFO/GetDataByCategoryId'
    };

    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': GROUP_NO,
        'VERSION_NO': versionNo,
        'PROCESS_ID': PROCESS_ID,
        'FACTORY_ID': factoryId,
        'MATERIAL_CATEGORY_ID': t,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: url,
        data: j,
        dataType: 'json',
        success: function (data) {
            //Table_PACKAGE_PROC_PN_INFO(data);
            $('#Table_PACKAGE_PROC_PN_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_PROC_PN_INFO(index) {
    if (editIndex_PACKAGE_PROC_PN_INFO != undefined)
        $('#Table_PACKAGE_PROC_PN_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_PN_INFO);
    $('#Table_PACKAGE_PROC_PN_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PROC_PN_INFO() {
    var row = $('#Table_PACKAGE_PROC_PN_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PROC_PN_INFO(row);
        }
    });
}
function Dialog_PACKAGE_PROC_PN_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PACKAGE_PROC_PN_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PROC_PN_INFO == 'add') {
                    Add_PACKAGE_PROC_PN_INFO();
                }
                else if (addOrEdit_PACKAGE_PROC_PN_INFO == 'edit') {
                    Edit_PACKAGE_PROC_PN_INFO();
                }
            }
        }]
    });
}
function Add_PACKAGE_PROC_PN_INFO() {
    var x = $('#GROUP_NO_PN_INFO_BATCH').combobox('getValues') + '';
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (y == null || x == '') {
        return;
    }
    var processId = y.PROCESS_ID;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUPS': x,
        'VERSION_NO': versionNo,
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'MATERIAL_PN_ID': $('#MATERIAL_PN_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_PROC_PN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_PROC_PN_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_PN_INFO/PostAdd',
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
                Init_Table_PACKAGE_PROC_PN_INFO();
                $('#Dialog_PACKAGE_PROC_PN_INFO').dialog('close');
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
function Edit_PACKAGE_PROC_PN_INFO() {
    //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    var z = $('#Table_PACKAGE_PROC_PN_INFO').datagrid('getSelected');
    if (x == null || y == null || z == null) {
        return;
    }
    var groupNo = x.GROUP_NO;
    var processId = y.PROCESS_ID;
    var materialPnId = z.MATERIAL_PN_ID;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': groupNo,
        'VERSION_NO': versionNo,
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'MATERIAL_PN_ID': materialPnId,
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_PROC_PN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_PROC_PN_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_PN_INFO/PostEdit',
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
                Init_Table_PACKAGE_PROC_PN_INFO();
                $('#Dialog_PACKAGE_PROC_PN_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_PROC_PN_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_PN_INFO/PostEdit',
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
function Delete_PACKAGE_PROC_PN_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_PN_INFO/PostDelete',
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
                Init_Table_PACKAGE_PROC_PN_INFO();
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO() {
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_CATEGORY_LIST/GetDataByFactoryIdAndTypeId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + "&queryStr= AND VALID_FLAG='1' ",
        async: false,
        success: function (data) {
            $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO').combogrid({
                idField: 'MATERIAL_CATEGORY_ID',
                textField: 'MATERIAL_CATEGORY_DESC',
                editable: false,
                required: false,
                multiple: false,
                method: 'get',
                panelWidth: 400,
                panelHeight: 250,
                columns: [[
                    { field: 'MATERIAL_CATEGORY_ID', title: '类型', width: 60 },
                    { field: 'MATERIAL_CATEGORY_DESC', title: '编号', width: 320 }
                ]],
                onClickRow: function (rowIndex, rowData) {
                    MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO();
                }
            });
            $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO').combogrid('grid').datagrid('loadData', data);
        }
    });
}
function MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO() {
    var x = $('#MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO').combogrid('getValue');
    if (x == '') return;
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (y == null) return;
    var j = {
        'PROCESS_ID': y.PROCESS_ID,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'MATERIAL_CATEGORY_ID': x,
        'queryStr': " AND B.VALID_FLAG='1' "
    };
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_MATERIAL_INFO/GetDataByProcessIdAndCategoryId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO').combogrid('grid').datagrid('loadData', data);
        }
    });


}
function GROUP_NO_PN_INFO_BATCH() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_PN_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
function Table_MATERIAL_PN_ID() {
    $('#Table_MATERIAL_PN_ID').datagrid({
        title: '',
        singleSelect: true,
        width: '300',
        height: '236',
        columns: [[
            { field: 'MATERIAL_PN_ID', title: '物料PN', width: 260, align: 'left' }
        ]],
        method: 'get',
        onClickRow: function (rowIndex, rowData) {
            $('#MATERIAL_PN_ID').val(rowData.MATERIAL_PN_ID);
            $('#Dialog_MATERIAL_PN_ID').dialog('close');
        }
    });
}

/***********************************设备*************************************/
function EQUIPMENT_TYPE_ID() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    //if (x == null) return;
    //var processId = x.PROCESS_ID;
    $.getJSON('/api/EQUIPMENT_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + factoryId + "&queryStr= AND VALID_FLAG = '1'",
        function (data) {
            data.push({ 'EQUIPMENT_TYPE_ID': "", 'EQUIPMENT_TYPE_DESC': "全部" })
            $('#EQUIPMENT_TYPE_ID').combobox({
                //panelHeight: 200,
                valueField: 'EQUIPMENT_TYPE_ID',
                textField: 'EQUIPMENT_TYPE_DESC',
                multiple: false,
                method: 'get',
                data: data,
                editable: false,
                onSelect: function (record) {
                    //var g = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                    var g = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                    if (g == null) {
                        return;
                    }
                    Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                    Init_Table_PACKAGE_PROC_EQUIP_INFO();
                }
            });
        });

}


/***********************************设备信息(设备类型)*************************************/
function Table_PACKAGE_PROC_EQUIP_CLASS_INFO() {
    $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid({
        title: '设备类型',
        singleSelect: true,
        width: '355',
        height: '236',
        fitColumns: false,
        striped: true,
        nowrap: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO();
                var t = $('#EQUIPMENT_TYPE_ID').combobox('getValue');
                if (t != '') {
                    $('#EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO').combobox('setValue', t);
                }
                $('#EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO').combobox('readonly', false);
                addOrEdit_PACKAGE_PROC_EQUIP_CLASS_INFO = 'add';
                GROUP_NO_EQUIP_CLASS_INFO_BATCH();

                $('#btn_EQUIPMENT_CLASS_ID').show();
                //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x != null) {
                    $('#GROUP_NO_EQUIP_CLASS_INFO_BATCH').combobox('setValue', x.GROUP_NO);
                }

                $('#EQUIPMENT_CLASS_ID').val('').attr('readonly', true);
                $('#REMARK').val('').attr('readonly', false);
                $('#UPDATE_USER_PACKAGE_PROC_EQUIP_CLASS_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_EQUIP_CLASS_INFO').val('').attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO').dialog('open');

                $('#btn_EQUIPMENT_CLASS_ID').click(function () {
                    EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO();
                    var x = $('#EQUIPMENT_TYPE_ID').combobox('getValue');
                    if (x != '') $('#EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO').combobox('setValue', x);

                    $('#Table_EQUIPMENT_CLASS_ID').datagrid({
                        title: '',
                        singleSelect: true,
                        width: '300',
                        height: '236',
                        columns: [[
                            { field: 'EQUIPMENT_CLASS_ID', title: '设备分类ID', width: 140, align: 'left' },
                            { field: 'EQUIPMENT_CLASS_DESC', title: '设备分类', width: 140, align: 'left' }
                        ]],
                        method: 'get',
                        onClickRow: function (rowIndex, rowData) {
                            $('#EQUIPMENT_CLASS_ID').val(rowData.EQUIPMENT_CLASS_ID);
                            $('#Dialog_EQUIPMENT_CLASS_ID').dialog('close');
                        }
                    });
                    $('#Table_EQUIPMENT_CLASS_ID').datagrid('loadData', []);
                    $('#Dialog_EQUIPMENT_CLASS_ID').dialog('open');
                    $('#btn_Search_EQUIPMENT_CLASS_ID').click(function () {
                        //var p = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                        var p = $('#Table_PROCESS_LIST').datagrid('getSelected');
                        if (p == null) {
                            return;
                        }
                        var j = {
                            'EQUIPMENT_TYPE_ID': $('#EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO').combobox('getValue'),
                            'PROCESS_ID': p.PROCESS_ID,
                            'FACTORY_ID': factoryId,
                            'EQUIPMENT_CLASS_ID': $('#Search_EQUIPMENT_CLASS_ID').val(),
                            'EQUIPMENT_CLASS_NAME': $('#Search_EQUIPMENT_CLASS_NAME').val(),
                            'EQUIPMENT_CLASS_DESC': $('#Search_EQUIPMENT_CLASS_DESC').val(),
                            'queryStr': " AND B.VALID_FLAG='1' "
                        };
                        $.ajax({
                            type: 'get',
                            url: '/api/EQUIPMENT_CLASS_PROC_INFO/GetDataQuery',
                            data: j,
                            dataType: 'json',
                            success: function (data) {
                                $('#Table_EQUIPMENT_CLASS_ID').datagrid('loadData', data);
                            }
                        });

                    });
                });
            }
        }, {
            text: '修改',
            handler: function () {
                var x = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
                if (x == null) return;
                addOrEdit_PACKAGE_PROC_EQUIP_CLASS_INFO = 'edit';
                GROUP_NO_EQUIP_CLASS_INFO_BATCH_Edit();
                EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO();
                $('#EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO').combobox('readonly', true);
                $('#EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO').combobox('setValue', x.EQUIPMENT_TYPE_ID);
                $('#btn_EQUIPMENT_CLASS_ID').hide();
                $('#EQUIPMENT_CLASS_ID').val(x.EQUIPMENT_CLASS_ID).attr('readonly', true);
                $('#REMARK').val(x.REMARK);
                $('#UPDATE_USER_PACKAGE_PROC_EQUIP_CLASS_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_EQUIP_CLASS_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO').dialog('open');
            }
        }, {
            text: '删除',

            handler: function () {
                Deleterow_PACKAGE_PROC_EQUIP_CLASS_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_EQUIP_CLASS_INFO);
                var changedRow = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_PROC_EQUIP_CLASS_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_PROC_EQUIP_CLASS_INFO = undefined;
                $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            handler: function () {
                $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            {
                field: 'EQUIPMENT_TYPE_ID', title: '类型', align: 'left', width: 35, formatter: function (value, row, index) {
                    switch (value) {
                        case 'MC':
                            return '设备';
                        case 'TESTER':
                            return '测试';
                        case 'FIXTURE':
                            return '夹具';
                    }
                }
            },
            {
                field: 'EQUIPMENT_CLASS_ID', title: '分类', align: 'left', width: 120, styler: function (value, row, index) { return 'color:blue'; }
            }

        ]],
        columns: [[{
            field: 'EQUIPMENT_CLASS_DESC', title: '名称', align: 'left', width: 120
        },
            {
                field: 'REMARK', title: '备注', align: 'left', width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[25]']
                    }
                }
            },
            { field: 'UPDATE_USER', title: '最后更新者', width: 100, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 130, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PROC_EQUIP_CLASS_INFO(rowIndex);
            CurrentParamType = ParamType.EQUIP_CLASS;
            switch (rowData.EQUIPMENT_TYPE_ID) {
                case 'MC':
                    QueryStr_ParamType = "AND PARAM.PARAM_TYPE_ID='MC'";
                    break;
                case 'FIXTURE':
                    QueryStr_ParamType = "AND PARAM.PARAM_TYPE_ID='FIXTURE'";
                    break;
                case 'TESTER':
                    QueryStr_ParamType = "AND PARAM.PARAM_TYPE_ID='TESTER'";
                    break;
            }
            $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('unselectAll');
            Init_Table_PACKAGE_PARAM_SETTING();
            $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);

        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PROC_EQUIP_CLASS_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('refreshRow', index);
        }
    });

}
function Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO() {
    var url = '';
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;
    var t = $('#EQUIPMENT_TYPE_ID').combobox('getValue');
    if (t == '') {
        url = '/api/PACKAGE_PROC_EQUIP_CLASS_INFO/GetDataByProcessIdAndGroupNo';
    } else {
        url = '/api/PACKAGE_PROC_EQUIP_CLASS_INFO/GetDataByProcessIdAndGroupNoAndTypeId'
    };
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': GROUP_NO,
        'VERSION_NO': versionNo,
        'PROCESS_ID': PROCESS_ID,
        'FACTORY_ID': factoryId,
        'EQUIPMENT_TYPE_ID': t,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: url,
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_PROC_EQUIP_CLASS_INFO(index) {
    if (editIndex_PACKAGE_PROC_EQUIP_CLASS_INFO != undefined)
        $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_EQUIP_CLASS_INFO);
    $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PROC_EQUIP_CLASS_INFO() {
    var row = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PROC_EQUIP_CLASS_INFO(row);
        }
    });
}
function Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PROC_EQUIP_CLASS_INFO == 'add') {
                    Add_PACKAGE_PROC_EQUIP_CLASS_INFO();
                }
                else if (addOrEdit_PACKAGE_PROC_EQUIP_CLASS_INFO == 'edit') {
                    Edit_PACKAGE_PROC_EQUIP_CLASS_INFO();
                }
            }
        }]
    });
}
function Validate_PACKAGE_PROC_EQUIP_CLASS_INFO() {
    if (!(
        $('#REMARK').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_PROC_EQUIP_CLASS_INFO() {
    if (!Validate_PACKAGE_PROC_EQUIP_CLASS_INFO()) { return; }
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var y = $('#GROUP_NO_EQUIP_CLASS_INFO_BATCH').combobox('getValues') + '';

    var j = {
        'PACKAGE_NO': packageNo,
        'GROUPS': y,
        'VERSION_NO': versionNo,
        'PROCESS_ID': x.PROCESS_ID,
        'EQUIPMENT_CLASS_ID': $('#EQUIPMENT_CLASS_ID').val(),
        'FACTORY_ID': factoryId,
        'REMARK': $('#REMARK').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_CLASS_INFO/PostAdd',
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
                Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                $('#Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO').dialog('close');
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
function Edit_PACKAGE_PROC_EQUIP_CLASS_INFO() {
    if (!Validate_PACKAGE_PROC_EQUIP_CLASS_INFO()) { return; }
    var x = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PACKAGE_NO': x.PACKAGE_NO,
        'GROUP_NO': x.GROUP_NO,
        'GROUPS': $('#GROUP_NO_EQUIP_CLASS_INFO_BATCH').combobox('getValues') + '',
        'VERSION_NO': x.VERSION_NO,
        'PROCESS_ID': x.PROCESS_ID,
        'EQUIPMENT_CLASS_ID': x.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'REMARK': $('#REMARK').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_CLASS_INFO/PostEdit',
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
                Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                $('#Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_PROC_EQUIP_CLASS_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'REMARK': row.REMARK,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_CLASS_INFO/PostEdit',
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
function Delete_PACKAGE_PROC_EQUIP_CLASS_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_CLASS_INFO/PostDelete',
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
                Init_Table_PACKAGE_PROC_EQUIP_CLASS_INFO();
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function GROUP_NO_EQUIP_CLASS_INFO_BATCH() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_EQUIP_CLASS_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
function GROUP_NO_EQUIP_CLASS_INFO_BATCH_Edit() {
    var x = $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('getSelected');
    if (x == null) {
        return;
    }
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_EQUIP_CLASS_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + x.FACTORY_ID + '&packageNo=' + x.PACKAGE_NO + '&versionNo=' + x.VERSION_NO + '&processId=' + x.PROCESS_ID + "&queryStr=AND A.GROUP_NO!='" + x.GROUP_NO + "'",
        editable: false
    });
}
function EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $.ajax({
        type: 'get',
        url: '/api/EQUIPMENT_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + factoryId + "&queryStr= AND VALID_FLAG = '1'",
        async: false,
        success: function (data) {
            $('#EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO').combobox({
                //panelHeight: 200,
                valueField: 'EQUIPMENT_TYPE_ID',
                textField: 'EQUIPMENT_TYPE_DESC',
                multiple: false,
                editable: false,
                data: data,
                onSelect: function (record) {
                }
            });
        }
    });
}


/***********************************设备信息(设备PN)*************************************/
function Table_PACKAGE_PROC_EQUIP_INFO() {
    $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid({
        title: '设备PN',
        singleSelect: true,
        width: '355',
        height: '236',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_PROC_EQUIP_INFO = 'add';
                GROUP_NO_EQUIP_INFO_BATCH();
                //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x != null) {
                    $('#GROUP_NO_EQUIP_INFO_BATCH').combobox('setValue', x.GROUP_NO);
                }
                $('#EQUIPMENT_TYPE_ID_EQUIP_INFO').combobox('readonly', false);
                $('#EQUIPMENT_ID').val('').attr('readonly', true);
                $('#REMARK_PACKAGE_PROC_EQUIP_INFO').val('').attr('readonly', false);
                $('#UPDATE_USER_PACKAGE_PROC_EQUIP_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_EQUIP_INFO').val('').attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_EQUIP_INFO').dialog('open');
                $('#btn_EQUIPMENT_ID').click(function () {
                    EQUIPMENT_TYPE_ID_EQUIP_INFO();

                    var t = $('#EQUIPMENT_TYPE_ID').combobox('getValue');
                    if (t != '') {
                        $('#EQUIPMENT_TYPE_ID_EQUIP_INFO').combobox('setValue', t);
                    }

                    $('#Table_EQUIPMENT_ID').datagrid({
                        title: '',
                        singleSelect: true,
                        width: '300',
                        height: '236',
                        columns: [[
                            { field: 'EQUIPMENT_ID', title: '编号', width: 100, align: 'left' },
                            { field: 'EQUIPMENT_NAME', title: '英文名', width: 90, align: 'left' },
                            { field: 'EQUIPMENT_DESC', title: '中文名', width: 90, align: 'left' }
                        ]],
                        method: 'get',
                        onClickRow: function (rowIndex, rowData) {
                            $('#EQUIPMENT_ID').val(rowData.EQUIPMENT_ID);
                            $('#Dialog_EQUIPMENT_ID').dialog('close');
                        }
                    });
                    $('#Table_EQUIPMENT_ID').datagrid('loadData', []);
                    $('#Dialog_EQUIPMENT_ID').dialog('open');
                    $('#btn_Search_EQUIPMENT_ID').click(function () {
                        //var p = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                        var p = $('#Table_PROCESS_LIST').datagrid('getSelected');
                        if (p == null) {
                            return;
                        }
                        var PROCESS_ID = p.PROCESS_ID;
                        var j = {
                            'PROCESS_ID': p.PROCESS_ID,
                            'FACTORY_ID': factoryId,
                            'EQUIPMENT_TYPE_ID': $('#EQUIPMENT_TYPE_ID_EQUIP_INFO').combobox('getValue'),
                            'EQUIPMENT_CLASS_ID': $('#EQUIPMENT_CLASS_ID_EQUIP_INFO').combobox('getValue'),
                            'EQUIPMENT_ID': $('#Search_EQUIPMENT_ID').val(),
                            'EQUIPMENT_NAME': $('#Search_EQUIPMENT_NAME').val(),
                            'EQUIPMENT_DESC': $('#Search_EQUIPMENT_DESC').val(),
                            'queryStr': ''
                        };
                        $.ajax({
                            type: 'get',
                            url: '/api/EQUIPMENT_PROC_INFO/GetDataQuery',
                            data: j,
                            dataType: 'json',
                            success: function (data) {
                                $('#Table_EQUIPMENT_ID').datagrid('loadData', data);
                            }
                        });

                    });
                });
            }
        }, {
            text: '修改',
            handler: function () {
                addOrEdit_PACKAGE_PROC_EQUIP_INFO = 'edit';
                var x = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
                if (x == null) return;
                GROUP_NO_EQUIP_INFO_BATCH_Edit();
                EQUIPMENT_TYPE_ID_EQUIP_INFO();
                $('#EQUIPMENT_TYPE_ID_EQUIP_INFO').combobox('setValue', x.EQUIPMENT_TYPE_ID).combobox('readonly', true);
                $('#EQUIPMENT_ID').val(x.EQUIPMENT_ID).attr('readonly', true);
                $('#REMARK_PACKAGE_PROC_EQUIP_INFO').val(x.REMARK);
                $('#UPDATE_USER_PACKAGE_PROC_EQUIP_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_EQUIP_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_EQUIP_INFO').dialog('open');
            }
        }, {
            text: '删除',
            handler: function () {
                Deleterow_PACKAGE_PROC_EQUIP_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_EQUIP_INFO);
                var changedRow = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_PROC_EQUIP_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_PROC_EQUIP_INFO = undefined;
                $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            handler: function () {
                $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            {
                field: 'EQUIPMENT_TYPE_ID', title: '类型', align: 'left', width: 35, formatter: function (value, row, index) {
                    switch (value) {
                        case 'MC':
                            return '设备';
                        case 'TESTER':
                            return '测试';
                        case 'FIXTURE':
                            return '夹具';
                    }
                }
            },
            {
                field: 'EQUIPMENT_ID', title: '设备PN', align: 'left', width: 150, styler: function (value, row, index) { return 'color:blue'; },
                formatter: function (value, row, index) {
                    return value + (row.EQUIPMENT_DESC == null ? '' : ' ' + row.EQUIPMENT_DESC);
                }
            }
        ]],
        columns: [[
            { field: 'REMARK', title: '备注', align: 'left', width: 100, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 80 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 140 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PROC_EQUIP_INFO(rowIndex);
            CurrentParamType = ParamType.EQUIP_INFO;
            switch (rowData.EQUIPMENT_TYPE_ID) {
                case 'MC':
                    QueryStr_ParamType = "AND PARAM.PARAM_TYPE_ID='MC'";
                    break;
                case 'FIXTURE':
                    QueryStr_ParamType = "AND PARAM.PARAM_TYPE_ID='FIXTURE'";
                    break;
                case 'TESTER':
                    QueryStr_ParamType = "AND PARAM.PARAM_TYPE_ID='TESTER'";
                    break;
            }
            $('#Table_PACKAGE_PROC_EQUIP_CLASS_INFO').datagrid('unselectAll');
            Init_Table_PACKAGE_PARAM_SETTING();
            $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PROC_EQUIP_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('refreshRow', index);
        }
    });


}
function Init_Table_PACKAGE_PROC_EQUIP_INFO() {
    var url = '';
    var j;
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;
    var t = $('#EQUIPMENT_TYPE_ID').combobox('getValue');
    if (t == '') {
        url = '/api/PACKAGE_PROC_EQUIP_INFO/GetDataByProcessIdAndGroupNo';
        j = {
            'PACKAGE_NO': packageNo,
            'GROUP_NO': GROUP_NO,
            'VERSION_NO': versionNo,
            'PROCESS_ID': PROCESS_ID,
            'FACTORY_ID': factoryId
        };
    } else {
        url = '/api/PACKAGE_PROC_EQUIP_INFO/GetDataByProcessIdAndGroupNoAndTypeId';
        j = {
            'PACKAGE_NO': packageNo,
            'GROUP_NO': GROUP_NO,
            'VERSION_NO': versionNo,
            'PROCESS_ID': PROCESS_ID,
            'FACTORY_ID': factoryId,
            'EQUIPMENT_TYPE_ID': t,
            'queryStr': ''
        };
    };
    $.ajax({
        type: 'get',
        url: url,
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_PROC_EQUIP_INFO(index) {
    if (editIndex_PACKAGE_PROC_EQUIP_INFO != undefined)
        $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('endEdit', editIndex_PACKAGE_PROC_EQUIP_INFO);
    $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PROC_EQUIP_INFO() {
    var row = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PROC_EQUIP_INFO(row);
        }
    });
}
function Dialog_PACKAGE_PROC_EQUIP_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PACKAGE_PROC_EQUIP_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PROC_EQUIP_INFO == 'add') {
                    Add_PACKAGE_PROC_EQUIP_INFO();
                }
                else if (addOrEdit_PACKAGE_PROC_EQUIP_INFO == 'edit') {
                    Edit_PACKAGE_PROC_EQUIP_INFO();
                }
            }
        }]
    });
}
function Add_PACKAGE_PROC_EQUIP_INFO() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var y = $('#GROUP_NO_EQUIP_INFO_BATCH').combobox('getValues') + '';
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUPS': y,
        'VERSION_NO': versionNo,
        'PROCESS_ID': x.PROCESS_ID,
        'EQUIPMENT_ID': $('#EQUIPMENT_ID').val(),
        'FACTORY_ID': factoryId,
        'REMARK': $('#REMARK_PACKAGE_PROC_EQUIP_INFO').val(),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_PROC_EQUIP_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_PROC_EQUIP_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_INFO/PostAdd',
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
                Init_Table_PACKAGE_PROC_EQUIP_INFO();
                $('#Dialog_PACKAGE_PROC_EQUIP_INFO').dialog('close');
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
function Edit_PACKAGE_PROC_EQUIP_INFO() {
    var x = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PACKAGE_NO': x.PACKAGE_NO,
        'GROUP_NO': x.GROUP_NO,
        'GROUPS': $('#GROUP_NO_EQUIP_INFO_BATCH').combobox('getValues') + '',
        'VERSION_NO': x.VERSION_NO,
        'PROCESS_ID': x.PROCESS_ID,
        'EQUIPMENT_TYPE_ID': x.EQUIPMENT_TYPE_ID,
        'EQUIPMENT_ID': x.EQUIPMENT_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'REMARK': $('#REMARK_PACKAGE_PROC_EQUIP_INFO').val(),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_PROC_EQUIP_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_PROC_EQUIP_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_INFO/PostEdit',
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
                Init_Table_PACKAGE_PROC_EQUIP_INFO();
                $('#Dialog_PACKAGE_PROC_EQUIP_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_PROC_EQUIP_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'EQUIPMENT_TYPE_ID': row.EQUIPMENT_TYPE_ID,
        'EQUIPMENT_ID': row.EQUIPMENT_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'REMARK': row.REMARK,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_INFO/PostEdit',
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
function Delete_PACKAGE_PROC_EQUIP_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'EQUIPMENT_ID': row.EQUIPMENT_ID,
        'EQUIPMENT_TYPE_ID': row.EQUIPMENT_TYPE_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_EQUIP_INFO/PostDelete',
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
                Init_Table_PACKAGE_PROC_EQUIP_INFO();
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function GROUP_NO_EQUIP_INFO_BATCH() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_EQUIP_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
function GROUP_NO_EQUIP_INFO_BATCH_Edit() {
    var x = $('#Table_PACKAGE_PROC_EQUIP_INFO').datagrid('getSelected');
    if (x == null) {
        return;
    }
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_EQUIP_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + x.FACTORY_ID + '&packageNo=' + x.PACKAGE_NO + '&versionNo=' + x.VERSION_NO + '&processId=' + x.PROCESS_ID + "&queryStr=AND A.GROUP_NO!='" + x.GROUP_NO + "'",
        editable: false
    });
}
function EQUIPMENT_TYPE_ID_EQUIP_INFO() {
    $.ajax({
        type: 'get',
        url: '/api/EQUIPMENT_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + factoryId + "&queryStr= AND VALID_FLAG = '1'",
        async: false,
        success: function (data) {
            $('#EQUIPMENT_TYPE_ID_EQUIP_INFO').combobox({
                valueField: 'EQUIPMENT_TYPE_ID',
                textField: 'EQUIPMENT_TYPE_DESC',
                multiple: false,
                editable: false,
                data: data,
                onSelect: function (record) {
                    EQUIPMENT_CLASS_ID_EQUIP_INFO();
                }
            });
        }
    });
}
function EQUIPMENT_CLASS_ID_EQUIP_INFO() {
    var x = $('#EQUIPMENT_TYPE_ID_EQUIP_INFO').combobox('getValue');
    if (x == null) return;
    //var p = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var p = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (p == null) {
        return;
    }
    var j = {
        'EQUIPMENT_TYPE_ID': x,
        'PROCESS_ID': p.PROCESS_ID,
        'FACTORY_ID': factoryId,
        'queryStr': queryStr
    };
    $.ajax({
        type: 'get',
        url: '/api/EQUIPMENT_CLASS_PROC_INFO/GetDataByProcessIdAndTypeId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#EQUIPMENT_CLASS_ID_EQUIP_INFO').combogrid({
                idField: 'EQUIPMENT_CLASS_ID',
                textField: 'EQUIPMENT_CLASS_DESC',
                multiple: false,
                method: 'get',
                panelWidth: 400,
                panelHeight: 250,
                columns: [[
                    { field: 'EQUIPMENT_CLASS_ID', title: '类型', width: 100 },
                    { field: 'EQUIPMENT_CLASS_NAME', title: '英文名', width: 130 },
                    { field: 'EQUIPMENT_CLASS_DESC', title: '中文名', width: 150 }
                ]],
                onClickRow: function (rowIndex, rowData) {
                }
            });
            $('#EQUIPMENT_CLASS_ID_EQUIP_INFO').combogrid('grid').datagrid('loadData', data);
        }
    });

}

/***********************************附图信息*************************************/
function Table_PACKAGE_ILLUSTRATION_INFO() {
    $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid({
        singleSelect: true,
        width: '712',
        height: '136',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_ILLUSTRATION_INFO = 'add';
                ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO();
                ILLUSTRATION_DATA_Upload();
                GROUP_NO_ILLUSTRATION_INFO_BATCH();
                //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x != null) {
                    $('#GROUP_NO_ILLUSTRATION_INFO_BATCH').combobox('setValue', x.GROUP_NO);
                }
                $('#ILLUSTRATION_DESC').val('').attr('readonly', true);
                $('#VALID_FLAG').val('1').attr('readonly', false);
                $('#UPDATE_USER_PACKAGE_ILLUSTRATION_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_ILLUSTRATION_INFO').val('').attr('readonly', true);
                $('#ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO').combobox('readonly', false);
                $('#dlg_showimg').html('');
                $('#Dialog_PACKAGE_ILLUSTRATION_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                var x = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
                if (x == null) return;
                addOrEdit_PACKAGE_ILLUSTRATION_INFO = 'edit';
                ILLUSTRATION_DATA_Upload();
                GROUP_NO_ILLUSTRATION_INFO_BATCH_Edit();
                ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO();
                $('#ILLUSTRATION_DESC').val(x.ILLUSTRATION_DESC).attr('readonly', true);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER_PACKAGE_ILLUSTRATION_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_ILLUSTRATION_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO').combobox('setValue', x.ILLUSTRATION_ID);
                $('#Dialog_PACKAGE_ILLUSTRATION_INFO').dialog('open');
                $('#dlg_showimg').html('<img src=\"PACKAGE_ILLUSTRATION_INFO_ShowImg.ashx?ILLUSTRATION_ID=' + x.ILLUSTRATION_ID + '&FACTORY_ID=' + x.FACTORY_ID + '&PACKAGE_NO=' + x.PACKAGE_NO + '&VERSION_NO=' + x.VERSION_NO + '&GROUP_NO=' + x.GROUP_NO + '&PROCESS_ID=' + x.PROCESS_ID + '\" \>');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_ILLUSTRATION_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('endEdit', editIndex_PACKAGE_ILLUSTRATION_INFO);
                var changedRow = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_ILLUSTRATION_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_ILLUSTRATION_INFO = undefined;
                $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            { field: 'ILLUSTRATION_DESC', title: '备注', align: 'left', width: 400 }
        ]],
        columns: [[
            {
                field: 'ILLUSTRATION_ID', title: '图片编号', align: 'left', width: 120
            },
            { field: 'VALID_FLAG', title: '启用', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_ILLUSTRATION_INFO(rowIndex);
            CurrentParamType = ParamType.IMG;
            Init_Table_PACKAGE_PARAM_SETTING();
            $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
            $('#showimg').html('<img src=\"PACKAGE_ILLUSTRATION_INFO_ShowImg.ashx?ILLUSTRATION_ID=' + rowData.ILLUSTRATION_ID + '&FACTORY_ID=' + rowData.FACTORY_ID + '&PACKAGE_NO=' + rowData.PACKAGE_NO + '&VERSION_NO=' + rowData.VERSION_NO + '&GROUP_NO=' + rowData.GROUP_NO + '&PROCESS_ID=' + rowData.PROCESS_ID + '\" />');



            //$('#msg').html('<img src=\"data:image/png;base64,' +rowData.ILLUSTRATION_DATA + '\" />');

        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_ILLUSTRATION_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PACKAGE_ILLUSTRATION_INFO() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': GROUP_NO,
        'VERSION_NO': versionNo,
        'PROCESS_ID': PROCESS_ID,
        'FACTORY_ID': factoryId
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_ILLUSTRATION_INFO/GetDataByProcessIdAndGroupNo',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_ILLUSTRATION_INFO(index) {
    if (editIndex_PACKAGE_ILLUSTRATION_INFO != undefined)
        $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('endEdit', editIndex_PACKAGE_ILLUSTRATION_INFO);
    $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_ILLUSTRATION_INFO() {
    var row = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_ILLUSTRATION_INFO(row);
        }
    });
}
function Dialog_PACKAGE_ILLUSTRATION_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PACKAGE_ILLUSTRATION_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_ILLUSTRATION_INFO == 'add') {
                    Add_PACKAGE_ILLUSTRATION_INFO();
                }
                else if (addOrEdit_PACKAGE_ILLUSTRATION_INFO == 'edit') {
                    Edit_PACKAGE_ILLUSTRATION_INFO();
                }
            }
        }]
    });
}
function Validate_PACKAGE_ILLUSTRATION_INFO() {
    if (!(
        $('#ILLUSTRATION_DESC').validatebox('isValid'),
        $('#ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO').combobox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_ILLUSTRATION_INFO() {
    if (!Validate_PACKAGE_ILLUSTRATION_INFO()) {
        return;
    }
    var x = $('#GROUP_NO_ILLUSTRATION_INFO_BATCH').combobox('getValues') + '';
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    var ILLUSTRATION_ID = $('#ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO').combobox('getValue');
    if (ILLUSTRATION_ID == '') return;

    if (x == '' || y == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择分组',
            showType: 'show'
        });
        return;
    }

    var processId = y.PROCESS_ID;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUPS': x,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PROCESS_ID': processId,
        'ILLUSTRATION_DESC': $('#ILLUSTRATION_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'ILLUSTRATION_ID': ILLUSTRATION_ID,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_ILLUSTRATION_INFO/PostAdd',
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
                Init_Table_PACKAGE_ILLUSTRATION_INFO();
                $('#Dialog_PACKAGE_ILLUSTRATION_INFO').dialog('close');
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
function Edit_PACKAGE_ILLUSTRATION_INFO() {
    if (!Validate_PACKAGE_ILLUSTRATION_INFO()) {
        return;
    }
    var z = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
    var ILLUSTRATION_ID = $('#ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO').combobox('getValue');
    if (ILLUSTRATION_ID == '') return;
    if (z == null) {
        return;
    }
    var j = {
        'PACKAGE_NO': z.PACKAGE_NO,
        'GROUPS': $('#GROUP_NO_ILLUSTRATION_INFO_BATCH').combobox('getValues') + '',
        'GROUP_NO': z.GROUP_NO,
        'VERSION_NO': z.VERSION_NO,
        'FACTORY_ID': z.FACTORY_ID,
        'PROCESS_ID': z.PROCESS_ID,
        'ILLUSTRATION_DESC': $('#ILLUSTRATION_DESC').val(),
        'ILLUSTRATION_DATA': $('#ILLUSTRATION_DATA').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'ILLUSTRATION_ID': ILLUSTRATION_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_ILLUSTRATION_INFO/PostEdit',
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
                Init_Table_PACKAGE_ILLUSTRATION_INFO();
                $('#Dialog_PACKAGE_ILLUSTRATION_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_ILLUSTRATION_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'ILLUSTRATION_DESC': row.ILLUSTRATION_DESC,
        'ILLUSTRATION_DATA': row.ILLUSTRATION_DATA,
        'VALID_FLAG': row.VALID_FLAG,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'ILLUSTRATION_ID': row.ILLUSTRATION_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_ILLUSTRATION_INFO/PostEdit',
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
function Delete_PACKAGE_ILLUSTRATION_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'ILLUSTRATION_ID': row.ILLUSTRATION_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_ILLUSTRATION_INFO/PostDelete',
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
                Init_Table_PACKAGE_ILLUSTRATION_INFO();
                $('#Table_PACKAGE_PARAM_SETTING').datagrid('loadData', []);
                $('#Table_PACKAGE_PARAM_SPEC_INFO').datagrid('loadData', []);
                $('#showimg').html('');
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO() {
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (y == null) {
        return;
    }
    var PROCESS_ID = y.PROCESS_ID;
    $('#ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO').combobox({
        panelHeight: 100,
        valueField: 'ILLUSTRATION_ID',
        textField: 'ILLUSTRATION_DESC',
        method: 'get',
        url: '/api/ILLUSTRATION_LIST/GetDataByFactoryIdAndTypeAndProcessId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&PROCESS_ID=' + PROCESS_ID,
        editable: false,
        required: true,
        onSelect: function (record) {
            $('#ILLUSTRATION_DESC').val(record.ILLUSTRATION_DESC);
            $('#dlg_showimg').html('<img src=\"\\BaseInfo\\ILLUSTRATION_LIST_ShowImg.ashx?ILLUSTRATION_ID=' + record.ILLUSTRATION_ID + '&FACTORY_ID=' + record.FACTORY_ID + '&PRODUCT_TYPE_ID=' + record.PRODUCT_TYPE_ID + '&PRODUCT_PROC_TYPE_ID=' + record.PRODUCT_PROC_TYPE_ID + '\" \>');
        }
    });
}
function ILLUSTRATION_DATA_Upload() {
    $('#ILLUSTRATION_DATA_Upload').click(function () {
        if (!Validate_PACKAGE_ILLUSTRATION_INFO()) {
            return;
        }
        //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
        var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
        var y = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
        var z = $('#ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO').combobox('getValue');
        if (x == null || z == '' || ILLUSTRATION_DESC == '') {
            $.messager.show({
                title: '消息',
                msg: '请填写',
                showType: 'show'
            });
            return;
        };
        PROCESS_ID = x.PROCESS_ID;
        ILLUSTRATION_ID = z;
        ILLUSTRATION_DESC = $('#ILLUSTRATION_DESC').val();
        VALID_FLAG = $('#VALID_FLAG').val();
        var x = $('#GROUP_NO_ILLUSTRATION_INFO_BATCH').combobox('getValues') + '';
        GROUPS = x;
        GROUP_NO = y == null ? null : y.GROUP_NO;
        $('#url').val('');
        $('#Dialog_PACKAGE_ILLUSTRATION_INFO_UploadImg').dialog('open');
    });
}
function GROUP_NO_ILLUSTRATION_INFO_BATCH() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_ILLUSTRATION_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
function GROUP_NO_ILLUSTRATION_INFO_BATCH_Edit() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var z = $('#Table_PACKAGE_ILLUSTRATION_INFO').datagrid('getSelected');
    if (z == null) {
        return;
    }
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_ILLUSTRATION_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + "&queryStr=AND A.GROUP_NO!='" + z.GROUP_NO + "'",
        editable: false
    });
}



/***********************************BOM信息*************************************/
function Table_PACKAGE_BOM_SPEC_INFO() {
    $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid({
        title: '',
        singleSelect: true,
        width: '712',
        height: '474',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (y == null) {
                    return;
                }
                addOrEdit_PACKAGE_BOM_SPEC_INFO = 'add';
                GROUP_NO_BOM_SPEC_INFO_BATCH();
                //var x = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x != null) {
                    $('#GROUP_NO_BOM_SPEC_INFO_BATCH').combobox('setValue', x.GROUP_NO);
                }
                $('#P_PART_ID').val('').attr('readonly', true);
                $('#btn_P_PART_ID').show();
                $('#C_PART_ID').val('').attr('readonly', true);
                $('#btn_C_PART_ID').show();
                $('#P_PART_QTY').val('').attr('readonly', false);
                $('#C_PART_QTY').val('').attr('readonly', false);
                $('#IS_VIRTUAL_PART').val('0').attr('readonly', false);
                $('#IS_IQC_MATERIAL').val('0').attr('readonly', false);
                $('#IS_SUBSTITUTE').val('0').attr('readonly', false);
                $('#SYNC_DATE').val('').attr('readonly', false);
                $('#UPDATE_USER_PACKAGE_BOM_SPEC_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_BOM_SPEC_INFO').val('').attr('readonly', true);


                $('#btn_P_PART_ID').click(function () {
                    $('#Table_P_PART_ID').datagrid({
                        title: '',
                        singleSelect: true,
                        width: '300',
                        height: '234',
                        columns: [[
                            { field: 'MATERIAL_PN_ID', title: '物料PN', width: 260, align: 'left' }
                        ]],
                        onClickRow: function (rowIndex, rowData) {
                            $('#P_PART_ID').val(rowData.MATERIAL_PN_ID);
                            $('#Dialog_Search_P_PART_ID').dialog('close');
                        }
                    });
                    $('#btn_Search_P_PART_ID').click(function () {
                        $.ajax({
                            type: 'get',
                            url: '/api/PROCESS_MATERIAL_PN_INFO/GetDataSearchById',
                            data: {
                                'PROCESS_ID': y.PROCESS_ID,
                                'MATERIAL_PN_ID': $('#Search_P_PART_ID').val() == '' ? '%' : $('#Search_P_PART_ID').val(),
                                'PRODUCT_TYPE_ID': productTypeId,
                                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                                'FACTORY_ID': factoryId,
                                'queryStr': ''
                            },
                            success: function (data) {
                                $('#Table_P_PART_ID').datagrid('loadData', data);
                            }
                        });
                    });

                    $('#Dialog_Search_P_PART_ID').dialog('open');
                });

                $('#btn_C_PART_ID').click(function () {
                    $('#Table_C_PART_ID').datagrid({
                        title: '',
                        singleSelect: true,
                        width: '300',
                        height: '234',
                        columns: [[
                            { field: 'MATERIAL_PN_ID', title: '物料PN', width: 260, align: 'left' }
                        ]],
                        onClickRow: function (rowIndex, rowData) {
                            $('#C_PART_ID').val(rowData.MATERIAL_PN_ID);
                            $('#Dialog_Search_C_PART_ID').dialog('close');
                        }
                    });
                    $('#btn_Search_C_PART_ID').click(function () {

                        $.ajax({
                            type: 'get',
                            url: '/api/PROCESS_MATERIAL_PN_INFO/GetDataSearchById',
                            data: {
                                'PROCESS_ID': y.PROCESS_ID,
                                'MATERIAL_PN_ID': $('#Search_C_PART_ID').val() == '' ? '%' : $('#Search_C_PART_ID').val(),
                                'PRODUCT_TYPE_ID': productTypeId,
                                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                                'FACTORY_ID': factoryId,
                                'queryStr': ''
                            },
                            success: function (data) {
                                $('#Table_C_PART_ID').datagrid('loadData', data);
                            }
                        });
                    });

                    $('#Dialog_Search_C_PART_ID').dialog('open');
                });

                $('#Dialog_PACKAGE_BOM_SPEC_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_BOM_SPEC_INFO = 'edit';
                var x = $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('getSelected');
                if (x == null) return;
                GROUP_NO_BOM_SPEC_INFO_BATCH_Edit();
                $('#P_PART_ID').val(x.P_PART_ID).attr('readonly', true);
                $('#btn_P_PART_ID').hide();
                $('#C_PART_ID').val(x.C_PART_ID).attr('readonly', true);
                $('#btn_C_PART_ID').hide();
                $('#P_PART_QTY').val(x.P_PART_QTY);
                $('#C_PART_QTY').val(x.C_PART_QTY);
                $('#IS_VIRTUAL_PART').val(x.IS_VIRTUAL_PART);
                $('#IS_IQC_MATERIAL').val(x.IS_IQC_MATERIAL);
                $('#IS_SUBSTITUTE').val(x.IS_SUBSTITUTE);
                $('#SYNC_DATE').val(x.SYNC_DATE);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PACKAGE_BOM_SPEC_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_BOM_SPEC_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('endEdit', editIndex_PACKAGE_BOM_SPEC_INFO);
                var changedRow = $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_BOM_SPEC_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_BOM_SPEC_INFO = undefined;
                $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            { field: 'P_PART_ID', title: '父件P/N', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'C_PART_ID', title: '子件P/N', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            {
                field: 'P_PART_QTY', title: '父件数量', align: 'left',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'C_PART_QTY', title: '子件数量', align: 'left',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'IS_VIRTUAL_PART', title: '虚拟件', align: 'left',
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "是";
                        case '0':
                            return "";
                    }
                }
            },
            {
                field: 'IS_IQC_MATERIAL', title: 'IQC来料', align: 'left',
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "是";
                        case '0':
                            return "";
                    }
                }
            },
            {
                field: 'IS_SUBSTITUTE', title: '替代件', align: 'left',
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "是";
                        case '0':
                            return "";
                    }
                }
            },
            { field: 'SYNC_DATE', title: '同步日期', align: 'left', editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }

            //下拉列表范例：
            //editor: {
            //            type: 'combobox',
            //            options: {
            //                valueField: 'FACTORY_ID',
            //                textField: 'FACTORY_NAME',
            //                url: '/api/FACTORY_LIST/GetData',
            //                method: 'get',
            //                required: false
            //            }
            //        }

            //选择框
            //editor: { type: 'checkbox', options: { on: '1', off: '0' } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_BOM_SPEC_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_BOM_SPEC_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('refreshRow', index);
        }
    });


}
function Init_Table_PACKAGE_BOM_SPEC_INFO() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var PROCESS_ID = x.PROCESS_ID;
    //var y = $('#Table_GROUP_NO_PACKAGE_PROCESS').datagrid('getSelected');
    var y = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (y == null) return;
    var GROUP_NO = y.GROUP_NO;
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': GROUP_NO,
        'VERSION_NO': versionNo,
        'PROCESS_ID': PROCESS_ID,
        'FACTORY_ID': factoryId
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_BOM_SPEC_INFO/GetDataByProcessIdAndGroupNo',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_BOM_SPEC_INFO(index) {
    if (editIndex_PACKAGE_BOM_SPEC_INFO != undefined)
        $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('endEdit', editIndex_PACKAGE_BOM_SPEC_INFO);
    $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_BOM_SPEC_INFO() {
    var row = $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_BOM_SPEC_INFO(row);
        }
    });
}
function Dialog_PACKAGE_BOM_SPEC_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PACKAGE_BOM_SPEC_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_BOM_SPEC_INFO == 'add') {
                    Add_PACKAGE_BOM_SPEC_INFO();
                }
                else if (addOrEdit_PACKAGE_BOM_SPEC_INFO == 'edit') {
                    Edit_PACKAGE_BOM_SPEC_INFO();
                }
            }
        }]
    });
}
function Validate_PACKAGE_BOM_SPEC_INFO() {
    if (!(
        $('#P_PART_QTY').validatebox('isValid') &&
        $('#C_PART_QTY').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_BOM_SPEC_INFO() {
    if (!Validate_PACKAGE_BOM_SPEC_INFO()) {
        return;
    }
    var x = $('#GROUP_NO_BOM_SPEC_INFO_BATCH').combobox('getValues') + '';
    //var y = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == '' || y == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择工序，分组',
            showType: 'show'
        });
        return;
    }

    var j = {
        'PACKAGE_NO': packageNo,
        'GROUPS': x,
        'PROCESS_ID': y.PROCESS_ID,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'P_PART_ID': $('#P_PART_ID').val(),
        'C_PART_ID': $('#C_PART_ID').val(),
        'P_PART_QTY': $('#P_PART_QTY').val(),
        'C_PART_QTY': $('#C_PART_QTY').val(),
        'IS_VIRTUAL_PART': $('#IS_VIRTUAL_PART').val(),
        'IS_IQC_MATERIAL': $('#IS_IQC_MATERIAL').val(),
        'IS_SUBSTITUTE': $('#IS_SUBSTITUTE').val(),
        'SYNC_DATE': $('#SYNC_DATE').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BOM_SPEC_INFO/PostAdd',
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
                Init_Table_PACKAGE_BOM_SPEC_INFO();
                $('#Dialog_PACKAGE_BOM_SPEC_INFO').dialog('close');
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
function Edit_PACKAGE_BOM_SPEC_INFO() {
    if (!Validate_PACKAGE_BOM_SPEC_INFO()) {
        return;
    }
    var x = $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('getSelected');
    if (x == null) return;
    var y = $('#GROUP_NO_BOM_SPEC_INFO_BATCH').combobox('getValues') + '';
    var j = {
        'PACKAGE_NO': x.PACKAGE_NO,
        'GROUP_NO': x.GROUP_NO,
        'GROUPS': y,
        'PROCESS_ID': x.PROCESS_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'VERSION_NO': x.VERSION_NO,
        'P_PART_ID': x.P_PART_ID,
        'C_PART_ID': x.C_PART_ID,
        'P_PART_QTY': $('#P_PART_QTY').val(),
        'C_PART_QTY': $('#C_PART_QTY').val(),
        'IS_VIRTUAL_PART': $('#IS_VIRTUAL_PART').val(),
        'IS_IQC_MATERIAL': $('#IS_IQC_MATERIAL').val(),
        'IS_SUBSTITUTE': $('#IS_SUBSTITUTE').val(),
        'SYNC_DATE': $('#SYNC_DATE').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BOM_SPEC_INFO/PostEdit',
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
                Init_Table_PACKAGE_BOM_SPEC_INFO();
                $('#Dialog_PACKAGE_BOM_SPEC_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_BOM_SPEC_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'P_PART_ID': row.P_PART_ID,
        'C_PART_ID': row.C_PART_ID,
        'P_PART_QTY': row.P_PART_QTY,
        'C_PART_QTY': row.C_PART_QTY,
        'IS_VIRTUAL_PART': row.IS_VIRTUAL_PART,
        'IS_IQC_MATERIAL': row.IS_IQC_MATERIAL,
        'IS_SUBSTITUTE': row.IS_SUBSTITUTE,
        'SYNC_DATE': row.SYNC_DATE,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BOM_SPEC_INFO/PostEdit',
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
function Delete_PACKAGE_BOM_SPEC_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'VERSION_NO': row.VERSION_NO,
        'P_PART_ID': row.P_PART_ID,
        'C_PART_ID': row.C_PART_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BOM_SPEC_INFO/PostDelete',
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
                Init_Table_PACKAGE_BOM_SPEC_INFO();
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function GROUP_NO_BOM_SPEC_INFO_BATCH() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    $('#GROUP_NO_BOM_SPEC_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + processId + '&queryStr=',
        editable: false
    });
}
function GROUP_NO_BOM_SPEC_INFO_BATCH_Edit() {
    var z = $('#Table_PACKAGE_BOM_SPEC_INFO').datagrid('getSelected');
    if (z == null) {
        return;
    }
    $('#GROUP_NO_BOM_SPEC_INFO_BATCH').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + z.PROCESS_ID + "&queryStr=AND A.GROUP_NO!='" + z.GROUP_NO + "'",
        editable: false
    });
}

/***********************************大分组*************************************/
function Table_PACKAGE_PROC_GRP_() {
    $('#Table_PACKAGE_PROC_GRP').datagrid({
        title: '大分组',
        singleSelect: true,
        width: '712',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
                var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (x == null) return;
                addOrEdit_PACKAGE_PROC_GRP = 'add';
                $('#grp').show();
                Init_GROUP_NO_PACKAGE_PROC_GRP();
                $('#PROC_GRP_ID').val('').attr('readonly', true);
                $('#PROC_GRP_DESC').val('').attr('readonly', false);
                $('#UPDATE_USER_PACKAGE_PROC_GRP').val('').attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_GRP').val('').attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_GRP').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                var x = $('#Table_PACKAGE_PROC_GRP').datagrid('getSelected');
                if (x == null) return;
                addOrEdit_PACKAGE_PROC_GRP = 'edit';
                $('#grp').hide();
                $('#PROC_GRP_DESC').val(x.PROC_GRP_DESC);
                $('#UPDATE_USER_PACKAGE_PROC_GRP').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PACKAGE_PROC_GRP').val(x.UPDATE_DATE).attr('readonly', true);
                $('#Dialog_PACKAGE_PROC_GRP').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_PROC_GRP();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_PROC_GRP').datagrid('endEdit', editIndex_PACKAGE_PROC_GRP);
                var changedRow = $('#Table_PACKAGE_PROC_GRP').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_PROC_GRP(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_PROC_GRP = undefined;
                $('#Table_PACKAGE_PROC_GRP').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_PROC_GRP').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PROC_GRP_ID', title: '编号', align: 'left', width: 140, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'PROC_GRP_DESC', title: '描述', align: 'left', width: 140, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', width: 140, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 140, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PROC_GRP(rowIndex);
            Init_Table_PACKAGE_PROC_GRP_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PROC_GRP = index;
            row.editing = true;
            $('#Table_PACKAGE_PROC_GRP').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_GRP').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_GRP').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_PACKAGE_PROC_GRP_() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PROCESS_ID': x.PROCESS_ID,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_PROC_GRP/GetDataByProcessId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_PROC_GRP').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_PROC_GRP(index) {
    if (editIndex_PACKAGE_PROC_GRP != undefined)
        $('#Table_PACKAGE_PROC_GRP').datagrid('endEdit', editIndex_PACKAGE_PROC_GRP);
    $('#Table_PACKAGE_PROC_GRP').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PROC_GRP() {
    var row = $('#Table_PACKAGE_PROC_GRP').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PROC_GRP(row);
        }
    });
}
function Dialog_PACKAGE_PROC_GRP() {
    $('#Dialog_PACKAGE_PROC_GRP').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PROC_GRP == 'add') {
                    Add_PACKAGE_PROC_GRP();
                }
                else if (addOrEdit_PACKAGE_PROC_GRP == 'edit') {
                    Edit_PACKAGE_PROC_GRP();
                }
            }
        }]
    });
}
function Validate_PACKAGE_PROC_GRP() {
    if (!(
        $('#PROC_GRP_ID').validatebox('isValid') &&
        $('#PACKAGE_NO').validatebox('isValid') &&
        $('#VERSION_NO').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PROCESS_ID').validatebox('isValid') &&
        $('#PROC_GRP_DESC').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_PROC_GRP() {
    var g = $('#GROUP_NO_PACKAGE_PROC_GRP').combogrid('getValues') + "";
    if (g == "") return;
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'GROUPS': g,
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PROCESS_ID': x.PROCESS_ID,
        'PROC_GRP_DESC': $('#PROC_GRP_DESC').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP/PostAdd',
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
                Init_Table_PACKAGE_PROC_GRP_();
                //Init_Table_PACKAGE_PROC_GRP(1,pageSize);
                $('#Dialog_PACKAGE_PROC_GRP').dialog('close');
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
function Edit_PACKAGE_PROC_GRP() {
    //if (!Validate_PACKAGE_PROC_GRP()) {
    //    return;
    //}
    var x = $('#Table_PACKAGE_PROC_GRP').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'PROC_GRP_ID': x.PROC_GRP_ID,
        'PACKAGE_NO': x.PACKAGE_NO,
        'VERSION_NO': x.VERSION_NO,
        'FACTORY_ID': x.FACTORY_ID,
        'PROCESS_ID': x.PROCESS_ID,
        'PROC_GRP_DESC': $('#PROC_GRP_DESC').val(),
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP/PostEdit',
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
                Init_Table_PACKAGE_PROC_GRP_();
                //Init_Table_PACKAGE_PROC_GRP(1,pageSize);
                $('#Dialog_PACKAGE_PROC_GRP').dialog('close');
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
function Edit_Cell_PACKAGE_PROC_GRP(row) {
    var j = {
        'PROC_GRP_ID': row.PROC_GRP_ID,
        'PACKAGE_NO': row.PACKAGE_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'PROC_GRP_DESC': row.PROC_GRP_DESC,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP/PostEdit',
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
function Delete_PACKAGE_PROC_GRP(row) {
    var j = {
        'PROC_GRP_ID': row.PROC_GRP_ID,
        'PACKAGE_NO': row.PACKAGE_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PROCESS_ID': row.PROCESS_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP/PostDelete',
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
                Init_Table_PACKAGE_PROC_GRP_();
                $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('loadData', []);
                //Init_Table_PACKAGE_PROC_GRP(1,pageSize);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}


function GROUP_NO_PACKAGE_PROC_GRP() {
    $('#GROUP_NO_PACKAGE_PROC_GRP').combogrid({
        idField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        panelWidth: 400,
        panelHeight: 250,
        columns: [[
            { checkbox: true },
            { field: 'GROUP_NO', title: '组别', width: 130 },
            { field: 'GROUP_QTY', title: '数量', width: 150 }
        ]]
    });
}
function Init_GROUP_NO_PACKAGE_PROC_GRP() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var j = {
        'factoryId': factoryId,
        'packageNo': packageNo,
        'versionNo': versionNo,
        'processId': processId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId',
        data: j,
        dataType: 'json',
        success: function (data) {
            GROUP_NO_PACKAGE_PROC_GRP();
            $('#GROUP_NO_PACKAGE_PROC_GRP').combogrid('grid').datagrid("loadData", data);
        }
    });
}

function Table_PACKAGE_PROC_GRP_LIST_() {
    $('#Table_PACKAGE_PROC_GRP_LIST').datagrid({
        title: '组别',
        singleSelect: true,
        width: '712',
        height: '223',
        fitColumns: false,
        //toolbar: [{
        //    text: '新增',
        //    iconCls: 'icon-add',
        //    handler: function () {
        //        addOrEdit_PACKAGE_PROC_GRP_LIST = 'add';
        //        GROUP_NO_PACKAGE_PROC_GRP_LIST();
        //        $('#GROUP_NO').val('').attr('readonly', false);
        //        $('#UPDATE_USER_PACKAGE_PROC_GRP_LIST').val('').attr('readonly', true);
        //        $('#UPDATE_DATE_PACKAGE_PROC_GRP_LIST').val('').attr('readonly', true);
        //        $('#Dialog_PACKAGE_PROC_GRP_LIST').dialog('open');
        //    }
        //},
        //{
        //    text: '修改',
        //    iconCls: 'icon-edit',
        //    handler: function () {
        //        addOrEdit_PACKAGE_PROC_GRP_LIST = 'edit';
        //        var x = $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('getSelected');
        //        if (x == null) return;
        //        $('#GROUP_NO').val(x.GROUP_NO).attr('readonly', true);
        //        $('#UPDATE_USER_PACKAGE_PROC_GRP_LIST').val(x.UPDATE_USER).attr('readonly', true);
        //        $('#UPDATE_DATE_PACKAGE_PROC_GRP_LIST').val(x.UPDATE_DATE).attr('readonly', true);
        //        $('#Dialog_PACKAGE_PROC_GRP_LIST').dialog('open');
        //    }
        //},
        //{
        //    text: '删除',
        //    iconCls: 'icon-cancel',
        //    handler: function () {
        //        Deleterow_PACKAGE_PROC_GRP_LIST();
        //    }
        //},
        //{
        //    text: '保存',
        //    iconCls: 'icon-save',
        //    handler: function () {
        //        $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('endEdit', editIndex_PACKAGE_PROC_GRP_LIST);
        //        var changedRow = $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('getChanges');
        //        if (changedRow.length > 0) {
        //            for (var i = 0; i < changedRow.length; i++) {
        //                Edit_Cell_PACKAGE_PROC_GRP_LIST(changedRow[i]);
        //            }
        //        }
        //        editIndex_PACKAGE_PROC_GRP_LIST = undefined;
        //        $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('acceptChanges');
        //    }
        //}, {
        //    text: '取消',
        //    iconCls: 'icon-undo',
        //    handler: function () {
        //        $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('rejectChanges');
        //    }
        //}
        //],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组别', align: 'left', width: 100, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: '最后更新者', width: 100, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 140, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_PROC_GRP_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_PROC_GRP_LIST = index;
            row.editing = true;
            $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_PACKAGE_PROC_GRP_LIST_() {
    var x = $('#Table_PACKAGE_PROC_GRP').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PROC_GRP_ID': x.PROC_GRP_ID,
        'PACKAGE_NO': x.PACKAGE_NO,
        'VERSION_NO': x.VERSION_NO,
        'FACTORY_ID': x.FACTORY_ID,
        'PROCESS_ID': x.PROCESS_ID,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_PROC_GRP_LIST/GetDataByGrpId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_PROC_GRP_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PACKAGE_PROC_GRP_LIST(index) {
    if (editIndex_PACKAGE_PROC_GRP_LIST != undefined)
        $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('endEdit', editIndex_PACKAGE_PROC_GRP_LIST);
    $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_PROC_GRP_LIST() {
    var row = $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PACKAGE_PROC_GRP_LIST(row);
        }
    });
}
function Dialog_PACKAGE_PROC_GRP_LIST() {
    $('#Dialog_PACKAGE_PROC_GRP_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_PROC_GRP_LIST == 'add') {
                    Add_PACKAGE_PROC_GRP_LIST();
                }
                else if (addOrEdit_PACKAGE_PROC_GRP_LIST == 'edit') {
                    Edit_PACKAGE_PROC_GRP_LIST();
                }
            }
        }]
    });
}
function Validate_PACKAGE_PROC_GRP_LIST() {
    if (!(
        $('#PROC_GRP_ID').validatebox('isValid') &&
        $('#PACKAGE_NO').validatebox('isValid') &&
        $('#VERSION_NO').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PROCESS_ID').validatebox('isValid') &&
        $('#GROUP_NO').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_PROC_GRP_LIST() {
    var x = $('#GROUP_NO_PACKAGE_PROC_GRP_LIST').combobox('getValue');
    if (x == '') return;
    var y = $('#Table_PACKAGE_PROC_GRP').datagrid('getSelected');
    if (y == null) return;
    var j = {
        'PROC_GRP_ID': y.PROC_GRP_ID,
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PROCESS_ID': y.PROCESS_ID,
        'GROUP_NO': x,
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_PROC_GRP_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_PROC_GRP_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP_LIST/PostAdd',
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
                Init_Table_PACKAGE_PROC_GRP_LIST_();
                //Init_Table_PACKAGE_PROC_GRP_LIST(1,pageSize);
                $('#Dialog_PACKAGE_PROC_GRP_LIST').dialog('close');
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
function Edit_PACKAGE_PROC_GRP_LIST() {
    //if (!Validate_PACKAGE_PROC_GRP_LIST()) {
    //    return;
    //}
    var x = $('#Table_PACKAGE_PROC_GRP_LIST').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'PROC_GRP_ID': x.PROC_GRP_ID,
        'PACKAGE_NO': x.PACKAGE_NO,
        'VERSION_NO': x.VERSION_NO,
        'FACTORY_ID': x.FACTORY_ID,
        'PROCESS_ID': x.PROCESS_ID,
        'GROUP_NO': x.GROUP_NO,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP_LIST/PostEdit',
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
                Init_Table_PACKAGE_PROC_GRP_LIST_();
                //Init_Table_PACKAGE_PROC_GRP_LIST(1,pageSize);
                $('#Dialog_PACKAGE_PROC_GRP_LIST').dialog('close');
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
function Edit_Cell_PACKAGE_PROC_GRP_LIST(row) {
    var j = {
        'PROC_GRP_ID': row.PROC_GRP_ID,
        'PACKAGE_NO': row.PACKAGE_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'GROUP_NO': row.GROUP_NO,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP_LIST/PostEdit',
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
function Delete_PACKAGE_PROC_GRP_LIST(row) {
    var j = {
        'PROC_GRP_ID': row.PROC_GRP_ID,
        'PACKAGE_NO': row.PACKAGE_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'PROCESS_ID': row.PROCESS_ID,
        'GROUP_NO': row.GROUP_NO
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_PROC_GRP_LIST/PostDelete',
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
                Init_Table_PACKAGE_PROC_GRP_LIST_();
                //Init_Table_PACKAGE_PROC_GRP_LIST(1,pageSize);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}

function GROUP_NO_PACKAGE_PROC_GRP_LIST() {
    //var x = $('#Table_PROCESS_LIST_PACKAGE').datagrid('getSelected');
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    $('#GROUP_NO_PACKAGE_PROC_GRP_LIST').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetGroupNoByProcessId?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&processId=' + x.PROCESS_ID + "&queryStr=",
        editable: false
    });
}



KindEditor.ready(function (K) {
    var editor = K.editor({
        allowFileManager: false
    });
    K('#image').click(function () {
        editor = K.editor({
            uploadJson: '/Package/PACKAGE_ILLUSTRATION_INFO.ashx?PACKAGE_NO=' + packageNo + '&VERSION_NO=' + versionNo + '&FACTORY_ID=' + factoryId + '&PROCESS_ID=' + PROCESS_ID + '&ILLUSTRATION_ID=' + ILLUSTRATION_ID + '&ILLUSTRATION_DESC=' + ILLUSTRATION_DESC + '&VALID_FLAG=' + VALID_FLAG + '&addOrEdit_PACKAGE_ILLUSTRATION_INFO=' + addOrEdit_PACKAGE_ILLUSTRATION_INFO + '&GROUPS=' + GROUPS + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&GROUP_NO=' + GROUP_NO,
            allowFileManager: true
        });
        editor.loadPlugin('image', function () {
            editor.plugin.imageDialog({
                showRemote: false,
                imageUrl: K('#url').val(),
                clickFn: function (url, title, width, height, border, align) {
                    K('#url').val(url);
                    editor.hideDialog();
                    $('#Dialog_PACKAGE_ILLUSTRATION_INFO_UploadImg').dialog('close');
                    $('#Dialog_PACKAGE_ILLUSTRATION_INFO').dialog('close');
                    Init_Table_PACKAGE_ILLUSTRATION_INFO();
                    $('#showimg').html('');
                }
            });
        });
    });
});

function InitBtnWf() {

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
}