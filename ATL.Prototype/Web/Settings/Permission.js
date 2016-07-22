var pageSize = 20;
var editIndex_PMES_USER_GROUP_LIST = undefined;
var addOrEdit_PMES_USER_GROUP_LIST = null;

var editIndex_PMES_TASK_LIST = undefined;
var addOrEdit_PMES_TASK_LIST = null;

var editIndex_PMES_USER_GRP_TASK_INFO = undefined;
var addOrEdit_PMES_USER_GRP_TASK_INFO = null;

var editIndex_PMES_USER_GROUP_INFO = undefined;
var addOrEdit_PMES_USER_GROUP_INFO = null;

var editIndex_PMES_USER_TASK_INFO = undefined;
var addOrEdit_PMES_USER_TASK_INFO = null;

$(function () {
    Init_FACTORY_ID_SEARCH();
    $('#FACTORY_ID_SEARCH').combobox('setValue', 'SSL-P');

    Table_PMES_USER_GROUP_LIST_();
    Init_Table_PMES_USER_GROUP_LIST_();
    Dialog_PMES_USER_GROUP_LIST();

    Table_PMES_TASK_LIST_();
    Init_Table_PMES_TASK_LIST_();
    Dialog_PMES_TASK_LIST();

    Table_PMES_USER_GRP_TASK_INFO_();
    Dialog_PMES_USER_GRP_TASK_INFO();

    Table_PMES_USER_GROUP_INFO_();
    Dialog_PMES_USER_GROUP_INFO();

    Table_PMES_USER_TASK_INFO_();
    Dialog_PMES_USER_TASK_INFO();
});
function Init_FACTORY_ID_SEARCH() {
    $('#FACTORY_ID_SEARCH').combobox({
        valueField: 'FACTORY_ID',
        textField: 'FACTORY_DESC',
        url: '/api/FACTORY_LIST/GetData',
        method: 'get',
        panelHeight: 100,
        editable: false,
        onSelect: function (record) {
            Init_Table_PMES_USER_GROUP_LIST_();
            Init_Table_PMES_TASK_LIST_();
        }
    });
}

