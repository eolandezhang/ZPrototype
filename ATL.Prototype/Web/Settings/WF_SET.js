var pageSize = 20;

var editIndex_WF_SET = undefined;
var addOrEdit_WF_SET = null;

var editIndex_WF_SET_STEP = undefined;
var addOrEdit_WF_SET_STEP = null;

$(function () {
    Init_FACTORY_ID();
    $('#FACTORY_ID').combobox('setValue', 'SSL-P');

    Table_WF_SET_();
    Init_Table_WF_SET_();
    Dialog_WF_SET();

    Table_WF_SET_STEP_();
    Dialog_WF_SET_STEP();
});
function Init_FACTORY_ID() {
    $('#FACTORY_ID').combobox({
        valueField: 'FACTORY_ID',
        textField: 'FACTORY_DESC',
        url: '/api/FACTORY_LIST/GetData',
        method: 'get',
        panelHeight: 100,
        editable: false,
        onSelect: function (record) {
            Init_Table_WF_SET_();
        }
    });
}
function Table_WF_SET_() {
    $('#Table_WF_SET').datagrid({
        title: '流程',
        singleSelect: true,
        width: '300',
        height: '500',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_WF_SET = 'add';
                $('#WF_SET_NUM').val('').attr('readonly', false);
                $('#WF_SET_NAME').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#VALID_FLAG').val('1').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_WF_SET').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_WF_SET = 'edit';
                var x = $('#Table_WF_SET').datagrid('getSelected');
                if (x == null) return;
                $('#WF_SET_NUM').val(x.WF_SET_NUM).attr('readonly', true);
                $('#WF_SET_NAME').val(x.WF_SET_NAME);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_WF_SET').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_WF_SET();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_WF_SET').datagrid('endEdit', editIndex_WF_SET);
                var changedRow = $('#Table_WF_SET').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_WF_SET(changedRow[i]);
                    }
                }
                editIndex_WF_SET = undefined;
                $('#Table_WF_SET').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_WF_SET').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'WF_SET_NUM', title: '编号', align: 'left', width: 100, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'WF_SET_NAME', title: '名称', align: 'left', width: 140, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 140 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 140 },
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 140, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_WF_SET(rowIndex);
            Init_Table_WF_SET_STEP_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_WF_SET = index;
            row.editing = true;
            $('#Table_WF_SET').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_WF_SET').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_WF_SET').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_WF_SET_() {
    var f = $('#FACTORY_ID').combobox('getValue');
    var j = {
        'FACTORY_ID': f,
        'queryStr': " AND VALID_FLAG='1' "
    };
    $.ajax({
        type: 'get',
        url: '/api/WF_SET/GetDataByFactoryId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_WF_SET').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_WF_SET(index) {
    if (editIndex_WF_SET != undefined)
        $('#Table_WF_SET').datagrid('endEdit', editIndex_WF_SET);
    $('#Table_WF_SET').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_WF_SET() {
    var row = $('#Table_WF_SET').datagrid('getSelected');
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
            Delete_WF_SET(row);
        }
    });
}
function Dialog_WF_SET() {
    $('#Dialog_WF_SET').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_WF_SET == 'add') {
                    Add_WF_SET();
                }
                else if (addOrEdit_WF_SET == 'edit') {
                    Edit_WF_SET();
                }
            }
        }]
    });
}
function Validate_WF_SET() {
    if (!(
        $('#WF_SET_NUM').validatebox('isValid') &&
        $('#WF_SET_NAME').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#VALID_FLAG').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid')
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
function Add_WF_SET() {
    var f = $('#FACTORY_ID').combobox('getValue');
    if (f == '') return;
    var j = {
        'WF_SET_NUM': $('#WF_SET_NUM').val(),
        'WF_SET_NAME': $('#WF_SET_NAME').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'FACTORY_ID': f
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET/PostAdd',
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
                Init_Table_WF_SET_();
                //Init_Table_WF_SET(1,pageSize);
                $('#Dialog_WF_SET').dialog('close');
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
function Edit_WF_SET() {
    var x = $('#Table_WF_SET').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'WF_SET_NUM': $('#WF_SET_NUM').val(),
        'WF_SET_NAME': $('#WF_SET_NAME').val(),
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'FACTORY_ID': x.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET/PostEdit',
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
                Init_Table_WF_SET_();
                //Init_Table_WF_SET(1,pageSize);
                $('#Dialog_WF_SET').dialog('close');
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
function Edit_Cell_WF_SET(row) {
    var j = {
        'WF_SET_NUM': row.WF_SET_NUM,
        'WF_SET_NAME': row.WF_SET_NAME,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET/PostEdit',
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
function Delete_WF_SET(row) {
    var j = {
        'WF_SET_NUM': row.WF_SET_NUM,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET/PostDelete',
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
                Init_Table_WF_SET_();
                //Init_Table_WF_SET(1,pageSize);
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

function Table_WF_SET_STEP_() {
    $('#Table_WF_SET_STEP').datagrid({
        title: '步骤',
        singleSelect: true,
        width: '660',
        height: '500',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_WF_SET_STEP = 'add';
                STEP_ID('AGREE_STEP_ID');
                STEP_ID('DISAGREE_STEP_ID');
                PMES_USER_GROUP_ID();
                $('#WF_SET_STEP_ID').val('').attr('readonly', false);
                $('#WF_SET_STEP_NAME').val('').attr('readonly', false);
                $('#ORDER_NUM').val('').attr('readonly', false);
                $('#AGREE_STEP_ID').val('').attr('readonly', false);
                $('#DISAGREE_STEP_ID').val('').attr('readonly', false);
                
                $('#STEP_FLAG').val('MDL').attr('readonly', false);
                $('#UPDATE_USER_WF_SET_STEP').val('').attr('readonly', true);
                $('#UPDATE_DATE_WF_SET_STEP').val('').attr('readonly', true);
                $('#VALID_FLAG_WF_SET_STEP').val('1').attr('readonly', false);
                $('#Dialog_WF_SET_STEP').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_WF_SET_STEP = 'edit';
                var x = $('#Table_WF_SET_STEP').datagrid('getSelected');
                if (x == null) return;
                STEP_ID('AGREE_STEP_ID');
                STEP_ID('DISAGREE_STEP_ID');
                PMES_USER_GROUP_ID();
                $('#WF_SET_STEP_ID').val(x.WF_SET_STEP_ID).attr('readonly', true);
                $('#ORDER_NUM').val(x.ORDER_NUM);
                $('#WF_SET_STEP_NAME').val(x.WF_SET_STEP_NAME);
                $('#AGREE_STEP_ID').combobox('setValue', x.AGREE_STEP_ID);
                $('#DISAGREE_STEP_ID').combobox('setValue', x.DISAGREE_STEP_ID);
                $('#PMES_USER_GROUP_ID').combobox('setValue', x.PMES_USER_GROUP_ID);
                $('#STEP_FLAG').val(x.STEP_FLAG);
                $('#UPDATE_USER_WF_SET_STEP').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_WF_SET_STEP').val(x.UPDATE_DATE).attr('readonly', true);
                $('#VALID_FLAG_WF_SET_STEP').val(x.VALID_FLAG);
                $('#Dialog_WF_SET_STEP').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_WF_SET_STEP();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_WF_SET_STEP').datagrid('endEdit', editIndex_WF_SET_STEP);
                var changedRow = $('#Table_WF_SET_STEP').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_WF_SET_STEP(changedRow[i]);
                    }
                }
                editIndex_WF_SET_STEP = undefined;
                $('#Table_WF_SET_STEP').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_WF_SET_STEP').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'WF_SET_STEP_ID', title: '编号', align: 'left', width: 60, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[

            { field: 'ORDER_NUM', title: '序号', align: 'left', width: 40, editor: 'text' },
            { field: 'WF_SET_STEP_NAME', title: '名称', align: 'left', width: 140, editor: 'text' },
            { field: 'AGREE_STEP_ID', title: '转交下一步', align: 'left', width: 80 },
            { field: 'DISAGREE_STEP_ID', title: '退回上一步', align: 'left', width: 80 },
            { field: 'PMES_USER_GROUP_ID', title: '签审用户组', align: 'left', width: 140 },
            {
                field: 'STEP_FLAG', title: '标识', align: 'left', width: 80,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'value',
                        textField: 'text',
                        data: [{
                            'text': '中间步骤',
                            'value': 'MDL'
                        }, {
                            'text': '第一步',
                            'value': 'FST'
                        }, {
                            'text': '最后一步',
                            'value': 'LST'
                        }],
                        method: 'get',
                        required: false
                    }
                }
            },
            { field: 'UPDATE_USER', title: '最后修改者', width: 140, align: 'left' },
            { field: 'UPDATE_DATE', title: '最后修改日期', width: 140, align: 'left' },
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 140, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_WF_SET_STEP(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_WF_SET_STEP = index;
            row.editing = true;
            $('#Table_WF_SET_STEP').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_WF_SET_STEP').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_WF_SET_STEP').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_WF_SET_STEP_() {
    var x = $('#Table_WF_SET').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'WF_SET_NUM': x.WF_SET_NUM,
        'FACTORY_ID': x.FACTORY_ID,
        'queryStr': " AND VALID_FLAG='1' "
    };
    $.ajax({
        type: 'get',
        url: '/api/WF_SET_STEP/GetDataBySetId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_WF_SET_STEP').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_WF_SET_STEP(index) {
    if (editIndex_WF_SET_STEP != undefined)
        $('#Table_WF_SET_STEP').datagrid('endEdit', editIndex_WF_SET_STEP);
    $('#Table_WF_SET_STEP').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_WF_SET_STEP() {
    var row = $('#Table_WF_SET_STEP').datagrid('getSelected');
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
            Delete_WF_SET_STEP(row);
        }
    });
}
function Dialog_WF_SET_STEP() {
    $('#Dialog_WF_SET_STEP').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_WF_SET_STEP == 'add') {
                    Add_WF_SET_STEP();
                }
                else if (addOrEdit_WF_SET_STEP == 'edit') {
                    Edit_WF_SET_STEP();
                }
            }
        }]
    });
}
function Validate_WF_SET_STEP() {
    if (!(
        $('#WF_SET_STEP_ID').validatebox('isValid') &&
        $('#WF_SET_NUM').validatebox('isValid') &&
        $('#ORDER_NUM').validatebox('isValid') &&
        $('#AGREE_STEP_ID').validatebox('isValid') &&
        $('#DISAGREE_STEP_ID').validatebox('isValid') &&
        $('#PMES_USER_GROUP_ID').validatebox('isValid') &&
        $('#STEP_FLAG').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#VALID_FLAG').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid')
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
function Add_WF_SET_STEP() {
    var x = $('#Table_WF_SET').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'WF_SET_STEP_ID': $('#WF_SET_STEP_ID').val(),
        'WF_SET_NUM': x.WF_SET_NUM,
        'ORDER_NUM': $('#ORDER_NUM').val(),
        'WF_SET_STEP_NAME': $('#WF_SET_STEP_NAME').val(),
        'AGREE_STEP_ID': $('#AGREE_STEP_ID').combobox('getValue'),
        'DISAGREE_STEP_ID': $('#DISAGREE_STEP_ID').combobox('getValue'),
        'PMES_USER_GROUP_ID': $('#PMES_USER_GROUP_ID').combobox('getValue'),
        'STEP_FLAG': $('#STEP_FLAG').val(),
        'UPDATE_USER': $('#UPDATE_USER_WF_SET_STEP').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_WF_SET_STEP').val(),
        'VALID_FLAG': $('#VALID_FLAG_WF_SET_STEP').val(),
        'FACTORY_ID': x.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET_STEP/PostAdd',
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
                Init_Table_WF_SET_STEP_();
                //Init_Table_WF_SET_STEP(1,pageSize);
                $('#Dialog_WF_SET_STEP').dialog('close');
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
function Edit_WF_SET_STEP() {
    //if (!Validate_WF_SET_STEP()) {
    //    return;
    //}
    var x = $('#Table_WF_SET_STEP').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'WF_SET_STEP_ID': x.WF_SET_STEP_ID,
        'WF_SET_NUM': x.WF_SET_NUM,
        'ORDER_NUM': x.ORDER_NUM,
        'WF_SET_STEP_NAME': $('#WF_SET_STEP_NAME').val(),
        'AGREE_STEP_ID': $('#AGREE_STEP_ID').combobox('getValue'),
        'DISAGREE_STEP_ID': $('#DISAGREE_STEP_ID').combobox('getValue'),
        'PMES_USER_GROUP_ID': $('#PMES_USER_GROUP_ID').combobox('getValue'),
        'STEP_FLAG': $('#STEP_FLAG').val(),
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'VALID_FLAG': $('#VALID_FLAG_WF_SET_STEP').val(),
        'FACTORY_ID': x.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET_STEP/PostEdit',
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
                Init_Table_WF_SET_STEP_();
                //Init_Table_WF_SET_STEP(1,pageSize);
                $('#Dialog_WF_SET_STEP').dialog('close');
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
function Edit_Cell_WF_SET_STEP(row) {
    var j = {
        'WF_SET_STEP_ID': row.WF_SET_STEP_ID,
        'WF_SET_NUM': row.WF_SET_NUM,
        'ORDER_NUM': row.ORDER_NUM,
        'WF_SET_STEP_NAME': row.WF_SET_STEP_NAME,
        'AGREE_STEP_ID': row.AGREE_STEP_ID,
        'DISAGREE_STEP_ID': row.DISAGREE_STEP_ID,
        'PMES_USER_GROUP_ID': row.PMES_USER_GROUP_ID,
        'STEP_FLAG': row.STEP_FLAG,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET_STEP/PostEdit',
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
function Delete_WF_SET_STEP(row) {
    var j = {
        'WF_SET_STEP_ID': row.WF_SET_STEP_ID,
        'WF_SET_NUM': row.WF_SET_NUM,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/WF_SET_STEP/PostDelete',
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
                Init_Table_WF_SET_STEP_();
                //Init_Table_WF_SET_STEP(1,pageSize);
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

function STEP_ID(id) {
    var x = $('#Table_WF_SET').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'WF_SET_NUM': x.WF_SET_NUM,
        'FACTORY_ID': x.FACTORY_ID,
        'queryStr': " AND VALID_FLAG='1' "
    };
    $.ajax({
        type: 'get',
        url: '/api/WF_SET_STEP/GetDataBySetId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#' + id).combobox({
                valueField: 'WF_SET_STEP_ID',
                textField: 'WF_SET_STEP_NAME',
                data: data,
                panelHeight: 100,
                editable: false
            });
        }
    });


}

function PMES_USER_GROUP_ID() {
    var x = $('#Table_WF_SET').datagrid('getSelected');
    if (x == null) return;
    $('#PMES_USER_GROUP_ID').combobox({
        valueField: 'PMES_USER_GROUP_ID',
        textField: 'USER_GROUP_DESC',
        method:'get',
        url: '/api/PMES_USER_GROUP_LIST/GetDataByFactoryId?FACTORY_ID=' + x.FACTORY_ID + "&queryStr= AND VALID_FLAG='1' ",
        panelHeight: 200,
        editable: true
    });
}