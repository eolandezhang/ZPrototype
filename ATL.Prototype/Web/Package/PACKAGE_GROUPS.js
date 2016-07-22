var editIndex_PACKAGE_GROUPS = undefined;
var addOrEdit_PACKAGE_GROUPS = null;
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
    document.title = packageNo + '-' + versionNo + '分组';
    Init_Table_PACKAGE_GROUPS();
    Dialog_PACKAGE_GROUPS();
    InitTabs("分组信息", packageNo, factoryId, versionNo, productTypeId, produceProcTypeId);
    //Init_tt(factoryId, packageNo, versionNo);
    $('#GROUP_NO').bind('keyup', function () {
        var v = $(this).val().toUpperCase();
        $(this).val(v);
    });
});
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
                $('#tt').tabs('disableTab', '工序信息');
                //$('#tt').tabs('disableTab', '工序明细');
            } else {
                $('#tt').tabs('enableTab', '设计信息');
                $('#tt').tabs('enableTab', '工序信息');
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