function Table_PMES_USER_GROUP_LIST_() {
    $('#Table_PMES_USER_GROUP_LIST').datagrid({
        title: '用户组',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PMES_USER_GROUP_LIST = 'add';
                $('#PMES_USER_GROUP_ID').val('').attr('readonly', false);

                $('#UPDATE_USER_PMES_USER_GROUP_LIST').val('').attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_GROUP_LIST').val('').attr('readonly', true);
                $('#USER_GROUP_NAME').val('').attr('readonly', false);
                $('#USER_GROUP_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG_PMES_USER_GROUP_LIST').val('1').attr('readonly', false);
                $('#Dialog_PMES_USER_GROUP_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PMES_USER_GROUP_LIST = 'edit';
                var x = $('#Table_PMES_USER_GROUP_LIST').datagrid('getSelected');
                if (x == null) return;
                $('#PMES_USER_GROUP_ID').val(x.PMES_USER_GROUP_ID).attr('readonly', true);

                $('#UPDATE_USER_PMES_USER_GROUP_LIST').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_GROUP_LIST').val(x.UPDATE_DATE).attr('readonly', true);
                $('#USER_GROUP_NAME').val(x.USER_GROUP_NAME);
                $('#USER_GROUP_DESC').val(x.USER_GROUP_DESC);
                $('#VALID_FLAG_PMES_USER_GROUP_LIST').val(x.VALID_FLAG);
                $('#Dialog_PMES_USER_GROUP_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PMES_USER_GROUP_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PMES_USER_GROUP_LIST').datagrid('endEdit', editIndex_PMES_USER_GROUP_LIST);
                var changedRow = $('#Table_PMES_USER_GROUP_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PMES_USER_GROUP_LIST(changedRow[i]);
                    }
                }
                editIndex_PMES_USER_GROUP_LIST = undefined;
                $('#Table_PMES_USER_GROUP_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PMES_USER_GROUP_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PMES_USER_GROUP_ID', title: '编号', align: 'left', width: 120, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'USER_GROUP_DESC', title: '中文名', align: 'left', width: 120, editor: 'text' },
            { field: 'USER_GROUP_NAME', title: '英文名', align: 'left', width: 120, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' },
            { field: 'VALID_FLAG', title: '状态', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PMES_USER_GROUP_LIST(rowIndex);
            Init_Table_PMES_USER_GRP_TASK_INFO_();
            Init_Table_PMES_USER_GROUP_INFO_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PMES_USER_GROUP_LIST = index;
            row.editing = true;
            $('#Table_PMES_USER_GROUP_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_GROUP_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_GROUP_LIST').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_PMES_USER_GROUP_LIST_() {
    var x = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (x == '') return;
    var j = {
        'FACTORY_ID': x,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PMES_USER_GROUP_LIST/GetDataByFactoryId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PMES_USER_GROUP_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PMES_USER_GROUP_LIST(index) {
    if (editIndex_PMES_USER_GROUP_LIST != undefined)
        $('#Table_PMES_USER_GROUP_LIST').datagrid('endEdit', editIndex_PMES_USER_GROUP_LIST);
    $('#Table_PMES_USER_GROUP_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PMES_USER_GROUP_LIST() {
    var row = $('#Table_PMES_USER_GROUP_LIST').datagrid('getSelected');
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
            Delete_PMES_USER_GROUP_LIST(row);
        }
    });
}
function Dialog_PMES_USER_GROUP_LIST() {
    $('#Dialog_PMES_USER_GROUP_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PMES_USER_GROUP_LIST == 'add') {
                    Add_PMES_USER_GROUP_LIST();
                }
                else if (addOrEdit_PMES_USER_GROUP_LIST == 'edit') {
                    Edit_PMES_USER_GROUP_LIST();
                }
            }
        }]
    });
}
function Validate_PMES_USER_GROUP_LIST() {
    if (!(
        $('#PMES_USER_GROUP_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#USER_GROUP_NAME').validatebox('isValid') &&
        $('#USER_GROUP_DESC').validatebox('isValid') &&
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
function Add_PMES_USER_GROUP_LIST() {
    var x = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (x == '') return;
    var j = {
        'PMES_USER_GROUP_ID': $('#PMES_USER_GROUP_ID').val(),
        'FACTORY_ID': x,
        'UPDATE_USER': $('#UPDATE_USER_PMES_USER_GROUP_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PMES_USER_GROUP_LIST').val(),
        'USER_GROUP_NAME': $('#USER_GROUP_NAME').val(),
        'USER_GROUP_DESC': $('#USER_GROUP_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_USER_GROUP_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_LIST/PostAdd',
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
                Init_Table_PMES_USER_GROUP_LIST_();
                //Init_Table_PMES_USER_GROUP_LIST(1,pageSize);
                $('#Dialog_PMES_USER_GROUP_LIST').dialog('close');
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
function Edit_PMES_USER_GROUP_LIST() {
    //if (!Validate_PMES_USER_GROUP_LIST()) {
    //    return;
    //}
    var x = $('#Table_PMES_USER_GROUP_LIST').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'PMES_USER_GROUP_ID': x.PMES_USER_GROUP_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'USER_GROUP_NAME': $('#USER_GROUP_NAME').val(),
        'USER_GROUP_DESC': $('#USER_GROUP_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_USER_GROUP_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_LIST/PostEdit',
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
                Init_Table_PMES_USER_GROUP_LIST_();
                //Init_Table_PMES_USER_GROUP_LIST(1,pageSize);
                $('#Dialog_PMES_USER_GROUP_LIST').dialog('close');
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
function Edit_Cell_PMES_USER_GROUP_LIST(row) {
    var j = {
        'PMES_USER_GROUP_ID': row.PMES_USER_GROUP_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'USER_GROUP_NAME': row.USER_GROUP_NAME,
        'USER_GROUP_DESC': row.USER_GROUP_DESC,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_LIST/PostEdit',
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
function Delete_PMES_USER_GROUP_LIST(row) {
    var j = {
        'PMES_USER_GROUP_ID': row.PMES_USER_GROUP_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_LIST/PostDelete',
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
                Init_Table_PMES_USER_GROUP_LIST_();
                //Init_Table_PMES_USER_GROUP_LIST(1,pageSize);
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

function Table_PMES_TASK_LIST_() {
    $('#Table_PMES_TASK_LIST').datagrid({
        title: '功能',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PMES_TASK_LIST = 'add';
                $('#PMES_TASK_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
               
                $('#TASK_NAME').val('').attr('readonly', false);
                $('#TASK_DESC').val('').attr('readonly', false);
                $('#PROGRAM_NAME').val('').attr('readonly', false);
                $('#MODULE_NAME').val('').attr('readonly', false);
                $('#FUNCTIONS').val('').attr('readonly', false);                
                $('#MENU_NAME').val('').attr('readonly', false);
                $('#MENU_LAYER').val('').attr('readonly', false);
                $('#PARENT_MENU').val('').attr('readonly', false);

                $('#UPDATE_USER_PMES_TASK_LIST').val('').attr('readonly', true);
                $('#UPDATE_DATE_PMES_TASK_LIST').val('').attr('readonly', true);
                $('#VALID_FLAG_PMES_TASK_LIST').val('1').attr('readonly', false);

                $('#Dialog_PMES_TASK_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PMES_TASK_LIST = 'edit';
                var x = $('#Table_PMES_TASK_LIST').datagrid('getSelected');
                if (x == null) return;
                $('#PMES_TASK_ID').val(x.PMES_TASK_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                
                $('#TASK_NAME').val(x.TASK_NAME);
                $('#TASK_DESC').val(x.TASK_DESC);
                $('#PROGRAM_NAME').val(x.PROGRAM_NAME);
                $('#MODULE_NAME').val(x.MODULE_NAME);
                $('#FUNCTIONS').val(x.FUNCTIONS);                
                $('#MENU_NAME').val(x.MENU_NAME);
                $('#MENU_LAYER').val(x.MENU_LAYER);
                $('#PARENT_MENU').val(x.PARENT_MENU);
                
                $('#UPDATE_USER_PMES_TASK_LIST').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PMES_TASK_LIST').val(x.UPDATE_DATE).attr('readonly', true);
                $('#VALID_FLAG_PMES_TASK_LIST').val(x.VALID_FLAG);

                $('#Dialog_PMES_TASK_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PMES_TASK_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PMES_TASK_LIST').datagrid('endEdit', editIndex_PMES_TASK_LIST);
                var changedRow = $('#Table_PMES_TASK_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PMES_TASK_LIST(changedRow[i]);
                    }
                }
                editIndex_PMES_TASK_LIST = undefined;
                $('#Table_PMES_TASK_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PMES_TASK_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PMES_TASK_ID', title: '编号', align: 'left', width: 120, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'MENU_NAME', title: '菜单名', align: 'left', width: 120, editor: 'text' },
            { field: 'TASK_NAME', title: '英文名', align: 'left', width: 120, editor: 'text' },
            { field: 'TASK_DESC', title: '中文名称', align: 'left', width: 120, editor: 'text' },
            { field: 'PROGRAM_NAME', title: '应用', align: 'left', width: 120, editor: 'text' },
            { field: 'MODULE_NAME', title: '模块', align: 'left', width: 120, editor: 'text' },
            { field: 'FUNCTIONS', title: '方法', align: 'left', width: 120, editor: 'text' },
            { field: 'MENU_LAYER', title: '层级', align: 'left', width: 120, editor: 'text' },
            { field: 'PARENT_MENU', title: '上一层菜单', align: 'left', width: 120, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 120 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 120 },
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 120, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PMES_TASK_LIST(rowIndex);
            Init_Table_PMES_USER_TASK_INFO_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PMES_TASK_LIST = index;
            row.editing = true;
            $('#Table_PMES_TASK_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_TASK_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_TASK_LIST').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_PMES_TASK_LIST_() {
    var x = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (x == '') return;
    var j = {
        'FACTORY_ID': x,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PMES_TASK_LIST/GetDataByFactoryId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PMES_TASK_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PMES_TASK_LIST(index) {
    if (editIndex_PMES_TASK_LIST != undefined)
        $('#Table_PMES_TASK_LIST').datagrid('endEdit', editIndex_PMES_TASK_LIST);
    $('#Table_PMES_TASK_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PMES_TASK_LIST() {
    var row = $('#Table_PMES_TASK_LIST').datagrid('getSelected');
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
            Delete_PMES_TASK_LIST(row);
        }
    });
}
function Dialog_PMES_TASK_LIST() {
    $('#Dialog_PMES_TASK_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PMES_TASK_LIST == 'add') {
                    Add_PMES_TASK_LIST();
                }
                else if (addOrEdit_PMES_TASK_LIST == 'edit') {
                    Edit_PMES_TASK_LIST();
                }
            }
        }]
    });
}
function Validate_PMES_TASK_LIST() {
    if (!(
        $('#PMES_TASK_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#TASK_NAME').validatebox('isValid') &&
        $('#TASK_DESC').validatebox('isValid') &&
        $('#PROGRAM_NAME').validatebox('isValid') &&
        $('#MODULE_NAME').validatebox('isValid') &&
        $('#FUNCTIONS').validatebox('isValid') &&
        $('#VALID_FLAG').validatebox('isValid') &&
        $('#MENU_NAME').validatebox('isValid') &&
        $('#MENU_LAYER').validatebox('isValid') &&
        $('#PARENT_MENU').validatebox('isValid')
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
function Add_PMES_TASK_LIST() {
    var x = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var j = {
        'PMES_TASK_ID': $('#PMES_TASK_ID').val(),
        'FACTORY_ID': x,
        'TASK_NAME': $('#TASK_NAME').val(),
        'TASK_DESC': $('#TASK_DESC').val(),
        'PROGRAM_NAME': $('#PROGRAM_NAME').val(),
        'MODULE_NAME': $('#MODULE_NAME').val(),
        'FUNCTIONS': $('#FUNCTIONS').val(),
        'MENU_NAME': $('#MENU_NAME').val(),
        'MENU_LAYER': $('#MENU_LAYER').val(),
        'PARENT_MENU': $('#PARENT_MENU').val(),
        'UPDATE_USER': $('#UPDATE_USER_PMES_TASK_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PMES_TASK_LIST').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_TASK_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_TASK_LIST/PostAdd',
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
                Init_Table_PMES_TASK_LIST_();
                //Init_Table_PMES_TASK_LIST(1,pageSize);
                $('#Dialog_PMES_TASK_LIST').dialog('close');
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
function Edit_PMES_TASK_LIST() {
    //if (!Validate_PMES_TASK_LIST()) {
    //    return;
    //}
    var x = $('#Table_PMES_TASK_LIST').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'PMES_TASK_ID': x.PMES_TASK_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'TASK_NAME': $('#TASK_NAME').val(),
        'TASK_DESC': $('#TASK_DESC').val(),
        'PROGRAM_NAME': $('#PROGRAM_NAME').val(),
        'MODULE_NAME': $('#MODULE_NAME').val(),
        'FUNCTIONS': $('#FUNCTIONS').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_TASK_LIST').val(),
        'MENU_NAME': $('#MENU_NAME').val(),
        'MENU_LAYER': $('#MENU_LAYER').val(),
        'PARENT_MENU': $('#PARENT_MENU').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_TASK_LIST/PostEdit',
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
                Init_Table_PMES_TASK_LIST_();
                //Init_Table_PMES_TASK_LIST(1,pageSize);
                $('#Dialog_PMES_TASK_LIST').dialog('close');
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
function Edit_Cell_PMES_TASK_LIST(row) {
    var j = {
        'PMES_TASK_ID': row.PMES_TASK_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'TASK_NAME': row.TASK_NAME,
        'TASK_DESC': row.TASK_DESC,
        'PROGRAM_NAME': row.PROGRAM_NAME,
        'MODULE_NAME': row.MODULE_NAME,
        'FUNCTIONS': row.FUNCTIONS,
        'VALID_FLAG': row.VALID_FLAG,
        'MENU_NAME': row.MENU_NAME,
        'MENU_LAYER': row.MENU_LAYER,
        'PARENT_MENU': row.PARENT_MENU
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_TASK_LIST/PostEdit',
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
function Delete_PMES_TASK_LIST(row) {
    var j = {
        'PMES_TASK_ID': row.PMES_TASK_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_TASK_LIST/PostDelete',
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
                Init_Table_PMES_TASK_LIST_();
                //Init_Table_PMES_TASK_LIST(1,pageSize);
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

function Table_PMES_USER_GRP_TASK_INFO_() {
    $('#Table_PMES_USER_GRP_TASK_INFO').datagrid({
        title: '组功能',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PMES_USER_GRP_TASK_INFO = 'add';
                $('#UPDATE_USER_PMES_USER_GRP_TASK_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_GRP_TASK_INFO').val('').attr('readonly', true);
                $('#VALID_FLAG_PMES_USER_GRP_TASK_INFO').val('1').attr('readonly', false);
                $('#Dialog_PMES_USER_GRP_TASK_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PMES_USER_GRP_TASK_INFO = 'edit';
                var x = $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('getSelected');
                if (x == null) return;
                $('#UPDATE_USER_PMES_USER_GRP_TASK_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_GRP_TASK_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#VALID_FLAG_PMES_USER_GRP_TASK_INFO').val(x.VALID_FLAG);
                $('#Dialog_PMES_USER_GRP_TASK_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PMES_USER_GRP_TASK_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('endEdit', editIndex_PMES_USER_GRP_TASK_INFO);
                var changedRow = $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PMES_USER_GRP_TASK_INFO(changedRow[i]);
                    }
                }
                editIndex_PMES_USER_GRP_TASK_INFO = undefined;
                $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PMES_TASK_ID', title: '功能', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' },
            { field: 'VALID_FLAG', title: '状态', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PMES_USER_GRP_TASK_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PMES_USER_GRP_TASK_INFO = index;
            row.editing = true;
            $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_PMES_USER_GRP_TASK_INFO_() {
    var x = $('#Table_PMES_USER_GROUP_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PMES_USER_GROUP_ID': x.PMES_USER_GROUP_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PMES_USER_GRP_TASK_INFO/GetDataByGroupId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PMES_USER_GRP_TASK_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PMES_USER_GRP_TASK_INFO(index) {
    if (editIndex_PMES_USER_GRP_TASK_INFO != undefined)
        $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('endEdit', editIndex_PMES_USER_GRP_TASK_INFO);
    $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PMES_USER_GRP_TASK_INFO() {
    var row = $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('getSelected');
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
            Delete_PMES_USER_GRP_TASK_INFO(row);
        }
    });
}
function Dialog_PMES_USER_GRP_TASK_INFO() {
    $('#Dialog_PMES_USER_GRP_TASK_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PMES_USER_GRP_TASK_INFO == 'add') {
                    Add_PMES_USER_GRP_TASK_INFO();
                }
                else if (addOrEdit_PMES_USER_GRP_TASK_INFO == 'edit') {
                    Edit_PMES_USER_GRP_TASK_INFO();
                }
            }
        }]
    });
}
function Validate_PMES_USER_GRP_TASK_INFO() {
    if (!(
        $('#PMES_USER_GROUP_ID').validatebox('isValid') &&
        $('#PMES_TASK_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
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
function Add_PMES_USER_GRP_TASK_INFO() {
    //if (!Validate_PMES_USER_GRP_TASK_INFO()) {
    //    return;
    //}   
    var x = $('#Table_PMES_USER_GROUP_LIST').datagrid('getSelected');
    if (x == null) return;
    var y = $('#Table_PMES_TASK_LIST').datagrid('getSelected');
    if (y == null) return;
    var j = {
        'PMES_USER_GROUP_ID': x.PMES_USER_GROUP_ID,
        'PMES_TASK_ID': y.PMES_TASK_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': $('#UPDATE_USER_PMES_USER_GRP_TASK_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PMES_USER_GRP_TASK_INFO').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_USER_GRP_TASK_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GRP_TASK_INFO/PostAdd',
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
                Init_Table_PMES_USER_GRP_TASK_INFO_();
                //Init_Table_PMES_USER_GRP_TASK_INFO(1,pageSize);
                $('#Dialog_PMES_USER_GRP_TASK_INFO').dialog('close');
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
function Edit_PMES_USER_GRP_TASK_INFO() {
    //if (!Validate_PMES_USER_GRP_TASK_INFO()) {
    //    return;
    //}
    var x = $('#Table_PMES_USER_GRP_TASK_INFO').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'PMES_USER_GROUP_ID': x.PMES_USER_GROUP_ID,
        'PMES_TASK_ID': x.PMES_TASK_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GRP_TASK_INFO/PostEdit',
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
                Init_Table_PMES_USER_GRP_TASK_INFO_();
                //Init_Table_PMES_USER_GRP_TASK_INFO(1,pageSize);
                $('#Dialog_PMES_USER_GRP_TASK_INFO').dialog('close');
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
function Edit_Cell_PMES_USER_GRP_TASK_INFO(row) {
    var j = {
        'PMES_USER_GROUP_ID': row.PMES_USER_GROUP_ID,
        'PMES_TASK_ID': row.PMES_TASK_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GRP_TASK_INFO/PostEdit',
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
function Delete_PMES_USER_GRP_TASK_INFO(row) {
    var j = {
        'PMES_USER_GROUP_ID': row.PMES_USER_GROUP_ID,
        'PMES_TASK_ID': row.PMES_TASK_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GRP_TASK_INFO/PostDelete',
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
                Init_Table_PMES_USER_GRP_TASK_INFO_();
                //Init_Table_PMES_USER_GRP_TASK_INFO(1,pageSize);
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

function Table_PMES_USER_GROUP_INFO_() {
    $('#Table_PMES_USER_GROUP_INFO').datagrid({
        title: '组用户',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PMES_USER_GROUP_INFO = 'add';
                $('#PMES_USER_ID').val('').attr('readonly', false);                
                $('#REMARK').val('').attr('readonly', false);

                $('#UPDATE_USER_PMES_USER_GROUP_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_GROUP_INFO').val('').attr('readonly', true);
                $('#VALID_FLAG_PMES_USER_GROUP_INFO').val('1').attr('readonly', false);
                
                $('#Dialog_PMES_USER_GROUP_INFO').dialog('open');

                $('#btn_PMES_USER_ID').click(function () {
                    Table_PMES_USER_ID();
                    $('#Dialog_PMES_USER_ID').dialog('open');
                    $('#btn_Search').click(function () {
                        Init_Table_PMES_USER_ID();
                    });
                });
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PMES_USER_GROUP_INFO = 'edit';
                var x = $('#Table_PMES_USER_GROUP_INFO').datagrid('getSelected');
                if (x == null) return;
                $('#PMES_USER_ID').val(x.PMES_USER_ID).attr('readonly', true);               
                $('#REMARK').val(x.REMARK);

                $('#UPDATE_USER_PMES_USER_GROUP_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_GROUP_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#VALID_FLAG_PMES_USER_GROUP_INFO').val(x.VALID_FLAG);
               
                $('#Dialog_PMES_USER_GROUP_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PMES_USER_GROUP_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PMES_USER_GROUP_INFO').datagrid('endEdit', editIndex_PMES_USER_GROUP_INFO);
                var changedRow = $('#Table_PMES_USER_GROUP_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PMES_USER_GROUP_INFO(changedRow[i]);
                    }
                }
                editIndex_PMES_USER_GROUP_INFO = undefined;
                $('#Table_PMES_USER_GROUP_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PMES_USER_GROUP_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PMES_USER_ID', title: '工号', align: 'left', width: 120, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'REMARK', title: '备注', align: 'left', width: 120, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', width: 120, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', width: 120, align: 'left' },
            { field: 'VALID_FLAG', title: '备注', align: 'left', width: 120, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PMES_USER_GROUP_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PMES_USER_GROUP_INFO = index;
            row.editing = true;
            $('#Table_PMES_USER_GROUP_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_GROUP_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_GROUP_INFO').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_PMES_USER_GROUP_INFO_() {
    var x = $('#Table_PMES_USER_GROUP_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PMES_USER_GROUP_ID': x.PMES_USER_GROUP_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PMES_USER_GROUP_INFO/GetDataByGroupId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PMES_USER_GROUP_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PMES_USER_GROUP_INFO(index) {
    if (editIndex_PMES_USER_GROUP_INFO != undefined)
        $('#Table_PMES_USER_GROUP_INFO').datagrid('endEdit', editIndex_PMES_USER_GROUP_INFO);
    $('#Table_PMES_USER_GROUP_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PMES_USER_GROUP_INFO() {
    var row = $('#Table_PMES_USER_GROUP_INFO').datagrid('getSelected');
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
            Delete_PMES_USER_GROUP_INFO(row);
        }
    });
}
function Dialog_PMES_USER_GROUP_INFO() {
    $('#Dialog_PMES_USER_GROUP_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PMES_USER_GROUP_INFO == 'add') {
                    Add_PMES_USER_GROUP_INFO();
                }
                else if (addOrEdit_PMES_USER_GROUP_INFO == 'edit') {
                    Edit_PMES_USER_GROUP_INFO();
                }
            }
        }]
    });
}
function Validate_PMES_USER_GROUP_INFO() {
    if (!(
        $('#PMES_USER_ID').validatebox('isValid') &&
        $('#PMES_USER_GROUP_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#REMARK').validatebox('isValid') &&
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
function Add_PMES_USER_GROUP_INFO() {
    var x = $('#Table_PMES_USER_GROUP_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PMES_USER_ID': $('#PMES_USER_ID').val(),
        'PMES_USER_GROUP_ID': x.PMES_USER_GROUP_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': $('#UPDATE_USER_PMES_USER_GROUP_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PMES_USER_GROUP_INFO').val(),
        'REMARK': $('#REMARK').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_USER_GROUP_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_INFO/PostAdd',
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
                Init_Table_PMES_USER_GROUP_INFO_();
                //Init_Table_PMES_USER_GROUP_INFO(1,pageSize);
                $('#Dialog_PMES_USER_GROUP_INFO').dialog('close');
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
function Edit_PMES_USER_GROUP_INFO() {
    //if (!Validate_PMES_USER_GROUP_INFO()) {
    //    return;
    //}
    var x = $('#Table_PMES_USER_GROUP_INFO').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'PMES_USER_ID': x.PMES_USER_ID,
        'PMES_USER_GROUP_ID': x.PMES_USER_GROUP_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'REMARK': $('#REMARK').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_USER_GROUP_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_INFO/PostEdit',
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
                Init_Table_PMES_USER_GROUP_INFO_();
                //Init_Table_PMES_USER_GROUP_INFO(1,pageSize);
                $('#Dialog_PMES_USER_GROUP_INFO').dialog('close');
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
function Edit_Cell_PMES_USER_GROUP_INFO(row) {
    var j = {
        'PMES_USER_ID': row.PMES_USER_ID,
        'PMES_USER_GROUP_ID': row.PMES_USER_GROUP_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'REMARK': row.REMARK,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_INFO/PostEdit',
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
function Delete_PMES_USER_GROUP_INFO(row) {
    var j = {
        'PMES_USER_ID': row.PMES_USER_ID,
        'PMES_USER_GROUP_ID': row.PMES_USER_GROUP_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_GROUP_INFO/PostDelete',
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
                Init_Table_PMES_USER_GROUP_INFO_();
                //Init_Table_PMES_USER_GROUP_INFO(1,pageSize);
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

function Table_PMES_USER_ID() {
    $('#Table_PMES_USER_ID').datagrid({
        title: '组用户',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        columns: [[
            { field: 'USERNAME', title: '用户名', width: 100, align: 'left' },
            { field: 'DESCRIPTION', title: '工号', width: 60, align: 'left' },
            { field: 'CNNAME', title: '姓名', width: 80, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            if (rowData.DESCRIPTION != '') {
                $('#PMES_USER_ID').val(rowData.DESCRIPTION);
                $('#Dialog_PMES_USER_ID').dialog('close');
            }

        }
    });
}
function Init_Table_PMES_USER_ID() {
    var url = '';
    var data = {};
    if ($('#Search_PMES_USER_ID').val() == '') {
        url = '/api/USERS/GetData';
    } else {
        url = '/api/USERS/GetDataByUserNum';
        data = { 'DESCRIPTION': $('#Search_PMES_USER_ID').val(), 'queryStr': '' };
    };
    $.ajax({
        type: 'get',
        url: url,
        data: data,
        dataType: 'json',
        success: function (d) {
            $('#Table_PMES_USER_ID').datagrid("loadData", { total: d[0].TOTAL, rows: d });
        }
    });
}


function Table_PMES_USER_TASK_INFO_() {
    $('#Table_PMES_USER_TASK_INFO').datagrid({
        title: '功能-用户',
        singleSelect: true,
        width: '300',
        height: '502',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PMES_USER_TASK_INFO = 'add';
                $('#PMES_USER_ID_PMES_USER_TASK_INFO').val('').attr('readonly', false);               
                $('#UPDATE_USER_PMES_USER_TASK_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_TASK_INFO').val('').attr('readonly', true);
                $('#VALID_FLAG_PMES_USER_TASK_INFO').val('').attr('readonly', false);
                
                $('#Dialog_PMES_USER_TASK_INFO').dialog('open');

                $('#btn_PMES_USER_ID_PMES_USER_TASK_INFO').click(function () {
                    Table_PMES_USER_ID_PMES_USER_TASK_INFO();
                    $('#Dialog_PMES_USER_ID_PMES_USER_TASK_INFO').dialog('open');
                    $('#btn_Search_PMES_USER_ID_PMES_USER_TASK_INFO').click(function () {
                        Init_Table_PMES_USER_ID_PMES_USER_TASK_INFO();
                    });
                });
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PMES_USER_TASK_INFO = 'edit';
                var x = $('#Table_PMES_USER_TASK_INFO').datagrid('getSelected');
                if (x == null) return;
                $('#PMES_USER_ID_PMES_USER_TASK_INFO').val(x.PMES_USER_ID).attr('readonly', true);
               
                $('#UPDATE_USER_PMES_USER_TASK_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_PMES_USER_TASK_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#VALID_FLAG_PMES_USER_TASK_INFO').val(x.VALID_FLAG);
               
                $('#Dialog_PMES_USER_TASK_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PMES_USER_TASK_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PMES_USER_TASK_INFO').datagrid('endEdit', editIndex_PMES_USER_TASK_INFO);
                var changedRow = $('#Table_PMES_USER_TASK_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PMES_USER_TASK_INFO(changedRow[i]);
                    }
                }
                editIndex_PMES_USER_TASK_INFO = undefined;
                $('#Table_PMES_USER_TASK_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PMES_USER_TASK_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PMES_USER_ID', title: '工号', align: 'left', width: 120, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 120 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 140 },
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 120, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }

        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PMES_USER_TASK_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PMES_USER_TASK_INFO = index;
            row.editing = true;
            $('#Table_PMES_USER_TASK_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_TASK_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PMES_USER_TASK_INFO').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_PMES_USER_TASK_INFO_() {
    var x = $('#Table_PMES_TASK_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PMES_TASK_ID': x.PMES_TASK_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PMES_USER_TASK_INFO/GetDataByTaskId',
        data: j,
        dataType: 'json',
        success: function (data) {           
            $('#Table_PMES_USER_TASK_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PMES_USER_TASK_INFO(index) {
    if (editIndex_PMES_USER_TASK_INFO != undefined)
        $('#Table_PMES_USER_TASK_INFO').datagrid('endEdit', editIndex_PMES_USER_TASK_INFO);
    $('#Table_PMES_USER_TASK_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PMES_USER_TASK_INFO() {
    var row = $('#Table_PMES_USER_TASK_INFO').datagrid('getSelected');
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
            Delete_PMES_USER_TASK_INFO(row);
        }
    });
}
function Dialog_PMES_USER_TASK_INFO() {
    $('#Dialog_PMES_USER_TASK_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PMES_USER_TASK_INFO == 'add') {
                    Add_PMES_USER_TASK_INFO();
                }
                else if (addOrEdit_PMES_USER_TASK_INFO == 'edit') {
                    Edit_PMES_USER_TASK_INFO();
                }
            }
        }]
    });
}
function Validate_PMES_USER_TASK_INFO() {
    if (!(
        $('#PMES_USER_ID').validatebox('isValid') &&
        $('#PMES_TASK_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
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
function Add_PMES_USER_TASK_INFO() {
    var x = $('#Table_PMES_TASK_LIST').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PMES_USER_ID': $('#PMES_USER_ID_PMES_USER_TASK_INFO').val(),
        'PMES_TASK_ID': x.PMES_TASK_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': $('#UPDATE_USER_PMES_USER_TASK_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PMES_USER_TASK_INFO').val(),
        'VALID_FLAG': $('#VALID_FLAG_PMES_USER_TASK_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_TASK_INFO/PostAdd',
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
                Init_Table_PMES_USER_TASK_INFO_();
                //Init_Table_PMES_USER_TASK_INFO(1,pageSize);
                $('#Dialog_PMES_USER_TASK_INFO').dialog('close');
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
function Edit_PMES_USER_TASK_INFO() {
    //if (!Validate_PMES_USER_TASK_INFO()) {
    //    return;
    //}
    var x = $('#Table_PMES_USER_TASK_INFO').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'PMES_USER_ID': $('#PMES_USER_ID_PMES_USER_TASK_INFO').val(),
        'PMES_TASK_ID': x.PMES_TASK_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'VALID_FLAG': $('#VALID_FLAG_PMES_USER_TASK_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_TASK_INFO/PostEdit',
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
                Init_Table_PMES_USER_TASK_INFO_();
                //Init_Table_PMES_USER_TASK_INFO(1,pageSize);
                $('#Dialog_PMES_USER_TASK_INFO').dialog('close');
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
function Edit_Cell_PMES_USER_TASK_INFO(row) {
    var j = {
        'PMES_USER_ID': row.PMES_USER_ID,
        'PMES_TASK_ID': row.PMES_TASK_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_TASK_INFO/PostEdit',
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
function Delete_PMES_USER_TASK_INFO(row) {
    var j = {
        'PMES_USER_ID': row.PMES_USER_ID,
        'PMES_TASK_ID': row.PMES_TASK_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PMES_USER_TASK_INFO/PostDelete',
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
                Init_Table_PMES_USER_TASK_INFO_();
                //Init_Table_PMES_USER_TASK_INFO(1,pageSize);
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

function Table_PMES_USER_ID_PMES_USER_TASK_INFO() {
    $('#Table_PMES_USER_ID_PMES_USER_TASK_INFO').datagrid({
        title: '组用户',
        singleSelect: true,
        width: '300',
        height: '250',
        fitColumns: false,
        columns: [[
            { field: 'USERNAME', title: '用户名', width: 100, align: 'left' },
            { field: 'DESCRIPTION', title: '工号', width: 60, align: 'left' },
            { field: 'CNNAME', title: '姓名', width: 80, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            if (rowData.DESCRIPTION != '') {
                $('#PMES_USER_ID_PMES_USER_TASK_INFO').val(rowData.DESCRIPTION);
                $('#Dialog_PMES_USER_ID_PMES_USER_TASK_INFO').dialog('close');
            }

        }
    });
}
function Init_Table_PMES_USER_ID_PMES_USER_TASK_INFO() {
    var url = '';
    var data = {};
    if ($('#Search_PMES_USER_ID_PMES_USER_TASK_INFO').val() == '') {
        url = '/api/USERS/GetData';
    } else {
        url = '/api/USERS/GetDataByUserNum';
        data = { 'DESCRIPTION': $('#Search_PMES_USER_ID_PMES_USER_TASK_INFO').val(), 'queryStr': '' };
    };
    $.ajax({
        type: 'get',
        url: url,
        data: data,
        dataType: 'json',
        success: function (d) {
            $('#Table_PMES_USER_ID_PMES_USER_TASK_INFO').datagrid("loadData", { total: d[0].TOTAL, rows: d });
        }
    });
}