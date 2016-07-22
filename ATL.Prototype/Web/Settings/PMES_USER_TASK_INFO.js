var pageSize = 20;
var editIndex_PMES_USER_TASK_INFO = undefined;
var addOrEdit_PMES_USER_TASK_INFO = null;
$(function () {
    Table_PMES_USER_TASK_INFO_();
    Init_Table_PMES_USER_TASK_INFO_();
    //Table_PMES_USER_TASK_INFO(1,pageSize)
    //Init_Table_PMES_USER_TASK_INFO(1,pageSize);   
    Dialog_PMES_USER_TASK_INFO();
});
//不分页
function Table_PMES_USER_TASK_INFO_() {
    $('#Table_PMES_USER_TASK_INFO').datagrid({
        title: 'PMES_USER_TASK_INFO',
        singleSelect: true,
        width: '840',
        height: '400',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PMES_USER_TASK_INFO = 'add';
                $('#PMES_USER_ID').val('').attr('readonly', false);
                $('#PMES_TASK_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PMES_USER_TASK_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PMES_USER_TASK_INFO = 'edit';
                var x = $('#Table_PMES_USER_TASK_INFO').datagrid('getSelected');
                if (x == null) return;
                $('#PMES_USER_ID').val(x.PMES_USER_ID).attr('readonly', true);
                $('#PMES_TASK_ID').val(x.PMES_TASK_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
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
            { field: 'PMES_USER_ID', title: 'PMES_USER_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PMES_TASK_ID', title: 'PMES_TASK_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' },
            { field: 'VALID_FLAG', title: 'VALID_FLAG', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } }

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

            //editor: {
            //        type: 'validatebox',
            //        options: {
            //            required: false,
            //            validType: ['maxLength[10]']//'number'
            //        }
            //    }

            //formatter: function (value,row,index) {
             //      switch (value) {
             //           case '0':
             //               return '';
             //           case '1':
             //               return '是';
             //       }
             //   }
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
    var j = {};
    $.ajax({
        type: 'get',
        url: '/api/PMES_USER_TASK_INFO/GetData',
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
    //if (!Validate_PMES_USER_TASK_INFO()) {
    //    return;
    //}
    if ($('#PMES_USER_ID').val() == '' && $('#PMES_TASK_ID').val() == '' && $('#FACTORY_ID').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var j = {
        'PMES_USER_ID': $('#PMES_USER_ID').val(),
        'PMES_TASK_ID': $('#PMES_TASK_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
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
        'PMES_USER_ID': x.PMES_USER_ID,
        'PMES_TASK_ID': x.PMES_TASK_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'VALID_FLAG': x.VALID_FLAG
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


