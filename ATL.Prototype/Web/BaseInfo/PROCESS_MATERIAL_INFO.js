var pageSize = 20;
var editIndex_PROCESS_MATERIAL_INFO = undefined;
var addOrEdit_PROCESS_MATERIAL_INFO = null;
var editIndex_MATERIAL_TYPE_LIST = undefined;
var addOrEdit_MATERIAL_TYPE_LIST = null;
var editIndex_MATERIAL_PN_LIST = undefined;
var addOrEdit_MATERIAL_PN_LIST = null;
var editIndex_PROCESS_MATERIAL_PN_INFO = undefined;
var addOrEdit_PROCESS_MATERIAL_PN_INFO = null;
var editIndex_MATERIAL_PARA_INFO = undefined;
var addOrEdit_MATERIAL_PARA_INFO = null;
var editIndex_PARAMETER_LIST = undefined;
var editIndex_MATERIAL_PN_PARA_INFO = undefined;
var addOrEdit_MATERIAL_PN_PARA_INFO = null;

var editIndex_MATERIAL_TYPE_GRP = undefined;
var addOrEdit_MATERIAL_TYPE_GRP = null;

var editIndex_MATERIAL_TYPE_GRP_LIST = undefined;
var addOrEdit_MATERIAL_TYPE_GRP_LIST = null;

$(function () {
    Table_PROCESS_MATERIAL_INFO_();
    Dialog_PROCESS_MATERIAL_INFO();
    InitCurrentFactoryId();

    Table_MATERIAL_CATEGORY_LIST_();

    Table_MATERIAL_TYPE_LIST_();
    Dialog_MATERIAL_TYPE_LIST();

    Table_MATERIAL_PN_LIST_();
    Dialog_MATERIAL_PN_LIST();

    Table_PROCESS_MATERIAL_PN_INFO_();
    Dialog_PROCESS_MATERIAL_PN_INFO();

    Table_MATERIAL_PARA_INFO_();
    Dialog_MATERIAL_PARA_INFO();

    Table_PARAMETER_LIST_();

    Table_MATERIAL_PN_PARA_INFO_();
    Dialog_MATERIAL_PN_PARA_INFO();

    Table_MATERIAL_TYPE_GRP_();
    Init_Table_MATERIAL_TYPE_GRP_();
    Dialog_MATERIAL_TYPE_GRP();

    Table_MATERIAL_TYPE_GRP_LIST_();
    Dialog_MATERIAL_TYPE_GRP_LIST();
});

