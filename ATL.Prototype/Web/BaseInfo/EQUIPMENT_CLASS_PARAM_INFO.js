var pageSize = 20;
var editIndex_EQUIPMENT_CLASS_PARAM_INFO = undefined;
var addOrEdit_EQUIPMENT_CLASS_PARAM_INFO = null;
var editIndex_EQUIPMENT_CLASS_PROC_INFO = undefined;
var addOrEdit_EQUIPMENT_CLASS_PROC_INFO = null;
var editIndex_PARAMETER_LIST = undefined;
$(function () {
    Table_EQUIPMENT_CLASS_PARAM_INFO_();

    Dialog_EQUIPMENT_CLASS_PARAM_INFO();
    InitCurrentFactoryId();

    Table_EQUIPMENT_CLASS_PROC_INFO_([]);
    Dialog_EQUIPMENT_CLASS_PROC_INFO();

    Table_PARAMETER_LIST_();
});

function Table_EQUIPMENT_CLASS_PARAM_INFO_() {
    $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid({
        title: '设备分类-参数',
        singleSelect: true,
        width: '300',
        height: '368',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_EQUIPMENT_CLASS_PARAM_INFO = 'add';
                PARAMETER_ID();
                $('#EQUIPMENT_CLASS_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PRODUCT_TYPE_ID').val('').attr('readonly', false);
                $('#PRODUCT_PROC_TYPE_ID').val('').attr('readonly', false);
                $('#PARAMETER_ID').val('').attr('readonly', false);
                $('#DISP_ORDER_NO').val('').attr('readonly', false);
                $('#IS_SC_PARAM').val('0');
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_EQUIPMENT_CLASS_PARAM_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_EQUIPMENT_CLASS_PARAM_INFO = 'edit';
                var x = $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('getSelected');
                if (x == null) return;

                $('#EQUIPMENT_CLASS_ID').val(x.EQUIPMENT_CLASS_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#PARAMETER_ID').val(x.PARAMETER_ID).attr('readonly', true);
                $('#DISP_ORDER_NO').val(x.DISP_ORDER_NO);
                $('#IS_SC_PARAM').val(x.IS_SC_PARAM);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_EQUIPMENT_CLASS_PARAM_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_EQUIPMENT_CLASS_PARAM_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('endEdit', editIndex_EQUIPMENT_CLASS_PARAM_INFO);
                var changedRow = $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_EQUIPMENT_CLASS_PARAM_INFO(changedRow[i]);
                    }
                }
                editIndex_EQUIPMENT_CLASS_PARAM_INFO = undefined;
                $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PARAMETER_ID', title: '参数编号', align: 'left', width: 140, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            {
                field: 'DISP_ORDER_NO', title: '序号', align: 'left', width: 50,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            { field: 'IS_SC_PARAM', title: '在规格牌中', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 140 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 140 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_EQUIPMENT_CLASS_PARAM_INFO(rowIndex);
            Init_Table_PARAMETER_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_EQUIPMENT_CLASS_PARAM_INFO = index;
            row.editing = true;
            $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_EQUIPMENT_CLASS_PARAM_INFO_() {
    var x = $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('getSelected');
    if (x == null) {
        return;
    }
    var y = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (y == '') return;
    var z = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    if (z == '') return;
    var z1 = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    if (z1 == '') return;

    $.ajax({
        type: 'get',
        url: '/api/EQUIPMENT_CLASS_PARAM_INFO/GetDataByClassId',
        data: {
            'EQUIPMENT_CLASS_ID': x.EQUIPMENT_CLASS_ID,
            'PRODUCT_TYPE_ID': z,
            'PRODUCT_PROC_TYPE_ID': z1,
            'FACTORY_ID': y,
            'queryStr': ''
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}

function editrow_EQUIPMENT_CLASS_PARAM_INFO(index) {
    if (editIndex_EQUIPMENT_CLASS_PARAM_INFO != undefined)
        $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('endEdit', editIndex_EQUIPMENT_CLASS_PARAM_INFO);
    $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_EQUIPMENT_CLASS_PARAM_INFO() {
    var row = $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('getSelected');
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
            Delete_EQUIPMENT_CLASS_PARAM_INFO(row);
        }
    });
}
function Dialog_EQUIPMENT_CLASS_PARAM_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_EQUIPMENT_CLASS_PARAM_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_EQUIPMENT_CLASS_PARAM_INFO == 'add') {
                    Add_EQUIPMENT_CLASS_PARAM_INFO();
                }
                else if (addOrEdit_EQUIPMENT_CLASS_PARAM_INFO == 'edit') {
                    Edit_EQUIPMENT_CLASS_PARAM_INFO();
                }
            }
        }]
    });
}
function Validate_EQUIPMENT_CLASS_PARAM_INFO() {
    if (!(
        $('#DISP_ORDER_NO').validatebox('isValid')
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
function Add_EQUIPMENT_CLASS_PARAM_INFO() {
    if (!Validate_EQUIPMENT_CLASS_PARAM_INFO()) return;
    var x = $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('getSelected');
    if (x == null) {
        return;
    }
    var y = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (y == '') return;
    var z = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    if (z == '') return;
    var z1 = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    if (z1 == '') return;

    var p = $('#PARAMETER_ID').combobox('getValue');
    if (p == '') return;

    var j = {
        'EQUIPMENT_CLASS_ID': x.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': y,
        'PRODUCT_TYPE_ID': z,
        'PRODUCT_PROC_TYPE_ID': z1,
        'PARAMETER_ID': p,
        'DISP_ORDER_NO': $('#DISP_ORDER_NO').val(),
        'IS_SC_PARAM':$('#IS_SC_PARAM').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PARAM_INFO/PostAdd',
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
                Init_Table_EQUIPMENT_CLASS_PARAM_INFO_();
                $('#Dialog_EQUIPMENT_CLASS_PARAM_INFO').dialog('close');
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
function Edit_EQUIPMENT_CLASS_PARAM_INFO() {
    if (!Validate_EQUIPMENT_CLASS_PARAM_INFO()) return;
    var x = $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'EQUIPMENT_CLASS_ID': x.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'PARAMETER_ID': x.PARAMETER_ID,
        'DISP_ORDER_NO': $('#DISP_ORDER_NO').val(),
        'IS_SC_PARAM': $('#IS_SC_PARAM').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PARAM_INFO/PostEdit',
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
                Init_Table_EQUIPMENT_CLASS_PARAM_INFO_();
                $('#Dialog_EQUIPMENT_CLASS_PARAM_INFO').dialog('close');
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
function Edit_Cell_EQUIPMENT_CLASS_PARAM_INFO(row) {
    var j = {
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'DISP_ORDER_NO': row.DISP_ORDER_NO,
        'IS_SC_PARAM': row.IS_SC_PARAM,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PARAM_INFO/PostEdit',
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
function Delete_EQUIPMENT_CLASS_PARAM_INFO(row) {
    var j = {
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PARAM_INFO/PostDelete',
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
                Init_Table_EQUIPMENT_CLASS_PARAM_INFO_();
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

function InitCurrentFactoryId() {
    Init_FACTORY_ID_SEARCH();
    $('#FACTORY_ID_SEARCH').combobox('setValue', 'SSL-P');
    Init_PRODUCT_TYPE_ID_SEARCH();
    $('#PRODUCT_TYPE_ID_SEARCH').combobox('setValue', 'CE');
    Init_PRODUCT_PROC_TYPE_ID_SEARCH();
    $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('setValue', 'M6S');
    Table_EQUIPMENT_TYPE_LIST();
    Init_Table_PROCESS_LIST();
}

//厂别
function Init_FACTORY_ID_SEARCH() {
    $('#FACTORY_ID_SEARCH').combobox({
        valueField: 'FACTORY_ID',
        textField: 'FACTORY_DESC',
        url: '/api/FACTORY_LIST/GetData',
        method: 'get',
        panelHeight: 100,
        editable: false,
        onSelect: function (record) {
            Init_PRODUCT_TYPE_ID_SEARCH();
            $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('clear');
            Table_EQUIPMENT_TYPE_LIST();
            Init_Table_EQUIPMENT_CLASS_LIST_();
        }
    });
}
//产品类型（下拉框）
function Init_PRODUCT_TYPE_ID_SEARCH() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (factoryId == '') {
        return;
    }
    $('#PRODUCT_TYPE_ID_SEARCH').combobox({
        panelHeight: 100,
        valueField: 'PRODUCT_TYPE_ID',
        textField: 'PRODUCT_TYPE_ID',
        method: 'get',
        url: '/api/PRODUCT_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + factoryId,
        editable: false,
        required: true,
        onSelect: function (record) {
            Init_PRODUCT_PROC_TYPE_ID_SEARCH();
        }
    });
}
//产品工艺（下拉框）
function Init_PRODUCT_PROC_TYPE_ID_SEARCH() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    if (factoryId == '' || productTypeId == '') {
        return;
    }
    $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox({
        panelHeight: 100,
        valueField: 'PRODUCT_PROC_TYPE_ID',
        textField: 'PRODUCT_PROC_TYPE_ID',
        method: 'get',
        url: '/api/PRODUCT_PROC_TYPE_LIST/GetDataByProductTypeId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId,
        editable: false,
        required: true,
        onSelect: function (record) {
            Init_Table_PROCESS_LIST();
        }
    });
}

//工序
function Table_PROCESS_LIST(data) {
    $('#Table_PROCESS_LIST').datagrid({
        title: '工序',
        singleSelect: true,
        width: '240',
        height: '296',
        fitColumns: true,
        autoRowHeight: false,
        columns: [[
            { field: 'PROCESS_ID', title: '编号', align: 'left', width: 40 },
            { field: 'PROCESS_DESC', title: '工序', align: 'left', width: 100 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_EQUIPMENT_CLASS_PROC_INFO_();
        }
    });
    $('#Table_PROCESS_LIST').datagrid("loadData", data);
}
function Init_Table_PROCESS_LIST() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
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
            Table_PROCESS_LIST(data);
        }
    });
}

//设备类型
function Table_EQUIPMENT_TYPE_LIST() {
    var x = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (x == '') return;

    $('#Table_EQUIPMENT_TYPE_LIST').datagrid({
        title: '设备类型',
        singleSelect: true,
        width: '240',
        height: '130',
        fitColumns: false,
        method: 'get',
        url: '/api/EQUIPMENT_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + x + '&queryStr=',
        columns: [[
            { field: 'EQUIPMENT_TYPE_ID', title: '设备类型', align: 'left', width: 200 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_EQUIPMENT_CLASS_PROC_INFO_();
        }
    });
}

//工序设备分类
function Table_EQUIPMENT_CLASS_PROC_INFO_() {
    $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid({
        title: '工序-设备分类',
        singleSelect: true,
        width: '240',
        height: '368',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            handler: function () {
                addOrEdit_EQUIPMENT_CLASS_PROC_INFO = 'add';
                EQUIPMENT_CLASS_ID();
                $('#EQUIPMENT_CLASS_ID').combobox('readonly', false);
                $('#UPDATE_USER_EQUIPMENT_CLASS_PROC_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_EQUIPMENT_CLASS_PROC_INFO').val('').attr('readonly', true);
                $('#REMARK').val('').attr('readonly', false);
                $('#Dialog_EQUIPMENT_CLASS_PROC_INFO').dialog('open');
            }
        }, {
            text: '修改',
            handler: function () {
                addOrEdit_EQUIPMENT_CLASS_PROC_INFO = 'edit';
                var x = $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('getSelected');
                if (x == null) return;
                EQUIPMENT_CLASS_ID();
                $('#EQUIPMENT_CLASS_ID').combobox('setValue', x.EQUIPMENT_CLASS_ID).combobox('readonly', true);
                $('#UPDATE_USER_EQUIPMENT_CLASS_PROC_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_EQUIPMENT_CLASS_PROC_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#REMARK').val(x.REMARK);
                $('#Dialog_EQUIPMENT_CLASS_PROC_INFO').dialog('open');
            }
        }, {
            text: '删除',
            handler: function () {
                Deleterow_EQUIPMENT_CLASS_PROC_INFO();
            }
        }, {
            text: '保存',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('endEdit', editIndex_EQUIPMENT_CLASS_PROC_INFO);
                var changedRow = $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_EQUIPMENT_CLASS_PROC_INFO(changedRow[i]);
                    }
                }
                editIndex_EQUIPMENT_CLASS_PROC_INFO = undefined;
                $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'EQUIPMENT_CLASS_ID', title: '编号', align: 'left', width: 120, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'REMARK', title: '备注', align: 'left', width: 120, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_EQUIPMENT_CLASS_PROC_INFO(rowIndex);
            Init_Table_EQUIPMENT_CLASS_PARAM_INFO_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_EQUIPMENT_CLASS_PROC_INFO = index;
            row.editing = true;
            $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_EQUIPMENT_CLASS_PROC_INFO_() {
    var x = $('#Table_EQUIPMENT_TYPE_LIST').datagrid('getSelected');
    if (x == null) {
        return;
    }
    var y = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (y == null) {
        return;
    }
    var z = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (z == '') return;
    var j = {
        'EQUIPMENT_TYPE_ID': x.EQUIPMENT_TYPE_ID,
        'PROCESS_ID': y.PROCESS_ID,
        'FACTORY_ID': z,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/EQUIPMENT_CLASS_PROC_INFO/GetDataByProcessIdAndTypeId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}

function editrow_EQUIPMENT_CLASS_PROC_INFO(index) {
    if (editIndex_EQUIPMENT_CLASS_PROC_INFO != undefined)
        $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('endEdit', editIndex_EQUIPMENT_CLASS_PROC_INFO);
    $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_EQUIPMENT_CLASS_PROC_INFO() {
    var row = $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('getSelected');
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
            Delete_EQUIPMENT_CLASS_PROC_INFO(row);
        }
    });
}
function Dialog_EQUIPMENT_CLASS_PROC_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_EQUIPMENT_CLASS_PROC_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_EQUIPMENT_CLASS_PROC_INFO == 'add') {
                    Add_EQUIPMENT_CLASS_PROC_INFO();
                }
                else if (addOrEdit_EQUIPMENT_CLASS_PROC_INFO == 'edit') {
                    Edit_EQUIPMENT_CLASS_PROC_INFO();
                }
            }
        }]
    });
}
function Validate_EQUIPMENT_CLASS_PROC_INFO() {
    if (!(
        $('#EQUIPMENT_CLASS_ID').validatebox('isValid') &&
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
function Add_EQUIPMENT_CLASS_PROC_INFO() {
    if (!Validate_EQUIPMENT_CLASS_PROC_INFO()) {
        return;
    }
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (factoryId == '') {
        return;
    }
    var y = $('#Table_EQUIPMENT_TYPE_LIST').datagrid('getSelected');
    if (y == null) {
        return;
    }
    var z = $('#EQUIPMENT_CLASS_ID').combobox('getValue');
    if (z == '') return;
    var j = {
        'PROCESS_ID': x.PROCESS_ID,
        'EQUIPMENT_CLASS_ID': z,
        'FACTORY_ID': factoryId,
        'UPDATE_USER': $('#UPDATE_USER_EQUIPMENT_CLASS_PROC_INFO').val(),
        'EQUIPMENT_TYPE_ID': y.EQUIPMENT_TYPE_ID,
        'UPDATE_DATE': $('#UPDATE_DATE_EQUIPMENT_CLASS_PROC_INFO').val(),
        'REMARK': $('#REMARK').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PROC_INFO/PostAdd',
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
                Init_Table_EQUIPMENT_CLASS_PROC_INFO_();

                $('#Dialog_EQUIPMENT_CLASS_PROC_INFO').dialog('close');
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
function Edit_EQUIPMENT_CLASS_PROC_INFO() {
    if (!Validate_EQUIPMENT_CLASS_PROC_INFO()) {
        return;
    }
    var x = $('#Table_EQUIPMENT_CLASS_PROC_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PROCESS_ID': x.PROCESS_ID,
        'EQUIPMENT_CLASS_ID': x.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': $('#UPDATE_USER_EQUIPMENT_CLASS_PROC_INFO').val(),
        'EQUIPMENT_TYPE_ID': x.EQUIPMENT_TYPE_ID,
        'UPDATE_DATE': $('#UPDATE_DATE_EQUIPMENT_CLASS_PROC_INFO').val(),
        'REMARK': $('#REMARK').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PROC_INFO/PostEdit',
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
                Init_Table_EQUIPMENT_CLASS_PROC_INFO_();
                $('#Dialog_EQUIPMENT_CLASS_PROC_INFO').dialog('close');
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
function Edit_Cell_EQUIPMENT_CLASS_PROC_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'EQUIPMENT_TYPE_ID': row.EQUIPMENT_TYPE_ID,
        'UPDATE_DATE': row.UPDATE_DATE,
        'REMARK': row.REMARK
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PROC_INFO/PostEdit',
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
function Delete_EQUIPMENT_CLASS_PROC_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_PROC_INFO/PostDelete',
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
                Init_Table_EQUIPMENT_CLASS_PROC_INFO_();
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

function EQUIPMENT_CLASS_ID() {
    var z = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (z == '') return;
    $('#EQUIPMENT_CLASS_ID').combobox({
        valueField: 'EQUIPMENT_CLASS_ID',
        textField: 'EQUIPMENT_CLASS_ID',
        url: '/api/EQUIPMENT_CLASS_LIST/GetDataByFactoryId?FACTORY_ID=' + z + "&queryStr= AND VALID_FLAG='1'",
        method: 'get',
        panelHeight: 100,
        editable: false,
        onSelect: function (record) {
        }
    });
}




function PARAMETER_ID() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');

    if (factoryId == "" ||
       productTypeId == "" ||
       produceProcTypeId == ""
       ) {
        return;
    }
    var PARAM_TYPE_ID;
    var y = $('#Table_EQUIPMENT_TYPE_LIST').datagrid('getSelected');
    if (y == null) return;

    switch (y.EQUIPMENT_TYPE_ID) {
        case 'MC':
            PARAM_TYPE_ID = 'MC';
            break;
        case 'FIXTURE':
            PARAM_TYPE_ID = 'FIXTURE';
            break
        case 'TESTER':
            PARAM_TYPE_ID = 'TESTER';
            break
    }
    $('#PARAMETER_ID').combogrid({
        url: "/api/PARAMETER_LIST/GetDataByPType?FACTORY_ID=" + factoryId + "&PRODUCT_TYPE_ID=" + productTypeId + "&PRODUCT_PROC_TYPE_ID=" + produceProcTypeId + "&PARAM_TYPE_ID=" + PARAM_TYPE_ID + "&queryStr= AND VALID_FLAG='1'",
        idField: 'PARAMETER_ID',
        textField: 'PARAM_DESC',
        multiple: false,
        method: 'get',
        panelWidth: 400,
        panelHeight: 300,
        columns: [[
        { field: 'PARAMETER_ID', title: '编号', width: 150 },
        { field: 'PARAM_DESC', title: '名称', width: 200 }
        ]],
        onClickRow: function (rowIndex, rowData) {
        }
    });
}



//参数
function Table_PARAMETER_LIST_() {
    $('#Table_PARAMETER_LIST').datagrid({
        title: '参数',
        singleSelect: true,
        width: '542',
        height: '130',
        fitColumns: false,
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PARAMETER_LIST').datagrid('endEdit', editIndex_PARAMETER_LIST);
                var changedRow = $('#Table_PARAMETER_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PARAMETER_LIST(changedRow[i]);
                    }
                }
                editIndex_PARAMETER_LIST = undefined;
                $('#Table_PARAMETER_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PARAMETER_LIST').datagrid('rejectChanges');
            }
        }],
        columns: [[
            {
                field: 'PARAM_IO', title: '输入/输出', align: 'left', width: 70,
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
                        case '2':
                            return "输出";
                    }
                }
            },
            {
                field: 'SOURCE', title: 'SOURCE', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_SPEC_PARAM', title: '规格牌', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_FIRST_CHECK_PARAM', title: '首件', align: 'left', width: 30, editor: {
                    type: 'checkbox', options: { on: '1', off: '0' }
                },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_PROC_MON_PARAM', title: '过程', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_OUTPUT_PARAM', title: '出货', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_VERSION_CTRL', title: '版本控制', align: 'left', width: 60, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'MEASURE_METHOD', title: '测量方法', align: 'left', width: 60, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_GROUP_PARAM', title: '分组参数', align: 'left', width: 60, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'PARAM_DATATYPE', title: '数据类型', align: 'left', width: 100, editor: {
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
                        case 'NUMBER':
                            return "数值";
                    }
                }
            },
            { field: 'PARAM_UNIT', title: '单位', align: 'left', width: 40, editor: 'text' },
            { field: 'TARGET', title: '目标值', align: 'left', width: 100, editor: 'text' },
            { field: 'USL', title: '上限', align: 'left', width: 100, editor: 'text' },
            { field: 'LSL', title: '下限', align: 'left', width: 100, editor: 'text' },

            { field: 'SAMPLING_FREQUENCY', title: '抽样频率', align: 'left', width: 100, editor: 'text' },
            { field: 'CONTROL_METHOD', title: '控制方法', align: 'left', width: 100, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更信者', align: 'left', width: 100 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 100 },
            { field: 'PARAM_NAME', title: '英文名', align: 'left', width: 100, editor: 'text' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PARAMETER_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PARAMETER_LIST = index;
            row.editing = true;
            $('#Table_PARAMETER_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PARAMETER_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PARAMETER_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PARAMETER_LIST_() {
    var x = $('#Table_EQUIPMENT_CLASS_PARAM_INFO').datagrid('getSelected');
    if (x == null) return;
    var parameterId = x.PARAMETER_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PARAMETER_ID': parameterId
    };
    $.ajax({
        type: 'get',
        url: '/api/PARAMETER_LIST/GetDataById',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PARAMETER_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PARAMETER_LIST(index) {
    if (editIndex_PARAMETER_LIST != undefined)
        $('#Table_PARAMETER_LIST').datagrid('endEdit', editIndex_PARAMETER_LIST);
    $('#Table_PARAMETER_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Edit_Cell_PARAMETER_LIST(row) {
    var j = {
        'PARAMETER_ID': row.PARAMETER_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PARAM_NAME': row.PARAM_NAME,
        'PARAM_DESC': row.PARAM_DESC,
        'PARAM_TYPE_ID': row.PARAM_TYPE_ID,
        'PARAM_IO': row.PARAM_IO,
        'SOURCE': row.SOURCE,
        'IS_SPEC_PARAM': row.IS_SPEC_PARAM,
        'IS_FIRST_CHECK_PARAM': row.IS_FIRST_CHECK_PARAM,
        'IS_PROC_MON_PARAM': row.IS_PROC_MON_PARAM,
        'IS_OUTPUT_PARAM': row.IS_OUTPUT_PARAM,
        'IS_VERSION_CTRL': row.IS_VERSION_CTRL,
        'MEASURE_METHOD': row.MEASURE_METHOD,
        'IS_GROUP_PARAM': row.IS_GROUP_PARAM,
        'PARAM_DATATYPE': row.PARAM_DATATYPE,
        'PARAM_UNIT': row.PARAM_UNIT,
        'TARGET': row.TARGET,
        'USL': row.USL,
        'LSL': row.LSL,
        'VALID_FLAG': row.VALID_FLAG,
        'SAMPLING_FREQUENCY': row.SAMPLING_FREQUENCY,
        'CONTROL_METHOD': row.CONTROL_METHOD
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAMETER_LIST/PostEdit',
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