function Table_PROCESS_MATERIAL_INFO_() {
    $('#Table_PROCESS_MATERIAL_INFO').datagrid({
        title: '工序物料类型',
        singleSelect: true,
        width: '300',
        height: '248',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {

                addOrEdit_PROCESS_MATERIAL_INFO = 'add';
                var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (x == null) return;
                var y = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
                if (y == null) return;
                var processId = x.PROCESS_ID;
                var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
                var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
                var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
                if (factoryId == '' || productTypeId == '' || produceProcTypeId == '') {
                    return;
                }
                $('#MATERIAL_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', true);
                $('#UPDATE_DATE').val('').attr('readonly', true);
                $('#VALID_FLAG').val('1');
                $('#Dialog_PROCESS_MATERIAL_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PROCESS_MATERIAL_INFO = 'edit';
                var x = $('#Table_PROCESS_MATERIAL_INFO').datagrid('getSelected');
                if (x == null) return;

                $('#MATERIAL_TYPE_ID').val(x.MATERIAL_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE').val(x.UPDATE_DATE).attr('readonly', true);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#Dialog_PROCESS_MATERIAL_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PROCESS_MATERIAL_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PROCESS_MATERIAL_INFO').datagrid('endEdit', editIndex_PROCESS_MATERIAL_INFO);
                var changedRow = $('#Table_PROCESS_MATERIAL_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PROCESS_MATERIAL_INFO(changedRow[i]);
                    }
                }
                editIndex_PROCESS_MATERIAL_INFO = undefined;
                $('#Table_PROCESS_MATERIAL_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PROCESS_MATERIAL_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'MATERIAL_TYPE_ID', title: '物料类型编号', align: 'left', width: 250, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PROCESS_MATERIAL_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PROCESS_MATERIAL_INFO = index;
            row.editing = true;
            $('#Table_PROCESS_MATERIAL_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_MATERIAL_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_MATERIAL_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PROCESS_MATERIAL_INFO_() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_MATERIAL_INFO/GetDataByProcessId',
        data: {
            'PROCESS_ID': processId,
            'PRODUCT_TYPE_ID': productTypeId,
            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
            'FACTORY_ID': factoryId,
            'queryStr': ''
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_PROCESS_MATERIAL_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}

function editrow_PROCESS_MATERIAL_INFO(index) {
    if (editIndex_PROCESS_MATERIAL_INFO != undefined)
        $('#Table_PROCESS_MATERIAL_INFO').datagrid('endEdit', editIndex_PROCESS_MATERIAL_INFO);
    $('#Table_PROCESS_MATERIAL_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PROCESS_MATERIAL_INFO() {
    var row = $('#Table_PROCESS_MATERIAL_INFO').datagrid('getSelected');
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
            Delete_PROCESS_MATERIAL_INFO(row);
        }
    });
}
function Dialog_PROCESS_MATERIAL_INFO() {
    $('#Dialog_PROCESS_MATERIAL_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PROCESS_MATERIAL_INFO == 'add') {
                    Add_PROCESS_MATERIAL_INFO();
                }
                else if (addOrEdit_PROCESS_MATERIAL_INFO == 'edit') {
                    Edit_PROCESS_MATERIAL_INFO();
                }
            }
        }]
    });
}
function Validate_PROCESS_MATERIAL_INFO() {
    if (!(
        $('#PROCESS_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PRODUCT_TYPE_ID').validatebox('isValid') &&
        $('#PRODUCT_PROC_TYPE_ID').validatebox('isValid') &&
        $('#MATERIAL_TYPE_ID').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#VALID_FLAG').validatebox('isValid')
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
function Add_PROCESS_MATERIAL_INFO() {
    //if (!Validate_PROCESS_MATERIAL_INFO()) {
    //    return;
    //}
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var y = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
    if (y == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var materialTypeId = y.MATERIAL_TYPE_ID;
    var j = {
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'MATERIAL_TYPE_ID': materialTypeId,
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_INFO/PostAdd',
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
                Init_Table_PROCESS_MATERIAL_INFO_();
                $('#Dialog_PROCESS_MATERIAL_INFO').dialog('close');
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
function Edit_PROCESS_MATERIAL_INFO() {
    //if (!Validate_PROCESS_MATERIAL_INFO()) {
    //    return;
    //}
    var x = $('#Table_PROCESS_MATERIAL_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PROCESS_ID': x.PROCESS_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_ID': $('#MATERIAL_TYPE_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_INFO/PostEdit',
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
                Init_Table_PROCESS_MATERIAL_INFO_();
                $('#Dialog_PROCESS_MATERIAL_INFO').dialog('close');
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
function Edit_Cell_PROCESS_MATERIAL_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_INFO/PostEdit',
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
function Delete_PROCESS_MATERIAL_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_INFO/PostDelete',
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
                Init_Table_PROCESS_MATERIAL_INFO_();
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
    $.ajax({
        type: 'get',
        url: '/api/Users/GetCurrentUser',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data != null) {
                $('#FACTORY_ID_SEARCH').combobox('setValue', data.FACTORY_ID);
                Init_PRODUCT_TYPE_ID_SEARCH();
                $('#PRODUCT_TYPE_ID_SEARCH').combobox('setValue', 'CE');
                Init_PRODUCT_PROC_TYPE_ID_SEARCH();
                $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('setValue', 'M6S');
                Init_Table_PROCESS_LIST();
                Init_Table_MATERIAL_CATEGORY_LIST_();
            }
            else {
                $('#FACTORY_ID_SEARCH').combobox('clear');
                $('#PRODUCT_TYPE_ID_SEARCH').combobox('clear');
                $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('clear');
                Table_PROCESS_LIST([]);
            }
        }
    });
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
            Table_PROCESS_LIST([]);

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
        height: '248',
        fitColumns: true,
        autoRowHeight: false,
        columns: [[{ field: 'PROCESS_ID', title: '编号', align: 'left', width: 50 },
                { field: 'PROCESS_DESC', title: '工序', align: 'left', width: 150 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_PROCESS_MATERIAL_INFO_();
            Init_Table_PROCESS_MATERIAL_PN_INFO_();
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

function Table_MATERIAL_CATEGORY_LIST_() {
    $('#Table_MATERIAL_CATEGORY_LIST').datagrid({
        title: '物料分类',
        singleSelect: true,
        width: '240',
        height: '585',
        fitColumns: false,
        autoRowHeight: false,
        columns: [[
            { field: 'MATERIAL_CATEGORY_ID', title: '编号', align: 'left', width: 50 },
            { field: 'MATERIAL_CATEGORY_DESC', title: '名称', align: 'left', width: 170, editor: 'text' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_MATERIAL_TYPE_LIST_();
            Init_Table_PROCESS_MATERIAL_INFO_();
            $('#Table_MATERIAL_PARA_INFO').datagrid('loadData', []);
            $('#Table_MATERIAL_PN_LIST').datagrid('loadData', []);
            $('#Table_MATERIAL_PN_PARA_INFO').datagrid('loadData', []);
            $('#Table_PARAMETER_LIST').datagrid('loadData', []);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { }
    });
}
function Init_Table_MATERIAL_CATEGORY_LIST_() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_CATEGORY_LIST/GetDataByFactoryIdAndTypeId',
        data: {
            'PRODUCT_TYPE_ID': productTypeId,
            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
            'FACTORY_ID': factoryId,
            'queryStr': " AND VALID_FLAG='1'"
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}

function Table_MATERIAL_TYPE_LIST_() {
    $('#Table_MATERIAL_TYPE_LIST').datagrid({
        title: '物料类型',
        singleSelect: true,
        width: '300',
        height: '272',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_MATERIAL_TYPE_LIST = 'add';
                $('#MATERIAL_TYPE_ID').val('').attr('readonly', false);
                $('#MATERIAL_TYPE_NAME').val('').attr('readonly', false);
                $('#MATERIAL_TYPE_DESC').val('').attr('readonly', false);
                $('#UPDATE_USER_MATERIAL_TYPE_LIST').val('').attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_TYPE_LIST').val('').attr('readonly', true);
                $('#VALID_FLAG_MATERIAL_TYPE_LIST').val(1);
                $('#Dialog_MATERIAL_TYPE_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_MATERIAL_TYPE_LIST = 'edit';
                var x = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
                if (x == null) return;
                $('#MATERIAL_TYPE_ID').val(x.MATERIAL_TYPE_ID).attr('readonly', true);
                $('#MATERIAL_TYPE_NAME').val(x.MATERIAL_TYPE_NAME);
                $('#MATERIAL_TYPE_DESC').val(x.MATERIAL_TYPE_DESC);
                $('#UPDATE_USER_MATERIAL_TYPE_LIST').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_TYPE_LIST').val(x.UPDATE_DATE).attr('readonly', true);
                $('#VALID_FLAG_MATERIAL_TYPE_LIST').val(x.VALID_FLAG);
                $('#Dialog_MATERIAL_TYPE_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_TYPE_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_TYPE_LIST').datagrid('endEdit', editIndex_MATERIAL_TYPE_LIST);
                var changedRow = $('#Table_MATERIAL_TYPE_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_TYPE_LIST(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_TYPE_LIST = undefined;
                $('#Table_MATERIAL_TYPE_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_TYPE_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'MATERIAL_TYPE_ID', title: '编号', align: 'left', width: 50, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'MATERIAL_TYPE_NAME', title: '名称', align: 'left', width: 90, editor: 'text' },
            { field: 'MATERIAL_TYPE_DESC', title: '名称', align: 'left', width: 100, editor: 'text' },
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_MATERIAL_TYPE_LIST(rowIndex);
            Init_Table_MATERIAL_PN_LIST_();
            Init_Table_MATERIAL_PARA_INFO_();
            $('#Table_MATERIAL_PN_PARA_INFO').datagrid('loadData', []);
            $('#Table_PARAMETER_LIST').datagrid('loadData', []);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_TYPE_LIST = index;
            row.editing = true;
            $('#Table_MATERIAL_TYPE_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_TYPE_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_TYPE_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_MATERIAL_TYPE_LIST_() {
    var x = $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getSelected');
    if (x == null) return;
    var materialCategoryId = x.MATERIAL_CATEGORY_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_TYPE_LIST/GetDataByCategoryId',
        data: {
            'MATERIAL_CATEGORY_ID': materialCategoryId,
            'PRODUCT_TYPE_ID': productTypeId,
            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
            'FACTORY_ID': factoryId,
            'queryStr': " AND VALID_FLAG='1'"
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_TYPE_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_MATERIAL_TYPE_LIST(index) {
    if (editIndex_MATERIAL_TYPE_LIST != undefined)
        $('#Table_MATERIAL_TYPE_LIST').datagrid('endEdit', editIndex_MATERIAL_TYPE_LIST);
    $('#Table_MATERIAL_TYPE_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_MATERIAL_TYPE_LIST() {
    var row = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
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
            Delete_MATERIAL_TYPE_LIST(row);
            $('#Table_MATERIAL_TYPE_LIST').datagrid('deleteRow', $('#Table_MATERIAL_TYPE_LIST').datagrid('getRowIndex'));
        }
    });
}
function Dialog_MATERIAL_TYPE_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_MATERIAL_TYPE_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_MATERIAL_TYPE_LIST == 'add') {
                    Add_MATERIAL_TYPE_LIST();
                }
                else if (addOrEdit_MATERIAL_TYPE_LIST == 'edit') {
                    Edit_MATERIAL_TYPE_LIST();
                }
            }
        }]
    });
}
function Add_MATERIAL_TYPE_LIST() {
    //var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    //if (x == null) return;
    var y = $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getSelected');
    if (y == null) return;
    //var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var materialCategoryId = y.MATERIAL_CATEGORY_ID;
    var j = {
        'MATERIAL_TYPE_ID': $('#MATERIAL_TYPE_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_TYPE_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_TYPE_LIST').val(),
        'MATERIAL_TYPE_NAME': $('#MATERIAL_TYPE_NAME').val(),
        'MATERIAL_TYPE_DESC': $('#MATERIAL_TYPE_DESC').val(),
        'MATERIAL_CATEGORY_ID': materialCategoryId,
        'VALID_FLAG': $('#VALID_FLAG_MATERIAL_TYPE_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_LIST/PostAdd',
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
                Init_Table_MATERIAL_TYPE_LIST_();
                $('#Dialog_MATERIAL_TYPE_LIST').dialog('close');
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
function Edit_MATERIAL_TYPE_LIST() {
    var x = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'MATERIAL_TYPE_ID': x.MATERIAL_TYPE_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_NAME': $('#MATERIAL_TYPE_NAME').val(),
        'MATERIAL_TYPE_DESC': $('#MATERIAL_TYPE_DESC').val(),
        'MATERIAL_CATEGORY_ID': x.MATERIAL_CATEGORY_ID,
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_TYPE_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_TYPE_LIST').val(),
        'VALID_FLAG': $('#VALID_FLAG_MATERIAL_TYPE_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_LIST/PostEdit',
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
                Init_Table_MATERIAL_TYPE_LIST_();
                $('#Dialog_MATERIAL_TYPE_LIST').dialog('close');
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
function Edit_Cell_MATERIAL_TYPE_LIST(row) {
    var j = {
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'MATERIAL_TYPE_NAME': row.MATERIAL_TYPE_NAME,
        'MATERIAL_TYPE_DESC': row.MATERIAL_TYPE_DESC,
        'MATERIAL_CATEGORY_ID': row.MATERIAL_CATEGORY_ID,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_LIST/PostEdit',
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
function Delete_MATERIAL_TYPE_LIST(row) {
    var j = {
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_LIST/PostDelete',
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
                Init_Table_MATERIAL_TYPE_LIST_();
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

function Table_MATERIAL_PN_LIST_() {
    $('#Table_MATERIAL_PN_LIST').datagrid({
        title: '物料编号',
        singleSelect: true,
        width: '300',
        height: '272',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_MATERIAL_PN_LIST = 'add';
                $('#MATERIAL_PN_ID').val('').attr('readonly', false);
                $('#UPDATE_USER_MATERIAL_PN_LIST').val('').attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_PN_LIST').val('').attr('readonly', true);
                $('#MATERIAL_PN_NAME').val('').attr('readonly', false);
                $('#MATERIAL_PN_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG_MATERIAL_PN_LIST').val(1);
                $('#Dialog_MATERIAL_PN_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_MATERIAL_PN_LIST = 'edit';
                var x = $('#Table_MATERIAL_PN_LIST').datagrid('getSelected');
                if (x == null) return;
                $('#MATERIAL_PN_ID').val(x.MATERIAL_PN_ID).attr('readonly', true);
                $('#UPDATE_USER_MATERIAL_PN_LIST').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_PN_LIST').val(x.UPDATE_DATE).attr('readonly', true);
                $('#MATERIAL_PN_NAME').val(x.MATERIAL_PN_NAME);
                $('#MATERIAL_PN_DESC').val(x.MATERIAL_PN_DESC);
                $('#VALID_FLAG_MATERIAL_PN_LIST').val(x.VALID_FLAG);
                $('#Dialog_MATERIAL_PN_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_PN_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_PN_LIST').datagrid('endEdit', editIndex_MATERIAL_PN_LIST);
                var changedRow = $('#Table_MATERIAL_PN_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_PN_LIST(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_PN_LIST = undefined;
                $('#Table_MATERIAL_PN_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_PN_LIST').datagrid('rejectChanges');
            }
        }],
        columns: [[
            { field: 'MATERIAL_PN_ID', title: '编号', align: 'left', width: 100, styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'MATERIAL_PN_NAME', title: '英文名', align: 'left', width: 70, editor: 'text' },
            { field: 'MATERIAL_PN_DESC', title: '名称', align: 'left', width: 70, editor: 'text' },
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_MATERIAL_PN_LIST(rowIndex);
            Init_Table_MATERIAL_PN_PARA_INFO_(rowData.MATERIAL_PN_ID);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_PN_LIST = index;
            row.editing = true;
            $('#Table_MATERIAL_PN_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_PN_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_PN_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_MATERIAL_PN_LIST_() {
    var x = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;
    var materialTypeId = x.MATERIAL_TYPE_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PN_LIST/GetDataByType',
        data: {
            'MATERIAL_TYPE_ID': materialTypeId,
            'PRODUCT_TYPE_ID': productTypeId,
            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
            'FACTORY_ID': factoryId,
            'queryStr': " AND VALID_FLAG='1'"
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_PN_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_MATERIAL_PN_LIST(index) {
    if (editIndex_MATERIAL_PN_LIST != undefined)
        $('#Table_MATERIAL_PN_LIST').datagrid('endEdit', editIndex_MATERIAL_PN_LIST);
    $('#Table_MATERIAL_PN_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_MATERIAL_PN_LIST() {
    var row = $('#Table_MATERIAL_PN_LIST').datagrid('getSelected');
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
            Delete_MATERIAL_PN_LIST(row);
            $('#Table_MATERIAL_PN_LIST').datagrid('deleteRow', $('#Table_MATERIAL_PN_LIST').datagrid('getRowIndex'));
        }
    });
}
function Dialog_MATERIAL_PN_LIST() {
    $('#Dialog_MATERIAL_PN_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_MATERIAL_PN_LIST == 'add') {
                    Add_MATERIAL_PN_LIST();
                }
                else if (addOrEdit_MATERIAL_PN_LIST == 'edit') {
                    Edit_MATERIAL_PN_LIST();
                }
            }
        }]
    });
}
function Add_MATERIAL_PN_LIST() {
    var y = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
    if (y == null) return;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var materialTypeId = y.MATERIAL_TYPE_ID;
    var j = {
        'MATERIAL_PN_ID': $('#MATERIAL_PN_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_PN_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_PN_LIST').val(),
        'MATERIAL_PN_NAME': $('#MATERIAL_PN_NAME').val(),
        'MATERIAL_PN_DESC': $('#MATERIAL_PN_DESC').val(),
        'MATERIAL_TYPE_ID': materialTypeId,
        'VALID_FLAG': $('#VALID_FLAG_MATERIAL_PN_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_LIST/PostAdd',
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
                Init_Table_MATERIAL_PN_LIST_();
                $('#Dialog_MATERIAL_PN_LIST').dialog('close');
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
function Edit_MATERIAL_PN_LIST() {
    var x = $('#Table_MATERIAL_PN_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'MATERIAL_PN_ID': x.MATERIAL_PN_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_ID': x.MATERIAL_TYPE_ID,
        'MATERIAL_PN_NAME': $('#MATERIAL_PN_NAME').val(),
        'MATERIAL_PN_DESC': $('#MATERIAL_PN_DESC').val(),
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_PN_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_PN_LIST').val(),
        'VALID_FLAG': $('#VALID_FLAG_MATERIAL_PN_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_LIST/PostEdit',
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
                Init_Table_MATERIAL_PN_LIST_();
                $('#Dialog_MATERIAL_PN_LIST').dialog('close');
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
function Edit_Cell_MATERIAL_PN_LIST(row) {
    var j = {
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'MATERIAL_PN_NAME': row.MATERIAL_PN_NAME,
        'MATERIAL_PN_DESC': row.MATERIAL_PN_DESC,
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_LIST/PostEdit',
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
function Delete_MATERIAL_PN_LIST(row) {
    var j = {
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_LIST/PostDelete',
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
                Init_Table_MATERIAL_PN_LIST_();
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

function Table_PROCESS_MATERIAL_PN_INFO_() {
    $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid({
        title: '工序物料编号',
        singleSelect: true,
        width: '300',
        height: '248',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PROCESS_MATERIAL_PN_INFO = 'add';
                $('#MATERIAL_PN_ID').val('').attr('readonly', false);
                $('#UPDATE_USER_PROCESS_MATERIAL_PN_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PROCESS_MATERIAL_PN_INFO').val('').attr('readonly', true);
                $('#Dialog_PROCESS_MATERIAL_PN_INFO').dialog('open');
            }
        }
        //, {
        //    text: '修改',
        //    iconCls: 'icon-edit',
        //    handler: function () {
        //        addOrEdit_PROCESS_MATERIAL_PN_INFO = 'edit';
        //        var x = $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('getSelected');
        //        if (x == null) return;
        //        $('#UPDATE_USER_PROCESS_MATERIAL_PN_INFO').val(x.UPDATE_USER).attr('readonly', true);
        //        $('#UPDATE_DATE_PROCESS_MATERIAL_PN_INFO').val(x.UPDATE_DATE).attr('readonly', true);
        //        $('#Dialog_PROCESS_MATERIAL_PN_INFO').dialog('open');
        //    }
        //}
        , {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PROCESS_MATERIAL_PN_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('endEdit', editIndex_PROCESS_MATERIAL_PN_INFO);
                var changedRow = $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PROCESS_MATERIAL_PN_INFO(changedRow[i]);
                    }
                }
                editIndex_PROCESS_MATERIAL_PN_INFO = undefined;
                $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('rejectChanges');
            }
        }],
        columns: [[
            { field: 'MATERIAL_PN_ID', title: '物料编号', align: 'left', width: 280, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PROCESS_MATERIAL_PN_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PROCESS_MATERIAL_PN_INFO = index;
            row.editing = true;
            $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PROCESS_MATERIAL_PN_INFO_() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_MATERIAL_PN_INFO/GetDataByProcessId',
        data: {
            'PROCESS_ID': processId,
            'PRODUCT_TYPE_ID': productTypeId,
            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
            'FACTORY_ID': factoryId,
            'queryStr': ''
        },
        dataType: 'json',
        success: function (data) {
            $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PROCESS_MATERIAL_PN_INFO(index) {
    if (editIndex_PROCESS_MATERIAL_PN_INFO != undefined)
        $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('endEdit', editIndex_PROCESS_MATERIAL_PN_INFO);
    $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PROCESS_MATERIAL_PN_INFO() {
    var row = $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('getSelected');
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
            Delete_PROCESS_MATERIAL_PN_INFO(row);
            $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('deleteRow', $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('getRowIndex'));
        }
    });
}
function Dialog_PROCESS_MATERIAL_PN_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PROCESS_MATERIAL_PN_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PROCESS_MATERIAL_PN_INFO == 'add') {
                    Add_PROCESS_MATERIAL_PN_INFO();
                }
                else if (addOrEdit_PROCESS_MATERIAL_PN_INFO == 'edit') {
                    Edit_PROCESS_MATERIAL_PN_INFO();
                }
            }
        }]
    });
}
function Add_PROCESS_MATERIAL_PN_INFO() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var y = $('#Table_MATERIAL_PN_LIST').datagrid('getSelected');
    if (y == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var materialPnId = y.MATERIAL_PN_ID;
    var j = {
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'MATERIAL_PN_ID': materialPnId,
        'UPDATE_USER': $('#UPDATE_USER_PROCESS_MATERIAL_PN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PROCESS_MATERIAL_PN_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_PN_INFO/PostAdd',
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
                Init_Table_PROCESS_MATERIAL_PN_INFO_();
                $('#Dialog_PROCESS_MATERIAL_PN_INFO').dialog('close');
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
function Edit_PROCESS_MATERIAL_PN_INFO() {
    var x = $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PROCESS_ID': x.PROCESS_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_PN_ID': x.MATERIAL_PN_ID,
        'UPDATE_USER': $('#UPDATE_USER_PROCESS_MATERIAL_PN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PROCESS_MATERIAL_PN_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_PN_INFO/PostEdit',
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
                Init_Table_PROCESS_MATERIAL_PN_INFO_();
                $('#Dialog_PROCESS_MATERIAL_PN_INFO').dialog('close');
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
function Edit_Cell_PROCESS_MATERIAL_PN_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_PN_INFO/PostEdit',
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
function Delete_PROCESS_MATERIAL_PN_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_MATERIAL_PN_INFO/PostDelete',
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
                Init_Table_PROCESS_MATERIAL_PN_INFO_();
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

function Table_MATERIAL_PARA_INFO_() {
    $('#Table_MATERIAL_PARA_INFO').datagrid({
        title: '物料类型参数',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (x == null) return;
                addOrEdit_MATERIAL_PARA_INFO = 'add';
                PARAMETER_ID();

                $('#REMARK').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', true);
                $('#UPDATE_DATE').val('').attr('readonly', true);
                $('#Dialog_MATERIAL_PARA_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_MATERIAL_PARA_INFO = 'edit';
                var x = $('#Table_MATERIAL_PARA_INFO').datagrid('getSelected');
                if (x == null) return;
                $('#PARAMETER_ID').val(x.PARAMETER_ID).attr('readonly', true);
                $('#REMARK').val(x.REMARK);
                $('#UPDATE_USER_MATERIAL_PARA_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_PARA_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#Dialog_MATERIAL_PARA_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_PARA_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_PARA_INFO').datagrid('endEdit', editIndex_MATERIAL_PARA_INFO);
                var changedRow = $('#Table_MATERIAL_PARA_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_PARA_INFO(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_PARA_INFO = undefined;
                $('#Table_MATERIAL_PARA_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_PARA_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PARAMETER_ID', title: '参数编号', align: 'left', width: 120, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'REMARK', title: '备注', align: 'left', width: 120, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新人', width: 120, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 120, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_MATERIAL_PARA_INFO(rowIndex);
            $('#Table_MATERIAL_PN_PARA_INFO').datagrid('unselectAll');
            Init_Table_PARAMETER_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_PARA_INFO = index;
            row.editing = true;
            $('#Table_MATERIAL_PARA_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_PARA_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_PARA_INFO').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_MATERIAL_PARA_INFO_() {
    var MATERIAL_TYPE_ID = '';
    var x = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
    if (x == null) {
        return;
    } else {
        MATERIAL_TYPE_ID = x.MATERIAL_TYPE_ID;
    };

    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'MATERIAL_TYPE_ID': MATERIAL_TYPE_ID,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PARA_INFO/GetDataByTypeId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_PARA_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_MATERIAL_PARA_INFO(index) {
    if (editIndex_MATERIAL_PARA_INFO != undefined)
        $('#Table_MATERIAL_PARA_INFO').datagrid('endEdit', editIndex_MATERIAL_PARA_INFO);
    $('#Table_MATERIAL_PARA_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_MATERIAL_PARA_INFO() {
    var row = $('#Table_MATERIAL_PARA_INFO').datagrid('getSelected');
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
            Delete_MATERIAL_PARA_INFO(row);
        }
    });
}
function Dialog_MATERIAL_PARA_INFO() {

    $('#Dialog_MATERIAL_PARA_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_MATERIAL_PARA_INFO == 'add') {
                    Add_MATERIAL_PARA_INFO();
                }
                else if (addOrEdit_MATERIAL_PARA_INFO == 'edit') {
                    Edit_MATERIAL_PARA_INFO();
                }
            }
        }]
    });
}
function Validate_MATERIAL_PARA_INFO() {
    if (!(
        $('#MATERIAL_TYPE_ID').validatebox('isValid') &&
        $('#PARAMETER_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PRODUCT_TYPE_ID').validatebox('isValid') &&
        $('#PRODUCT_PROC_TYPE_ID').validatebox('isValid') &&
        $('#REMARK').validatebox('isValid') &&
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
function Add_MATERIAL_PARA_INFO() {
    //if (!Validate_MATERIAL_PARA_INFO()) {
    //    return;
    //}   
    var x = $('#PARAMETER_ID').combogrid('getValue');
    var y = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'MATERIAL_TYPE_ID': y.MATERIAL_TYPE_ID,
        'PARAMETER_ID': x,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'REMARK': $('#REMARK').val(),
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_PARA_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_PARA_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PARA_INFO/PostAdd',
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
                Init_Table_MATERIAL_PARA_INFO_();
                $('#Dialog_MATERIAL_PARA_INFO').dialog('close');
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
function Edit_MATERIAL_PARA_INFO() {
    //if (!Validate_MATERIAL_PARA_INFO()) {
    //    return;
    //}
    var x = $('#Table_MATERIAL_PARA_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'MATERIAL_TYPE_ID': x.MATERIAL_TYPE_ID,
        'PARAMETER_ID': x.PARAMETER_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'REMARK': $('#REMARK').val(),
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_PARA_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_PARA_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PARA_INFO/PostEdit',
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
                Init_Table_MATERIAL_PARA_INFO_();
                $('#Dialog_MATERIAL_PARA_INFO').dialog('close');
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
function Edit_Cell_MATERIAL_PARA_INFO(row) {
    var j = {
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'REMARK': row.REMARK,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PARA_INFO/PostEdit',
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
function Delete_MATERIAL_PARA_INFO(row) {
    var j = {
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'PARAMETER_ID': row.PARAMETER_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PARA_INFO/PostDelete',
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
                Init_Table_MATERIAL_PARA_INFO_();
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

    $('#PARAMETER_ID').combogrid({
        url: "/api/PARAMETER_LIST/GetDataByPType?FACTORY_ID=" + factoryId + "&PRODUCT_TYPE_ID=" + productTypeId + "&PRODUCT_PROC_TYPE_ID=" + produceProcTypeId + "&PARAM_TYPE_ID=MATERIAL" + "&queryStr= AND VALID_FLAG='1'",
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
        width: '602',
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
            { field: 'PARAM_DESC', title: '参数', align: 'left', width: 100, },
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
    var PARAMETER_ID = '';
    var x = $('#Table_MATERIAL_PARA_INFO').datagrid('getSelected');
    var y = $('#Table_MATERIAL_PN_PARA_INFO').datagrid('getSelected');
    if (x != null) PARAMETER_ID = x.PARAMETER_ID;
    else {
        if (y != null) PARAMETER_ID = y.PARAMETER_ID;
        else return;
    }
    var parameterId = PARAMETER_ID;
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

function Table_MATERIAL_PN_PARA_INFO_() {
    $('#Table_MATERIAL_PN_PARA_INFO').datagrid({
        title: '物料编号参数',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                var x = $('#Table_MATERIAL_PARA_INFO').datagrid('getSelected');
                if (x == null) return;
                addOrEdit_MATERIAL_PN_PARA_INFO = 'add';

                $('#TARGET').val('').attr('readonly', false);
                $('#USL').val('').attr('readonly', false);
                $('#LSL').val('').attr('readonly', false);
                $('#UPDATE_USER_MATERIAL_PN_PARA_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_PN_PARA_INFO').val('').attr('readonly', true);
                $('#REMARK_PN_PARA_INFO').val('').attr('readonly', false);
                $('#Dialog_MATERIAL_PN_PARA_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_MATERIAL_PN_PARA_INFO = 'edit';
                var x = $('#Table_MATERIAL_PN_PARA_INFO').datagrid('getSelected');
                if (x == null) return;

                $('#TARGET').val(x.TARGET);
                $('#USL').val(x.USL);
                $('#LSL').val(x.LSL);
                $('#UPDATE_USER_MATERIAL_PN_PARA_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_PN_PARA_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#REMARK_MATERIAL_PN_PARA_INFO').val(x.REMARK);
                $('#Dialog_MATERIAL_PN_PARA_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_PN_PARA_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_PN_PARA_INFO').datagrid('endEdit', editIndex_MATERIAL_PN_PARA_INFO);
                var changedRow = $('#Table_MATERIAL_PN_PARA_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_PN_PARA_INFO(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_PN_PARA_INFO = undefined;
                $('#Table_MATERIAL_PN_PARA_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_PN_PARA_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PARAMETER_ID', title: '参数编号', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'TARGET', title: '目标值', align: 'left', width: 100, editor: 'text' },
            { field: 'USL', title: '上限', align: 'left', width: 100, editor: 'text' },
            { field: 'LSL', title: '下限', align: 'left', width: 100, editor: 'text' },
            { field: 'REMARK', title: '备注', align: 'left', width: 100, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', width: 100, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 130, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_MATERIAL_PN_PARA_INFO(rowIndex);
            $('#Table_MATERIAL_PARA_INFO').datagrid('unselectAll');
            Init_Table_PARAMETER_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_PN_PARA_INFO = index;
            row.editing = true;
            $('#Table_MATERIAL_PN_PARA_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_PN_PARA_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_PN_PARA_INFO').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_MATERIAL_PN_PARA_INFO_() {
    var MATERIAL_PN_ID = '';
    var x = $('#Table_MATERIAL_PN_LIST').datagrid('getSelected');
    if (x == null) {
        return;
    } else {
        MATERIAL_PN_ID = x.MATERIAL_PN_ID;
    }
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'MATERIAL_PN_ID': MATERIAL_PN_ID,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PN_PARA_INFO/GetDataByPN',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_PN_PARA_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_MATERIAL_PN_PARA_INFO(index) {
    if (editIndex_MATERIAL_PN_PARA_INFO != undefined)
        $('#Table_MATERIAL_PN_PARA_INFO').datagrid('endEdit', editIndex_MATERIAL_PN_PARA_INFO);
    $('#Table_MATERIAL_PN_PARA_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_MATERIAL_PN_PARA_INFO() {
    var row = $('#Table_MATERIAL_PN_PARA_INFO').datagrid('getSelected');
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
            Delete_MATERIAL_PN_PARA_INFO(row);
        }
    });
}
function Dialog_MATERIAL_PN_PARA_INFO() {
    $('#Dialog_MATERIAL_PN_PARA_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_MATERIAL_PN_PARA_INFO == 'add') {
                    Add_MATERIAL_PN_PARA_INFO();
                }
                else if (addOrEdit_MATERIAL_PN_PARA_INFO == 'edit') {
                    Edit_MATERIAL_PN_PARA_INFO();
                }
            }
        }]
    });
}
function Validate_MATERIAL_PN_PARA_INFO() {
    if (!(
        $('#MATERIAL_PN_ID').validatebox('isValid') &&
        $('#PARAMETER_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PRODUCT_TYPE_ID').validatebox('isValid') &&
        $('#PRODUCT_PROC_TYPE_ID').validatebox('isValid') &&
        $('#TARGET').validatebox('isValid') &&
        $('#USL').validatebox('isValid') &&
        $('#LSL').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
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
function Add_MATERIAL_PN_PARA_INFO() {
    var MATERIAL_PN_ID = '';
    var x1 = $('#Table_MATERIAL_PN_LIST').datagrid('getSelected');
    var y1 = $('#Table_PROCESS_MATERIAL_PN_INFO').datagrid('getSelected');
    if (x1 == null) {
        if (y1 == null) {
            return;
        } else {
            MATERIAL_PN_ID = y1.MATERIAL_PN_ID;
        }
    } else {
        MATERIAL_PN_ID = x1.MATERIAL_PN_ID;
    }

    var x = $('#Table_MATERIAL_PARA_INFO').datagrid('getSelected');
    if (x == null) return;

    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'MATERIAL_PN_ID': MATERIAL_PN_ID,
        'PARAMETER_ID': x.PARAMETER_ID,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_PN_PARA_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_PN_PARA_INFO').val(),
        'REMARK': $('#REMARK_MATERIAL_PN_PARA_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_PARA_INFO/PostAdd',
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
                Init_Table_MATERIAL_PN_PARA_INFO_();
                $('#Dialog_MATERIAL_PN_PARA_INFO').dialog('close');
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
function Edit_MATERIAL_PN_PARA_INFO() {
    //if (!Validate_MATERIAL_PN_PARA_INFO()) {
    //    return;
    //}
    var x = $('#Table_MATERIAL_PN_PARA_INFO').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'MATERIAL_PN_ID': x.MATERIAL_PN_ID,
        'PARAMETER_ID': x.PARAMETER_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_PN_PARA_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_PN_PARA_INFO').val(),
        'REMARK': $('#REMARK_MATERIAL_PN_PARA_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_PARA_INFO/PostEdit',
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
                Init_Table_MATERIAL_PN_PARA_INFO_();
                $('#Dialog_MATERIAL_PN_PARA_INFO').dialog('close');
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
function Edit_Cell_MATERIAL_PN_PARA_INFO(row) {
    var j = {
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'TARGET': row.TARGET,
        'USL': row.USL,
        'LSL': row.LSL,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'REMARK': row.REMARK
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_PARA_INFO/PostEdit',
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
function Delete_MATERIAL_PN_PARA_INFO(row) {
    var j = {
        'MATERIAL_PN_ID': row.MATERIAL_PN_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'PARAMETER_ID': row.PARAMETER_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_PN_PARA_INFO/PostDelete',
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
                Init_Table_MATERIAL_PN_PARA_INFO_();
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


function Table_MATERIAL_TYPE_GRP_() {
    $('#Table_MATERIAL_TYPE_GRP').datagrid({
        title: '物料类型组',
        singleSelect: true,
        width: '300',
        height: '272',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_MATERIAL_TYPE_GRP = 'add';
                $('#MATERIAL_TYPE_GRP_NUM').val('').attr('readonly', false);
                $('#MATERIAL_TYPE_GRP_DESC').val('').attr('readonly', false);
                $('#UPDATE_USER_MATERIAL_TYPE_GRP').val('').attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_TYPE_GRP').val('').attr('readonly', true);
                $('#Dialog_MATERIAL_TYPE_GRP').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_MATERIAL_TYPE_GRP = 'edit';
                var x = $('#Table_MATERIAL_TYPE_GRP').datagrid('getSelected');
                if (x == null) return;
                $('#MATERIAL_TYPE_GRP_NUM').val(x.MATERIAL_TYPE_GRP_NUM).attr('readonly', true);
                $('#MATERIAL_TYPE_GRP_DESC').val(x.MATERIAL_TYPE_GRP_DESC);
                $('#UPDATE_USER_MATERIAL_TYPE_GRP').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_TYPE_GRP').val(x.UPDATE_DATE).attr('readonly', true);
                $('#Dialog_MATERIAL_TYPE_GRP').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_TYPE_GRP();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_TYPE_GRP').datagrid('endEdit', editIndex_MATERIAL_TYPE_GRP);
                var changedRow = $('#Table_MATERIAL_TYPE_GRP').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_TYPE_GRP(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_TYPE_GRP = undefined;
                $('#Table_MATERIAL_TYPE_GRP').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_TYPE_GRP').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'MATERIAL_TYPE_GRP_NUM', title: '编号', width: 100, align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'MATERIAL_TYPE_GRP_DESC', title: '描述', width: 100, align: 'left', editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', width: 120, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 140, align: 'left' }

        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_MATERIAL_TYPE_GRP(rowIndex);
            Init_Table_MATERIAL_TYPE_GRP_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_TYPE_GRP = index;
            row.editing = true;
            $('#Table_MATERIAL_TYPE_GRP').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_TYPE_GRP').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_TYPE_GRP').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_MATERIAL_TYPE_GRP_() {
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_TYPE_GRP/GetData',
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_TYPE_GRP').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_MATERIAL_TYPE_GRP(index) {
    if (editIndex_MATERIAL_TYPE_GRP != undefined)
        $('#Table_MATERIAL_TYPE_GRP').datagrid('endEdit', editIndex_MATERIAL_TYPE_GRP);
    $('#Table_MATERIAL_TYPE_GRP').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_MATERIAL_TYPE_GRP() {
    var row = $('#Table_MATERIAL_TYPE_GRP').datagrid('getSelected');
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
            Delete_MATERIAL_TYPE_GRP(row);
        }
    });
}
function Dialog_MATERIAL_TYPE_GRP() {
    $('#Dialog_MATERIAL_TYPE_GRP').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_MATERIAL_TYPE_GRP == 'add') {
                    Add_MATERIAL_TYPE_GRP();
                }
                else if (addOrEdit_MATERIAL_TYPE_GRP == 'edit') {
                    Edit_MATERIAL_TYPE_GRP();
                }
            }
        }]
    });
}
function Validate_MATERIAL_TYPE_GRP() {
    if (!(
        $('#MATERIAL_TYPE_GRP_NUM').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PRODUCT_TYPE_ID').validatebox('isValid') &&
        $('#PRODUCT_PROC_TYPE_ID').validatebox('isValid') &&
        $('#MATERIAL_TYPE_GRP_DESC').validatebox('isValid') &&
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
function Add_MATERIAL_TYPE_GRP() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'MATERIAL_TYPE_GRP_NUM': $('#MATERIAL_TYPE_GRP_NUM').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'MATERIAL_TYPE_GRP_DESC': $('#MATERIAL_TYPE_GRP_DESC').val(),
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_TYPE_GRP').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_TYPE_GRP').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP/PostAdd',
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
                Init_Table_MATERIAL_TYPE_GRP_();
                $('#Dialog_MATERIAL_TYPE_GRP').dialog('close');
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
function Edit_MATERIAL_TYPE_GRP() {
    var x = $('#Table_MATERIAL_TYPE_GRP').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'MATERIAL_TYPE_GRP_NUM': x.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_GRP_DESC': $('#MATERIAL_TYPE_GRP_DESC').val(),
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP/PostEdit',
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
                Init_Table_MATERIAL_TYPE_GRP_();
                //Init_Table_MATERIAL_TYPE_GRP(1,pageSize);
                $('#Dialog_MATERIAL_TYPE_GRP').dialog('close');
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
function Edit_Cell_MATERIAL_TYPE_GRP(row) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': row.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_GRP_DESC': row.MATERIAL_TYPE_GRP_DESC,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP/PostEdit',
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
function Delete_MATERIAL_TYPE_GRP(row) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': row.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP/PostDelete',
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
                Init_Table_MATERIAL_TYPE_GRP_();
                //Init_Table_MATERIAL_TYPE_GRP(1,pageSize);
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

function Table_MATERIAL_TYPE_GRP_LIST_() {
    $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid({
        title: '物料类型组-对应的物料类型',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_MATERIAL_TYPE_GRP_LIST = 'add';
                $('#UPDATE_USER_MATERIAL_TYPE_GRP_LIST').val('').attr('readonly', true);
                $('#UPDATE_DATE_MATERIAL_TYPE_GRP_LIST').val('').attr('readonly', true);
                $('#Dialog_MATERIAL_TYPE_GRP_LIST').dialog('open');
            }
        },
        //{
        //    text: '修改',
        //    iconCls: 'icon-edit',
        //    handler: function () {
        //        addOrEdit_MATERIAL_TYPE_GRP_LIST = 'edit';
        //        var x = $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('getSelected');
        //        if (x == null) return;
        //        $('#MATERIAL_TYPE_GRP_NUM').val(x.MATERIAL_TYPE_GRP_NUM).attr('readonly', true);
        //        $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
        //        $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
        //        $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
        //        $('#MATERIAL_TYPE_ID').val(x.MATERIAL_TYPE_ID).attr('readonly', true);
        //        $('#UPDATE_USER').val(x.UPDATE_USER);
        //        $('#UPDATE_DATE').val(x.UPDATE_DATE);
        //        $('#UPDATE_USER').attr('readonly', true);
        //        $('#UPDATE_DATE').attr('readonly', true);
        //        $('#Dialog_MATERIAL_TYPE_GRP_LIST').dialog('open');
        //    }
        //},
        {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_TYPE_GRP_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('endEdit', editIndex_MATERIAL_TYPE_GRP_LIST);
                var changedRow = $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_TYPE_GRP_LIST(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_TYPE_GRP_LIST = undefined;
                $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'MATERIAL_TYPE_ID', title: '物料类型', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'MATERIAL_TYPE_DESC', title: '物料类型', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }

        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_MATERIAL_TYPE_GRP_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_TYPE_GRP_LIST = index;
            row.editing = true;
            $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_MATERIAL_TYPE_GRP_LIST_() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var x = $('#Table_MATERIAL_TYPE_GRP').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'MATERIAL_TYPE_GRP_NUM': x.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_TYPE_GRP_LIST/GetDataByGrpId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_MATERIAL_TYPE_GRP_LIST(index) {
    if (editIndex_MATERIAL_TYPE_GRP_LIST != undefined)
        $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('endEdit', editIndex_MATERIAL_TYPE_GRP_LIST);
    $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_MATERIAL_TYPE_GRP_LIST() {
    var row = $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('getSelected');
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
            Delete_MATERIAL_TYPE_GRP_LIST(row);
        }
    });
}
function Dialog_MATERIAL_TYPE_GRP_LIST() {
    $('#Dialog_MATERIAL_TYPE_GRP_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_MATERIAL_TYPE_GRP_LIST == 'add') {
                    Add_MATERIAL_TYPE_GRP_LIST();
                }
                else if (addOrEdit_MATERIAL_TYPE_GRP_LIST == 'edit') {
                    Edit_MATERIAL_TYPE_GRP_LIST();
                }
            }
        }]
    });
}
function Validate_MATERIAL_TYPE_GRP_LIST() {
    if (!(
        $('#MATERIAL_TYPE_GRP_NUM').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PRODUCT_TYPE_ID').validatebox('isValid') &&
        $('#PRODUCT_PROC_TYPE_ID').validatebox('isValid') &&
        $('#MATERIAL_TYPE_ID').validatebox('isValid') &&
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
function Add_MATERIAL_TYPE_GRP_LIST() {
    //if (!Validate_MATERIAL_TYPE_GRP_LIST()) {
    //    return;
    //}
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var x = $('#Table_MATERIAL_TYPE_GRP').datagrid('getSelected');
    var y = $('#Table_MATERIAL_TYPE_LIST').datagrid('getSelected');
    if (x == null || y == null) return;
    var j = {
        'MATERIAL_TYPE_GRP_NUM': x.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'MATERIAL_TYPE_ID': y.MATERIAL_TYPE_ID,
        'UPDATE_USER': $('#UPDATE_USER_MATERIAL_TYPE_GRP_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_MATERIAL_TYPE_GRP_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP_LIST/PostAdd',
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
                Init_Table_MATERIAL_TYPE_GRP_LIST_();
                //Init_Table_MATERIAL_TYPE_GRP_LIST(1,pageSize);
                $('#Dialog_MATERIAL_TYPE_GRP_LIST').dialog('close');
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
function Edit_MATERIAL_TYPE_GRP_LIST() {
    //if (!Validate_MATERIAL_TYPE_GRP_LIST()) {
    //    return;
    //}
    var x = $('#Table_MATERIAL_TYPE_GRP_LIST').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'MATERIAL_TYPE_GRP_NUM': x.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_ID': x.MATERIAL_TYPE_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP_LIST/PostEdit',
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
                Init_Table_MATERIAL_TYPE_GRP_LIST_();
                //Init_Table_MATERIAL_TYPE_GRP_LIST(1,pageSize);
                $('#Dialog_MATERIAL_TYPE_GRP_LIST').dialog('close');
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
function Edit_Cell_MATERIAL_TYPE_GRP_LIST(row) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': row.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP_LIST/PostEdit',
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
function Delete_MATERIAL_TYPE_GRP_LIST(row) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': row.MATERIAL_TYPE_GRP_NUM,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'MATERIAL_TYPE_ID': row.MATERIAL_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_TYPE_GRP_LIST/PostDelete',
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
                Init_Table_MATERIAL_TYPE_GRP_LIST_();
                //Init_Table_MATERIAL_TYPE_GRP_LIST(1,pageSize);
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

